clear,clc,close all
load('stereo_pair_param.mat')

%% 初始化测试
param.dxl=1e-5;param.dyl=1e-5;
param.dxr=1e-5;param.dyr=1e-5;
param.fl=-50/1000;param.fr=-50/1000;

width = 640;height = 480;
param.ul=320;param.vl=240;
param.ur=320;param.vr=240;
param.ul0 = width/2;param.vl0 = height/2;
param.ur0 = width/2;param.vr0 = height/2;
param.tx = 50/1000;
param.ty = 0/1000;
param.tz = 0/1000;
param.Rc = angle2dcm(0,0,0);
% c11 = Rc(1,1);c12 = Rc(1,2);c13 = Rc(1,3);
% c21 = Rc(2,1);c22 = Rc(2,2);c23 = Rc(2,3);
% c31 = Rc(3,1);c32 = Rc(3,2);c33 = Rc(3,3);

out = camCoordinate(param);


%% 特征点位置影响对精度的影响
% 令特征位于两个相机的中心线处，距相机50mm-10m的距离变化
pos_real = [];
pos_err = [];
i = 1;
st = 20;
et = 280;
for delta = st:et
	param.ul = width/2+delta;
	param.ur = width/2-delta;
	out = camCoordinate(param);
	pos_real(:,i) = out;
	noise = 1;
	param.ul = width/2+delta+noise;%一个像素的偏差
	param.ur = width/2-delta-noise;
	out = camCoordinate(param);
	pos_err(:,i) = out;
	i = i + 1;
end
figure
subplot(1,2,1)
semilogy(width/2+st:width/2+et,1000*(pos_real(3,:)),'-r.',...
	width/2+st:width/2+et,1000*(pos_err(3,:)),'-b*')
xlabel('像素坐标')
ylabel('Z_c(mm)')
title('特征点深度')
hold on
for i = 1:size(pos_err,2)
	if (pos_real(3,i)-pos_err(3,i))*1000<10
		plot([width/2+st,width/2+st+i-1,width/2+st+i-1],[1000*(pos_real(3,i)),1000*(pos_real(3,i)),0],'*-g');
	end
end
grid on
subplot(1,2,2)
semilogy(width/2+st:width/2+et,1000*(pos_real(3,:)-pos_err(3,:)),'-g*')
xlabel('像素坐标')
ylabel('Z_c(mm)')
title('亚像素误差下的深度偏差')
h1=refline(0,10);
set(h1,'color','red');%辅助线颜色
hold on
for i = 1:size(pos_err,2)
	if (pos_real(3,i)-pos_err(3,i))*1000<10
		plot([width/2+st+i-1,width/2+st+i-1],[0,(pos_real(3,i)-pos_err(3,i))*1000],'*-b');
	end
end
grid on
%% 坐标函数
function out = camCoordinate(param)
dxl=param.dxl;dyl=param.dyl;
dxr=param.dxr;dyr=param.dyr;
fl=param.fl;fr=param.fr;

ul=param.ul;vl=param.vl;
ur=param.ur;vr=param.vr;
ul0 = param.ul0;vl0 = param.vl0;
ur0 = param.ur0;vr0 = param.vr0;
tx = param.tx;
ty = param.ty;
tz = param.tz;

c11 = param.Rc(1,1);c12 = param.Rc(1,2);c13 = param.Rc(1,3);
c21 = param.Rc(2,1);c22 = param.Rc(2,2);c23 = param.Rc(2,3);
c31 = param.Rc(3,1);c32 = param.Rc(3,2);c33 = param.Rc(3,3);


X_cl = (dxl*(ul - ul0)*( fr*tx - tz*dxr*( ur - ur0 ) ))/( c31*dxl*( ul - ul0 ) + c32*dyl*( vl - vl0 ) + fl*c33 )*dxr*( ur - ur0 ) - fr*( c11*dxl*( ul - ul0 ) + c12*dyl*( vl - vl0 ) + fl*c13 );
Y_cl = (dyl*(vl - vl0)*( fr*tx - tz*dxr*( ur - ur0 ) ))/( c31*dxl*( ul - ul0 ) + c32*dyl*( vl - vl0 ) + fl*c33 )*dxr*( ur - ur0 ) - fr*( c11*dxl*( ul - ul0 ) + c12*dyl*( vl - vl0 ) + fl*c13 );
Z_cl = (fl*( fr*tx - tz*dxr*( ur - ur0 ) ))/(( c31*dxl*( ul - ul0 ) + c32*dyl*( vl - vl0 ) + fl*c33 )*dxr*( ur - ur0 ) - fr*( c11*dxl*( ul - ul0 ) + c12*dyl*( vl - vl0 ) + fl*c13 ));
out = [X_cl;Y_cl;Z_cl];
end