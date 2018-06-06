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
	frameLeftGray  = rgb2gray(snapshot1);
	frameRightGray = rgb2gray(snapshot2);
	disparityMap = disparity(frameLeftGray, frameRightGray);
	imshow(disparityMap, [0, 64]);
	frame1{i} = snapshot1;
	frame2{i} = snapshot2;
end


%%
delete(obj1);
clear obj1
delete(obj2);
clear obj2