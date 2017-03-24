using System;
using System.Collections;
using System.Data;
using System.Xml;

namespace Trouble_Ze
{
    public class XMLOperate
    {
        #region 构造函数

        public XMLOperate()
        {
            //
            // TODO: 在此处添加构造函数逻辑
            //
        }

        #endregion 构造函数

        #region 节点转DataSet

        /// <summary>
        /// 父节点ParentNode中节点名为NodeName重复记录转化为DataSet
        /// </summary>
        /// <param name="XmlString">父节点</param>
        /// <param name="NodeName">节点名称</param>
        /// <param name="DS">转化后的DataSet</param>
        /// <param name="ErrMsg">返回的错误信息</param>
        /// <returns></returns>
        public static bool NodeToDataSet(XmlNode ParentNode, string NodeName, out DataSet DS, out string ErrMsg)
        {
            DS = null;
            ErrMsg = "";
            DataSet TempDS = new DataSet();
            DataTable DT = new DataTable();
            DT = TempDS.Tables.Add(NodeName);
            Queue myQueue = new Queue();
            XmlNode TempNode;
            TempNode = ParentNode;
            try
            {
                myQueue.Enqueue(TempNode);
                while (myQueue.Count > 0)
                {
                    XmlNode Node = (XmlNode)myQueue.Dequeue();
                    if (Node.Name.ToUpper() == NodeName.ToUpper())
                    {
                        if (!Node.HasChildNodes)               //没有子节点
                        {
                            TempDS = null;
                        }
                        for (int i = 0; i < Node.ChildNodes.Count; i++)
                        {
                            if (!DT.Columns.Contains(Node.ChildNodes[i].Name))
                            {
                                DT.Columns.Add(Node.ChildNodes[i].Name, typeof(string));
                            }
                        }
                        DataRow DR = DT.NewRow();
                        foreach (XmlNode eachchild in Node.ChildNodes)
                        {
                            DR[eachchild.Name] = eachchild.InnerText;
                        }
                        DT.Rows.Add(DR);
                    }
                    if (Node.HasChildNodes)
                    {
                        foreach (XmlNode eachchild in Node.ChildNodes)
                        {
                            myQueue.Enqueue(eachchild);
                        }
                    }
                }
                DS = TempDS;
            }
            catch (Exception e)
            {
                ErrMsg = e.Message;
                return false;
            }
            finally
            {
                #region 释放资源

                if (DT != null)
                {
                    DT = null;
                }
                if (myQueue != null)
                {
                    myQueue = null;
                }
                if (TempDS != null)
                {
                    TempDS = null;
                }

                #endregion 释放资源
            }
            return true;
        }

        #endregion 节点转DataSet

        #region 检查Xml报文的合法性

        /// <summary>
        /// 检验XML文档的合法性
        /// </summary>
        /// <param name="XmlString">源XML文档的字符串形式</param>
        /// <param name="Valid">合法true不合法false</param>
        /// <param name="ErrMsg">返回的错误信息</param>
        /// <returns></returns>
        public static bool CheckXml(string XmlString, out bool Valid, out string ErrMsg)//检查Xml文档合法性
        {
            ErrMsg = "";
            string TempString;
            string XmlTemp = XmlString;
            XmlTemp = XmlTemp.Trim();
            TempString = XmlTemp.Substring(2, 3);
            if (TempString.ToUpper() != "XML")
            {
                XmlTemp = "<?xml version=\"1.0\" encoding=\"GB2312\" standalone=\"no\" ?>" + XmlTemp;
            }
            XmlDocument Doc = new XmlDocument();
            try
            {
                Doc.LoadXml(XmlTemp);
            }
            catch (Exception E)
            {
                ErrMsg = "非法XML文档:" + E.Message;
                Valid = false;
                return false;
            }
            finally
            {
                if (Doc != null)
                {
                    Doc = null;
                }
            }
            Valid = true;
            return true;
        }

        #endregion 检查Xml报文的合法性

        #region 父节点下增加子节点

