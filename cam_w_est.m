clear,clc,close all
if isempty(imaqfind)~=1
	delete(imaqfind)% 关闭正在占用的摄像头
end

imaqhwinfo;
obj1 = videoinput('winvideo',1,'YUY2_640x480');
set(obj1,'ReturnedColorSpace','rgb');
triggerconfig(obj1,'manual');
fig1=figure(1);
load('cam_w_param.mat');

hImage = imshow(zeros(480,640));
setappdata(hImage,'UpdatePreviewWindowFcn',@update_livehistogram_display);

start(obj1);
% for i = 1:500
i = 1;
while(1)
	snapshot1 = getsnapshot(obj1);
	snapshot2 = getsnapshot(obj1);
% 	subplot(1,2,1)
% 	imagesc(snapshot1);
% 	subplot(1,2,2)
% 	imagesc(snapshot2);
% 	drawnow
	img1 = rgb2gray(snapshot1);
	img2 = rgb2gray(snapshot2);
	points1 = detectSURFFeatures(img1);
	points2 = detectSURFFeatures(img2); 
end
