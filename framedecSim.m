function out = framedecSim(indata)
sp = 1;
off = 2;% 数据段起始偏移量
accx = uint2int(indata(sp+off+3)*(2^24)+indata(sp+off+2)*(2^16)+indata(sp+off+1)*256+indata(sp+off));off = off + 4;
accy = uint2int(indata(sp+off+3)*(2^24)+indata(sp+off+2)*(2^16)+indata(sp+off+1)*256+indata(sp+off));off = off + 4;
accz = uint2int(indata(sp+off+3)*(2^24)+indata(sp+off+2)*(2^16)+indata(sp+off+1)*256+indata(sp+off));off = off + 4;
gyrox = uint2int(indata(sp+off+3)*(2^24)+indata(sp+off+2)*(2^16)+indata(sp+off+1)*256+indata(sp+off));off = off + 4;
gyroy = uint2int(indata(sp+off+3)*(2^24)+indata(sp+off+2)*(2^16)+indata(sp+off+1)*256+indata(sp+off));off = off + 4;
gyroz = uint2int(indata(sp+off+3)*(2^24)+indata(sp+off+2)*(2^16)+indata(sp+off+1)*256+indata(sp+off));off = off + 4;

global gyrobuff gyrostatic count
acc = [accx;accy;accz];
gyro = [gyrox;gyroy;gyroz];
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

out = [acc_now;gyro_now];
end

function [outputs] = uint2int(a)

if a>2^31
	b = -(bitxor(a,hex2dec('ffffffff'))+1);
	outputs = b;
else
	outputs = a;
end

end