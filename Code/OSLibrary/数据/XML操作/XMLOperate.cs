using System;
using System.Collections;
using System.Data;
using System.Xml;

namespace Trouble_Ze
{
    public class XMLOperate
    {
        #region ���캯��

        public XMLOperate()
        {
            //
            // TODO: �ڴ˴���ӹ��캯���߼�
            //
        }

        #endregion ���캯��

        #region �ڵ�תDataSet

        /// <summary>
        /// ���ڵ�ParentNode�нڵ���ΪNodeName�ظ���¼ת��ΪDataSet
        /// </summary>
        /// <param name="XmlString">���ڵ�</param>
        /// <param name="NodeName">�ڵ�����</param>
        /// <param name="DS">ת�����DataSet</param>
        /// <param name="ErrMsg">���صĴ�����Ϣ</param>
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
                        if (!Node.HasChildNodes)               //û���ӽڵ�
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
                #region �ͷ���Դ

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

                #endregion �ͷ���Դ
            }
            return true;
        }

        #endregion �ڵ�תDataSet

        #region ���Xml���ĵĺϷ���

        /// <summary>
        /// ����XML�ĵ��ĺϷ���
        /// </summary>
        /// <param name="XmlString">ԴXML�ĵ����ַ�����ʽ</param>
        /// <param name="Valid">�Ϸ�true���Ϸ�false</param>
        /// <param name="ErrMsg">���صĴ�����Ϣ</param>
        /// <returns></returns>
        public static bool CheckXml(string XmlString, out bool Valid, out string ErrMsg)//���Xml�ĵ��Ϸ���
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
                ErrMsg = "�Ƿ�XML�ĵ�:" + E.Message;
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

        #endregion ���Xml���ĵĺϷ���

        #region ���ڵ��������ӽڵ�

        /// <summary>
        /// ����XML�ĵ�
        /// </summary>
        /// <param name="ParentNode">���ڵ�</param>
        /// <param name="NodeName">�ڵ�����</param>
        /// <param name="NodeValue">�ڵ�ֵ</param>
        /// <param name="XmlDoc">�������ɺ��XMLDocument</param>
        /// <param name="ErrMsg">���صĴ�����Ϣ</param>
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

        #endregion ���ڵ��������ӽڵ�

        #region ��ȡ�ڵ��ֵ

        /// <summary>
        /// ���ظýڵ��ֵ���ýڵ����ΪElement���ͣ���������ֵ
        /// </summary>
        /// <param name="Node">�ڵ�</param>
        /// <param name="NodeValue">�ڵ�ֵ</param>
        /// <param name="ErrMsg">���صĴ�����Ϣ</param>
        /// <returns>���ؽڵ��ֵ</returns>
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
        /// ��ȡXML�����ִ��е�һ�����Ͻڵ��ֵ,Ч�ʱȽϵͣ����鲻ʹ�á�
        /// </summary>
        /// <param name="XmlString">ԴXML�ĵ����ַ�����ʽ</param>
        /// <param name="NodeName">�ڵ�����</param>
        /// <param name="NodeValue">���صĽڵ�ֵ</param>
        /// <param name="ErrMsg">���صĴ�����Ϣ</param>
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
        /// ��ȡ���ڵ�ParentNode�����ӽڵ���Ϊ NodeName�Ľڵ��ֵ
        /// </summary>
        /// <param name="ParentNode">���ڵ� (XmlNode����)</param>
        /// <param name="NodeName">�ӽڵ���</param>
        /// <param name="NodeValue">�ӽڵ�ֵ</param>
        /// <param name="ErrMsg">���ص��쳣��Ϣ</param>
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
        /// ��ȡ�ڵ��ֵ(��ʱ��Ҫʹ�����),δ��������.
        /// </summary>
        /// <param name="XmlString">ԴXML�ĵ����ַ�����ʽ</param>
        /// <param name="ParentNode">���ڵ�</param>
        /// <param name="NodeName">�ڵ�����</param>
        /// <param name="NodeValue">���صĽڵ��ֵ</param>
        /// <param name="ErrMsg">����Ĵ�����Ϣ</param>
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
                #region �ͷ���Դ

                if (Doc != null)
                {
                    Doc = null;
                }
                if (XNR != null)
                {
                    XNR = null;
                }

                #endregion �ͷ���Դ
            }
            return true;
        }

        /// <summary>
        /// ��ȡXML�ַ����У����ڵ�������ӽڵ��ֵ.
        /// </summary>
        /// <param name="XmlString">ԴXML�ĵ����ַ�����ʽ</param>
        /// <param name="ParentNodeName">���ڵ�����</param>
        /// <param name="NodeName">�ڵ�����</param>
        /// <param name="NodeValue">���صĽڵ�ֵ</param>
        /// <param name="ErrMsg">���صĴ�����Ϣ</param>
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
                #region �ͷ���Դ

                if (Doc != null)
                {
                    Doc = null;
                }
                if (XNR != null)
                {
                    XNR = null;
                }

                #endregion �ͷ���Դ
            }
            return true;
        }

        #endregion ��ȡ�ڵ��ֵ

        #region ��ȡ�ڵ����

        /// <summary>
        /// ���ҽ��
        /// �ҵ���㣬return��û���˳���������ɷ��ش��󣬳ɹ����ü�FindNodeExNew
        /// </summary>
        /// <param name="NodeName">�������</param>
        /// <param name="Node">Դ���</param>
        /// <returns>���ҵ��Ľ��</returns>
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
        /// ��XML�����еõ����
        /// </summary>
        /// <param name="NodeName">Ҫ���ҽ������</param>
        /// <param name="sDocXml">����</param>
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
        /// ���ҽ������
        /// </summary>
        /// <param name="NodeName">�������</param>
        /// <param name="XmlDoc">XmlDoc��</param>
        /// <returns>���</returns>
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
        /// ���ҽ��
        /// </summary>
        /// <param name="NodeName">�������</param>
        /// <param name="Node">Դ���</param>
        /// <returns>���ҵ��Ľ��</returns>
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
        /// ����ParentNode�ڵ��µĵ�һ���ӽڵ�
        /// </summary>
        /// <param name="ParentNode">���ڵ�</param>
        /// <param name="NodeName">�ӽڵ�����</param>
        /// <param name="ErrMsg">���صĴ�����Ϣ</param>
        /// <returns>�ҵ��ӽڵ��򷵻ظýڵ㣬�Ҳ�������null</returns>
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
        /// ��ȡָ��·���Ľ��
        /// </summary>
        /// <param name="NodePath">���·��</param>
        /// <param name="XMLDoc">XML�ĵ�����</param>
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
        /// ��ȡָ��·���Ľ��
        /// </summary>
        /// <param name="NodePath">���·��</param>
        /// <param name="sDocXml">XML�ĵ��ַ���</param>
        /// <param name="AddList">ArrayList���󣬴����XML�����ռ���Ϣ��ʽ����:xsi^http://www.w3.org/2001/XMLSchema-instance</param>
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
        /// ��ȡָ��·���Ľ��
        /// </summary>
        /// <param name="NodePath">���·��</param>
        /// <param name="sDocXml">XML�ĵ��ַ���</param>
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
        /// ��ȡָ��·���Ľ��
        /// </summary>
        /// <param name="NodePath">���·��</param>
        /// <param name="sDocXml">XML�ĵ��ַ���</param>
        /// <param name="AddList">ArrayList���󣬴����XML�����ռ���Ϣ��ʽ����:xsi^http://www.w3.org/2001/XMLSchema-instance</param>
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
        /// ��ȡXML���ĵ�LocalNameֵ
        /// </summary>
        /// <param name="sDocXml">XML�ĵ�����</param>
        /// <param name="sLocalName">����ֵ:LocalName</param>
        /// <param name="ErrMsg">����ֵ:����ʱ����ʾ</param>
        /// <returns>����ֵ:�ɹ� true;ʧ�� false</returns>
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
        /// ��ȡXML���ĵ�NameSpaceURLֵ
        /// </summary>
        /// <param name="sDocXml">XML�ĵ�����</param>
        /// <param name="sLocalName">����ֵ:NameSpaceURL</param>
        /// <param name="ErrMsg">����ֵ:����ʱ����ʾ</param>
        /// <returns>����ֵ:�ɹ� true;ʧ�� false</returns>
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
        /// ��ȡXML���ĵ�Profixֵ
        /// </summary>
        /// <param name="sDocXml">XML�ĵ�����</param>
        /// <param name="sLocalName">����ֵ:Profix</param>
        /// <param name="ErrMsg">����ֵ:����ʱ����ʾ</param>
        /// <returns>����ֵ:�ɹ� true;ʧ�� false</returns>
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

        #endregion ��ȡ�ڵ����

        #region ����ĳ����ֵ

        /// <summary>
        /// ָ������·����������ֵ
        /// </summary>
        /// <param name="NodePath">���·������:"lcbank/head/TuxName"</param>
        /// <param name="NodeValue">��ֵ</param>
        /// <param name="Doc">XML�ĵ�����</param>
        /// <param name="errMsg">������Ϣ</param>
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
        /// ָ������·����������ֵ
        /// </summary>
        /// <param name="NodePath">���·������:"lcbank/head/TuxName"</param>
        /// <param name="NodeValue">��ֵ</param>
        /// <param name="Doc">XML�ĵ�����</param>
        /// <param name="List">ArrayList���󣬴����XML�����ռ���Ϣ��ʽ����:xsi^http://www.w3.org/2001/XMLSchema-instance</param>
        /// <param name="errMsg">������Ϣ</param>
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
        /// ָ������·����������ֵ
        /// </summary>
        /// <param name="NodePath">���·������:"lcbank/head/TuxName"</param>
        /// <param name="NodeValue">��ֵ</param>
        /// <param name="sXmlDoc">XML�ĵ�����</param>
        /// <param name="List">ArrayList���󣬴����XML�����ռ���Ϣ��ʽ����:xsi^http://www.w3.org/2001/XMLSchema-instance</param>
        /// <param name="errMsg">������Ϣ</param>
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
        /// ���ý���ֵ
        /// </summary>
        /// <param name="NodeName">�ڵ�����</param>
        /// <param name="NodeValue">�ڵ�ֵ</param>
        /// <param name="Doc">���Ķ���</param>
        /// <param name="errMsg">������Ϣ</param>
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
        /// ���ý���ֵ��ָ���˸����
        /// </summary>
        /// <param name="ParentNodeName">���������</param>
        /// <param name="NodeName">�ڵ�����</param>
        /// <param name="NodeValue">�ڵ�ֵ</param>
        /// <param name="Doc">���Ķ���</param>
        /// <param name="errMsg">������Ϣ</param>
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

        #endregion ����ĳ����ֵ
    }
}