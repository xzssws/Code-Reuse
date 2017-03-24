﻿using System;
using System.Data;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
//using System.Runtime.Serialization.Json;
using System.Runtime.Serialization.Formatters.Soap;
using System.Text;
using System.Xml;
using System.Xml.Serialization;

namespace OSLibrary
{
    public class SerializeHelper
    {
        public SerializeHelper()
        { }

        #region XML序列化

        /// <summary>
        /// 文件化XML序列化
        /// </summary>
        /// <param name="obj">对象</param>
        /// <param name="filename">文件路径</param>
        public static void Save(object obj, string filename)
        {
            FileStream fs = null;
            try
            {
                fs = new FileStream(filename, FileMode.Create, FileAccess.Write, FileShare.ReadWrite);
                XmlSerializer serializer = new XmlSerializer(obj.GetType());
                serializer.Serialize(fs, obj);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (fs != null) fs.Close();
            }
        }

        /// <summary>
        /// 文件化XML反序列化
        /// </summary>
        /// <param name="type">对象类型</param>
        /// <param name="filename">文件路径</param>
        public static object Load(Type type, string filename)
        {
            FileStream fs = null;
            try
            {
                fs = new FileStream(filename, FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
                XmlSerializer serializer = new XmlSerializer(type);
                return serializer.Deserialize(fs);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (fs != null) fs.Close();
            }
        }

        /// <summary>
        /// 文本化XML序列化
        /// </summary>
        /// <param name="item">对象</param>
        public string ToXml<T>(T item)
        {
            XmlSerializer serializer = new XmlSerializer(item.GetType());
            StringBuilder sb = new StringBuilder();
            using (XmlWriter writer = XmlWriter.Create(sb))
            {
                serializer.Serialize(writer, item);
                return sb.ToString();
            }
        }

        /// <summary>
        /// 文本化XML反序列化
        /// </summary>
        /// <param name="str">字符串序列</param>
        public T FromXml<T>(string str)
        {
            XmlSerializer serializer = new XmlSerializer(typeof(T));
            using (XmlReader reader = new XmlTextReader(new StringReader(str)))
            {
                return (T)serializer.Deserialize(reader);
            }
        }

        #endregion XML序列化

        #region Json序列化

        /// <summary>
        /// JsonSerializer序列化
        /// </summary>
        /// <param name="item">对象</param>
        //public string ToJson<T>(T item)
        //{
        //    DataContractJsonSerializer serializer = new DataContractJsonSerializer(item.GetType());
        //    using (MemoryStream ms = new MemoryStream())
        //    {
        //        serializer.WriteObject(ms, item);
        //        return Encoding.UTF8.GetString(ms.ToArray());
        //    }
        //}

        ///// <summary>
        ///// JsonSerializer反序列化
        ///// </summary>
        ///// <param name="str">字符串序列</param>
        //public T FromJson<T>(string str) where T : class
        //{
        //    DataContractJsonSerializer serializer = new DataContractJsonSerializer(typeof(T));
        //    using (MemoryStream ms = new MemoryStream(Encoding.UTF8.GetBytes(str)))
        //    {
        //        return serializer.ReadObject(ms) as T;
        //    }
        //}

        #endregion Json序列化

        #region SoapFormatter序列化

        /// <summary>
        /// SoapFormatter序列化
        /// </summary>
        /// <param name="item">对象</param>
        public string ToSoap<T>(T item)
        {
            SoapFormatter formatter = new SoapFormatter();
            using (MemoryStream ms = new MemoryStream())
            {
                formatter.Serialize(ms, item);
                ms.Position = 0;
                XmlDocument xmlDoc = new XmlDocument();
                xmlDoc.Load(ms);
                return xmlDoc.InnerXml;
            }
        }

        /// <summary>
        /// SoapFormatter反序列化
        /// </summary>
        /// <param name="str">字符串序列</param>
        public T FromSoap<T>(string str)
        {
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.LoadXml(str);
            SoapFormatter formatter = new SoapFormatter();
            using (MemoryStream ms = new MemoryStream())
            {
                xmlDoc.Save(ms);
                ms.Position = 0;
                return (T)formatter.Deserialize(ms);
            }
        }

        #endregion SoapFormatter序列化

        #region BinaryFormatter序列化

        /// <summary>
        /// BinaryFormatter序列化
        /// </summary>
        /// <param name="item">对象</param>
        public string ToBinary<T>(T item)
        {
            BinaryFormatter formatter = new BinaryFormatter();
            using (MemoryStream ms = new MemoryStream())
            {
                formatter.Serialize(ms, item);
                ms.Position = 0;
                byte[] bytes = ms.ToArray();
                StringBuilder sb = new StringBuilder();
                foreach (byte bt in bytes)
                {
                    sb.Append(string.Format("{0:X2}", bt));
                }
                return sb.ToString();
            }
        }

        /// <summary>
        /// BinaryFormatter反序列化
        /// </summary>
        /// <param name="str">字符串序列</param>
        public T FromBinary<T>(string str)
        {
            int intLen = str.Length / 2;
            byte[] bytes = new byte[intLen];
            for (int i = 0; i < intLen; i++)
            {
                int ibyte = Convert.ToInt32(str.Substring(i * 2, 2), 16);
                bytes[i] = (byte)ibyte;
            }
            BinaryFormatter formatter = new BinaryFormatter();
            using (MemoryStream ms = new MemoryStream(bytes))
            {
                return (T)formatter.Deserialize(ms);
            }
        }
        /// <summary>
        /// 对象序列化
        /// </summary>
        /// <param name="o">要序列化的对象</param>
        /// <returns>Byte数组</returns>
        public static byte[] Serialize(Object o)
        {
            using (MemoryStream stream = new MemoryStream())
            {
                IFormatter formatter = new BinaryFormatter();
                formatter.Serialize(stream, o);
                stream.Position = 0;
                return stream.ToArray();
            }
        }
        /// <summary>
        /// 对象反序列化
        /// </summary>
        /// <param name="bytes"></param>
        /// <returns></returns>
        public static Object Deserialize(byte[] bytes)
        {
            using (MemoryStream stream = new MemoryStream(bytes))
            {
                IFormatter formatter = new BinaryFormatter();
                return formatter.Deserialize(stream);
            }
        }

        /// <summary>
        /// 反序列化Bytes到DataSet
        /// </summary>
        /// <param name="bytes">Byte</param>
        /// <returns>DataSet对象</returns>
        public static DataSet ByteToDataSet(byte[] bytes)
        {
            return (DataSet)Deserialize(bytes);
        }
        /// <summary>
        /// 序列化DataSet到byte
        /// </summary>
        /// <param name="dataset">DataSet对象</param>
        /// <returns>Byte数组</returns>
        public static byte[] DataSetToByte(DataSet dataset)
        {
            return Serialize(dataset);
        }

        #endregion BinaryFormatter序列化
    }
}