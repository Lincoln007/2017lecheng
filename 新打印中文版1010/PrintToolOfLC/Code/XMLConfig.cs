using System;
using System.Xml;
using System.Configuration;
using System.Data;
using System.Collections;
using System.IO;
using System.Collections.Generic;

namespace XMLClass
{
    public class NewXmlControl : Object
    {
        protected string strXmlFile;
        protected XmlDocument objXmlDoc = new XmlDocument();

        /// <summary>
        /// ���췽��
        /// </summary>
        /// <param name="XmlFile"></param>
        /// <param name="bOverWrite"></param>
        /// <param name="sRoot"></param>
        public NewXmlControl(string XmlFile, Boolean bOverWrite, string sRoot)
        {
            try
            {
                //�������ģʽ����ǿ�д���һ��xml�ĵ�
                if (bOverWrite)
                {
                    objXmlDoc.AppendChild(objXmlDoc.CreateXmlDeclaration("1.0", "utf-8", null));//����xml�İ汾����ʽ��Ϣ
                    objXmlDoc.AppendChild(objXmlDoc.CreateElement("", sRoot,""));//������Ԫ��
                    objXmlDoc.Save(XmlFile);//����
                }
                else //���򣬼���ļ��Ƿ���ڣ��������򴴽�
                {
                    if (!(File.Exists(XmlFile)))
                    {
                        //XMLͷ
                        objXmlDoc.AppendChild(objXmlDoc.CreateXmlDeclaration("1.0", "utf-8", null));
                        //���ڵ�
                        objXmlDoc.AppendChild(objXmlDoc.CreateElement(sRoot));
                        //����
                        objXmlDoc.Save(XmlFile);
                    }
                }
                objXmlDoc.Load(XmlFile);
            }
            catch (System.Exception ex)
            {
                throw ex;
            }
            strXmlFile = XmlFile;
        }



        /// <summary>
        /// ����xPathֵ������xPath�µ������¼��ӽ�ڵ�һ��DataView
        /// </summary>
        /// <param name="XmlPathNode">xPathֵ</param>
        /// <returns>�������򷵻�DataView�����򷵻�null</returns>
        public DataView GetData(string XmlPathNode)
        {
            //�������ݡ�����һ��DataView
            DataSet ds = new DataSet();
            try
            {
                StringReader read = new StringReader(objXmlDoc.SelectSingleNode(XmlPathNode).OuterXml);
                ds.ReadXml(read);
                return ds.Tables[0].DefaultView;
            }
            catch
            {
                //throw;
                return null;
            }
        }
        /// <summary>
        /// ��ȡ�����ļ�
        /// </summary>
        public DataTable ReadXMLfile()
        {
            string innertext=string.Empty;
            DataTable table = new DataTable();
            table.Columns.Add("ID", typeof(string));
            table.Columns.Add("page", typeof(string));
            table.Columns.Add("Model", typeof(string));
            table.Columns.Add("printmachine", typeof(string));
            try
            {
                XmlNodeList nodes = objXmlDoc.GetElementsByTagName("Printer"); //objXmlDoc.GetElementById("Printer");
                for (int i = 0; i < nodes.Count; i++) 
                {
                    XmlNodeList childnodes = nodes[i].ChildNodes;
                    DataRow dr = table.NewRow();
                    dr["ID"] = nodes[i].Attributes["ID"].Value.ToString();
                    dr["printmachine"] = childnodes[0].InnerText;
                    dr["page"] = childnodes[1].InnerText;
                    dr["Model"] = childnodes[2].InnerText;
                    table.Rows.Add(dr);
                }
                return table;
            }
            catch
            {

                return null;
            } 
        }

