function out = framedecSim(indata)
sp = 1;
off = 3;% 数据段起始偏移量
out.accx = uint2int(indata(sp+off+3)*(2^24)+indata(sp+off+2)*(2^16)+indata(sp+off+1)*256+indata(sp+off));off = off + 4;
out.accy = uint2int(indata(sp+off+3)*(2^24)+indata(sp+off+2)*(2^16)+indata(sp+off+1)*256+indata(sp+off));off = off + 4;
out.accz = uint2int(indata(sp+off+3)*(2^24)+indata(sp+off+2)*(2^16)+indata(sp+off+1)*256+indata(sp+off));off = off + 4;
out.gyrox = uint2int(indata(sp+off+3)*(2^24)+indata(sp+off+2)*(2^16)+indata(sp+off+1)*256+indata(sp+off));off = off + 4;
out.gyroy = uint2int(indata(sp+off+3)*(2^24)+indata(sp+off+2)*(2^16)+indata(sp+off+1)*256+indata(sp+off));off = off + 4;
out.gyroz = uint2int(indata(sp+off+3)*(2^24)+indata(sp+off+2)*(2^16)+indata(sp+off+1)*256+indata(sp+off));off = off + 4;
end