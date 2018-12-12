using MathNet.Numerics.LinearAlgebra;
using MathNet.Numerics.LinearAlgebra.Double;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImageForm
{
	public class CamPara
	{
		Matrix<double> K;//相机内参
		public CamPara(double[,] k)
		{
			this.K = DenseMatrix.OfArray(k);
		}
	}
}
