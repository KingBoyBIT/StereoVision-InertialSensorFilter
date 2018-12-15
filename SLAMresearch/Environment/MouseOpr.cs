using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Environment
{
	class MouseOpr
	{
		/// <summary>
		/// 引用user32.dll动态链接库（windows api），
		/// 使用库中定义 API：SetCursorPos
		/// </summary>
		[DllImport("user32.dll")]
		private static extern int SetCursorPos(int x, int y);

		public int globMoCtx;
		/// <summary>
		/// 移动鼠标到指定的坐标点
		/// </summary>
		/// <param name="p">指定坐标</param>
		public void MoveMouseToPoint(Point p)
		{
			SetCursorPos(p.X, p.Y);
		}
		/// <summary>
		/// 设置鼠标的移动范围
		/// <param name="rectangle">范围矩形</param>
		/// </summary>
		public void SetMouseRectangle(Rectangle rectangle)
		{
			System.Windows.Forms.Cursor.Clip = rectangle;
		}
		/// <summary>
		/// 设置鼠标位于屏幕中心
		/// </summary>
		public void SetMouseAtCenterScreen()
		{
			int winHeight = System.Windows.Forms.Screen.PrimaryScreen.WorkingArea.Height;
			int winWidth = System.Windows.Forms.Screen.PrimaryScreen.WorkingArea.Width;
			Point centerP = new Point(0, 0);
			MoveMouseToPoint(centerP);
		}
	}
}
