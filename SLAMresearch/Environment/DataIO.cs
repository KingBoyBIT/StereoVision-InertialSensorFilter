using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace Environment
{
	/// <summary>
	/// 管理数据IO
	/// </summary>
	public class DataIO
	{

		public void CreateXmlMapFile(MapClass map)
		{
			XmlDocument xmlDoc = new XmlDocument();
			//创建类型声明节点    
			XmlNode node = xmlDoc.CreateXmlDeclaration("1.0", "utf-8", "");
			xmlDoc.AppendChild(node);

			List<CP> clist = new List<CP>();
			clist = con.CP.ToList();
			int ii = clist.Count;
			//创建根节点    
			XmlNode root = xmlDoc.CreateElement("User");

			xmlDoc.AppendChild(root);
			for (int i = 0; i < ii; i++)
			{
				CreateNode(xmlDoc, root, "Ids", clist[i].Ids.ToString());
				CreateNode(xmlDoc, root, "Id", clist[i].ID.ToString());
				CreateNode(xmlDoc, root, "PC", clist[i].PJ.ToString());
			}
			try
			{
				xmlDoc.Save("c://data2.xml");
			}
			catch (Exception e)
			{
				//显示错误信息    
				Console.WriteLine(e.Message);
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
		public void CreateNode(XmlDocument xmlDoc, XmlNode parentNode, string name, string value)
		{
			XmlNode node = xmlDoc.CreateNode(XmlNodeType.Element, name, null);
			node.InnerText = value;
			parentNode.AppendChild(node);
		}
	}
}
