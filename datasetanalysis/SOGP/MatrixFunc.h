//
// Created by KingBoy on 2019/2/11.
//

#ifndef SOGP_MATRIXFUNC_H
#define SOGP_MATRIXFUNC_H

/**
 * @brief cholesky分解函数
 * @param orig 源矩阵 n x n
 * @param n 维数
 * @param aug 增广矩阵
 * @param mcol 增广矩阵的列数
 * @param chol 上三角矩阵
 * @param cholaug
 * @param ofs
 * @return
 */
int cholesky(double **orig, int n, double **aug, int mcol,double **chol, double **cholaug, int ofs);

#endif //SOGP_MATRIXFUNC_H
