clear,clc,close all

p = 2;
R = 3;
for i = 1:50
	if i == 1
		P(i,1) = p;
	else
		P(i,1) = P(i-1,1)*(1-P(i-1,1)*(1/(P(i-1,1)+R)));
	end
end

figure
plot(P)