        /// <summary>
        /// �ж�����ֵ�Ƿ���������Ľڵ㷵��ģʽ
        /// </summary>
        /// <returns></returns>
        public bool IsExitinnertextOfNode(string text,string node)
        {
            
            XmlNodeList nodes = objXmlDoc.GetElementsByTagName(node);
            List<string> text2 = new List<string>();
            for (int i = 0; i < nodes.Count; i++)
            {
                text2.Add(nodes[i].InnerText);
            }
            if (text2.Contains(text))
            {
                return true;
            }
            else
            {
                return false;
            }
        
        }
        /// <summary>
        /// �õ�page�ڵ��model�ڵ������
        /// </summary>
        /// <returns></returns>
        public string GetinnertextOfModel(string text, string node)
        {
            Dictionary<string, XmlNode> mytest = new Dictionary<string, XmlNode>();
            string back = string.Empty;
            XmlNodeList nodes = objXmlDoc.GetElementsByTagName(node);
            if (nodes.Count <= 0)
            {
                return "empty";

            }
            else
            {
                for (int i = 0; i < nodes.Count; i++)
                {
                    mytest.Add(nodes[i].InnerText, (XmlNode)nodes[i]);

                }
                if (mytest.ContainsKey(text))
                {
                    back = mytest[text].ParentNode.ChildNodes[2].InnerText;
                    //back = nodes[i].ParentNode.ChildNodes[0].InnerText;

                }
                else
                {
                    back = "empty";
                }
                return back;
            }
        }
        /// <summary>
        /// �õ�page�ڵ��printmachine�ڵ������
        /// </summary>
        /// <returns></returns>
        public string GetinnertextOfPrintName(string text, string node)
        {
            Dictionary<string, XmlNode> mytest = new Dictionary<string, XmlNode>();
            string back = string.Empty;
            XmlNodeList nodes = objXmlDoc.GetElementsByTagName(node);
            if (nodes.Count <= 0)
            {
                return "empty";

            }
            else
            {
                for (int i = 0; i < nodes.Count; i++)
                {
                    mytest.Add(nodes[i].InnerText, (XmlNode)nodes[i]);
                   
                }
                if (mytest.ContainsKey(text))
                {
                    back=mytest[text].ParentNode.ChildNodes[0].InnerText;
                    //back = nodes[i].ParentNode.ChildNodes[0].InnerText;

                }
                else
                {
                    back = "empty";
                }
                return back;
            }
        }
        /// <summary>
        /// ��ȡ�����ļ��Ƿ�ͨ����֤
        /// </summary>
        public bool ReadXMLfileOfPassport(string Nodename)
        {
        
            string pass = objXmlDoc.SelectSingleNode(Nodename).InnerText;
            if ("up"==pass)
            {
                return true;
            }
            else
            {
                return false;
            }

         
        }
        /// <summary>
        /// ��ȡ�ڵ������
        /// </summary>
        /// <param name="Nodename"></param>
        /// <returns></returns>
        public string ReadNodeInnerText(string Nodename)
        {
           return objXmlDoc.SelectSingleNode(Nodename).InnerText;
        
        }
        /// <summary>
        /// ���½ڵ�����
        /// </summary>
        /// <param name="xmlPathNode"></param>
        /// <param name="content"></param>
        public void UpdateNode(string xmlPathNode, string content)
        {
            objXmlDoc.SelectSingleNode(xmlPathNode).InnerText = content;
            objXmlDoc.Save(Comm.StartupPath + "//config//regedit.xml");//����
        }
        /// <summary>
        /// ɾ�������ļ��еĽڵ���Ϣ
        /// </summary>
        /// <returns></returns>
        public bool DeleteNodeByName(string Xpath)
        {
            try
            {
                XmlElement selectXe = (XmlElement)objXmlDoc.SelectSingleNode(Xpath);
                selectXe.ParentNode.RemoveChild(selectXe);
                objXmlDoc.Save(Comm.StartupPath + "//config//config.xml");//����
                return true;

            }
            catch
            {

                return false;
            }
        
        
        }
       

        /// <summary>
        /// ���½ڵ��ĳ������
        /// </summary>
        /// <param name="xmlPathNode">Ҫ�����Ľڵ�</param>
        /// <param name="AttribName">������</param>
        /// <param name="AttribValue">����ֵ</param>
        public void UpdateNode(string xmlPathNode, string AttribName, string AttribValue)
        {

            ((XmlElement)(objXmlDoc.SelectSingleNode(xmlPathNode))).SetAttribute(AttribName, AttribValue);
        }


        /// <summary>
        /// �޸Ľڵ�(ͬ���������ݺ�����)
        /// </summary>
        /// <param name="xmlPathNode">Ҫ�����ڵ��xpath���</param>
        /// <param name="arrAttribName">���������ַ�������</param>
        /// <param name="arrAttribContent">���������ַ�������</param>
        /// <param name="content">�ڵ�����</param>
        public void UpdateNode(string xmlPathNode, string[] arrAttribName, string[] arrAttribContent, string content)
        {

            XmlNode xn = objXmlDoc.SelectSingleNode(xmlPathNode);
            if (xn != null)
            {
                xn.InnerText = content;
                xn.Attributes.RemoveAll();
                for (int i = 0; i <= arrAttribName.GetUpperBound(0); i++)
                {
                    ((XmlElement)(xn)).SetAttribute(arrAttribName[i], arrAttribContent[i]);
                }

            }
        }

