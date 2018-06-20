clearvars -except s
clc,close all
if isempty(instrfind)~=1
	fclose(instrfind)% 关闭正在占用的串口
end

global gyrobuff gyrostatic count q eInt last_e

gyrobuff = [];
gyrostatic = [];
count = 0;

q0 = 1;
q1 = 0;
q2 = 0;
q3 = 0;

q = [q0;q1;q2;q3];
eInt = zeros(3,1);
last_e = zeros(3,1);