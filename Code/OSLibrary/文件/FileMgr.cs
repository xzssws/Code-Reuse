using System;
using System.IO;
using Microsoft.Win32;
using System.Collections;
using System.Text;//StringBuilder
using System.Text.RegularExpressions;
using System.Xml;

//C＃中对Win32的API函数的互操作是通过命名空间“System.Runtime.InteropServices”中的“DllImport”特征类来实现的。
//它的主要作用是指示此属性化方法是作为非托管DLL的输出实现的。
//利用命名空间“System.Runtime.InteropServices”中的“DllImport”特征类申明下面二个Win32的API函数
//WritePrivateProfileString（）和GetPrivateProfileString（）函数
using System.Runtime.InteropServices;

namespace Genersoft.ZJGL.Client.PublicCom
{
	/// <summary>
	/// 文本文件及INI文件的处理
	/// FileMgrc.cs
	/// 李强
	/// 2006.05.11
	/// </summary>


	#region 文本文件的操作
    public class TextFile
    {
        private  string _FileFullName = "";
        public TextFile(string sFileFullName)
        {
            _FileFullName = sFileFullName;
        }
        public  bool ReadFile(out string txtContent, out string ErrMsg)
        {
            txtContent = "";
            ErrMsg = "";
            bool result = false;
            System.IO.StreamReader reader = null;
            try
            {
                if (File.Exists(_FileFullName))
                    reader = new StreamReader(_FileFullName);
                else
                {
                    ErrMsg = "文件不存在！(" + _FileFullName + ")";
                    return false;
                }
                txtContent = reader.ReadToEnd();
                result = true;
            }
            catch (IOException e)
            {
                ErrMsg = e.Message;
                return false;
            }
            finally
            {
                if (reader != null)
                    reader.Close();
            }
            return result;
        }

    }

	public class TxTFile
	{        
		public void TxtFile()
		{			
		}

		/// <summary>
		/// 写日志信息到文本文件
		/// </summary>
		/// <param name="FileContent">日志信息</param>
		/// <param name="TxtFileName">文件名称</param>
		/// <param name="TxtFilePath">文件子所在子目录</param>
		/// <param name="ErrMsg">返回的错误信息</param>
		/// <returns>ture:成功；false:失败</returns>
		public static bool WriteFile(string FileContent,string TxtFileName,string TxtFilePath,out string ErrMsg)
		{
			
			ErrMsg = "";
			StreamWriter writer=null; 
			string sCurDate = System.DateTime.Now.ToString("yyyy-MM-dd");
			string sFile = sCurDate+TxtFileName+".txt";
            string sFileDir = System.Environment.CurrentDirectory+"\\logs\\"+TxtFilePath;			
            //string sFileDir = "C:\\logs\\"+TxtFilePath;			
			sFile = sFileDir+"\\"+sFile;
			try
			{
				if (!Directory.Exists(sFileDir))
					Directory.CreateDirectory(sFileDir);
				
				if( File.Exists(sFile) ) 
					writer = new StreamWriter(sFile,true,System.Text.Encoding.GetEncoding("gb2312"));
				else				
					writer = new StreamWriter(sFile,false,System.Text.Encoding.GetEncoding("gb2312"));				
				string sDateTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:sss");
				writer.WriteLine(sDateTime+" "+FileContent);
				writer.WriteLine("");
			} 
			catch(IOException e) 
			{ 
				ErrMsg = e.Message;
				return false;
			} 
			finally 
			{ 
				if(writer!=null) 
				writer.Close();
			}		
			return true;
		}

		public static bool WriteFileEx(string FileContent,string TxtFileName,string TxtFilePath,out string ErrMsg)
		{
			string sHead= "";
			string sXml = "";
			int i = 0;
			ErrMsg = "";		

			StreamWriter writer=null;
			XmlDocument doc = new XmlDocument();
			i = FileContent.IndexOf("<?",0,FileContent.Length);
			if (i>0)
			{
				sHead = FileContent.Substring(0,i);
				sXml = FileContent.Substring(i,FileContent.Length-i);
			}
			else 
				sHead = FileContent;
			string sCurDate = System.DateTime.Now.ToString("yyyy-MM-dd");
			string sFile = sCurDate+TxtFileName+".txt";
			string sFileDir = System.Environment.CurrentDirectory+"\\logs\\"+TxtFilePath;			
			sFile = sFileDir+"\\"+sFile;
			try
			{
				
				if (!Directory.Exists(sFileDir))
					Directory.CreateDirectory(sFileDir);
				
				if( File.Exists(sFile) ) 
				{
                    writer = new StreamWriter(sFile, true, System.Text.Encoding.GetEncoding("gb2312"));//gb2312 UTF-8
					writer.WriteLine("");
					writer.WriteLine("");
				}
				else			
				{
                    writer = new StreamWriter(sFile, false, System.Text.Encoding.GetEncoding("gb2312"));//gb2312
				}			  
				string sDateTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:sss");				
				writer.WriteLine(sDateTime+" "+sHead);	
				if (sXml != "")
				{
					doc.LoadXml(sXml);
					doc.Save(writer);	
				}
			} 
			catch(IOException e) 
			{ 
				ErrMsg = e.Message;
				return false;
			} 
			finally 
			{ 
				if(writer!=null) 
					writer.Close(); 
				if(doc!= null)
					doc = null;
			}	
			return true;			
		}

