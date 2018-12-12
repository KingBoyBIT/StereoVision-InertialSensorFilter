using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CommClient
{
	public partial class ClientForm : Form
	{
		private static readonly byte[] Buffer = new byte[1024];

		public ClientForm()
		{
			InitializeComponent();
		}
		
		private void AskServiceBtn_Click(object sender, EventArgs e)
		{
			try
			{
				//①创建一个Socket
				var socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

				//②连接到指定服务器的指定端口
				socket.Connect("127.0.0.1", 7788);

				//WriteLine("Client: Connect to server success!", ConsoleColor.White);

				//③实现异步接受消息的方法 客户端不断监听消息
				socket.BeginReceive(Buffer, 0, Buffer.Length, SocketFlags.None, new AsyncCallback(ReceiveMessage), socket);

				//④接受用户输入，将消息发送给服务器端
				var message = "hello!";
				var outputBuffer = Encoding.UTF8.GetBytes(message);
				socket.BeginSend(outputBuffer, 0, outputBuffer.Length, SocketFlags.None, null, null);
			}
			catch (Exception ex)
			{
				MessageBox.Show("Client: Error " + ex.Message);
			}
			finally
			{
				Console.Read();
			}
		}
		// 接收信息
		public static void ReceiveMessage(IAsyncResult ar)
		{
			try
			{
				var socket = ar.AsyncState as Socket;

				//方法参考：http://msdn.microsoft.com/zh-cn/library/system.net.sockets.socket.endreceive.aspx
				if (socket != null)
				{
					var length = socket.EndReceive(ar);
					var message = Encoding.ASCII.GetString(Buffer, 0, length);
					MessageBox.Show(message);
				}

				//接收下一个消息
				if (socket != null)
				{
					socket.BeginReceive(Buffer, 0, Buffer.Length, SocketFlags.None, new AsyncCallback(ReceiveMessage), socket);
				}
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
			}
		}
	}
}
