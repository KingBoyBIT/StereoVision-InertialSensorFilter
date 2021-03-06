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
accx = accx/(2^16-1)*16*9.8*2;
accy = accy/(2^16-1)*16*9.8*2;
accz = accz/(2^16-1)*16*9.8*2;
gyrox = gyrox/(2^16-1)*500*pi/180*2;
gyroy = gyroy/(2^16-1)*500*pi/180*2;
gyroz = gyroz/(2^16-1)*500*pi/180*2;

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

%% 互补滤波处理
% 加速度计低通滤波
beta = 0.001;
for i = 1:length(accx)
	if i >1
		accx(i) = accx(i-1)*(1-beta)+beta*accx(i);
		accy(i) = accy(i-1)*(1-beta)+beta*accy(i);
		accz(i) = accz(i-1)*(1-beta)+beta*accz(i);
	end
end

% 加速度归一化
accx = accx./(sqrt(accx.^2+accy.^2+accz.^2));
accy = accy./(sqrt(accx.^2+accy.^2+accz.^2));
accz = accz./(sqrt(accx.^2+accy.^2+accz.^2));

%[          cy*cz,          cy*sz,            -sy]
%[ sy*sx*cz-sz*cx, sy*sx*sz+cz*cx,          cy*sx]
%[ sy*cx*cz+sz*sx, sy*cx*sz-cz*sx,          cy*cx]

% accx = -sy
% accy = cy*sx
% accz = cy*cx

xa = atan(accy./accz);
ya = -asin(accx);

xg = [];
yg = [];
zg = [];
dt = 0.03;
for i = 1:length(gyrox)
	if i == 1
		xg = xa(1);
		yg = ya(1);
		zg = 0;
	else
		dw = Omega(xg(i-1),yg(i-1),zg(i-1))*[gyrox(i-1)-mean(gyrox);
			gyroy(i-1)-mean(gyroy);
			gyroz(i-1)-mean(gyroz)];
		xg(i,1) = xg(i-1)+dw(1)*dt;
		yg(i,1) = yg(i-1)+dw(2)*dt;
		zg(i,1) = zg(i-1)+dw(3)*dt;
	end
end

alpha = 0.8;
x_est = xg.*alpha+(1-alpha).*xa';
y_est = yg.*alpha+(1-alpha).*ya';
figure
subplot(3,2,1)
plot(xa/pi*180)
subplot(3,2,2)
plot(ya/pi*180)
subplot(3,2,3)
plot(xg/pi*180)
subplot(3,2,4)
plot(yg/pi*180)
subplot(3,2,5)
plot(x_est/pi*180)
subplot(3,2,6)
plot(y_est/pi*180)

function out = Omega(x,y,z)
out = [1 tan(y)*sin(x) tan(y)*cos(x);
	0 cos(x) -sin(x);
	0 sin(x)/cos(y) cos(x)/cos(y)];
end
