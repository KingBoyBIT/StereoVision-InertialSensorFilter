%GP regression
%a simple example for a GP regression for predicting a non-linear function
% Reference: http://www.cs.ubc.ca/~nando/540-2013/lectures/
%Author: Michael J Mathew
%Date: 05-04-2016
clear,clc,close all

nTrain = 10; %训练点的个数
nTest = 50; %测试点的个数
noSamples = 10; % no of samples to be plotted
trData = trainData(nTrain); % get the training data; this is a structure
teData = testData(nTest); % generate some test data; this is a structure

teData = findMeanVarTest(trData,teData); % compute the mean and variance of test data

figure(1)
hold on
plot(trData.x, trData.y, '--');
plot(teData.x, teData.y, '.');
xpl = [teData.x', fliplr(teData.x')]; %for making to a continous polygon for filling
ypl = [(teData.mu-3*teData.sig)', fliplr((teData.mu+3*teData.sig)')];
fill(xpl,ypl,[0.5,0.5,0.5],'facealpha',0.35,'EdgeColor','None');
plot(teData.x, teData.mu, 'r--'); %predicted function
legend('train','true','variance','predicted');
title('Mean predictions plus 3 st.deviations')
axis([-5, 5, -3, 3]); hold off;

% draw samples from the prior at our test points.
fPrior =  teData.Lpr*randn(teData.n,noSamples); %draw 10 samples out of the prior distribution
figure(2); cla(); hold on;
plot(teData.x, fPrior);
title(strcat(num2str(noSamples), ' samples from the GP prior'));
axis([-5, 5, -3, 3]); hold off;

% draw samples from the posterior at our test points.
fPost = repmat(teData.mu,1,noSamples) + teData.Lpo*randn(teData.n,noSamples);
figure(3);cla(); hold on;
plot(teData.x, fPost);
title(strcat(num2str(noSamples), ' samples from the GP posterior'));
axis([-5, 5, -3, 3]); hold off;



function y = trueFunction(x)
y = sin(0.9*x) + 0.02*(x.^2) + 0.02*(x.^3); %put any function you want
%y = 0.67*x.^2); %another function
%y = sin(0.6*x)
end

function trData = trainData(nTrain)
s = 0.0005; %noise variance in input data assumed to be guassian.
trData.n = nTrain;
trData.x = sort((10)*rand(nTrain,1) - 5); %random numbers between -5 and 5
trData.y = trueFunction(trData.x) + s*randn(nTrain,1); %compute the function and add some noise
%%%noisy GP
trData.K = Kernel(trData.x,trData.x) + s.*eye(nTrain); % compute the kernel of the data,
%%%noiseless GP
%trData.K = findKernel(trData.x,trData.x);
trData.L = chol(trData.K + 1e-6*eye(trData.n)); %cholesky decomposition
end

function teData = testData(nTest)
teData.n = nTest;
teData.x = linspace(-5, 5, nTest)'; %test data
teData.y = trueFunction(teData.x); %for plotting purposes
end

function K = Kernel(x1,x2)
K = zeros(size(x1,1),size(x2,1));
param1 = 1; %parameters of kernel that could be modified
param2 = 0.10; % this controls the width of the kernel. Large values makes the fit a straight line
for i = 1:size(x1,1)
	for j = 1:size(x2,1)
		K(i,j) = param1*exp(-0.5*param2*(x1(i)-x2(j))^2);
	end
end
end

function teData = findMeanVarTest(trData,teData)
Ks = Kernel(trData.x, teData.x); %Kstar
Kss = Kernel(teData.x, teData.x); %Kstar_star
teData.mu = Ks'*(trData.K\trData.y); %mean
teData.sig = sqrt((diag(Kss - Ks'*(trData.K\Ks)))); %variance
teData.Lpr = chol(Kss + 1e-6*eye(teData.n)); %prior L using cholesky
teData.Lpo = chol(Kss - Ks'*(trData.K\Ks) + 1e-5*eye(teData.n)); %posterior L
end
