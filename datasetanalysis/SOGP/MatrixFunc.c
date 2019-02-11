//
// Created by KingBoy on 2019/2/11.
//

#include "MatrixFunc.h"
/* file: cholesky.c */

#include <math.h>
#include <stdio.h>
#include <stdlib.h>




int cholesky(double **orig, int n, double **aug, int mcol,double **chol, double **cholaug, int ofs)
/*
Do the augmented cholesky decomposition as described in FA Graybill
(1976) Theory and Application of the Linear Model. The original matrix
must be symmetric positive definite. The augmentation matrix, or
series of column vectors, are multiplied by C^-t, where C is the
upper triangular cholesky matrix, ie C^t * C = M and M is the original
matrix. Returns with a value of 0 if M is a non-positive definite
matrix. Returns with a value of 1 with succesful completion.


Arguments:


orig (input) double n x n array. The matrix to take the Cholesky
decomposition of.
n    (input) integer. Number of rows and columns in orig.
aug  (input) double n x mcol array. The matrix for the augmented
part of the decomposition.
mcol (input) integer. Number of columns in aug.
chol (output) double n x n array. Holds the upper triangular matrix
C on output. The lower triangular portion remains unchanged.
This maybe the same as orig, in which case the upper triangular
portion of orig is overwritten.
cholaug (output) double n x mcol array. Holds the product C^-t * aug.
   May be the same as aug, in which case aug is over written.
ofs (input) integer. The index of the first element in the matrices.
Normally this is 0, but commonly is 1 (but may be any integer).
*/
{
	int i, j, k, l;
	int retval = 1;


	for (i = ofs; i < n + ofs; i++)
	{
		chol[i][i] = orig[i][i];
		for (k = ofs; k < i; k++)
		{
			chol[i][i] -= chol[k][i] * chol[k][i];
		}
		if (chol[i][i] <= 0)
		{
			fprintf(stderr, "\nERROR: non-positive definite matrix!\n");
			printf("\nproblem from %d %f\n", i, chol[i][i]);
			retval = 0;
			return retval;
		}
		chol[i][i] = sqrt(chol[i][i]);


		/*This portion multiplies the extra matrix by C^-t */
		for (l = ofs; l < mcol + ofs; l++)
		{
			cholaug[i][l] = aug[i][l];
			for (k = ofs; k < i; k++)
			{
				cholaug[i][l] -= cholaug[k][l] * chol[k][i];
			}
			cholaug[i][l] /= chol[i][i];
		}


		for (j = i + 1; j < n + ofs; j++)
		{
			chol[i][j] = orig[i][j];
			for (k = ofs; k < i; k++)
				chol[i][j] -= chol[k][i] * chol[k][j];
			chol[i][j] /= chol[i][i];
		}
	}


	return retval;
}