        /// <summary>
        /// 生成XML文档
        /// </summary>
        /// <param name="ParentNode">父节点</param>
        /// <param name="NodeName">节点名称</param>
        /// <param name="NodeValue">节点值</param>
        /// <param name="XmlDoc">返回生成后的XMLDocument</param>
        /// <param name="ErrMsg">返回的错误信息</param>
        /// <returns></returns>
        public static XmlNode AddXmlNode(XmlNode ParentNode, string NodeName, string NodeValue, ref XmlDocument XMLDoc, out string ErrMsg)
        {
            ErrMsg = "";
            XmlNode ReturnNode;
            ReturnNode = null;
            XmlNode E = XMLDoc.CreateNode(XmlNodeType.Element, NodeName, "");
            E.InnerText = NodeValue;
            try
            {
                if (ParentNode == null)
                {
                    ReturnNode = XMLDoc.AppendChild(E);
                }
                else
                {
                    ReturnNode = ParentNode.AppendChild(E);
                }
            }
            catch (Exception e)
            {
                ErrMsg = e.Message;
                return null;
            }
            finally
            {
                if (E != null)
                {
                    E = null;
                }
            }
            return ReturnNode;
        }

        #endregion 父节点下增加子节点

        #region 获取节点的值

        /// <summary>
        /// 返回该节点的值，该节点必须为Element类型，否则不期望值
        /// </summary>
        /// <param name="Node">节点</param>
        /// <param name="NodeValue">节点值</param>
        /// <param name="ErrMsg">返回的错误信息</param>
        /// <returns>返回节点的值</returns>
        public static bool GetNodeValue(XmlNode Node, out string NodeValue, out string ErrMsg)
        {
            ErrMsg = "";
            NodeValue = "";
            string TempNodeValue = "";
            try
            {
                TempNodeValue = Node.InnerText;
            }
            catch (Exception E)
            {
                ErrMsg = E.ToString();
                return false;
            }
            NodeValue = TempNodeValue;
            return true;
        }

        /// <summary>
        /// 获取XML报文字串中第一个符合节点的值,效率比较低，建议不使用。
        /// </summary>
        /// <param name="XmlString">源XML文档的字符串形式</param>
        /// <param name="NodeName">节点名称</param>
        /// <param name="NodeValue">返回的节点值</param>
        /// <param name="ErrMsg">返回的错误信息</param>
        /// <returns></returns>
        public static bool GetNodeValue(string XmlString, string NodeName, out string NodeValue, out string ErrMsg)
        {
            NodeValue = "";
            ErrMsg = "";
            XmlNodeReader XNR = null;
            XmlDocument Doc = new XmlDocument();
            Doc.LoadXml(XmlString);
            XNR = new XmlNodeReader(Doc);
            try
            {
                while (XNR.Read())
                {
                    switch (XNR.NodeType)
                    {
                        case XmlNodeType.Element:
                            if (XNR.Name.ToUpper() == NodeName.ToUpper())
                            {
                                if (XNR.IsStartElement())
                                {
                                    XNR.Read();
                                    NodeValue = XNR.Value;
                                    return true;
                                }
                            }
                            break;
                    }
                }
            }
            catch (Exception e)
            {
                ErrMsg = e.Message;
                return false;
            }
            finally
            {
                if (Doc != null)
                {
                    Doc = null;
                }
                if (XNR != null)
                {
                    XNR = null;
                }
            }
            return true;
        }

        /// <summary>
        /// 获取父节点ParentNode下面子节点名为 NodeName的节点的值
        /// </summary>
        /// <param name="ParentNode">父节点 (XmlNode类型)</param>
        /// <param name="NodeName">子节点名</param>
        /// <param name="NodeValue">子节点值</param>
        /// <param name="ErrMsg">返回的异常信息</param>
        /// <returns></returns>
        public static bool GetNodeValue(XmlNode ParentNode, string NodeName, out string NodeValue, out string ErrMsg)
        {
            ErrMsg = "";
            NodeValue = "";
            try
            {
                if (!ParentNode.HasChildNodes)
                {
                    NodeValue = "";
                }
                else
                {
                    foreach (XmlNode eachchild in ParentNode)
                    {
                        if (eachchild.Name.ToUpper() == NodeName.ToUpper())
                        {
                            NodeValue = eachchild.InnerText;
                        }
                    }
                }
            }
            catch (Exception E)
            {
                ErrMsg = E.Message.Trim();
                return false;
            }
            return true;
        }