        public static bool WriteFileEx(string FileContent, string TxtFileName, string TxtFilePath, string TxtEncoding,out string ErrMsg)
        {
            string sHead = "";
            string sXml = "";
            int i = 0;
            ErrMsg = "";

            StreamWriter writer = null;
            XmlDocument doc = new XmlDocument();
            i = FileContent.IndexOf("<?", 0, FileContent.Length);
            if (i > 0)
            {
                sHead = FileContent.Substring(0, i);
                sXml = FileContent.Substring(i, FileContent.Length - i);
            }
            else
                sHead = FileContent;
            string sCurDate = System.DateTime.Now.ToString("yyyy-MM-dd");
            string sFile = sCurDate + TxtFileName + ".txt";
            string sFileDir = System.Environment.CurrentDirectory + "\\logs\\" + TxtFilePath;
            sFile = sFileDir + "\\" + sFile;
            try
            {

                if (!Directory.Exists(sFileDir))
                    Directory.CreateDirectory(sFileDir);

                if (File.Exists(sFile))
                {
                    writer = new StreamWriter(sFile, true, System.Text.Encoding.GetEncoding(TxtEncoding));//gb2312 UTF-8
                    writer.WriteLine("");
                    writer.WriteLine("");
                }
                else
                {
                    writer = new StreamWriter(sFile, false, System.Text.Encoding.GetEncoding(TxtEncoding));//gb2312
                }
                string sDateTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:sss");
                writer.WriteLine(sDateTime + " " + sHead);
                if (sXml != "")
                {
                    doc.LoadXml(sXml);
                    doc.Save(writer);
                }
            }
            catch (IOException e)
            {
                ErrMsg = e.Message;
                return false;
            }
            finally
            {
                if (writer != null)
                    writer.Close();
                if (doc != null)
                    doc = null;
            }
            return true;
        }

        #region 文件操作    仪洪彪2008.08.12添加
        #region 获取文件
        /// <summary>
        /// 获取文件文件
        /// </summary>
        /// <param name="filePath">文件夹路径</param>
        /// <param name="fileList">文件名列表</param>
        /// <param name="errMsg">错误信息</param>
        /// <returns></returns>
        public static bool GetFileNames(string filePath, string pattern, out ArrayList fileList,out string errMsg)
        {
            fileList = new ArrayList();
            errMsg = "";
            try
            {
                #region 取文件
                if (Directory.Exists(filePath))//(directory.Exists || pattern.Trim() != string.Empty)
                {
                    string[] fileNames = Directory.GetFiles(filePath, pattern);
                    if (fileNames.Length == 0)//文件夹下午文件情况
                    {
                        errMsg = "指定文件夹为空文件夹";
                        return false;
                    }
                    for (int i = 0; i < fileNames.Length; i++)
                    {
                        fileList.Add(fileNames[i].Trim());
                    }
                }
                #endregion
            }
            catch (Exception er)
            {
                errMsg = er.Message;
                return false;
            }
            return true; ;

        }

        /// <summary>
        /// 获取全部类型文集
        /// </summary>
        /// <param name="filePath">文件夹路径</param>
        /// <param name="fileList">文件列表</param>
        /// <param name="errMsg">错误信息</param>
        /// <returns></returns>
        public static bool GetFileNames(string filePath, out ArrayList fileList,out string errMsg)
        {
            fileList = new ArrayList();
            errMsg = "";
            try
            {
                #region 取文件
                if (Directory.Exists(filePath))//(directory.Exists || pattern.Trim() != string.Empty)
                {
                    string[] fileNames = Directory.GetFiles(filePath);
                    if (fileNames.Length == 0)//文件夹下午文件情况
                    {
                        errMsg = "指定文件夹为空文件夹";
                        return false;
                    }
                    for (int i = 0; i < fileNames.Length; i++)
                    {
                        fileList.Add(fileNames[i].Trim());
                    }
                }
                #endregion//resultString = result;
            }
            catch (Exception er)
            {
                errMsg = er.Message;
                return false;
            }
            
            return true; ;

        }
        #endregion

