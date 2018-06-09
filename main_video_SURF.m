clear,clc,close all
if isempty(imaqfind)~=1
	delete(imaqfind)% 关闭正在占用的摄像头
end
imaqhwinfo
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
global sp1 sp2
while(1)
    snapshot1 = getsnapshot(obj1);
	snapshot2 = getsnapshot(obj2);
	I1 = rgb2gray(snapshot1);
	I2 = rgb2gray(snapshot2);
	[J1_valid,J2_valid] = rectifyStereoImages(I1,I2,stereoParams, 'OutputView','valid');
	
	subplot(2,2,1);
%     imagesc(J1_valid);
	drawnow 
	subplot(2,2,2);
%     imagesc(J2_valid);
	drawnow
	subplot(2,2,3);
	points1 = detectKAZEFeatures(J1_valid);
	points2 = detectKAZEFeatures(J2_valid);
	[f1,vpts1] = extractFeatures(J1_valid,points1);
	[f2,vpts2] = extractFeatures(J2_valid,points2);
	indexPairs = matchFeatures(f1,f2) ;
	matchedPoints1 = vpts1(indexPairs(:,1));
	matchedPoints2 = vpts2(indexPairs(:,2));
% 	showMatchedFeatures(J1_valid,J2_valid,matchedPoints1,matchedPoints2);
	drawnow
	subplot(2,2,4)
	Z = [];
% 	for i = 1:size(indexPairs,1)
% 		Z(i,1) = stereoParams.TranslationOfCamera2(1)/10 ...
% 			*stereoParams.CameraParameters1.FocalLength(1)/...
% 			(matchedPoints2.Location(i,1)-matchedPoints1.Location(i,1));
% 	end
	for i = 1:size(indexPairs,1)
		Z{i,1} = sprintf('%4.2f', stereoParams.TranslationOfCamera2(1)/10 ...
			*stereoParams.CameraParameters1.FocalLength(1)/...
			(matchedPoints2.Location(i,1)-matchedPoints1.Location(i,1)));
	end
	position = [matchedPoints1.Location,5*ones(size(matchedPoints1.Location,1),1)];
	% 绘制测量的距离
	if isempty(Z)~=1
		
		RGB = insertObjectAnnotation(J1_valid,'circle',position,Z,...
			'TextBoxOpacity',0.9,'FontSize',18);
		imshow(RGB);
	end
	sp1 = snapshot1;
	sp2 = snapshot2;
end


%%
delete(obj1);
clear obj1
delete(obj2);
clear obj2