        /// <summary>
        /// �Ƴ�ѡ���ڵ㼯����������
        /// </summary>
        /// <param name="xmlPathNode"></param>
        public void RemoveAllAttribute(string xmlPathNode)
        {
            XmlNodeList xnl = objXmlDoc.SelectNodes(xmlPathNode);
            foreach (XmlNode xn in xnl)
            {
                xn.Attributes.RemoveAll();
            }
        }

        /// <summary>
        /// ɾ��һ���ڵ�
        /// </summary>
        /// <param name="Node"></param>
        public void DeleteNode(string Node)
        {
            //�h��һ���ڵ㡣
            try
            {
                string mainNode = Node.Substring(0, Node.LastIndexOf("/"));
                objXmlDoc.SelectSingleNode(mainNode).RemoveChild(objXmlDoc.SelectSingleNode(Node));
            }
            catch
            {
                //throw;  
                return;
            }
        }

     /// <summary>
        /// ����һ�ڵ�ʹ˽ڵ��һ�ӽڵ㡣
     /// </summary>
     /// <param name="mainNode"></param>
     /// <param name="ChildNode"></param>
     /// <param name="AttribContent"></param>
     /// <param name="Element"></param>
     /// <param name="Content"></param>
        public void InsertNodeWithChild(string mainNode, string ChildNode, string AttribContent, string Element, string cont1, string Element2, string cont2, string Element3, string cont3)
        {
            //���ҽڵ������Ǹ��ڵ�
            XmlNode objRootNode = objXmlDoc.SelectSingleNode(mainNode);
            //�����ӽڵ�
            XmlElement objChildNode = objXmlDoc.CreateElement(ChildNode);
            //�����ӽڵ������ID
            objChildNode.SetAttribute("ID", AttribContent);
            //�����ӽڵ���ӽڵ�
            XmlElement Childone = objXmlDoc.CreateElement(Element);
            Childone.InnerText = cont1;
            objChildNode.AppendChild(Childone);
            XmlElement Childtwo = objXmlDoc.CreateElement(Element2);
            Childtwo.InnerText = cont2;
            objChildNode.AppendChild(Childtwo);
            XmlElement Childthree = objXmlDoc.CreateElement(Element3);
            Childthree.InnerText = cont3;
            objChildNode.AppendChild(Childthree);
            ////����ڵ�
            objRootNode.AppendChild(objChildNode);
            objXmlDoc.Save(Comm.StartupPath + "//config//config.xml");//����
           
        }

        /// <summary>
        /// ����һ���ڵ㣬��һ��Attribute��innerText
        /// </summary>
        /// <param name="mainNode"></param>
        /// <param name="Element">�ڵ�����</param>
        /// <param name="Attrib">Attribute����</param>
        /// <param name="AttribContent">Attributeֵ</param>
        /// <param name="Content">innerTextֵ</param>
        public void InsertNode(string mainNode, string Element, string Attrib, string AttribContent, string Content)
        {
            //�ҵ��ڵ�
            XmlNode objNode = objXmlDoc.SelectSingleNode(mainNode);
            //�����ڵ�
            XmlElement objElement = objXmlDoc.CreateElement(Element);
            //��������
            objElement.SetAttribute(Attrib, AttribContent);
            //��������
            objElement.InnerText = Content;
            objNode.AppendChild(objElement);
            objXmlDoc.Save(Comm.StartupPath + "//config//config.xml");//����
        }

        /// <summary>
        /// ����һ���ڵ㣬��һ��Attribute
        /// </summary>
        /// <param name="mainNode"></param>
        /// <param name="Element">�ڵ�����</param>
        /// <param name="Attrib">Attribute����</param>
        /// <param name="AttribContent">Attributeֵ</param>  
        public void InsertNode(string mainNode, string Element, string Attrib, string AttribContent)
        {
            XmlNode objNode = objXmlDoc.SelectSingleNode(mainNode);
            XmlElement objElement = objXmlDoc.CreateElement(Element);
            objElement.SetAttribute(Attrib, AttribContent);
            objNode.AppendChild(objElement);
          

            objXmlDoc.Save(Comm.StartupPath + "//config//config.xml");//����
        }


        /// <summary>
        /// ����һ���ڵ�
        /// </summary>
        /// <param name="mainNode"></param>
        /// <param name="Element">�ڵ�����</param>      
        public void InsertNode(string mainNode, string Element)
        {
            XmlNode objNode = objXmlDoc.SelectSingleNode(mainNode);
            XmlElement objElement = objXmlDoc.CreateElement(Element);
            objNode.AppendChild(objElement);
        }


