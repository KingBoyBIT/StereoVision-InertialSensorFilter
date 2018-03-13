#include "QtGuiVisionPrj.h"
#include <QMessageBox>
#include <QTimer>
#include <QTime>
#include <qapplication.h>
#include <opencv2/core.hpp>
#include <opencv2/highgui.hpp>
#include <opencv2/videoio.hpp> // for camera
#include <opencv.hpp>
#include <QtSerialPort/QSerialPort>  
#include <QtSerialPort/QSerialPortInfo>  
#include "cxcore.h"

using namespace cv;

QtGuiVisionPrj::QtGuiVisionPrj(QWidget *parent)
	: QMainWindow(parent)
{
	ui.setupUi(this);
	// 初始化
	timer = new QTimer(this);
	timer->stop();
	connect(ui.OpenCamBtn, SIGNAL(clicked()), this, SLOT(opencam()));
	connect(timer, SIGNAL(timeout()), this, SLOT(nextFrame()));
	connect(ui.CloseCamBtn, SIGNAL(clicked()), this, SLOT(closeCamara()));
	connect(ui.CamshotBtn, SIGNAL(clicked()), this, SLOT(camshot()));

	foreach(const QSerialPortInfo &info, QSerialPortInfo::availablePorts())
	{
		QSerialPort serial;
		serial.setPort(info);
		if (serial.open(QIODevice::ReadWrite))
		{
			ui.PortBox->addItem(serial.portName());
			serial.close();
		}
	}
	ui.PortBox->setCurrentIndex(1);
	connect(ui.openSerialButton, SIGNAL(clicked()), this, SLOT(OpenSerial()));
	connect(ui.closeSerialButton, SIGNAL(clicked()), this, SLOT(CloseSerial()));
}
void QtGuiVisionPrj::opencam()
{
	if (cap1.isOpened())
	{
		cap1.release();
	}
		
	if (cap2.isOpened())
	{
		cap2.release();
	}
	double rate = cap1.get(CV_CAP_PROP_FPS);
	try
	{
		cap1.open(1);
		//cap1.set(CV_CAP_PROP_FRAME_WIDTH, 320);
		//cap1.set(CV_CAP_PROP_FRAME_HEIGHT, 240);
		//delay(500);
		cap2.open(0);
		//cap2.set(CV_CAP_PROP_FRAME_WIDTH, 320);
		//cap2.set(CV_CAP_PROP_FRAME_HEIGHT, 240);
		cap1 >> framewhite;
		cap2 >> frameblack;

		if (!framewhite.empty())
		{
			timer->setInterval(rate);
			timer->start();
		}

	}
	catch (const std::exception&)
	{
		QMessageBox::critical(NULL, "ERROR", "打开失败", QMessageBox::Close);
	}
}
static void delay(int mils)
{
	QTime reachtime = QTime::currentTime().addMSecs(mils);
	while (QTime::currentTime()< reachtime)
	{
		QCoreApplication::processEvents(QEventLoop::AllEvents, 100);
	}
}
static QImage Mat2QImage(Mat& image)
{
	QImage img;

	if (image.channels() == 3) {
		cvtColor(image, image, CV_BGR2RGB);
		img = QImage((const unsigned char *)(image.data), image.cols, image.rows,
			image.cols*image.channels(), QImage::Format_RGB888);
	}
	else if (image.channels() == 1) {
		img = QImage((const unsigned char *)(image.data), image.cols, image.rows,
			image.cols*image.channels(), QImage::Format_ARGB32);
	}
	else {
		img = QImage((const unsigned char *)(image.data), image.cols, image.rows,
			image.cols*image.channels(), QImage::Format_RGB888);
	}

	return img;
}