        #region 读文件
        /// <summary>
        /// 读文件
        /// </summary>
        /// <param name="sFileName">文件名</param>
        /// <param name="resultString">文件内容</param>
        /// <param name="errMsg">错误信息</param>
        /// <returns>成功与否</returns>
        public static bool ReadFiles(string sFileName, out string resultString, out  string errMsg)
        {
            errMsg = "";
            resultString = "";
            try
            {
                if (!File.Exists(sFileName))
                {
                    errMsg = "文件:" + sFileName + "不存在！";
                    return false;
                }
                FileStream fileStream = new FileStream(sFileName, FileMode.Open, FileAccess.Read);
                StreamReader streamReader = new StreamReader(fileStream, System.Text.Encoding.Default);
                resultString = streamReader.ReadToEnd();
                streamReader.Close();
            }
            catch (Exception er)
            {
                errMsg = er.Message;
                return false;
            }
            finally 
            {
                GC.Collect();
            }
            return true; ;

        }

        #endregion

        #region 读文件
        /// <summary>
        /// 读文件
        /// </summary>
        /// <param name="sFileName">文件名</param>
        /// <param name="fileContent">文件内容数组</param>
        /// <param name="errMsg">错误信息</param>
        /// <returns>成功与否</returns>
        public static bool ReadFiles(string sFileName, out ArrayList fileContent, out  string errMsg)
        {
            errMsg = "";
            fileContent = new ArrayList();
            string resultString = "";
            try
            {
                if (!File.Exists(sFileName))
                {
                    errMsg = "文件:" + sFileName + "不存在！";
                    return false;
                }
                FileStream fileStream = new FileStream(sFileName, FileMode.Open, FileAccess.Read);
                StreamReader streamReader = new StreamReader(fileStream, System.Text.Encoding.Default);
                while ((resultString = streamReader.ReadLine()) != null)
                {
                    fileContent.Add(resultString);
                }
                streamReader.Close();
            }
            catch (Exception er)
            {
                errMsg = er.Message;
                return false;
            }
            finally
            {
                GC.Collect();
            }
            return true; ;

        }

        #endregion

        #region 删除文件
        /// <summary>
        /// 删除文件
        /// </summary>
        /// <param name="sFileName">文件全名带路径</param>
        /// <param name="resultString">文件内容</param>
        /// <param name="errMsg">错误信息</param>
        /// <returns>成功与否</returns>
        public static bool DeleteFiles(string sFileName, out  string errMsg)
        {
            errMsg = "";
            try
            {
                if (File.Exists(sFileName))
                {
                    File.Delete(sFileName);//
                    errMsg = "删除文件" + sFileName + "成功！";
                }
                else
                {
                    errMsg = "文件" + sFileName + "不存在！";
                }
            }
            catch (Exception er)
            {
                errMsg = er.Message;
                return false;
            }
            return true; ;

        }

        #endregion

        #region 移动文件
        /// <summary>
        /// 移动文件
        /// </summary>
        /// <param name="filePath">文件路径</param>
        /// <param name="targetPath">目标路径</param>
        /// <param name="errMsg">错误信息</param>
        /// <returns></returns>
        public static bool MoveFiles(string filePath, string targetPath, out  string errMsg)
        {
            errMsg = "";
            try
            {
                if (File.Exists(filePath))//判断文件是否存在
                {
                    if (!File.Exists(targetPath))//判断目标文件夹是否存在同名文件
                    {
                        File.Move(filePath, targetPath);
                    }
                    else
                    {
                        errMsg = "目标文件已存在同名文件！";
                        return false;
                    }
                }
                else
                {
                    errMsg = "文件:" + filePath + "不存在！";
                    return false;
                }
            }
            catch (Exception er)
            {
                errMsg = er.Message;
                return false;
            }
            return true; ;

        }
        #endregion

        #region 移动文件
        /// <summary>
        /// 移动文件
        /// </summary>
        /// <param name="filePath">文件路径</param>
        /// <param name="targetPath">目标路径</param>
        /// <param name="errMsg">错误信息</param>
        /// <returns></returns>
        public static bool MoveFiles(string filePath,string fileName, string targetPath, out  string errMsg)
        {
            errMsg = "";
            string Path = "";
            try
            {
                if (File.Exists(filePath))//判断文件是否存在
                {
                    if (!Directory.Exists(targetPath))
                        Directory.CreateDirectory(targetPath);
                    Path = targetPath + "\\" + fileName;
                    if (!File.Exists(targetPath))//判断目标文件夹是否存在同名文件
                    {
                        File.Move(filePath, Path);
                    }
                    else
                    {
                        errMsg = "目标文件已存在同名文件！";
                        return false;
                    }
                }
                else
                {
                    errMsg = "文件:" + filePath + "不存在！";
                    return false;
                }
            }
            catch (Exception er)
            {
                errMsg = er.Message;
                return false;
            }
            return true; ;

        }
        #endregion
        #endregion
    }
	#endregion

