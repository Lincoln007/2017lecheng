using System;
using System.Xml;
using System.Configuration;
using System.Data;
using System.Collections;
using System.IO;

namespace XMLClass
{
    public class NewXmlControl : Object
    {
        protected string strXmlFile;
        protected XmlDocument objXmlDoc = new XmlDocument();


        public NewXmlControl(string XmlFile, Boolean bOverWrite, string sRoot)
        {
            try
            {
                //�������ģʽ����ǿ�д���һ��xml�ĵ�
                if (bOverWrite)
                {
                    objXmlDoc.AppendChild(objXmlDoc.CreateXmlDeclaration("1.0", "utf-8", null));//����xml�İ汾����ʽ��Ϣ
                    objXmlDoc.AppendChild(objXmlDoc.CreateElement("", sRoot, ""));//������Ԫ��
                    objXmlDoc.Save(XmlFile);//����
                }
                else //���򣬼���ļ��Ƿ���ڣ��������򴴽�
                {
                    if (!(File.Exists(XmlFile)))
                    {
                        objXmlDoc.AppendChild(objXmlDoc.CreateXmlDeclaration("1.0", "utf-8", null));
                        objXmlDoc.AppendChild(objXmlDoc.CreateElement("", sRoot, ""));
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
        /// ���½ڵ�����
        /// </summary>
        /// <param name="xmlPathNode"></param>
        /// <param name="content"></param>
        public void UpdateNode(string xmlPathNode, string content)
        {
            objXmlDoc.SelectSingleNode(xmlPathNode).InnerText = content;
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


        public void InsertNodeWithChild(string mainNode, string ChildNode, string Element, string Content)
        {
            //����һ�ڵ�ʹ˽ڵ��һ�ӽڵ㡣
            XmlNode objRootNode = objXmlDoc.SelectSingleNode(mainNode);
            XmlElement objChildNode = objXmlDoc.CreateElement(ChildNode);
            objRootNode.AppendChild(objChildNode);//����ڵ�
            XmlElement objElement = objXmlDoc.CreateElement(Element);
            objElement.InnerText = Content;
            objChildNode.AppendChild(objElement);//�����ӽڵ�
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
            XmlNode objNode = objXmlDoc.SelectSingleNode(mainNode);
            XmlElement objElement = objXmlDoc.CreateElement(Element);
            objElement.SetAttribute(Attrib, AttribContent);
            objElement.InnerText = Content;
            objNode.AppendChild(objElement);
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


        //<summary>
        //����һ���ڵ㣬��������Ժ�һ��inner text
        //</summary>
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