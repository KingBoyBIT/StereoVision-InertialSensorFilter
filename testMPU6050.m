clearvars -except s
clc,close all
fclose(instrfind)% 关闭正在占用的串口

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
while(count>0)
	cmd = [];
	while(1)
		ch = fread(sport,1,'uchar');% 接收帧头1字节
		if ch == 36%'$'
			cmd = [cmd;ch];
			ch = fread(sport,18,'uchar');% 接收18个字节，无校验
			if ch(1) == 85&&ch(2) == 170
				cmd = [cmd;ch];
				break;
			end
		end
	end
	%解析帧
	out = framedec(cmd);
	data = [data;out];
	count = count - 1;
	disp(count)
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
plot(accx);
title('accx')
subplot(3,2,2)
plot(gyrox);
title('gyrox')
subplot(3,2,3)
plot(accy);
title('accy')
subplot(3,2,4)
plot(gyroy);
title('gyroy')
subplot(3,2,5)
plot(accz);
title('accz')
subplot(3,2,6)
plot(gyroz);
title('gyroz')



function [outputs] = ushort2short(a)
if a>2^15
	b = -(bitxor(a,hex2dec('ffff'))+1);
	outputs = b;
else
	outputs = a;
end

end

function out = framedec(indata)
sp = 1;
off = 3;% 数据段起始偏移量

out.accx = ushort2short(indata(sp+off+1)*256+indata(sp+off));off = off + 2;
out.accy = ushort2short(indata(sp+off+1)*256+indata(sp+off));off = off + 2;
out.accz = ushort2short(indata(sp+off+1)*256+indata(sp+off));off = off + 2;
out.temp = ushort2short(indata(sp+off+1)*256+indata(sp+off));off = off + 2;
out.gyrox = ushort2short(indata(sp+off+1)*256+indata(sp+off));off = off + 2;
out.gyroy = ushort2short(indata(sp+off+1)*256+indata(sp+off));off = off + 2;
out.gyroz = ushort2short(indata(sp+off+1)*256+indata(sp+off));off = off + 2;
end
