//﻿***************************************
// Update to MPU6050 by shinetop
// MCU: STC89C52
// 2012.3.1
// 功能: 显示加速度计和陀螺仪的10位原始数据
//****************************************
// GY-52 MPU3050 IIC测试程序
// 使用单片机STC89C51
// 晶振：11.0592M
// 显示：LCD1602
// 编译环境 Keil uVision2
// 参考宏晶网站24c04通信程序
// 时间：2011年9月1日
// QQ：531389319
//****************************************
#include <REG52.H>
#include <math.h>    //Keil library
#include <stdio.h>   //Keil library
#include <INTRINS.H>
typedef unsigned char  uchar;
typedef unsigned short ushort;
typedef unsigned int   uint;
//****************************************
// 定义51单片机端口
//****************************************
#define DataPort P0		//LCD1602数据端口
sbit    SCL=P0^0;			//IIC时钟引脚定义
sbit    SDA=P0^1;			//IIC数据引脚定义
sbit	XDA=P0^2;
sbit	XCL=P0^3;
sbit	AD0=P0^4;
sbit	INT=P0^5;
sbit    LCM_RS=P2^0;		//LCD1602命令端口
sbit    LCM_RW=P2^1;		//LCD1602命令端口
sbit    LCM_EN=P2^2;		//LCD1602命令端口
//****************************************
// 定义MPU6050内部地址
//****************************************
#define	SMPLRT_DIV		0x19	//陀螺仪采样率，典型值：0x07(125Hz)
#define	CONFIG			0x1A	//低通滤波频率，典型值：0x06(5Hz)
#define	GYRO_CONFIG		0x1B	//陀螺仪自检及测量范围，典型值：0x18(不自检，2000deg/s)
#define	ACCEL_CONFIG	0x1C	//加速计自检、测量范围及高通滤波频率，典型值：0x01(不自检，2G，5Hz)
#define	ACCEL_XOUT_H	0x3B
#define	ACCEL_XOUT_L	0x3C
#define	ACCEL_YOUT_H	0x3D
#define	ACCEL_YOUT_L	0x3E
#define	ACCEL_ZOUT_H	0x3F
#define	ACCEL_ZOUT_L	0x40
#define	TEMP_OUT_H		0x41
#define	TEMP_OUT_L		0x42
#define	GYRO_XOUT_H		0x43
#define	GYRO_XOUT_L		0x44
#define	GYRO_YOUT_H		0x45
#define	GYRO_YOUT_L		0x46
#define	GYRO_ZOUT_H		0x47
#define	GYRO_ZOUT_L		0x48
#define	PWR_MGMT_1		0x6B	//电源管理，典型值：0x00(正常启用)
#define	WHO_AM_I		0x75	//IIC地址寄存器(默认数值0x68，只读)
#define	SlaveAddress	0xD0	//IIC写入时的地址字节数据，+1为读取
//****************************************
//定义类型及变量
//****************************************
uchar dis[6];							//显示数字(-511至512)的字符数组
int	dis_data;						//变量
//int	Temperature,Temp_h,Temp_l;	//温度及高低位数据

//****************************************
//整数转字符串
//****************************************
void lcd_printf(uchar *s,int temp_data)
{
	if(temp_data<0)
	{
		temp_data=-temp_data;
		*s='-';
	}
	else *s=' ';

	*++s =temp_data/10000+0x30;
	temp_data=temp_data%10000;     //取余运算

	*++s =temp_data/1000+0x30;
	temp_data=temp_data%1000;     //取余运算

	*++s =temp_data/100+0x30;
	temp_data=temp_data%100;     //取余运算
	*++s =temp_data/10+0x30;
	temp_data=temp_data%10;      //取余运算
	*++s =temp_data+0x30;
}
//****************************************

void  SeriPushSend(uchar send_data)
{
    SBUF=send_data;
	while(!TI);TI=0;
}
//****************************************
//延时
//****************************************
void delay(unsigned int k)
{
	unsigned int i,j;
	for(i=0;i<k;i++)
	{
		for(j=0;j<121;j++);
	}
}

