#include "stdio.h"
#include "opencv2\opencv.hpp"
#include "commonHeader\types.h"
#include "commonHeader\err_def.h"
#include "commonHeader\alldef.h"
using namespace cv;

int main()
{
	UINT32 ret = 0;
	Mat img = imread("test.jpg");
	if (img.data == NULL)
	{
		return ERR_OPEN_IMG;
	}
#if DEBUG_FLAG
	printf("size of img:%f,%f\n", img.rows, img.cols);
#endif // DEBUG_FLAG

	imshow("lena", img);
	waitKey(1000);
	return 0;
}