	#region ini文件的操作

	public class INIFile
	{
		[DllImport("kernel32")]
		private static extern long WritePrivateProfileString(string section,string KEY1,string val,string filePath) ;
		[DllImport("kernel32")]
		private static extern int GetPrivateProfileString(string section,string KEY1,string def,StringBuilder retVal,int size,string filePath) ;

		public string path;

		public INIFile()
		{}		
		public INIFile(string INIPath)
		{
			path = INIPath;
		}

		public void IniWriteValue(string Section,string KEY1,string Value)
		{
			WritePrivateProfileString(Section,KEY1,Value,this.path);
		}

		public string IniReadValue(string Section,string KEY1)
		{
			StringBuilder temp = new StringBuilder(255);
			int i = GetPrivateProfileString(Section,KEY1,"",temp, 255, this.path);
			return temp.ToString();
		}

	}


	/// <summary>
	/// 普通ini文件实体类
	/// </summary>
	public class INIFileEntity
	{
		#region 类构造
		/// <summary>
		/// ini文件实体类构造
		/// </summary>
		public INIFileEntity()
		{}
		#endregion

		#region 类的变量声明
		private string _svFilePath;
		private string _svFileName;
		private string _svSection;
		private string _svKeyWord;
		private string _svValue;
		#endregion

		#region 类的公有属性声明
		/// <summary>
		/// ini文件所在路径
		/// </summary>
		public string svFilePath
		{
			get{return _svFilePath;}
			set{_svFilePath =value;}
		}
		/// <summary>
		/// ini文件的文件名
		/// </summary>
		public string svFileName
		{
			get{return _svFileName;}
			set{_svFileName =value;}
		}
		/// <summary>
		/// ini文件中的组名
		/// </summary>
		public string svSection
		{
			get{return _svSection;}
			set{_svSection =value;}
		}
		/// <summary>
		/// ini文件中的键名
		/// </summary>
		public string svKeyWord
		{
			get{return _svKeyWord;}
			set{_svKeyWord =value;}
		}
		/// <summary>
		/// ini文件中的键值
		/// </summary>
		public string svValue
		{
			get{return _svValue;}
			set{_svValue =value;}
		}
		#endregion
	}

	/// <summary>
	/// 普通ini文件管理类
	/// </summary>
	public class INIFileMgr
	{
		#region 类的构造函数
		/// <summary>
		/// 普通ini文件管理类
		/// </summary>
		public INIFileMgr()
		{}
		#endregion

		#region　类的公有方法声明
		/// <summary>
		/// 写ini文件
		/// </summary>
		/// <param name="objFile">ini文件实体类</param>
		public static void IniFile_Write(INIFileEntity objFile)
		{	
			string iniFile = objFile.svFilePath+"\\"+objFile.svFileName;
			if (!File.Exists(iniFile))
			{
				using (FileStream fs = File.Create(iniFile))
				{
					fs.Close();
				}
			}

			try
			{
				INIFile myINI = new INIFile(iniFile);
				myINI.IniWriteValue(objFile.svSection,objFile.svKeyWord,objFile.svValue);			
			}
			catch(Exception Err)
			{
				if(Err!=null)
					throw new Exception("将信息写入INI文件时发生错误！\n"+objFile.svSection+"__"+objFile.svKeyWord+"__"+objFile.svValue+"\n\n"+Err.Message);
			}
		}
		
		/// <summary>
		/// 读ini文件信息
		/// </summary>
		/// <param name="objFile">ini文件信息</param>
		/// <returns>ini文件中的一个键值</returns>
		public static string IniFile_Read(INIFileEntity objFile)
		{			
			try
			{
				string iniFile = objFile.svFilePath+"\\"+objFile.svFileName;
				if (!File.Exists(iniFile))
				{
					using (FileStream fs = File.Create(iniFile))
					{
						fs.Close();
					}
				}
				INIFile myINI = new INIFile(iniFile);
				objFile.svValue = myINI.IniReadValue(objFile.svSection,objFile.svKeyWord);
			}
			catch(Exception Err)
			{
				if(Err!=null)
					throw new Exception("读取INI文件信息错误！\n"+objFile.svSection+"__"+objFile.svKeyWord+"\n\n"+Err.Message);
			}
			return objFile.svValue;
		}		
		#endregion
	}
	#endregion

}