        /// <summary>
        /// ����һ���ڵ㣬��������Ժ�һ��inner text
        /// </summary>
        /// <param name="mainNode"></param>
        /// <param name="elementName"></param>
        /// <param name="arrAttributeName"></param>
        /// <param name="arrAttributeContent"></param>
        /// <param name="elementContent"></param>
        public void InsertNode(string mainNode, string elementName, string[] arrAttributeName, string[] arrAttributeContent, string elementContent)
        {
            try
            {
                XmlNode objNode = objXmlDoc.SelectSingleNode(mainNode);
                XmlElement objElement = objXmlDoc.CreateElement(elementName);
                for (int i = 0; i <= arrAttributeName.GetUpperBound(0); i++)
                {
                    objElement.SetAttribute(arrAttributeName[i], arrAttributeContent[i]);
                }
                objElement.InnerText = elementContent;
                objNode.AppendChild(objElement);
            }
            catch
            {
                throw;
                //string t = mainNode;
                //;
            }
        }

        ///<summary>
        ///����һ���ڵ㣬���������
        ///</summary>
        public void InsertNode(string mainNode, string elementName, string[] arrAttributeName, string[] arrAttributeContent)
        {
            try
            {
                XmlNode objNode = objXmlDoc.SelectSingleNode(mainNode);
                XmlElement objElement = objXmlDoc.CreateElement(elementName);
                for (int i = 0; i <= arrAttributeName.GetUpperBound(0); i++)
                {
                    objElement.SetAttribute(arrAttributeName[i], arrAttributeContent[i]);
                }
                //objElement.InnerText = elementContent;
                objNode.AppendChild(objElement);
            }
            catch
            {
                throw;
                //string t = mainNode;
                //;
            }
        }

        /// <summary>
        /// �����ӽڵ�(���������)
        /// </summary>
        /// <param name="parentNode">Ҫ����ĸ��ڵ�</param>
        /// <param name="elementName">����Ľڵ�����</param>
        /// <param name="arrAttributeName">��������[����]</param>
        /// <param name="arrAttributeContent">��������[����]</param>
        /// <param name="elementContent">�ڵ�����</param>
        public void AddChildNode(string parentNodePath, string elementName, string[] arrAttributeName, string[] arrAttributeContent, string elementContent)
        {
            try
            {
                XmlNode parentNode = objXmlDoc.SelectSingleNode(parentNodePath);
                XmlElement objChildElement = objXmlDoc.CreateElement(elementName);
                for (int i = 0; i <= arrAttributeName.GetUpperBound(0); i++)
                {
                    objChildElement.SetAttribute(arrAttributeName[i], arrAttributeContent[i]);
                }
                objChildElement.InnerText = elementContent;
                parentNode.AppendChild(objChildElement);
            }
            catch
            {
                return;
            }

        }

        /// <summary>
        /// �����ӽڵ�(��������CData��ʽд��)
        /// </summary>
        /// <param name="parentNode">Ҫ����ĸ��ڵ�</param>
        /// <param name="elementName">����Ľڵ�����</param>
        /// <param name="elementContent">�ڵ�����</param>
        public void AddChildNodeCData(string parentNodePath, string elementName, string elementContent)
        {
            try
            {
                XmlNode parentNode = objXmlDoc.SelectSingleNode(parentNodePath);
                XmlElement objChildElement = objXmlDoc.CreateElement(elementName);

                //д��cData����
                XmlCDataSection xcds = objXmlDoc.CreateCDataSection(elementContent);

                objChildElement.AppendChild(xcds);
                parentNode.AppendChild(objChildElement);
            }
            catch
            {
                return;
            }

        }


        /// <summary>
        /// �����ӽڵ�(�����ݣ���������)
        /// </summary>
        /// <param name="parentNode">Ҫ����ĸ��ڵ�</param>
        /// <param name="elementName">����Ľڵ�����</param>
        /// <param name="elementContent">�ڵ�����</param>
        public void AddChildNode(string parentNodePath, string elementName, string elementContent)
        {
            try
            {
                XmlNode parentNode = objXmlDoc.SelectSingleNode(parentNodePath);
                XmlElement objChildElement = objXmlDoc.CreateElement(elementName);

                objChildElement.InnerText = elementContent;
                parentNode.AppendChild(objChildElement);
            }
            catch
            {
                return;
            }

        }

