using System;
using System.Data;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Xml;
using System.Xml.Serialization;

namespace CC_CommonClass
{
    /// <summary>
    /// 序列化帮助类
    /// </summary>
    public class SerializeHelper
    {
        #region XML序列化

        /// <summary>
        /// 文件化XML序列化
        /// </summary>
        /// <param name="obj">序列化对象</param>
        /// <param name="file">文件路径</param>
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
        /// <param name="type">反序列化对象类型</param>
        /// <param name="file">文件路径</param>
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
        /// <param name="obj">对象</param>
        public static string ToXml<T>(T obj)
        {
            XmlSerializer serializer = new XmlSerializer(obj.GetType());
            StringBuilder sb = new StringBuilder();
            using (XmlWriter writer = XmlWriter.Create(sb))
            {
                serializer.Serialize(writer, obj);
                return sb.ToString();
            }
        }

        /// <summary>
        /// 文本化XML反序列化
        /// </summary>
        /// <param name="file">字符串序列</param>
        public static T FromXml<T>(string file)
        {
            FileStream fs = null;
            try
            {
                T t = default(T);
                fs = new FileStream(file, FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
                XmlSerializer serializer = new XmlSerializer(t.GetType());
                t = (T)serializer.Deserialize(fs);
                return t;
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
        /// 序列化到文件[XML]
        /// </summary>
        /// <typeparam name="T">需要序列化对象的类型</typeparam>
        /// <param name="obj">序列化的对象</param>
        /// <param name="file">文件路径</param>
        /// <returns>
        /// 从序列化文件中获得的对象
        /// </returns>
        public static bool ToXmlFile<T>(T obj, string file)
        {
            FileStream fs = null;
            try
            {
                fs = new FileStream(file, FileMode.Create, FileAccess.Write, FileShare.ReadWrite);
                XmlSerializer serializer = new XmlSerializer(obj.GetType());
                serializer.Serialize(fs, obj);
                return true;
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
        /// 从文件反序列化
        /// </summary>
        /// <typeparam name="T">需要反序列化对象的类型</typeparam>
        /// <param name="file">文件路径</param>
        /// <returns>
        /// 从序列化文件中获得的对象
        /// </returns>
        public static T FromXmlFile<T>(string file)
        {
            XmlSerializer serializer = new XmlSerializer(typeof(T));
            using (XmlReader reader = new XmlTextReader(new StringReader(file)))
            {
                return (T)serializer.Deserialize(reader);
            }
        }
        #endregion XML序列化

        #region BinaryFormatter序列化

        /// <summary>
        ///  序列化到字符串[二进制]
        /// </summary>
        /// <typeparam name="T">序列化对象类型</typeparam>
        /// <param name="obj">序列化对象</param>
        /// <returns>序列化后字符串</returns>
        public static string ToBinary<T>(T item)
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
        /// <typeparam name="T">反序列化对象类型</typeparam>
        /// <param name="str">序列化后的字符串</param>
        /// <returns>
        /// 对象
        /// </returns>
        public static T FromBinary<T>(string str)
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
        /// 序列化到文件[二进制]
        /// </summary>
        /// <param name="obj">对象</param>
        public static void ToBinaryPath(string filepath, object obj)
        {
            BinaryFormatter bf = new BinaryFormatter();

            using (FileStream fs = new FileStream(filepath, FileMode.CreateNew))
            {
                bf.Serialize(fs, obj);
            }
        }

        /// <summary>
        /// 从文件中反序列化[二进制]
        /// </summary>
        /// <typeparam name="T">对象类型</typeparam>
        /// <param name="filepath">文件路径</param>
        /// <returns>
        /// 获得的反序列化对象
        /// </returns>
        public static T FromBinaryPath<T>(string filepath)
        {
            if (!File.Exists(filepath)) return default(T);

            BinaryFormatter bf = new BinaryFormatter();

            using (FileStream fs = new FileStream(filepath, FileMode.CreateNew))
            {
                return (T)bf.Deserialize(fs);
            }
        }

        /// <summary>
        /// 序列化到内存[二进制]
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
        /// 从内存中反序列化[二进制]
        /// </summary>
        /// <param name="bytes">Byte数组</param>
        /// <returns>对象</returns>
        public static Object Deserialize(byte[] bytes)
        {
            using (MemoryStream stream = new MemoryStream(bytes))
            {
                IFormatter formatter = new BinaryFormatter();
                return formatter.Deserialize(stream);
            }
        }

        #endregion BinaryFormatter序列化
    }
}