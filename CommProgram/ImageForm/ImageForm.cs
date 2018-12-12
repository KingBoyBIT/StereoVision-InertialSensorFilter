using Emgu.CV;
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
		const string datasetFilePath = @"D:\Codes\githubcodes\StereoVision-InertialSensorFilter\datasetanalysis\dataset";
		const string testPath = @"\testing";
		const string trainPath = @"\training";
		string[] trainPathList = new string[11];

		
		public ImageForm()
		{
			InitializeComponent();
		}


		private void ImageForm_Load(object sender, EventArgs e)
		{
			trainPathList[0] = "disp_noc_0";             //左灰度相机视差图，无遮挡
			trainPathList[1] = "disp_noc_1";             //右灰度相机视差图，无遮挡
			trainPathList[2] = "disp_occ_0";             //左灰度相机视差图，有遮挡
			trainPathList[3] = "disp_occ_1";             //右灰度相机视差图，有遮挡
			trainPathList[4] = "flow_noc";               //表示在求取光流的两幅图像上都出现的区域的位移误差
			trainPathList[5] = "flow_occ";               //表示在第一幅图像中可见而在第二副图像中被遮挡的区域的位移误
			trainPathList[6] = "image_2";                    //左侧彩色相机
			trainPathList[7] = "image_3";                    //右侧彩色相机
			trainPathList[8] = "obj_map";                    //目标
			trainPathList[9] = "viz_flow_occ";           //光流可视化，有遮挡
			trainPathList[10] = "viz_flow_occ_dilate_1";    //光流可视化，有遮挡，膨胀
			
		}

		/// <summary>
		/// 载入图像数据
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void LoadImageBtn_Click(object sender, EventArgs e)
		{
			Image<Bgr, Byte> image = new Image<Bgr, byte>("src.png");

		}
	}
}
