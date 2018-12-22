using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace FaceRec
{
	public class database
	{
		public string person;
		public int imagenum;
		public string url;
		public int[] rect;
		public string md5sum;

		public database(string str)
		{
			string[] datas = str.Split('\t');
			this.person = datas[0];
			this.imagenum = Convert.ToInt32(datas[1]);
			this.url = datas[2];

			string tmpstr = datas[3];
			this.rect = new int[tmpstr.Split(',').Length];
			for (int i = 0; i < tmpstr.Split(',').Length; i++)
			{
				this.rect[i] = Convert.ToInt32(tmpstr.Split(',')[i]);
			}
			this.md5sum = (datas[4]);

		}

		/// <summary>
		/// 字符串转16进制字节数组
		/// </summary>
		/// <param name="hexString"></param>
		/// <returns></returns>
		private static byte[] strToToHexByte(string hexString)
		{
			hexString = hexString.Replace(" ", "");
			if ((hexString.Length % 2) != 0)
				hexString += " ";
			byte[] returnBytes = new byte[hexString.Length / 2];
			for (int i = 0; i < returnBytes.Length; i++)
				returnBytes[i] = Convert.ToByte(hexString.Substring(i * 2, 2), 16);
			return returnBytes;
		}
		/// <summary>
		/// 从图片地址下载图片到本地磁盘
		/// </summary>
		/// <param name="ToLocalPath">图片本地磁盘地址</param>
		/// <param name="Url">图片网址</param>
		/// <returns></returns>
		public static bool SavePhotoFromUrl(string FileName, string Url)
		{
			bool Value = false;
			WebResponse response = null;
			Stream stream = null;

			try
			{
				HttpWebRequest request = (HttpWebRequest)WebRequest.Create(Url);

				response = request.GetResponse();
				stream = response.GetResponseStream();

				if (!response.ContentType.ToLower().StartsWith("text/"))
				{
					Value = SaveBinaryFile(response, FileName);
				}

			}
			catch (Exception err)
			{
				string aa = err.ToString();
			}
			return Value;
		}
		/// <summary>
		/// 将二进制文件保存到磁盘
		/// </summary>
		/// <param name="response"></param>
		/// <param name="FileName"></param>
		/// <returns></returns>
		private static bool SaveBinaryFile(WebResponse response, string FileName)
		{
			bool Value = true;
			byte[] buffer = new byte[1024];

			try
			{
				if (File.Exists(FileName))
					File.Delete(FileName);
				Stream outStream = System.IO.File.Create(FileName);
				Stream inStream = response.GetResponseStream();

				int l;
				do
				{
					l = inStream.Read(buffer, 0, buffer.Length);
					if (l > 0)
						outStream.Write(buffer, 0, l);
				}
				while (l > 0);

				outStream.Close();
				inStream.Close();

			}
			catch
			{
				Value = false;
			}
			return Value;
		}

		public static string GetMD5HashFromFile(string fileName)
		{
			try
			{
				FileStream file = new FileStream(fileName, FileMode.Open);
				System.Security.Cryptography.MD5 md5 = new System.Security.Cryptography.MD5CryptoServiceProvider();
				byte[] retVal = md5.ComputeHash(file);
				file.Close();

				StringBuilder sb = new StringBuilder();
				for (int i = 0; i < retVal.Length; i++)
				{
					sb.Append(retVal[i].ToString("x2"));
				}
				return sb.ToString();
			}
			catch (Exception ex)
			{
				throw new Exception("GetMD5HashFromFile() fail,error:" + ex.Message);
			}
		}
	}
}
