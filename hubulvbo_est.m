function out = hubulvbo_est(data)
acc_now = data(1:3);
gyro_now = data(4:6);
global count x_est y_est
dt = 0.01;
xa = atan(acc_now(2)./acc_now(3));
ya = -asin(acc_now(1));
if count == 1
	x_est = 0;
	y_est = 0;
	dw = zeros(3,1);
else
	dw = Omega(gyro_now(1),gyro_now(2))*gyro_now;
end

alpha = 0.8 ;

x_est = (x_est + dw(1)*dt).*alpha+(1-alpha).*xa;
y_est = (y_est + dw(2)*dt).*alpha+(1-alpha).*ya;
out = [x_est/pi*180;y_est/pi*180];
end

function out = Omega(x,y)
% out = [1 tan(y)*sin(x) tan(y)*cos(x);
% 	0 cos(x) -sin(x);
% 	0 sin(x)/cos(y) cos(x)/cos(y)];
out = [1 tan(y)*sin(x) tan(y)*cos(x);
	0 cos(x) -sin(x);
	0 sin(x)/cos(y) cos(x)];
end