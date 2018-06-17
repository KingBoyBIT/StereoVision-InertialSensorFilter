clear,clc,close all
load('stereo_pair_param.mat')

%% 初始化
param.dxl=1e-5;param.dyl=1e-5;
param.dxr=1e-5;param.dyr=1e-5;
param.fl=50/1000;param.fr=50/1000;

param.ul=160;param.vl=120;
param.ur=160;param.vr=120;
param.ul0 = 320;param.vl0 = 240;
param.ur0 = 320;param.vr0 = 240;
param.tx = 50/1000;
param.ty = 0/1000;
param.tz = 0/1000;
param.Rc = angle2dcm(0,0,0);
% c11 = Rc(1,1);c12 = Rc(1,2);c13 = Rc(1,3);
% c21 = Rc(2,1);c22 = Rc(2,2);c23 = Rc(2,3);
% c31 = Rc(3,1);c32 = Rc(3,2);c33 = Rc(3,3);

out = camCoordinate(param);

%% 特征点位置影响对精度的影响



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