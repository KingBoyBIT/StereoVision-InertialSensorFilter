function out = AHRS_est(data)
acc_now = data(1:3);
gyro_now = data(4:6);
% AHRS·½·¨
global q eInt last_e
q0 = q(1);
q1 = q(2);
q2 = q(3);
q3 = q(4);
vx = 2 * (q1 * q3 - q0 * q2);
vy = 2 * (q0 * q1 + q2 * q3);
vz = q0 * q0 - q1 * q1 - q2 * q2 + q3 * q3;
ax = acc_now(1);
ay = acc_now(2);
az = acc_now(3);
ex = (ay * vz - az * vy);
ey = (az * vx - ax * vz);
ez = (ax * vy - ay * vx);
Kp = 16.000;
Ki = 0.001;
Kd = 0.05;
eInt = eInt + [ex;ey;ez]*Ki;
eDif = [ex;ey;ez] - last_e;

last_e = [ex;ey;ez];
gx = gyro_now(1);
gy = gyro_now(2);
gz = gyro_now(3);
gx = gx + Kp * ex + eInt(1) + Kd * eDif(1);
gy = gy + Kp * ey + eInt(2) + Kd * eDif(2);
gz = gz + Kp * ez + eInt(3) + Kd * eDif(3);
halfT = 0.005;
q0 = q0 + (-q1 * gx - q2 * gy - q3 * gz) * halfT;
q1 = q1 + (q0 * gx + q2 * gz - q3 * gy) * halfT;
q2 = q2 + (q0 * gy - q1 * gz + q3 * gx) * halfT;
q3 = q3 + (q0 * gz + q1 * gy - q2 * gx) * halfT;
[r1,r2,r3] = quat2angle([q0,q1,q2,q3]);
out = [-r3/pi*180;r2/pi*180];
end