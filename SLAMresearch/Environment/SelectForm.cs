using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Environment
{
	public partial class SelectForm : Form
	{
		List<int> del;
		//实例主窗体
		MainForm pall;
		public SelectForm(MainForm mf)
		{
			InitializeComponent();
			pall = mf;
		}

		private void DeletePointBtn_Click(object sender, EventArgs e)
		{
			del = pall.delete_pt_idx;
			
			List<PointF> delptflst = new List<PointF>();
			for (int i = 0; i < del.Count; i++)
			{
				delptflst.Add(pall.keypoints[del[i]]);
			}
		}


	}
}
