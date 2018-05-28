clearvars -except s
clc,close all
if isempty(instrfind)~=1
	fclose(instrfind)% 关闭正在占用的串口
end

baudrate = 9600;
portnames = seriallist;
for i = 1:length(portnames)
	if portnames(i) ~= "COM1"
		portname = portnames(i);
		break
	end
end
% 创建串口OBJ
sport=serial(portname,'BaudRate',baudrate);
% s.status %串口状态
fopen(sport)%打开串口
% fwrite(s,255)%发送，用不到
cmd = [];

% 接收200条
% for i = 1:1
dt = 0.03;
data = [];
count = 0;
gyrobuff = [];
gyrostatic = [];
beta = 0.95;
acc_before = 0;
gyro_before = 0;
xg_before = 0;
yg_before = 0;
figure(1)
axis([0 50 -10 10])
acc_plotbuff =[];
gyro_plotbuff = [];
x_est_plotbuff = [];
y_est_plotbuff = [];
% fig = figure(2);
% axis equal
% hold on;
% view(3);
% h1 = fill3([-1 -1 1 1 ],[1 1 1 1],[-1 1 1 -1],'g');
% h2 = fill3([1 1 1 1 ],[-1 -1 1 1],[-1 1 1 -1],'b');
% h3 = fill3([-1 -1 1 1 ],[-1 1 1 -1],[1 1 1 1],'y');
% h4 = fill3([-1 -1 1 1 ],[-1 1 1 -1],[-1 -1 -1 -1],'c');
% h5 = fill3([-1 1 1 -1 ],[-1 -1 -1 -1],[-1 -1 1 1],'r');
% h6 = fill3([-1 -1 -1 -1 ],[-1 1 1 -1],[-1 -1 1 1],'m');
% dcm = angle2dcm(rotationAng1, rotationAng2, 0);

% r = angle2rod(0.01,0.02,0);
% direction = [1 1 1];
% rotate(h1,direction,90)
% rotate(h2,direction,90)
% rotate(h3,direction,90)
% rotate(h4,direction,90)
% rotate(h5,direction,90)
% rotate(h6,direction,90)