        /// <summary>
        /// 获取节点的值(暂时不要使用这个),未经过测试.
        /// </summary>
        /// <param name="XmlString">源XML文档的字符串形式</param>
        /// <param name="ParentNode">父节点</param>
        /// <param name="NodeName">节点名称</param>
        /// <param name="NodeValue">返回的节点的值</param>
        /// <param name="ErrMsg">反活的错误信息</param>
        /// <returns></returns>
        public static bool GetNodeValue(string XmlString, XmlNode ParentNode, string NodeName, out string NodeValue, out string ErrMsg)
        {
            NodeValue = "";
            ErrMsg = "";
            XmlDocument Doc = new XmlDocument();
            Doc.LoadXml(XmlString);
            XmlNodeReader XNR = new XmlNodeReader(Doc);
            try
            {
                while (XNR.Read())
                {
                    switch (XNR.NodeType)
                    {
                        case XmlNodeType.Element:
                            if (XNR.Name.ToUpper() == ParentNode.Name.ToUpper())
                            {
                                while (XNR.Read())
                                {
                                    switch (XNR.NodeType)
                                    {
                                        case XmlNodeType.Element:
                                            if (XNR.IsStartElement())
                                            {
                                                if (XNR.Name.ToUpper() == NodeName.ToString())
                                                {
                                                    XNR.Read();
                                                    NodeValue = XNR.Value;
                                                    return true;
                                                }
                                            }
                                            break;

                                        default:
                                            break;
                                    }
                                }
                            }
                            break;

                        default:
                            break;
                    }
                }
            }
            catch (Exception e)
            {
                ErrMsg = e.Message;
                return false;
            }
            finally
            {
                #region 释放资源

                if (Doc != null)
                {
                    Doc = null;
                }
                if (XNR != null)
                {
                    XNR = null;
                }

                #endregion 释放资源
            }
            return true;
        }

        /// <summary>
        /// 获取XML字符串中，父节点下面的子节点的值.
        /// </summary>
        /// <param name="XmlString">源XML文档的字符串形式</param>
        /// <param name="ParentNodeName">父节点名称</param>
        /// <param name="NodeName">节点名称</param>
        /// <param name="NodeValue">返回的节点值</param>
        /// <param name="ErrMsg">返回的错误信息</param>
        /// <returns></returns>
        public static bool GetNodeValue(string XmlString, string ParentNodeName, string NodeName, out string NodeValue, out string ErrMsg)
        {
            NodeValue = "";
            ErrMsg = "";
            XmlDocument Doc = new XmlDocument();
            Doc.LoadXml(XmlString);
            XmlNodeReader XNR = new XmlNodeReader(Doc);
            try
            {
                while (XNR.Read())
                {
                    switch (XNR.NodeType)
                    {
                        case XmlNodeType.Element:
                            if (XNR.Name.ToUpper() == ParentNodeName.ToUpper())
                            {
                                while (XNR.Read())
                                {
                                    switch (XNR.NodeType)
                                    {
                                        case XmlNodeType.Element:
                                            if (XNR.IsStartElement())
                                            {
                                                if (XNR.Name.ToUpper() == NodeName.ToUpper())
                                                {
                                                    XNR.Read();
                                                    NodeValue = XNR.Value;
                                                    return true;
                                                }
                                            }
                                            break;

                                        default:
                                            break;
                                    }
                                }
                            }
                            break;

                        default:
                            break;
                    }
                }
            }
            catch (Exception e)
            {
                ErrMsg = e.Message;
                return false;
            }
            finally
            {
                #region 释放资源

                if (Doc != null)
                {
                    Doc = null;
                }
                if (XNR != null)
                {
                    XNR = null;
                }

                #endregion 释放资源
            }
            return true;
        }

