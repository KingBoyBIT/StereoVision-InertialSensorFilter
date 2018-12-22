using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FaceRec
{
	public partial class Form1 : Form
	{
		int count1 = 0;
		int count2 = 0;
		public Form1()
		{
			InitializeComponent();
			progressBar1.Maximum = 500000;
			progressBar2.Maximum = 500000;
			timer1.Tick += new EventHandler(timer1_Tick);
			timer1.Start();
		}
		List<database> data;
		private void button1_Click(object sender, EventArgs e)
		{
			FileStream fs = new FileStream("dev_urls.txt", FileMode.Open);
			StreamReader sr = new StreamReader(fs);

			List<string> strlst = new List<string>();

			string str = sr.ReadLine();
			while (str!=null)
			{
				if (str[0]!='#')
				{
					strlst.Add(str);
				}
				str = sr.ReadLine();
			}
			if (strlst.Count!= 16336)
			{
				MessageBox.Show("数据集数目错误");
				return;
			}
			progressBar1.Maximum = strlst.Count;//设置最大长度值
			progressBar1.Value = 0;//设置当前值
			progressBar1.Step = 1;//设置没次增长多少
			data = new List<database>();
			for (int i = 0; i < strlst.Count; i++)
			{
				count1 = i;
				data.Add(new database(strlst[i]));
				//progressBar1.Value += progressBar1.Step; //让进度条增加一次
			}
			//database.SavePhotoFromUrl(data[0].person+".jpg", data[0].url);

		}

		private void button2_Click(object sender, EventArgs e)
		{
			int num = data.Count;
			bool ret = false;
			//num = 5;
			progressBar2.Maximum = num;//设置最大长度值
			progressBar2.Value = 0;//设置当前值
			progressBar2.Step = 1;//设置没次增长多少
			for (int i = 0; i < num; i++)
			{
				count2 = i;
				string filename = data[i].person +" "+ data[i].imagenum.ToString() + ".jpg";
				//filename = i.ToString() + ".jpg";
				ret = database.SavePhotoFromUrl(filename, data[i].url);
				//Thread.Sleep(500);
				if (ret != true)
				{
					//MessageBox.Show("下载失败！");
					//return;
				}
				//string md5calc = database.GetMD5HashFromFile(filename);
				//if (md5calc.Equals(data[i].md5sum)==false)
				//{
				//	MessageBox.Show("校验失败！");
				//	return;
				//}
				//progressBar2.Value += progressBar2.Step;
			}
		}

		private void timer1_Tick(object sender, EventArgs e)
		{
			progressBar1.Value = count1;
			progressBar2.Value = count2;
		}
	}
}
