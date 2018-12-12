﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CommProgram
{
	public partial class ServerForm : Form
	{
		private static readonly byte[] Buffer = new byte[1024];
		private static int count;

		public ServerForm()
		{
			InitializeComponent();
		}

		private void StartServiceBtn_Click(object sender, EventArgs e)
		{
			MessageBox.Show("Server is ready.");
			//①创建一个新的Socket,这里我们使用最常用的基于TCP的Stream Socket（流式套接字）
			var socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
			//②将该socket绑定到主机上面的某个端口
			//方法参考：http://msdn.microsoft.com/zh-cn/library/system.net.sockets.socket.bind.aspx
			socket.Bind(new IPEndPoint(IPAddress.Loopback, 7788));
			//③启动监听，并且设置一个最大的队列长度
			//方法参考：http://msdn.microsoft.com/zh-cn/library/system.net.sockets.socket.listen(v=VS.100).aspx
			socket.Listen(3);
			//④开始接受客户端连接请求
			//方法参考：http://msdn.microsoft.com/zh-cn/library/system.net.sockets.socket.beginaccept.aspx
			socket.BeginAccept(new AsyncCallback(ClientAccepted), socket);
			//Console.ReadLine();
		}
		// 客户端连接成功
		public static void ClientAccepted(IAsyncResult ar)
		{
			//设置计数器
			count++;
			var socket = ar.AsyncState as Socket;
			//这就是客户端的Socket实例，我们后续可以将其保存起来
			if (socket != null)
			{
				var client = socket.EndAccept(ar);

				//客户端IP地址和端口信息
				IPEndPoint clientipe = (IPEndPoint)client.RemoteEndPoint;
				MessageBox.Show((clientipe + " is connected，total connects " + count).ToString());
				//接收客户端的消息
				client.BeginReceive(Buffer, 0, Buffer.Length, SocketFlags.None, new AsyncCallback(ReceiveMessage), client);
			}

			//准备接受下一个客户端连接请求
			if (socket != null) socket.BeginAccept(new AsyncCallback(ClientAccepted), socket);
		}

		// 接收客户端的信息
		public static void ReceiveMessage(IAsyncResult ar)
		{
			var socket = ar.AsyncState as Socket;
			//客户端IP地址和端口信息
			if (socket != null)
			{
				IPEndPoint clientipe = (IPEndPoint)socket.RemoteEndPoint;
				try
				{
					//方法参考：http://msdn.microsoft.com/zh-cn/library/system.net.sockets.socket.endreceive.aspx
					var length = socket.EndReceive(ar);
					//读取出来消息内容
					var message = Encoding.UTF8.GetString(Buffer, 0, length);
					//输出接收信息
					MessageBox.Show(clientipe + " ：" + message);
					//服务器发送消息
					socket.Send(Encoding.UTF8.GetBytes("Server received data"));
					//接收下一个消息
					socket.BeginReceive(Buffer, 0, Buffer.Length, SocketFlags.None, new AsyncCallback(ReceiveMessage), socket);
				}
				catch (Exception)
				{
					//设置计数器
					count--;
					//断开连接
					MessageBox.Show(clientipe + " is disconnected，total connects " + (count));
				}
			}
		}
	}
}