        /// <summary>
        /// ����xpathֵ���ҽڵ�
        /// </summary>
        /// <param name="NodePath">Ҫ���ҽڵ��xpathֵ</param>
        /// <returns>�ҵ�����true,���򷵻�true</returns>
        public bool FindNode(string NodePath)
        {
            try
            {
                if (objXmlDoc.SelectSingleNode(NodePath) != null)
                {
                    return true;
                    
                }
                else
                {
                    return false;
                }
            }
            catch
            {
                return false;
            }
        }


        /// <summary>
        ///�����ĵ�
        /// </summary>
        public void Save()
        {
            //�����ĵ���
            try
            {
                objXmlDoc.Save(strXmlFile);
            }
            catch (System.Exception ex)
            {
                throw ex;
            }
            objXmlDoc = null;
        }
        //---------------------------------------------11.25���Ӻ���-------------------------------------------------------------

        /// <summary>    
        /// �����ӽڵ�ڵ�    
        /// </summary>    
        /// <param name="xmldoc"></param>  xml�ĵ�  
        /// <param name="parentnode"></param>���ڵ�    
        /// <param name="name"></param>  �ڵ���  
        /// <param name="value"></param>  �ڵ�ֵ  
        ///   
        public void CreatechildNode(XmlNode parentNode, string name, string value)
        {
            XmlNode node = objXmlDoc.CreateNode(XmlNodeType.Element, name, null);
            node.InnerText = value;
            parentNode.AppendChild(node);
        }
         /// <summary>
         /// �������ڵ���ӽڵ�
         /// </summary>
         /// <param name="parentNode"></param>
         /// <param name="name"></param>
         /// <param name="value"></param>
        public void CreateRootChildNode(string name)
        {
            XmlNode node = objXmlDoc.CreateNode(XmlNodeType.Element, name, null);
           // CreateRootChildNode("PrintList",name,"");
        }
        /// <summary>
        /// �жϽڵ��Ƿ����
        /// </summary>
        /// <param name="ID"></param>
        /// <returns></returns>
        public bool IsExist(int ID)
        {

            return true;

        } 



    }
}

//���÷���
// NewXmlControl xc = new NewXmlControl(Server.MapPath("~/rss.xml"), true, "rss");
// xc.UpdateNode("//rss", "version", "2.0");
// xc.InsertNode("//rss", "channel");
// xc.AddChildNode("/rss/channel", "title", Shop.DAL.sp_netconfig.GetConfigObj().webname);
//// xc.AddChildNode("/rss/channel", "slogan", Shop.DAL.sp_netconfig.GetConfigObj().webname);
// xc.AddChildNode("/rss/channel", "link", Shop.DAL.sp_netconfig.GetConfigObj().weburl);
// xc.AddChildNode("/rss/channel", "language", "zh-cn");
// xc.AddChildNode("/rss/channel", "description", Shop.DAL.sp_netconfig.GetConfigObj().metatwo);
//// xc.AddChildNode("/rss/channel", "copyright", Shop.DAL.sp_netconfig.GetConfigObj().copyright);
// xc.AddChildNode("/rss/channel", "author", Shop.DAL.sp_netconfig.GetConfigObj().webname);
// xc.AddChildNode("/rss/channel", "generator", "Rss Generator By Taoxian");
// DataSet ds = DbHelperSQL.Query("select top 20 pro_ID,pro_Name,pro_CreateTime,pro_Content from sp_product where pro_SaleType=1 and  pro_Stock>0 and pro_Audit=1 order by pro_ID desc");
// for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
// {
//     int j = i + 1;
//     xc.InsertNode("/rss/channel", "item");
//     xc.AddChildNode("/rss/channel/item[" + j.ToString() + "]", "title", ds.Tables[0].Rows[i]["pro_Name"].ToString());
//     xc.AddChildNode("/rss/channel/item[" + j.ToString() + "]", "link", Shop.DAL.sp_netconfig.GetConfigObj().weburl + "/Product/ProductInfo_" + ds.Tables[0].Rows[i]["pro_ID"].ToString() + ".html");
//     xc.AddChildNode("/rss/channel/item[" + j.ToString() + "]", "pubDate", Convert.ToDateTime(ds.Tables[0].Rows[i]["pro_CreateTime"].ToString()).GetDateTimeFormats('r')[0].ToString());               
//     xc.AddChildNode("/rss/channel/item[" + j.ToString() + "]", "author", Shop.DAL.sp_netconfig.GetConfigObj().webname);
//     xc.AddChildNodeCData("/rss/channel/item[" + j.ToString() + "]", "description", ds.Tables[0].Rows[i]["pro_Content"].ToString());
// }
// ds.Dispose();
// xc.Save();
// YZControl.staticFunction.FinalMessage("����RSS�ɹ���", "html.aspx", 0, 2); 