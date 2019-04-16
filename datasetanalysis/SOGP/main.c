#include <stdio.h>
#include "MatrixFunc.h"




int main()
{
	//printf("Hello, World!\n");
	double orig[3][3] = {0};
	double aug[3][3] = {0};
	double chol[3][3] = {0};
	double cholaug[3][3] = {0};


	int ret = 0;
	//ret = cholesky(orig, 3, aug, 3, chol, cholaug, 0);

	printf("ret = %d\n", ret);
	return 0;
}