void QtGuiVisionPrj::nextFrame()
{
	cap1 >> framewhite;
	cap2 >> frameblack;
	if (!framewhite.empty())
	{
		imagewhite = Mat2QImage(framewhite);
		QImage* imgScaled = new QImage;
		QImage* imgc = &imagewhite;
		*imgScaled = imgc->scaled(ui.campicwhite->width(), ui.campicwhite->height(), Qt::KeepAspectRatio);

		ui.campicwhite->setPixmap(QPixmap::fromImage(*imgScaled));
	}
	if (!frameblack.empty())
	{
		imageblack = Mat2QImage(frameblack);
		QImage* imgScaled = new QImage;
		QImage* imgc = &imageblack;
		*imgScaled = imgc->scaled(ui.campicblack->width(), ui.campicblack->height(), Qt::KeepAspectRatio);

		ui.campicblack->setPixmap(QPixmap::fromImage(*imgScaled));
	}
}
void QtGuiVisionPrj::closeCamara()
{
	timer->stop();//停止读取数据。
	cap1.release();//释放内存；
	cap2.release();
}
void QtGuiVisionPrj::camshot()
{
	QImage* imgScaled = new QImage;
	QImage* imgc = &imagewhite;
	*imgScaled = imgc->scaled(ui.campicwhite->width(), ui.campicwhite->height(), Qt::KeepAspectRatio);

	ui.campicwhiteshot->setPixmap(QPixmap::fromImage(*imgScaled));
	imgc = &imageblack;

	*imgScaled = imgc->scaled(ui.campicblack->width(), ui.campicblack->height(), Qt::KeepAspectRatio);

	ui.campicblackshot->setPixmap(QPixmap::fromImage(*imgScaled));
}
void QtGuiVisionPrj::OpenSerial()
{
	serial = new QSerialPort;
	//设置串口名  
	serial->setPortName(ui.PortBox->currentText());
	//打开串口  
	serial->open(QIODevice::ReadWrite);
	//设置波特率  
	serial->setBaudRate(9600);
	//设置数据位数  
	serial->setDataBits(QSerialPort::Data8);
	//设置奇偶校验  
	serial->setParity(QSerialPort::NoParity);
	//设置停止位  
	serial->setStopBits(QSerialPort::OneStop);
	//设置流控制  
	serial->setFlowControl(QSerialPort::NoFlowControl);
	connect(serial, &QSerialPort::readyRead, this, &QtGuiVisionPrj::Read_Data);
}

void QtGuiVisionPrj::	CloseSerial()
{
	serial->clear();
	serial->close();
	serial->deleteLater();
}

static uchar checkckv(QByteArray buff, uchar len)
{
	uchar ret = 0x5a;
	uchar i = 0;
	for (i = 0; i < len; i++) {
		ret += buff[i] ^ ret;
	}
	return ret;
}

void QtGuiVisionPrj::Read_Data()
{
	int flag = 0;
	int i = 0,startpt = 0;
	int accx = 0, accy = 0, accz = 0, gyrox = 0, gyroy = 0, gyroz = 0;
	QByteArray buf;
	
	try
	{
		buf = serial->readAll();
		if (!buf.isEmpty())
		{
			i = 0;
			while (buf[i++] != '$')
			{
				;
			}
			startpt = i--;
			QByteArray data;
			for (i = 0; i < 30; i++)
			{
				data.append(buf[i]);
			}
			if ((uchar)data[27] == checkckv(data, 28))
			{
				int ct = 3;
				flag = 1;
				accx |= data[ct++];
				accx = (accx << 8);
				accx |= data[ct++];
				accx = (accx << 8);
				accx |= data[ct++];
				accx = (accx << 8);
				accx |= data[ct++];
				accy |= data[ct++];
				accy = (accy << 8);
				accy |= data[ct++];
				accy = (accy << 8);
				accy |= data[ct++];
				accy = (accy << 8);
				accy |= data[ct++];
				accz |= data[ct++];
				accz = (accz << 8);
				accz |= data[ct++];
				accz = (accz << 8);
				accz |= data[ct++];
				accz = (accz << 8);
				accz |= data[ct++];
			}
		}
	}
	catch (const std::exception&)
	{
		flag = 0;
	}
	
	if (flag)
	{
		QString str = "";
		//QString str = ui.textEditin->toPlainText();
		str += "accx:";
		str += QString::number(accx);
		str += "accy:";
		str += QString::number(accy);
		str += "accz:";
		str += QString::number(accz);
		
		//ui.textEditin->clear();
		ui.textEditin->append(str);
	}
	buf.clear();
}