function [target] = OptimTargetDiffTarget(paras)
rng(0);
% ѵ��ǰ׼������
nTrain = 10; %ѵ����ĸ���
nTest = 50; %���Ե�ĸ���
noSamples = 10; % no of samples to be plotted
trData = trainData(nTrain); % ѵ������
teData = testData(nTest); % ��������
y = trData.y;
s = trData.K;
x1 = trData.x;
x2 = trData.x;

K = zeros(size(x1,1),size(x2,1));
for i = 1:size(x1,1)
	for j = 1:size(x2,1)
		param1 = paras(1); %�ɵ�������1
		param2 = paras(3);
		K(i,j) = param1*exp(-0.5*param2*(x1(i)-x2(j))^2);
	end
end
s = K+paras(2).*eye(trData.n);
target = -0.5*log(det(s)) -0.5*y'*inv(s)*y -trData.n /2*log(pi);
s = trData.K-paras(2).*eye(trData.n);
diffs = zeros(trData.n,1);
x1 = trData.x;
x2 = trData.x;
dl1=zeros(length(x1),length(x2));
dl2=zeros(length(x1),length(x2));
dl3=zeros(length(x1),length(x2));

for i = 1:size(x1,1)
	for j = 1:size(x2,1)
		dl1(i,j) = 2*paras(1)*exp(-0.5*((x1(i)-x2(j))/paras(3))^2);
		dl2(i,j) = 2*paras(2)*delta(i,j);
		dl3(i,j) = paras(2)^2*exp(-0.5*((x1(i)-x2(j))/paras(3))^2)*((x1(i)-x2(j))^2)/((paras(3))^3);
	end
end
dl = {dl1,dl2,dl3};
for i = 1:length(paras)
	l = dl{i};
	diffs(i) = 0.5*trace((inv(s)*y)*(inv(s)*y)'*l);
end
end

function trData = trainData(nTrain)
s = 0.0005; %��˹�ֲ��Ĺ۲�����
trData.n = nTrain;
trData.x = sort((10)*rand(nTrain,1) - 5); %[-5 5]��nTrain�������
trData.y = trueFunction(trData.x) + s*randn(nTrain,1); %����ֵ
para0 = [1 0.0005 0.1];
%%%������GP
trData.K = Kernel(trData.x,trData.x,para0) + para0(2).*eye(nTrain); % ����ѵ�����ݵĺ˺���
%%%��������GP
% trData.K = Kernel(trData.x,trData.x,para0);
trData.L = chol(trData.K + 1e-6*eye(trData.n)); %cholesky�ֽ⣨�����������㣩
end

function teData = testData(nTest)
teData.n = nTest;
teData.x = linspace(-5, 5, nTest)';
teData.y = trueFunction(teData.x);
end

function y = trueFunction(x)
    y = sin(0.9*x) + 0.02*(x.^2) + 0.02*(x.^3);
    %y = 0.67*x.^2);
    %y = sin(0.6*x)
end
function K = Kernel(x1,x2,para)
    K = zeros(size(x1,1),size(x2,1));
    param1 = para(1); %�ɵ�������1
    param2 = para(3); % �ɵ�������2��this controls the width of the kernel. Large values makes the fit a straight line
    for i = 1:size(x1,1)
    	for j = 1:size(x2,1)
    	   K(i,j) = param1*exp(-0.5*param2*(x1(i)-x2(j))^2);
    	end
    end
end
function out = delta(i,j)
    if i==j
        out = 1;
    else
        out = 0;
    end
end