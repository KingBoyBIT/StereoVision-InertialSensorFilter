#include "QtGuiVisionPrj.h"
#include <QMessageBox>
#include <QTimer>
#include <opencv2/core.hpp>
#include <opencv2/highgui.hpp>
#include <opencv2/videoio.hpp> // for camera
#include <opencv.hpp>

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
}
void QtGuiVisionPrj::opencam()
{
	if (cap1.isOpened())
		cap1.release();
	if (cap2.isOpened())
		cap2.release();
	double rate = cap1.get(CV_CAP_PROP_FPS);
	try
	{
		cap1.open(0);
		//Sleep(50);
		cap2.open(1);
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