//**************************************
//延时5微秒(STC90C52RC@12M)
//不同的工作环境,需要调整此函数
//当改用1T的MCU时,请调整此延时函数
//**************************************
void Delay5us()
{
	_nop_();_nop_();_nop_();_nop_();
	_nop_();_nop_();_nop_();_nop_();
	_nop_();_nop_();_nop_();_nop_();
	_nop_();_nop_();_nop_();_nop_();
	_nop_();_nop_();_nop_();_nop_();
	_nop_();_nop_();_nop_();_nop_();
}
//**************************************
//I2C起始信号
//**************************************
void I2C_Start()
{
    SDA = 1;                    //拉高数据线
    SCL = 1;                    //拉高时钟线
    Delay5us();                 //延时
    SDA = 0;                    //产生下降沿
    Delay5us();                 //延时
    SCL = 0;                    //拉低时钟线
}
//**************************************
//I2C停止信号
//**************************************
void I2C_Stop()
{
    SDA = 0;                    //拉低数据线
    SCL = 1;                    //拉高时钟线
    Delay5us();                 //延时
    SDA = 1;                    //产生上升沿
    Delay5us();                 //延时
}
//**************************************
//I2C发送应答信号
//入口参数:ack (0:ACK 1:NAK)
//**************************************
void I2C_SendACK(bit ack)
{
    SDA = ack;                  //写应答信号
    SCL = 1;                    //拉高时钟线
    Delay5us();                 //延时
    SCL = 0;                    //拉低时钟线
    Delay5us();                 //延时
}
//**************************************
//I2C接收应答信号
//**************************************
bit I2C_RecvACK()
{
    SCL = 1;                    //拉高时钟线
    Delay5us();                 //延时
    CY = SDA;                   //读应答信号
    SCL = 0;                    //拉低时钟线
    Delay5us();                 //延时
    return CY;
}
//**************************************
//向I2C总线发送一个字节数据
//**************************************
void I2C_SendByte(uchar dat)
{
    uchar i;
    for (i=0; i<8; i++)         //8位计数器
    {
        dat <<= 1;              //移出数据的最高位
        SDA = CY;               //送数据口
        SCL = 1;                //拉高时钟线
        Delay5us();             //延时
        SCL = 0;                //拉低时钟线
        Delay5us();             //延时
    }
    I2C_RecvACK();
}
//**************************************
//从I2C总线接收一个字节数据
//**************************************
uchar I2C_RecvByte()
{
    uchar i;
    uchar dat = 0;
    SDA = 1;                    //使能内部上拉,准备读取数据,
    for (i=0; i<8; i++)         //8位计数器
    {
        dat <<= 1;
        SCL = 1;                //拉高时钟线
        Delay5us();             //延时
        dat |= SDA;             //读数据
        SCL = 0;                //拉低时钟线
        Delay5us();             //延时
    }
    return dat;
}
//**************************************
//向I2C设备写入一个字节数据
//**************************************
void Single_WriteI2C(uchar REG_Address,uchar REG_data)
{
    I2C_Start();                  //起始信号
    I2C_SendByte(SlaveAddress);   //发送设备地址+写信号
    I2C_SendByte(REG_Address);    //内部寄存器地址，
    I2C_SendByte(REG_data);       //内部寄存器数据，
    I2C_Stop();                   //发送停止信号
}
//**************************************
//从I2C设备读取一个字节数据
//**************************************
uchar Single_ReadI2C(uchar REG_Address)
{
	uchar REG_data;
	I2C_Start();                   //起始信号
	I2C_SendByte(SlaveAddress);    //发送设备地址+写信号
	I2C_SendByte(REG_Address);     //发送存储单元地址，从0开始
	I2C_Start();                   //起始信号
	I2C_SendByte(SlaveAddress+1);  //发送设备地址+读信号
	REG_data=I2C_RecvByte();       //读出寄存器数据
	I2C_SendACK(1);                //接收应答信号
	I2C_Stop();                    //停止信号
	return REG_data;
}
//**************************************
//初始化MPU6050
//**************************************
void InitMPU6050()
{
	Single_WriteI2C(PWR_MGMT_1, 0x00);	//解除休眠状态
	Single_WriteI2C(SMPLRT_DIV, 0x07);
	Single_WriteI2C(CONFIG, 0x04);
	Single_WriteI2C(GYRO_CONFIG, 0x08);
	Single_WriteI2C(ACCEL_CONFIG, 0x18);
}
//**************************************
//合成数据
//**************************************
int GetData(uchar REG_Address)
{
	uchar H,L;
	H=Single_ReadI2C(REG_Address);
	L=Single_ReadI2C(REG_Address+1);
	return (H<<8)+L;   //合成数据
}
void Delay2us(void)
{
	unsigned char i;
	i = 2;
	while (--i);
}
/**
 * 直接从6050读取数据
 *
 * @author KingBoy (2018/5/20)
 *
 * @param buf
 */