        #endregion 获取节点的值

        #region 获取节点对象

        /// <summary>
        /// 查找结点
        /// 找到结点，return后没有退出函数，造成返回错误，成功调用见FindNodeExNew
        /// </summary>
        /// <param name="NodeName">结点名称</param>
        /// <param name="Node">源结点</param>
        /// <returns>查找到的结点</returns>
        public static XmlNode FindNodeEx(string NodeName, XmlNode Node)
        {
            int i;
            XmlNode tmpNode = null;
            try
            {
                if (Node.Name == NodeName)
                {
                    tmpNode = Node;
                    return tmpNode;
                }
                for (i = 0; i <= Node.ChildNodes.Count - 1; i++)
                {
                    if (Node.ChildNodes[i].Name == NodeName)
                    {
                        tmpNode = Node.ChildNodes[i];
                        return tmpNode;
                    }
                    if (Node.ChildNodes[i].HasChildNodes)
                    {
                        tmpNode = FindNodeEx(NodeName, Node.ChildNodes[i]);
                    }
                }
                return tmpNode;
            }
            catch
            {
                return null;
            }
        }

        /// <summary>
        /// 从XML报文中得到结点
        /// </summary>
        /// <param name="NodeName">要查找结点名称</param>
        /// <param name="sDocXml">报文</param>
        /// <returns></returns>
        public static XmlNode FindNodeEx(string NodeName, string sDocXml)
        {
            XmlDocument XMLDoc = new XmlDocument();
            XmlNode retNode = null;
            try
            {
                XMLDoc.LoadXml(sDocXml);
                retNode = FindNodeEx(NodeName, XMLDoc);
            }
            catch
            {
                retNode = null;
            }
            finally
            {
                if (XMLDoc != null) XMLDoc = null;
            }
            return retNode;
        }

        /// <summary>
        /// 查找结点重载
        /// </summary>
        /// <param name="NodeName">结点名称</param>
        /// <param name="XmlDoc">XmlDoc体</param>
        /// <returns>结点</returns>
        public static XmlNode FindNodeEx(string NodeName, XmlDocument XmlDoc)
        {
            try
            {
                XmlNode Node, NodeTemp;
                Node = XmlDoc.DocumentElement;
                NodeTemp = FindNodeEx(NodeName, Node);
                return NodeTemp;
            }
            catch
            {
                return null;
            }
        }

        /// <summary>
        /// 查找结点
        /// </summary>
        /// <param name="NodeName">结点名称</param>
        /// <param name="Node">源结点</param>
        /// <returns>查找到的结点</returns>
        public static XmlNode FindNodeExNew(string NodeName, XmlNode Node)
        {
            int i;
            XmlNode tmpNode = null;
            try
            {
                if (Node.Name == NodeName)
                {
                    tmpNode = Node;
                    return tmpNode;
                }
                for (i = 0; i <= Node.ChildNodes.Count - 1; i++)
                {
                    if (tmpNode != null)
                    {
                        break;
                    }

                    if (Node.ChildNodes[i].Name == NodeName)
                    {
                        tmpNode = Node.ChildNodes[i];
                        return tmpNode;
                    }
                    if (Node.ChildNodes[i].HasChildNodes)
                    {
                        tmpNode = FindNodeExNew(NodeName, Node.ChildNodes[i]);
                    }
                }
                return tmpNode;
            }
            catch
            {
                return null;
            }
        }

        /// <summary>
        /// 查找ParentNode节点下的第一个子节点
        /// </summary>
        /// <param name="ParentNode">父节点</param>
        /// <param name="NodeName">子节点名称</param>
        /// <param name="ErrMsg">返回的错误信息</param>
        /// <returns>找到子节点则返回该节点，找不到返回null</returns>
        public static XmlNode GetNode(XmlNode ParentNode, string NodeName, out string ErrMsg)
        {
            ErrMsg = "";
            XmlNode TempNode = null;
            try
            {
                if (!ParentNode.HasChildNodes)
                {
                    TempNode = null;
                }
                else
                {
                    foreach (XmlNode eachchild in ParentNode.ChildNodes)
                    {
                        if (eachchild.Name == NodeName)
                        {
                            TempNode = eachchild;
                            break;
                        }
                    }
                }
            }
            catch (Exception E)
            {
                ErrMsg = E.ToString();
            }
            return TempNode;
        }

