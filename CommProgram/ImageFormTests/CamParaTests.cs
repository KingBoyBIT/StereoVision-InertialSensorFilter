using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using Emgu.CV;
using Emgu.CV.Structure;

namespace ImageForm.Tests
{
	[TestClass()]
	public class CamParaTests
	{
		/// <summary>
		/// 获取或设置测试上下文，上下文提供
		/// 有关当前测试运行及其功能的信息。
		///</summary>
		private TestContext testContextInstance;

		const string datasetFilePath = @"D:\Codes\githubcodes\StereoVision-InertialSensorFilter\datasetanalysis\dataset\data_scene_flow\";
		const string testPath = @"testing\";
		const string trainPath = @"training\";
		string[] trainPathList = new string[11];

		#region 附加测试特性
		// 
		//编写测试时，还可使用以下特性:
		//
		//使用 ClassInitialize 在运行类中的第一个测试前先运行代码
		//[ClassInitialize()]
		//public static void MyClassInitialize(TestContext testContext)
		//{
		//}
		//
		//使用 ClassCleanup 在运行完类中的所有测试后再运行代码
		//[ClassCleanup()]
		//public static void MyClassCleanup()
		//{
		//}
		//
		//使用 TestInitialize 在运行每个测试前先运行代码
		[TestInitialize()]
		public void MyTestInitialize()
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
		//
		//使用 TestCleanup 在运行完每个测试后运行代码
		//[TestCleanup()]
		//public void MyTestCleanup()
		//{
		//}
		//
		#endregion

		/// <summary>
		/// 相机模型测试
		/// </summary>
		[TestMethod()]
		public void CamParaTest()
		{

			string imagename = "000037_10.png";
			string path = datasetFilePath + trainPath + trainPathList[0] + imagename;
			Image<Bgr, byte> image = new Image<Bgr, byte>(path);

			CvInvoke.Imshow("testimage", image);
			System.Console.WriteLine(datasetFilePath + trainPath + trainPathList[0]);


		}
	}
}