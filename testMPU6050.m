clearvars -except s
clc,close all
fclose(instrfind)% �ر�����ռ�õĴ���

baudrate = 9600;
portnames = seriallist;
for i = 1:length(portnames)
	if portnames(i) ~= "COM1"
		portname = portnames(i);
		break
	end
end
% ��������OBJ
sport=serial(portname,'BaudRate',baudrate);
% s.status %����״̬
fopen(sport)%�򿪴���
% fwrite(s,255)%���ͣ��ò���
cmd = [];

% ����200��
% for i = 1:1
while(1)
	while(1)
		ch = fread(sport,1,'uchar');% ����֡ͷ1�ֽ�
		if ch == 36%'$'
			cmd = [cmd;ch];
			ch = fread(sport,2,'uchar');% ����֡��
			cmd = [cmd;ch];
% 			ch = fread(sport,ch(1)*256+ch(2)-1,'uchar');%��������֡ʣ�ಿ��
% 			cmd = [cmd;ch];
% 			% CRCУ����ʱpass
% 			break;
		end
	end
	%����֡
	if length(cmd)~=11
		data = CMDdec(cmd);
		fprintf("ת�٣�%f\n",data.speed);
		fprintf("0ƫ��%f\n",data.zerobias)
		fprintf("��ʼ��ƫ��%f\n",data.iniradangle)
		for i = 1:length(data.strength)
			fprintf("��%d���ź�ǿ�ȣ�%f\n",i,data.strength(i))
			fprintf("��%d��ʵ�ʾ��룺%f mm\n",i,data.dis(i))
		end
	else
		cmd = [];
		continue;
	end
	cmd = [];
end
%% CLOSE
fclose(sport)%�رմ���2
fclose(instrfind)