void Read_MPU6050(unsigned char *buf)
{
	unsigned char i;

	I2C_Start();                  //起始信号
	I2C_SendByte(SlaveAddress);   //发送设备地址+写信号
	I2C_SendByte(ACCEL_XOUT_H);    //内部寄存器地址，
	I2C_Start();                   //起始信号
	I2C_SendByte(SlaveAddress + 1);  //发送设备地址+读信号
	for (i = 0; i < 13; i++)
	{
		buf[i] = I2C_RecvByte();    //读出寄存器数据
		SDA = 0;                    //写应答信号
		SCL = 1;                    //拉高时钟线
		Delay2us();
		SCL = 0;                    //拉低时钟线
		Delay2us();
	}
	buf[i] = I2C_RecvByte();    //最后一个字节
	SDA = 1;                    //写非应答信号
	SCL = 1;                    //拉高时钟线
	Delay2us();
	SCL = 0;                    //拉低时钟线
	Delay2us();
	I2C_Stop();                    //停止信号
}
//**************************************
//在1602上显示10位数据
//**************************************
void Display10BitData(int value,uchar x,uchar y)
{  uchar i;
//	value/=64;							//转换为10位数据
	lcd_printf(dis, value);			//转换数据显示
	for(i=0;i<6;i++)
	{
    	SeriPushSend(dis[i]);
    }

  // 	DisplayListChar(x,y,dis,4);	//启始列，行，显示数组，显示长度
}
//**************************************
//显示温度
//**************************************
//void display_temp()
//{
//	Temp_h=Single_ReadI2C(TEMP_OUT_H); //读取温度
//	Temp_l=Single_ReadI2C(TEMP_OUT_L); //读取温度
//	Temperature=Temp_h<<8|Temp_l;     //合成温度
//	Temperature = 35+ ((double) (Temperature + 13200)) / 280; // 计算出温度
//	lcd_printf(dis,Temperature);     //转换数据显示
//	DisplayListChar(11,1,dis,4);     //启始列，行，显示数组，显示位数
//}

void init_uart()
{
	TMOD=0x21;
	TH1=0xfd;
	TL1=0xfd;

	SCON=0x50;
	PS=1;      //串口中断设为高优先级别
	TR0=1;	   //启动定时器
	TR1=1;
	ET0=1;     //打开定时器0中断
	ES=1;
	EA=1;
}
void SerilSendStr(uchar* str,uchar len) {
	uchar i = 0;
	for (i = 0; i < len; i++) {
		SeriPushSend(*(str+i));
	}
}
uchar checkckv(uchar* buff,uchar len)
{
	uchar ret = 0x5a;
	uchar i = 0;
	for (i = 0; i < len; i++) {
		ret += *(buff+i)^ret;
	}
	return ret;
}

unsigned char tp[16];
//*********************************************************
//主程序
//*********************************************************
void main()
{
	uchar sendbuff[200];
	uchar len,ckvlen;
	int tempdata,i;
	delay(5000);		//上电延时
//	InitLcd();		//液晶初始化
	init_uart();
	delay(5000);		//上电延时
	InitMPU6050();	//初始化MPU6050
	while(Single_ReadI2C(WHO_AM_I)!=0x68);
	delay(1500);
	AD0 = 0;
	XDA = 0;
	XCL = 0;
	INT = 0;
	while(1)
	{
		Read_MPU6050(tp);
		len = 0;
		sendbuff[len++] = '$';
		sendbuff[len++] = 0x55;
		sendbuff[len++] = 0xaa;
		for(i = 0;i<14;i++)
			sendbuff[len++] = tp[i];
		/*
		tempdata = GetData(ACCEL_XOUT_H);
		sendbuff[len++] = tempdata&0xff;
		sendbuff[len++] = (tempdata>>8)&0xff;
		sendbuff[len++] = (tempdata>>16)&0xff;
		sendbuff[len++] = (tempdata>>24)&0xff;
		tempdata = GetData(ACCEL_YOUT_H);
		sendbuff[len++] = tempdata&0xff;
		sendbuff[len++] = (tempdata>>8)&0xff;
		sendbuff[len++] = (tempdata>>16)&0xff;
		sendbuff[len++] = (tempdata>>24)&0xff;
		tempdata = GetData(ACCEL_ZOUT_H);
		sendbuff[len++] = tempdata&0xff;
		sendbuff[len++] = (tempdata>>8)&0xff;
		sendbuff[len++] = (tempdata>>16)&0xff;
		sendbuff[len++] = (tempdata>>24)&0xff;
		tempdata = GetData(GYRO_XOUT_H);
		sendbuff[len++] = tempdata&0xff;
		sendbuff[len++] = (tempdata>>8)&0xff;
		sendbuff[len++] = (tempdata>>16)&0xff;
		sendbuff[len++] = (tempdata>>24)&0xff;
		tempdata = GetData(GYRO_YOUT_H);
		sendbuff[len++] = tempdata&0xff;
		sendbuff[len++] = (tempdata>>8)&0xff;
		sendbuff[len++] = (tempdata>>16)&0xff;
		sendbuff[len++] = (tempdata>>24)&0xff;
		tempdata = GetData(GYRO_ZOUT_H);
		sendbuff[len++] = tempdata&0xff;
		sendbuff[len++] = (tempdata>>8)&0xff;
		sendbuff[len++] = (tempdata>>16)&0xff;
		sendbuff[len++] = (tempdata>>24)&0xff;
		ckvlen = len;
		sendbuff[len++] = checkckv(sendbuff,ckvlen);
		*/
		sendbuff[len++] = 0x0d;
		sendbuff[len++] = 0x0a;
		SerilSendStr(sendbuff,len);
		delay(100);
	}
}
