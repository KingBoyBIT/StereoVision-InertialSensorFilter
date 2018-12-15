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
		MainForm mf;
		public SelectForm(MainForm mform)
		{
			InitializeComponent();
			this.mf = mform;
			del = this.mf.delete_pt_idx;
			//添加列表
			List<MapKeyPoint> delptflst = new List<MapKeyPoint>();
			for (int i = 0; i < del.Count; i++)
			{
				delptflst.Add(this.mf.Posptlst[del[i]]);
				string str = "point:" + this.mf.Posptlst[del[i]].p.X.ToString() + "," + this.mf.Posptlst[del[i]].p.Y.ToString();
				this.checkedListBox1.Items.Add(str, false);
			}
			//绘制局部大图

		}

		private void DeletePointBtn_Click(object sender, EventArgs e)
		{
			
		}


	}
}
