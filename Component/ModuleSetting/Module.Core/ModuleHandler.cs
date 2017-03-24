using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Module.Core
{

    /// <summary>
    /// 
    /// </summary>
    public static class ModuleHandler
    {

        /// <summary>
        /// 保存
        /// </summary>
        /// <param name="layout">The layout.</param>
        /// <param name="path">The path.</param>
        /// <exception cref="System.Exception"></exception>
        public static void Save(ILayout layout,string path)
        {
            try
            {
                string filename = path;
                if (System.IO.File.Exists(filename))
                    System.IO.File.Delete(filename);
                using (FileStream fileStream = new FileStream(filename, FileMode.Create))
                {
                    // 用二进制格式序列化
                    BinaryFormatter binaryFormatter = new BinaryFormatter();
                    binaryFormatter.Serialize(fileStream, layout);
                    fileStream.Close();
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }


        /// <summary>
        /// 加载布局文件
        /// </summary>
        /// <param name="path">The path.</param>
        /// <returns></returns>
        /// <exception cref="System.Exception">File Not Exist</exception>
        public static ILayout Load(string path)
        {

            System.Runtime.Serialization.IFormatter formatter = new BinaryFormatter();
            //二进制格式反序列化
            object obj;
            string filename = path ;
            if (!System.IO.File.Exists(filename)) throw new Exception("File Not Exist");
            using (Stream stream = new FileStream(filename, FileMode.Open, FileAccess.Read, FileShare.Read))
            {
                obj = formatter.Deserialize(stream);
                stream.Close();
            }
       
            return obj as ILayout;
        }
    }
}
