clear,clc,close all
load('mpu6050static1000origin_data.mat')

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
plot(gyrox/(2^16-1)*500*pi/180);
title('gyrox')
subplot(3,2,3)
plot(accy/(2^16-1)*16*9.8*2);
title('accy')
subplot(3,2,4)
plot(gyroy/(2^16-1)*500*pi/180);
title('gyroy')
subplot(3,2,5)
plot(accz/(2^16-1)*16*9.8*2);
title('accz')
subplot(3,2,6)
plot(gyroz/(2^16-1)*500*pi/180);
title('gyroz')