        /// <summary>
        /// 获取指定路径的结点
        /// </summary>
        /// <param name="NodePath">结点路径</param>
        /// <param name="XMLDoc">XML文档对象</param>
        /// <returns></returns>
        public static XmlNode FindNode(string NodePath, ref XmlDocument XMLDoc)
        {
            try
            {
                return XMLDoc.SelectSingleNode(@NodePath);
            }
            catch
            {
                return null;
            }
        }

        /// <summary>
        /// 获取指定路径的结点
        /// </summary>
        /// <param name="NodePath">结点路径</param>
        /// <param name="sDocXml">XML文档字符串</param>
        /// <param name="AddList">ArrayList对象，存放着XML命名空间信息格式如下:xsi^http://www.w3.org/2001/XMLSchema-instance</param>
        /// <returns></returns>
        public static XmlNode FindNode(string NodePath, XmlDocument XMLDoc, ArrayList AddList)
        {
            XmlNode retNode = null;
            string sAddFlag = "liq";
            string[] sRow = null;
            try
            {
                XmlNamespaceManager mn = new XmlNamespaceManager(XMLDoc.NameTable);
                for (int i = 0; i < AddList.Count; i++)
                {
                    sRow = AddList[i].ToString().Split('^');
                    if (i == 0)
                    {
                        mn.AddNamespace(sAddFlag, sRow[1].ToString());
                    }
                    else
                    {
                        mn.AddNamespace(sRow[0].ToString(), sRow[1].ToString());
                    }
                }
                NodePath = NodePath.Replace("/", "/" + sAddFlag + ":");
                if (!(NodePath.Substring(0, 1) == "/"))
                {
                    NodePath = sAddFlag + ":" + NodePath;
                }
                retNode = XMLDoc.SelectSingleNode(@NodePath, mn);
            }
            catch
            {
                retNode = null;
            }
            return retNode;
        }

        /// <summary>
        /// 获取指定路径的结点
        /// </summary>
        /// <param name="NodePath">结点路径</param>
        /// <param name="sDocXml">XML文档字符串</param>
        /// <returns></returns>
        public static XmlNode FindNode(string NodePath, string sDocXml)
        {
            XmlDocument XMLDoc = new XmlDocument();
            XmlNode retNode = null;
            try
            {
                XMLDoc.LoadXml(sDocXml);
                retNode = XMLDoc.SelectSingleNode(@NodePath);
            }
            catch
            {
                retNode = null;
            }
            finally
            {
                if (XMLDoc != null) XMLDoc = null;
            }
            return retNode;
        }

        /// <summary>
        /// 获取指定路径的结点
        /// </summary>
        /// <param name="NodePath">结点路径</param>
        /// <param name="sDocXml">XML文档字符串</param>
        /// <param name="AddList">ArrayList对象，存放着XML命名空间信息格式如下:xsi^http://www.w3.org/2001/XMLSchema-instance</param>
        /// <returns></returns>
        public static XmlNode FindNode(string NodePath, string sDocXml, ArrayList AddList)
        {
            XmlDocument XMLDoc = new XmlDocument();
            XmlNode retNode = null;
            string sAddFlag = "liq";
            string[] sRow = null;
            try
            {
                XMLDoc.LoadXml(sDocXml);
                XmlNamespaceManager mn = new XmlNamespaceManager(XMLDoc.NameTable);
                for (int i = 0; i < AddList.Count; i++)
                {
                    sRow = AddList[i].ToString().Split('^');
                    if (i == 0)
                    {
                        mn.AddNamespace(sAddFlag, sRow[1].ToString());
                    }
                    else
                    {
                        mn.AddNamespace(sRow[0].ToString(), sRow[1].ToString());
                    }
                }
                NodePath = NodePath.Replace("/", "/" + sAddFlag + ":");
                if (!(NodePath.Substring(0, 1) == "/"))
                {
                    NodePath = sAddFlag + ":" + NodePath;
                }
                retNode = XMLDoc.SelectSingleNode(@NodePath, mn);
            }
            catch
            {
                retNode = null;
            }
            finally
            {
                if (XMLDoc != null) XMLDoc = null;
            }
            return retNode;
        }

