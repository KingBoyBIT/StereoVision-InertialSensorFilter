clear,clc,close all
rng(0);

P = [10 15 20]';
f = 50/1000;
% cx = 320;
% cy = 240;
cx = tand(60)*f;
cy = tand(60)*f;
K = [f 0 cx 0;
	0 f cy 0;
	0 0 1 0];
R = angle2dcm(0.03,0.04,0.05)
t = [0.05;0.06;0.04];
num = 10000;
for i = 1:num
	P = 10*rand(3,1);
	p = 1/P(3)*K*[P;1];
	pp_a{i,1} = p;
	p = 1/P(3)*K*[R*P+t;1];
	pp_b{i,1} = p;
end

A = [];
for i = 1:num
	% 		for i = 1:length(inliermatch1)
	p1 = pp_b{i,1};
	p2 = pp_a{i,1};
	u1 = p1(1);v1 = p1(2);
	u2 = p2(1);v2 = p2(2);
	A(i,:)=[u1*u2 u1*v2 u1 v1*u2 v1*v2 v1 u2 v2 1];
end
% 		b = zeros(8,1);
% 		y = pinv(A)*b;
r = rank(A);
e = null(A,r);
E = [e(1) e(2) e(3);e(4) e(5) e(6);e(7) e(8) e(9)];
[U,S,V]=svd(E);
s = diag(S);
EE = U*diag([(s(1)+s(2))/2 (s(1)+s(2))/2 0])*V;
% [U,S,V]=svd(EE);

Rz_pi_2 = angle2dcm(pi/2,0,0);
ss=diag([(s(1)+s(2))/2 (s(1)+s(2))/2 0]);
t1_m = U*Rz_pi_2*ss*U';
t2_m = U*Rz_pi_2'*ss*U';
t1 = [-t1_m(2,3);t1_m(1,3);-t1_m(1,2)]
t2 = [-t2_m(2,3);t2_m(1,3);-t2_m(1,2)]
R1 = U*Rz_pi_2'*V'
R2 = U*Rz_pi_2*V'

