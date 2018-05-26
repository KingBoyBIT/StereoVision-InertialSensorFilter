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
data = [];
count = 1000;
while(1)
	cmd = [];
	while(1)
		ch = fread(sport,1,'uchar');% 接收帧头1字节
		if ch == 36%'$'
			cmd = [cmd;ch];
			ch = fread(sport,29,'uchar');% 接收18个字节，无校验
			if ch(1) == 85&&ch(2) == 170
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
	%data = [data;out];
	%count = count - 1;
	%disp(count)
end
%% CLOSE
fclose(sport);%关闭串口
fclose(instrfind);

%% result
accx = [];
accy = [];
accz = [];
gyrox = [];
gyroy = [];
gyroz = [];
for i = 1:1000
	accx = [accx data(i).accx];
	accy = [accy data(i).accy];
	accz = [accz data(i).accz];
	gyrox = [gyrox data(i).gyrox];
	gyroy = [gyroy data(i).gyroy];
	gyroz = [gyroz data(i).gyroz];
end
figure
subplot(3,2,1)
plot(accx/(2^16-1)*16*9.8*2);
title('accx')
subplot(3,2,2)
plot(gyrox/(2^16-1)*2000*pi/180);
title('gyrox')
subplot(3,2,3)
plot(accy/(2^16-1)*16*9.8*2);
title('accy')
subplot(3,2,4)
plot(gyroy/(2^16-1)*2000*pi/180);
title('gyroy')
subplot(3,2,5)
plot(accz/(2^16-1)*16*9.8*2);
title('accz')
subplot(3,2,6)
plot(gyroz/(2^16-1)*2000*pi/180);
title('gyroz')



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