        /// <summary>
        /// 获取XML报文的LocalName值
        /// </summary>
        /// <param name="sDocXml">XML文档内容</param>
        /// <param name="sLocalName">返回值:LocalName</param>
        /// <param name="ErrMsg">返回值:出错时的提示</param>
        /// <returns>返回值:成功 true;失败 false</returns>
        public static bool GetXMLLocalName(string sDocXml, out string sXMLLocalName, out string ErrMsg)
        {
            ErrMsg = "";
            bool result = false;
            string vLoacalName = "";
            System.Xml.XmlDocument XMLDoc = new System.Xml.XmlDocument();
            try
            {
                XMLDoc.LoadXml(sDocXml);
                System.Xml.XmlElement myElement = XMLDoc.DocumentElement;
                vLoacalName = myElement.LocalName;
                //string sNameSpaceURL = myElement.NamespaceURI;
                //string sProfix = myElement.Prefix;
                result = true;
            }
            catch (Exception err)
            {
                ErrMsg = err.Message.Trim();
                result = false;
            }
            finally
            {
                if (XMLDoc != null) XMLDoc = null;
            }
            sXMLLocalName = vLoacalName;
            return result;
        }

        /// <summary>
        /// 获取XML报文的NameSpaceURL值
        /// </summary>
        /// <param name="sDocXml">XML文档内容</param>
        /// <param name="sLocalName">返回值:NameSpaceURL</param>
        /// <param name="ErrMsg">返回值:出错时的提示</param>
        /// <returns>返回值:成功 true;失败 false</returns>
        public static bool GetXMLNameSpaceURL(string sDocXml, out string sXMLNameSpaceURL, out string ErrMsg)
        {
            ErrMsg = "";
            bool result = false;
            string vNameSpaceURL = "";
            System.Xml.XmlDocument XMLDoc = new System.Xml.XmlDocument();
            try
            {
                XMLDoc.LoadXml(sDocXml);
                System.Xml.XmlElement myElement = XMLDoc.DocumentElement;
                vNameSpaceURL = myElement.NamespaceURI;
                result = true;
            }
            catch (Exception err)
            {
                ErrMsg = err.Message.Trim();
                result = false;
            }
            finally
            {
                if (XMLDoc != null) XMLDoc = null;
            }
            sXMLNameSpaceURL = vNameSpaceURL;
            return result;
        }

        /// <summary>
        /// 获取XML报文的Profix值
        /// </summary>
        /// <param name="sDocXml">XML文档内容</param>
        /// <param name="sLocalName">返回值:Profix</param>
        /// <param name="ErrMsg">返回值:出错时的提示</param>
        /// <returns>返回值:成功 true;失败 false</returns>
        public static bool GetXMLProfix(string sDocXml, out string sXMLProfix, out string ErrMsg)
        {
            ErrMsg = "";
            bool result = false;
            string vProfix = "";
            System.Xml.XmlDocument XMLDoc = new System.Xml.XmlDocument();
            try
            {
                XMLDoc.LoadXml(sDocXml);
                System.Xml.XmlElement myElement = XMLDoc.DocumentElement;
                vProfix = myElement.Prefix;
                result = true;
            }
            catch (Exception err)
            {
                ErrMsg = err.Message.Trim();
                result = false;
            }
            finally
            {
                if (XMLDoc != null) XMLDoc = null;
            }
            sXMLProfix = vProfix;
            return result;
        }

        #endregion 获取节点对象

        #region 设置某结点的值

