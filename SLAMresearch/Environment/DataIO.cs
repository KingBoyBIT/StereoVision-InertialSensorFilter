using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;

namespace Environment
{
	/// <summary>
	/// 管理数据IO
	/// </summary>
	public class DataIO
	{

		public static void CreateXmlMapFile(MapClass map,string filepath)
		{
			XmlDocument xmlDoc = new XmlDocument();
			//创建类型声明节点    
			XmlNode node = xmlDoc.CreateXmlDeclaration("1.0", "utf-8", "");
			xmlDoc.AppendChild(node);

			//创建根节点    
			XmlNode root = xmlDoc.CreateElement("Posptlst");
			xmlDoc.AppendChild(root);
			for (int i = 0; i < map.Posptlst.Count; i++)
			{
				CreateNode(xmlDoc, root, "t", map.Posptlst[i].t.ToString());
				CreateNode(xmlDoc, root, "p", map.Posptlst[i].p.ToString());
			}
			//XmlNode root_lines = xmlDoc.CreateElement("LineStr");
			//xmlDoc.AppendChild(root_lines);
			//for (int i = 0; i < map.linestrlst.Count; i++)
			//{
			//	CreateNode(xmlDoc, root_lines, "t", map.linestrlst[i].startpt.ToString());
			//	CreateNode(xmlDoc, root_lines, "p", map.linestrlst[i].endpt.ToString());
			//}

			try
			{
				xmlDoc.Save(filepath);
			}
			catch (Exception e)
			{
				//显示错误信息    
				MessageBox.Show(e.Message);
			}
			//Console.ReadLine();    
		}
		/// <summary>      
		/// 创建节点      
		/// </summary>      
		/// <param name="xmldoc"></param>  xml文档    
		/// <param name="parentnode"></param>父节点      
		/// <param name="name"></param>  节点名    
		/// <param name="value"></param>  节点值    
		public static void CreateNode(XmlDocument xmlDoc, XmlNode parentNode, string name, string value)
		{
			XmlNode node = xmlDoc.CreateNode(XmlNodeType.Element, name, null);
			node.InnerText = value;
			parentNode.AppendChild(node);
		}
	}
}
