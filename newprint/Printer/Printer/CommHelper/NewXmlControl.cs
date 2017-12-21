using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml;

namespace Printer.CommHelper
{
    public class NewXmlControl : Object
    {
        protected string strXmlFile;
        protected XmlDocument objXmlDoc = new XmlDocument();

        /// <summary>
        /// 构造方法
        /// </summary>
        /// <param name="XmlFile"></param>
        /// <param name="bOverWrite"></param>
        /// <param name="sRoot"></param>
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
                        //XML头
                        objXmlDoc.AppendChild(objXmlDoc.CreateXmlDeclaration("1.0", "utf-8", null));
                        //根节点
                        objXmlDoc.AppendChild(objXmlDoc.CreateElement(sRoot));
                        //保存
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
        /// 读取配置文件
        /// </summary>
        public DataTable ReadXMLfile()
        {
            string innertext = string.Empty;
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

        public string Readconnstring()
        {
            string conn=string.Empty;
            XmlNodeList nodes = objXmlDoc.GetElementsByTagName("configSections");
            conn = nodes[0].InnerText;
            return conn;
        }

        /// <summary>
        /// 判断内容值是否存在这样的节点返回模式
        /// </summary>
        /// <returns></returns>
        public bool IsExitinnertextOfNode(string text, string node)
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
        /// 得到page节点的model节点的内容
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
        /// 得到page节点的printmachine节点的内容
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
                    back = mytest[text].ParentNode.ChildNodes[0].InnerText;
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
        /// 读取配置文件是否通过验证
        /// </summary>
        public bool ReadXMLfileOfPassport(string Nodename)
        {

            string pass = objXmlDoc.SelectSingleNode(Nodename).InnerText;
            if ("up" == pass)
            {
                return true;
            }
            else
            {
                return false;
            }


        }
        /// <summary>
        /// 读取节点的内容
        /// </summary>
        /// <param name="Nodename"></param>
        /// <returns></returns>
        public string ReadNodeInnerText(string Nodename)
        {
            return objXmlDoc.SelectSingleNode(Nodename).InnerText;

        }
        /// <summary>
        /// 更新节点内容
        /// </summary>
        /// <param name="xmlPathNode"></param>
        /// <param name="content"></param>
        public void UpdateNode(string xmlPathNode, string content)
        {
            objXmlDoc.SelectSingleNode(xmlPathNode).InnerText = content;
            objXmlDoc.Save(Comm.StartupPath + "//config//regedit.xml");//保存
        }
        /// <summary>
        /// 删除配置文件中的节点信息
        /// </summary>
        /// <returns></returns>
        public bool DeleteNodeByName(string Xpath)
        {
            try
            {
                XmlElement selectXe = (XmlElement)objXmlDoc.SelectSingleNode(Xpath);
                selectXe.ParentNode.RemoveChild(selectXe);
                objXmlDoc.Save(Comm.StartupPath + "//config//config.xml");//保存
                return true;

            }
            catch
            {

                return false;
            }


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

        /// <summary>
        /// 删除一个节点
        /// </summary>
        /// <param name="Node"></param>
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

        /// <summary>
        /// 插入一节点和此节点的一子节点。
        /// </summary>
        /// <param name="mainNode"></param>
        /// <param name="ChildNode"></param>
        /// <param name="AttribContent"></param>
        /// <param name="Element"></param>
        /// <param name="Content"></param>
        public void InsertNodeWithChild(string mainNode, string ChildNode, string AttribContent, string Element, string cont1, string Element2, string cont2, string Element3, string cont3)
        {
            //查找节点这里是根节点
            XmlNode objRootNode = objXmlDoc.SelectSingleNode(mainNode);
            //创建子节点
            XmlElement objChildNode = objXmlDoc.CreateElement(ChildNode);
            //设置子节点的属性ID
            objChildNode.SetAttribute("ID", AttribContent);
            //创建子节点的子节点
            XmlElement Childone = objXmlDoc.CreateElement(Element);
            Childone.InnerText = cont1;
            objChildNode.AppendChild(Childone);
            XmlElement Childtwo = objXmlDoc.CreateElement(Element2);
            Childtwo.InnerText = cont2;
            objChildNode.AppendChild(Childtwo);
            XmlElement Childthree = objXmlDoc.CreateElement(Element3);
            Childthree.InnerText = cont3;
            objChildNode.AppendChild(Childthree);
            ////插入节点
            objRootNode.AppendChild(objChildNode);
            objXmlDoc.Save(Comm.StartupPath + "//config//config.xml");//保存

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
            //找到节点
            XmlNode objNode = objXmlDoc.SelectSingleNode(mainNode);
            //创建节点
            XmlElement objElement = objXmlDoc.CreateElement(Element);
            //设置属性
            objElement.SetAttribute(Attrib, AttribContent);
            //设置内容
            objElement.InnerText = Content;
            objNode.AppendChild(objElement);
            objXmlDoc.Save(Comm.StartupPath + "//config//config.xml");//保存
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


            objXmlDoc.Save(Comm.StartupPath + "//config//config.xml");//保存
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


        /// <summary>
        /// 插入一个节点，带多个属性和一个inner text
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
        //---------------------------------------------11.25增加函数-------------------------------------------------------------

        /// <summary>    
        /// 创建子节点节点    
        /// </summary>    
        /// <param name="xmldoc"></param>  xml文档  
        /// <param name="parentnode"></param>父节点    
        /// <param name="name"></param>  节点名  
        /// <param name="value"></param>  节点值  
        ///   
        public void CreatechildNode(XmlNode parentNode, string name, string value)
        {
            XmlNode node = objXmlDoc.CreateNode(XmlNodeType.Element, name, null);
            node.InnerText = value;
            parentNode.AppendChild(node);
        }
        /// <summary>
        /// 创建根节点的子节点
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
        /// 判断节点是否存在
        /// </summary>
        /// <param name="ID"></param>
        /// <returns></returns>
        public bool IsExist(int ID)
        {

            return true;

        }



    }
}