while(1)
	cmd = [];
	while(1)
		ch = fread(sport,1,'uchar');% 接收帧头1字节
		if ch == 36%'$'
			cmd = [cmd;ch];
			ch = fread(sport,29,'uchar');% 接收18个字节，无校验
			if ch(1) == 85&&ch(2) == 170% 不做校验处理
				cmd = [cmd;ch];
				break;
			end
		else
			cmd = [];
		end
	end
	%解析帧
	data = framedec(cmd);
	acc = [data.accx;data.accy;data.accz];
	gyro = [data.gyrox;data.gyroy;data.gyroz];
	acc = floor(acc/4)*4;
	acc_now = floor(acc/4)*4;
	acc_now = acc_now/(2^16-1)*16*9.8;
	% AD补偿
	acc_now = acc_now*2;
	if size(gyrobuff,2)<4
		gyrobuff = [gyrobuff,gyro];
	else
		gyrobuff = gyrobuff(:,2:end);
		gyrobuff = [gyrobuff,gyro];
	end
	gyro_now = mean(gyrobuff,2);
	if count<200
		gyrostatic = [gyrostatic,gyro_now];
		count = count + 1;
	end
	gyro_now = gyro_now - mean(gyrostatic,2);
	gyro_now = gyro_now/(2^16-1)*2000*pi/180;
	% AD补偿
	gyro_now = gyro_now*2;
	% 加速度一阶惯性滤波
	acc_now = acc_before*(1-beta)+beta*acc_now;
	gyro_now = gyro_before*(1-beta)+beta*gyro_now;
	acc_now = acc_now./sqrt(acc_now'*acc_now);
	if size(acc_plotbuff,2)<50
		acc_plotbuff = [acc_plotbuff,acc_now];
		gyro_plotbuff = [gyro_plotbuff,gyro_now];
	else
		acc_plotbuff = acc_plotbuff(:,2:end);
		gyro_plotbuff = gyro_plotbuff(:,2:end);
		acc_plotbuff = [acc_plotbuff,acc_now];
		gyro_plotbuff = [gyro_plotbuff,gyro_now];
	end
	figure(1)
	subplot(2,2,1)
	ylim([-1 10])
	axis([0 size(acc_plotbuff,2) -10 10])
	axis manual
	plot(1:size(acc_plotbuff,2),acc_plotbuff(1,:),'r',...
		1:size(acc_plotbuff,2),acc_plotbuff(2,:),'g',...
		1:size(acc_plotbuff,2),acc_plotbuff(3,:),'b');
	subplot(2,2,2)
	ylim([-5 5])
	axis([0 size(gyro_plotbuff,2) -5 5])
	axis manual
	plot(1:size(gyro_plotbuff,2),gyro_plotbuff(1,:),'r',...
		1:size(gyro_plotbuff,2),gyro_plotbuff(2,:),'g',...
		1:size(gyro_plotbuff,2),gyro_plotbuff(3,:),'b');
	
	% 互补滤波
	xa = atan(acc_now(2)./acc_now(3));
	ya = -asin(acc_now(1));
	if count == 1
% 		xg = xa(1);
% 		yg = ya(1);
		x_est = xa(1);
		y_est = ya(1);
		dw = zeros(3,1);
	else
		dw = Omega(gyro_now(1),gyro_now(2))*gyro_now;
% 		xg = rem(xg_before+dw(1)*dt,2*pi);
% 		yg = rem(yg_before+dw(2)*dt,2*pi);
%  		xg = (xg_before+dw(1)*dt);
% 		yg = (yg_before+dw(2)*dt);
				
	end
	
	alpha = 0.8 ;
% 	x_est = xg.*alpha+(1-alpha).*xa;
% 	y_est = yg.*alpha+(1-alpha).*ya;
	x_est = (x_est + dw(1)*dt).*alpha+(1-alpha).*xa;
	y_est = (y_est + dw(2)*dt).*alpha+(1-alpha).*ya;
	if size(acc_plotbuff,2)<50
		x_est_plotbuff = [x_est_plotbuff,x_est];
		y_est_plotbuff = [y_est_plotbuff,y_est];
	else
		x_est_plotbuff = x_est_plotbuff(:,2:end);
		y_est_plotbuff = y_est_plotbuff(:,2:end);
		x_est_plotbuff = [x_est_plotbuff,x_est];
		y_est_plotbuff = [y_est_plotbuff,y_est];
	end
	subplot(2,2,3)
	axis manual
	plot(1:length(x_est_plotbuff),x_est_plotbuff/pi*180,'r',...
		1:length(x_est_plotbuff),y_est_plotbuff/pi*180,'b');
	dcm = angle2dcm( x_est, y_est, 0 );
	%c初始化正方形的三个坐标点
    xd=[5 5 -5 -5]; 
    yd=[-5 5 5 -5];
    zd=[0 0 0 0];
    for i = 1:4
		v = [xd(i);yd(i);zd(i)];
		v = dcm*v;
		xd(i) = v(1);
		yd(i) = v(2);
		zd(i) = v(3);
	end
%     %坐标变换
%     temp = [xd(1,1) yd(1,1) zd(1,1);
%             xd(1,2) yd(1,2) zd(1,2);
%             xd(2,2) yd(2,2) zd(2,2)];
%     temp = temp*q;
%     xd = [temp(1:2,1)';temp(1,1),temp(3,1)];
%     yd = [temp(1:2,2)';temp(1,2),temp(3,2)];
%     zd = [temp(1:2,3)';temp(1,3),temp(3,3)];
	subplot(2,2,4)
	fill3(xd,yd,zd,'b');
	view(3);
% 	axis equal
	drawnow limitrate
% 	pause(0.001);
% 	xg_before = xg;
% 	yg_before = yg;
	acc_before = acc_now;
	gyro_before = gyro_now;
end
%% CLOSE
fclose(sport);%关闭串口
fclose(instrfind);

function [outputs] = ushort2short(a)
if a>2^15
	b = -(bitxor(a,hex2dec('ffff'))+1);
	outputs = b;
else
	outputs = a;
end

end
function [outputs] = uint2int(a)

if a>2^31
	b = -(bitxor(a,hex2dec('ffffffff'))+1);
	outputs = b;
else
	outputs = a;
end

end
function out = framedec(indata)
sp = 1;
off = 3;% 数据段起始偏移量
out.accx = uint2int(indata(sp+off+3)*(2^24)+indata(sp+off+2)*(2^16)+indata(sp+off+1)*256+indata(sp+off));off = off + 4;
out.accy = uint2int(indata(sp+off+3)*(2^24)+indata(sp+off+2)*(2^16)+indata(sp+off+1)*256+indata(sp+off));off = off + 4;
out.accz = uint2int(indata(sp+off+3)*(2^24)+indata(sp+off+2)*(2^16)+indata(sp+off+1)*256+indata(sp+off));off = off + 4;
out.gyrox = uint2int(indata(sp+off+3)*(2^24)+indata(sp+off+2)*(2^16)+indata(sp+off+1)*256+indata(sp+off));off = off + 4;
out.gyroy = uint2int(indata(sp+off+3)*(2^24)+indata(sp+off+2)*(2^16)+indata(sp+off+1)*256+indata(sp+off));off = off + 4;
out.gyroz = uint2int(indata(sp+off+3)*(2^24)+indata(sp+off+2)*(2^16)+indata(sp+off+1)*256+indata(sp+off));off = off + 4;
% out.accx = ushort2short(indata(sp+off)*256+indata(sp+off+1));off = off + 2;
% out.accy = ushort2short(indata(sp+off)*256+indata(sp+off+1));off = off + 2;
% out.accz = ushort2short(indata(sp+off)*256+indata(sp+off+1));off = off + 2;
% % out.temp = ushort2short(indata(sp+off)*256+indata(sp+off+1));off = off + 2;
% out.gyrox = ushort2short(indata(sp+off)*256+indata(sp+off+1));off = off + 2;
% out.gyroy = ushort2short(indata(sp+off)*256+indata(sp+off+1));off = off + 2;
% out.gyroz = ushort2short(indata(sp+off)*256+indata(sp+off+1));off = off + 2;
end
function out = Omega(x,y)
% out = [1 tan(y)*sin(x) tan(y)*cos(x);
% 	0 cos(x) -sin(x);
% 	0 sin(x)/cos(y) cos(x)/cos(y)];
out = [1 tan(y)*sin(x) tan(y)*cos(x);
	0 cos(x) -sin(x);
	0 sin(x)/cos(y) cos(x)];
end