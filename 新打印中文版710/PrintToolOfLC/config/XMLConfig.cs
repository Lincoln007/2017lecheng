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
                //如果覆盖模式，则强行创建一个xml文档
                if (bOverWrite)
                {
                    objXmlDoc.AppendChild(objXmlDoc.CreateXmlDeclaration("1.0", "utf-8", null));//设置xml的版本，格式信息
                    objXmlDoc.AppendChild(objXmlDoc.CreateElement("", sRoot, ""));//创建根元素
                    objXmlDoc.Save(XmlFile);//保存
                }
                else //否则，检查文件是否存在，不存在则创建
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
        /// 根据xPath值，返回xPath下的所有下级子结节到一个DataView
        /// </summary>
        /// <param name="XmlPathNode">xPath值</param>
        /// <returns>有数据则返回DataView，否则返回null</returns>
        public DataView GetData(string XmlPathNode)
        {
            //查找数据。返回一个DataView
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
        /// 更新节点内容
        /// </summary>
        /// <param name="xmlPathNode"></param>
        /// <param name="content"></param>
        public void UpdateNode(string xmlPathNode, string content)
        {
            objXmlDoc.SelectSingleNode(xmlPathNode).InnerText = content;
        }

        /// <summary>
        /// 更新节点的某个属性
        /// </summary>
        /// <param name="xmlPathNode">要操作的节点</param>
        /// <param name="AttribName">属性名</param>
        /// <param name="AttribValue">属性值</param>
        public void UpdateNode(string xmlPathNode, string AttribName, string AttribValue)
        {

            ((XmlElement)(objXmlDoc.SelectSingleNode(xmlPathNode))).SetAttribute(AttribName, AttribValue);
        }


        /// <summary>
        /// 修改节点(同步更新内容和属性)
        /// </summary>
        /// <param name="xmlPathNode">要操作节点的xpath语句</param>
        /// <param name="arrAttribName">属性名称字符串数组</param>
        /// <param name="arrAttribContent">属性内容字符串数组</param>
        /// <param name="content">节点内容</param>
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
        /// 移除选定节点集的所有属性
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
            //刪除一个节点。
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
            //插入一节点和此节点的一子节点。
            XmlNode objRootNode = objXmlDoc.SelectSingleNode(mainNode);
            XmlElement objChildNode = objXmlDoc.CreateElement(ChildNode);
            objRootNode.AppendChild(objChildNode);//插入节点
            XmlElement objElement = objXmlDoc.CreateElement(Element);
            objElement.InnerText = Content;
            objChildNode.AppendChild(objElement);//插入子节点
        }

        /// <summary>
        /// 插入一个节点，带一个Attribute和innerText
        /// </summary>
        /// <param name="mainNode"></param>
        /// <param name="Element">节点名称</param>
        /// <param name="Attrib">Attribute名称</param>
        /// <param name="AttribContent">Attribute值</param>
        /// <param name="Content">innerText值</param>
        public void InsertNode(string mainNode, string Element, string Attrib, string AttribContent, string Content)
        {
            XmlNode objNode = objXmlDoc.SelectSingleNode(mainNode);
            XmlElement objElement = objXmlDoc.CreateElement(Element);
            objElement.SetAttribute(Attrib, AttribContent);
            objElement.InnerText = Content;
            objNode.AppendChild(objElement);
        }

        /// <summary>
        /// 插入一个节点，带一个Attribute
        /// </summary>
        /// <param name="mainNode"></param>
        /// <param name="Element">节点名称</param>
        /// <param name="Attrib">Attribute名称</param>
        /// <param name="AttribContent">Attribute值</param>  
        public void InsertNode(string mainNode, string Element, string Attrib, string AttribContent)
        {
            XmlNode objNode = objXmlDoc.SelectSingleNode(mainNode);
            XmlElement objElement = objXmlDoc.CreateElement(Element);
            objElement.SetAttribute(Attrib, AttribContent);
            objNode.AppendChild(objElement);
        }


        /// <summary>
        /// 插入一个节点
        /// </summary>
        /// <param name="mainNode"></param>
        /// <param name="Element">节点名称</param>      
        public void InsertNode(string mainNode, string Element)
        {
            XmlNode objNode = objXmlDoc.SelectSingleNode(mainNode);
            XmlElement objElement = objXmlDoc.CreateElement(Element);
            objNode.AppendChild(objElement);
        }


        //<summary>
        //插入一个节点，带多个属性和一个inner text
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
        ///插入一个节点，带多个属性
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
        /// 插入子节点(带多个属性)
        /// </summary>
        /// <param name="parentNode">要插入的父节点</param>
        /// <param name="elementName">插入的节点名称</param>
        /// <param name="arrAttributeName">属性名称[数组]</param>
        /// <param name="arrAttributeContent">属性内容[数组]</param>
        /// <param name="elementContent">节点内容</param>
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
        /// 插入子节点(将内容以CData形式写入)
        /// </summary>
        /// <param name="parentNode">要插入的父节点</param>
        /// <param name="elementName">插入的节点名称</param>
        /// <param name="elementContent">节点内容</param>
        public void AddChildNodeCData(string parentNodePath, string elementName, string elementContent)
        {
            try
            {
                XmlNode parentNode = objXmlDoc.SelectSingleNode(parentNodePath);
                XmlElement objChildElement = objXmlDoc.CreateElement(elementName);

                //写入cData数据
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
        /// 插入子节点(仅内容，不带属性)
        /// </summary>
        /// <param name="parentNode">要插入的父节点</param>
        /// <param name="elementName">插入的节点名称</param>
        /// <param name="elementContent">节点内容</param>
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
        /// 根据xpath值查找节点
        /// </summary>
        /// <param name="NodePath">要查找节点的xpath值</param>
        /// <returns>找到返回true,否则返回true</returns>
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
        ///保存文档
        /// </summary>
        public void Save()
        {
            //保存文档。
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

//调用方法
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
// YZControl.staticFunction.FinalMessage("生成RSS成功！", "html.aspx", 0, 2); 