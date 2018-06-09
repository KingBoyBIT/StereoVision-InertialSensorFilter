clear,clc,close all
if isempty(imaqfind)~=1
	delete(imaqfind)% 关闭正在占用的摄像头
end
imaqhwinfo;
obj1 = videoinput('winvideo',1,'YUY2_640x480');
obj2 = videoinput('winvideo',2,'YUY2_640x480');
set(obj1,'ReturnedColorSpace','rgb');
set(obj2,'ReturnedColorSpace','rgb');
triggerconfig(obj1,'manual');  
triggerconfig(obj2,'manual');  
fig1=figure(1);
load('stereo_pair_param.mat');

hImage = imshow(zeros(480,640));
setappdata(hImage,'UpdatePreviewWindowFcn',@update_livehistogram_display);

start(obj1);
start(obj2);
% for i = 1:500
i = 1;
while(1)
    snapshot1 = getsnapshot(obj1);
	snapshot2 = getsnapshot(obj2);
	subplot(2,2,1);
    imagesc(snapshot1);
	drawnow
	subplot(2,2,2);
    imagesc(snapshot2);
	drawnow
	subplot(2,2,3);
	I1 = rgb2gray(snapshot1);
	I2 = rgb2gray(snapshot2);
	points1 = detectSURFFeatures(I1);
	points2 = detectSURFFeatures(I2);
	[f1,vpts1] = extractFeatures(I1,points1);
	[f2,vpts2] = extractFeatures(I2,points2);
	indexPairs = matchFeatures(f1,f2) ;
	matchedPoints1 = vpts1(indexPairs(:,1));
	matchedPoints2 = vpts2(indexPairs(:,2));
	showMatchedFeatures(snapshot1,snapshot2,matchedPoints1,matchedPoints2);
	drawnow
end


%%
delete(obj1);
clear obj1
delete(obj2);
clear obj2