using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FaceRec
{
	public partial class Form1 : Form
	{
		public Form1()
		{
			InitializeComponent();
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
				data.Add(new database(strlst[i]));
				progressBar1.Value += progressBar1.Step; //让进度条增加一次
			}
			//database.SavePhotoFromUrl(data[0].person+".jpg", data[0].url);

		}
	}
}
