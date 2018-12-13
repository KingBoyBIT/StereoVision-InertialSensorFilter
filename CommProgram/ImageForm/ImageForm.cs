using Emgu.CV;
using Emgu.CV.CvEnum;
using Emgu.CV.Structure;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ImageForm
{
	public partial class ImageForm : Form
	{
		const string datasetFilePath = @"D:\Codes\githubcodes\StereoVision-InertialSensorFilter\datasetanalysis\dataset\data_scene_flow\";
		const string testPath = @"\testing\";
		const string trainPath = @"\training\";
		string[] trainPathList = new string[11];

		
		public ImageForm()
		{
			InitializeComponent();
		}


		private void ImageForm_Load(object sender, EventArgs e)
		{
			trainPathList[0] = @"disp_noc_0\";             //左灰度相机视差图，无遮挡
			trainPathList[1] = @"disp_noc_1\";             //右灰度相机视差图，无遮挡
			trainPathList[2] = @"disp_occ_0\";             //左灰度相机视差图，有遮挡
			trainPathList[3] = @"disp_occ_1\";             //右灰度相机视差图，有遮挡
			trainPathList[4] = @"flow_noc\";               //表示在求取光流的两幅图像上都出现的区域的位移误差
			trainPathList[5] = @"flow_occ\";               //表示在第一幅图像中可见而在第二副图像中被遮挡的区域的位移误
			trainPathList[6] = @"image_2\";                    //左侧彩色相机
			trainPathList[7] = @"image_3\";                    //右侧彩色相机
			trainPathList[8] = @"obj_map\";                    //目标
			trainPathList[9] = @"viz_flow_occ\";           //光流可视化，有遮挡
			trainPathList[10] = @"viz_flow_occ_dilate_1\";    //光流可视化，有遮挡，膨胀

		}

		/// <summary>
		/// 载入图像数据
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void LoadImageBtn_Click(object sender, EventArgs e)
		{
			string imagename = "000037_10.png";
			string path = "";
			path = datasetFilePath + trainPath + trainPathList[0] + imagename;
			Image<Gray, byte> image_disp_noc_0 = new Image<Gray, byte>(path);

			path = datasetFilePath + trainPath + trainPathList[1] + imagename;
			Image<Gray, byte> image_disp_noc_1 = new Image<Gray, byte>(path);

			int rowNumber = image_disp_noc_0.Rows;
			int colNumber = image_disp_noc_0.Cols;

			pictureBox1.Image = image_disp_noc_0.ToBitmap();

			double[,] disp_noc_0_data = new double[rowNumber, colNumber];
			for (int i = 0; i < rowNumber; i++)
			{
				for (int j = 0; j < colNumber; j++)
				{
					//最后一个参数为通道数，例如Bgr图片的 0：蓝色，1：绿色，2：红色，Gray的0：灰度，返回TDepth类型
					disp_noc_0_data[i, j] = image_disp_noc_0[i, j].Intensity;
				}
			}

			Bitmap bm = image_disp_noc_0.ToBitmap();

			//CvInvoke.Imshow("test", image_disp_noc_0);
			
			
		}
	}
}