        /// <summary>
        /// 指定结点的路径，设置其值
        /// </summary>
        /// <param name="NodePath">结点路径，如:"lcbank/head/TuxName"</param>
        /// <param name="NodeValue">新值</param>
        /// <param name="Doc">XML文档对象</param>
        /// <param name="errMsg">错误信息</param>
        /// <returns></returns>
        public static bool SetNodeValues(string NodePath, string NodeValue, ref XmlDocument Doc, out string errMsg)
        {
            errMsg = "";
            try
            {
                XmlNode nd = XMLOperate.FindNode(NodePath, ref Doc);
                nd.InnerXml = NodeValue;
            }
            catch (Exception err)
            {
                errMsg = err.Message;
                return false;
            }
            return true;
        }

        /// <summary>
        /// 指定结点的路径，设置其值
        /// </summary>
        /// <param name="NodePath">结点路径，如:"lcbank/head/TuxName"</param>
        /// <param name="NodeValue">新值</param>
        /// <param name="Doc">XML文档对象</param>
        /// <param name="List">ArrayList对象，存放着XML命名空间信息格式如下:xsi^http://www.w3.org/2001/XMLSchema-instance</param>
        /// <param name="errMsg">错误信息</param>
        /// <returns></returns>
        public static bool SetNodeValues(string NodePath, string NodeValue, XmlDocument Doc, ArrayList List, out string errMsg)
        {
            errMsg = "";
            try
            {
                XmlNode nd = XMLOperate.FindNode(NodePath, Doc, List);
                nd.InnerXml = NodeValue;
            }
            catch (Exception err)
            {
                errMsg = err.Message;
                return false;
            }
            return true;
        }

        /// <summary>
        /// 指定结点的路径，设置其值
        /// </summary>
        /// <param name="NodePath">结点路径，如:"lcbank/head/TuxName"</param>
        /// <param name="NodeValue">新值</param>
        /// <param name="sXmlDoc">XML文档对象</param>
        /// <param name="List">ArrayList对象，存放着XML命名空间信息格式如下:xsi^http://www.w3.org/2001/XMLSchema-instance</param>
        /// <param name="errMsg">错误信息</param>
        /// <returns></returns>
        public static bool SetNodeValues(string NodePath, string NodeValue, string sXmlDoc, ArrayList List, out string errMsg)
        {
            errMsg = "";
            try
            {
                XmlNode nd = XMLOperate.FindNode(NodePath, sXmlDoc, List);
                nd.InnerXml = NodeValue;
            }
            catch (Exception err)
            {
                errMsg = err.Message;
                return false;
            }
            return true;
        }

        /// <summary>
        /// 设置结点的值
        /// </summary>
        /// <param name="NodeName">节点名称</param>
        /// <param name="NodeValue">节点值</param>
        /// <param name="Doc">报文对象</param>
        /// <param name="errMsg">错误信息</param>
        /// <returns></returns>
        public static bool SetNodeValue(string NodeName, string NodeValue, ref XmlDocument Doc, out string errMsg)
        {
            errMsg = "";
            try
            {
                XmlNode nd = XMLOperate.FindNodeExNew(NodeName, Doc);
                nd.InnerXml = NodeValue;
            }
            catch (Exception err)
            {
                errMsg = err.Message;
                return false;
            }
            return true;
        }

        /// <summary>
        /// 设置结点的值，指定了父结点
        /// </summary>
        /// <param name="ParentNodeName">父结点名称</param>
        /// <param name="NodeName">节点名称</param>
        /// <param name="NodeValue">节点值</param>
        /// <param name="Doc">报文对象</param>
        /// <param name="errMsg">错误信息</param>
        /// <param name="errMsg"></param>
        /// <returns></returns>
        public static bool SetNodeValue(string ParentNodeName, string NodeName, string NodeValue, ref XmlDocument Doc, out string errMsg)
        {
            errMsg = "";
            try
            {
                XmlNode nd = XMLOperate.FindNodeExNew(NodeName, XMLOperate.FindNodeExNew(ParentNodeName, Doc));
                nd.InnerXml = NodeValue;
            }
            catch (Exception err)
            {
                errMsg = err.Message;
                return false;
            }
            return true;
        }

        #endregion 设置某结点的值
    }
}