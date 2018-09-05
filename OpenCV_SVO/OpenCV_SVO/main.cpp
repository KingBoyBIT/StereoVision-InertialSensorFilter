#include <stdio.h>
#include <stdlib.h>
#include <opencv2\core\core.hpp>
#include <opencv2\opencv.hpp>
#include <opencv\highgui.h>
#include "commonHeader\types.h"
#include "commonHeader\err_def.h"
#include "commonHeader\alldef.h"

using namespace cv;
UINT32 ret = 0;
int main()
{
	
	VideoCapture cap;
	cap.open(0);

	if (cap.isOpened() != true)
	{
		return ERR_OPEN_CAM;
	}
	while (1)
	{
		//Mat img = imread("Lenna_(test_image).png");
		Mat frame;
		cap >> frame;
		if (frame.data == NULL)
		{
			return ERR_OPEN_IMG;
		}
#if DEBUG_FLAG
		printf("size of img:%d,%d\n", frame.rows, frame.cols);
		imshow("lena", frame);
#if 1
		ret = waitKey(20);//会崩溃，没事不要用
#endif
		if (ret != -1)
		{
			return ret;
		}
#endif // DEBUG_FLAG
	}
	return ret;
}