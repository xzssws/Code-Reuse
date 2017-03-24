using System;
using System.IO;
using Microsoft.Win32;
using System.Collections;
using System.Text;//StringBuilder
using System.Text.RegularExpressions;
using System.Xml;

//C���ж�Win32��API�����Ļ�������ͨ�������ռ䡰System.Runtime.InteropServices���еġ�DllImport����������ʵ�ֵġ�
//������Ҫ������ָʾ�����Ի���������Ϊ���й�DLL�����ʵ�ֵġ�
//���������ռ䡰System.Runtime.InteropServices���еġ�DllImport�������������������Win32��API����
//WritePrivateProfileString������GetPrivateProfileString��������
using System.Runtime.InteropServices;

namespace Genersoft.ZJGL.Client.PublicCom
{
	/// <summary>
	/// �ı��ļ���INI�ļ��Ĵ���
	/// FileMgrc.cs
	/// ��ǿ
	/// 2006.05.11
	/// </summary>


	#region �ı��ļ��Ĳ���
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
                    ErrMsg = "�ļ������ڣ�(" + _FileFullName + ")";
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
		/// д��־��Ϣ���ı��ļ�
		/// </summary>
		/// <param name="FileContent">��־��Ϣ</param>
		/// <param name="TxtFileName">�ļ�����</param>
		/// <param name="TxtFilePath">�ļ���������Ŀ¼</param>
		/// <param name="ErrMsg">���صĴ�����Ϣ</param>
		/// <returns>ture:�ɹ���false:ʧ��</returns>
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

        #region �ļ�����    �Ǻ��2008.08.12���
        #region ��ȡ�ļ�
        /// <summary>
        /// ��ȡ�ļ��ļ�
        /// </summary>
        /// <param name="filePath">�ļ���·��</param>
        /// <param name="fileList">�ļ����б�</param>
        /// <param name="errMsg">������Ϣ</param>
        /// <returns></returns>
        public static bool GetFileNames(string filePath, string pattern, out ArrayList fileList,out string errMsg)
        {
            fileList = new ArrayList();
            errMsg = "";
            try
            {
                #region ȡ�ļ�
                if (Directory.Exists(filePath))//(directory.Exists || pattern.Trim() != string.Empty)
                {
                    string[] fileNames = Directory.GetFiles(filePath, pattern);
                    if (fileNames.Length == 0)//�ļ��������ļ����
                    {
                        errMsg = "ָ���ļ���Ϊ���ļ���";
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
        /// ��ȡȫ�������ļ�
        /// </summary>
        /// <param name="filePath">�ļ���·��</param>
        /// <param name="fileList">�ļ��б�</param>
        /// <param name="errMsg">������Ϣ</param>
        /// <returns></returns>
        public static bool GetFileNames(string filePath, out ArrayList fileList,out string errMsg)
        {
            fileList = new ArrayList();
            errMsg = "";
            try
            {
                #region ȡ�ļ�
                if (Directory.Exists(filePath))//(directory.Exists || pattern.Trim() != string.Empty)
                {
                    string[] fileNames = Directory.GetFiles(filePath);
                    if (fileNames.Length == 0)//�ļ��������ļ����
                    {
                        errMsg = "ָ���ļ���Ϊ���ļ���";
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

        #region ���ļ�
        /// <summary>
        /// ���ļ�
        /// </summary>
        /// <param name="sFileName">�ļ���</param>
        /// <param name="resultString">�ļ�����</param>
        /// <param name="errMsg">������Ϣ</param>
        /// <returns>�ɹ����</returns>
        public static bool ReadFiles(string sFileName, out string resultString, out  string errMsg)
        {
            errMsg = "";
            resultString = "";
            try
            {
                if (!File.Exists(sFileName))
                {
                    errMsg = "�ļ�:" + sFileName + "�����ڣ�";
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

        #region ���ļ�
        /// <summary>
        /// ���ļ�
        /// </summary>
        /// <param name="sFileName">�ļ���</param>
        /// <param name="fileContent">�ļ���������</param>
        /// <param name="errMsg">������Ϣ</param>
        /// <returns>�ɹ����</returns>
        public static bool ReadFiles(string sFileName, out ArrayList fileContent, out  string errMsg)
        {
            errMsg = "";
            fileContent = new ArrayList();
            string resultString = "";
            try
            {
                if (!File.Exists(sFileName))
                {
                    errMsg = "�ļ�:" + sFileName + "�����ڣ�";
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

        #region ɾ���ļ�
        /// <summary>
        /// ɾ���ļ�
        /// </summary>
        /// <param name="sFileName">�ļ�ȫ����·��</param>
        /// <param name="resultString">�ļ�����</param>
        /// <param name="errMsg">������Ϣ</param>
        /// <returns>�ɹ����</returns>
        public static bool DeleteFiles(string sFileName, out  string errMsg)
        {
            errMsg = "";
            try
            {
                if (File.Exists(sFileName))
                {
                    File.Delete(sFileName);//
                    errMsg = "ɾ���ļ�" + sFileName + "�ɹ���";
                }
                else
                {
                    errMsg = "�ļ�" + sFileName + "�����ڣ�";
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

        #region �ƶ��ļ�
        /// <summary>
        /// �ƶ��ļ�
        /// </summary>
        /// <param name="filePath">�ļ�·��</param>
        /// <param name="targetPath">Ŀ��·��</param>
        /// <param name="errMsg">������Ϣ</param>
        /// <returns></returns>
        public static bool MoveFiles(string filePath, string targetPath, out  string errMsg)
        {
            errMsg = "";
            try
            {
                if (File.Exists(filePath))//�ж��ļ��Ƿ����
                {
                    if (!File.Exists(targetPath))//�ж�Ŀ���ļ����Ƿ����ͬ���ļ�
                    {
                        File.Move(filePath, targetPath);
                    }
                    else
                    {
                        errMsg = "Ŀ���ļ��Ѵ���ͬ���ļ���";
                        return false;
                    }
                }
                else
                {
                    errMsg = "�ļ�:" + filePath + "�����ڣ�";
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

        #region �ƶ��ļ�
        /// <summary>
        /// �ƶ��ļ�
        /// </summary>
        /// <param name="filePath">�ļ�·��</param>
        /// <param name="targetPath">Ŀ��·��</param>
        /// <param name="errMsg">������Ϣ</param>
        /// <returns></returns>
        public static bool MoveFiles(string filePath,string fileName, string targetPath, out  string errMsg)
        {
            errMsg = "";
            string Path = "";
            try
            {
                if (File.Exists(filePath))//�ж��ļ��Ƿ����
                {
                    if (!Directory.Exists(targetPath))
                        Directory.CreateDirectory(targetPath);
                    Path = targetPath + "\\" + fileName;
                    if (!File.Exists(targetPath))//�ж�Ŀ���ļ����Ƿ����ͬ���ļ�
                    {
                        File.Move(filePath, Path);
                    }
                    else
                    {
                        errMsg = "Ŀ���ļ��Ѵ���ͬ���ļ���";
                        return false;
                    }
                }
                else
                {
                    errMsg = "�ļ�:" + filePath + "�����ڣ�";
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

	#region ini�ļ��Ĳ���

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
	/// ��ͨini�ļ�ʵ����
	/// </summary>
	public class INIFileEntity
	{
		#region �๹��
		/// <summary>
		/// ini�ļ�ʵ���๹��
		/// </summary>
		public INIFileEntity()
		{}
		#endregion

		#region ��ı�������
		private string _svFilePath;
		private string _svFileName;
		private string _svSection;
		private string _svKeyWord;
		private string _svValue;
		#endregion

		#region ��Ĺ�����������
		/// <summary>
		/// ini�ļ�����·��
		/// </summary>
		public string svFilePath
		{
			get{return _svFilePath;}
			set{_svFilePath =value;}
		}
		/// <summary>
		/// ini�ļ����ļ���
		/// </summary>
		public string svFileName
		{
			get{return _svFileName;}
			set{_svFileName =value;}
		}
		/// <summary>
		/// ini�ļ��е�����
		/// </summary>
		public string svSection
		{
			get{return _svSection;}
			set{_svSection =value;}
		}
		/// <summary>
		/// ini�ļ��еļ���
		/// </summary>
		public string svKeyWord
		{
			get{return _svKeyWord;}
			set{_svKeyWord =value;}
		}
		/// <summary>
		/// ini�ļ��еļ�ֵ
		/// </summary>
		public string svValue
		{
			get{return _svValue;}
			set{_svValue =value;}
		}
		#endregion
	}

	/// <summary>
	/// ��ͨini�ļ�������
	/// </summary>
	public class INIFileMgr
	{
		#region ��Ĺ��캯��
		/// <summary>
		/// ��ͨini�ļ�������
		/// </summary>
		public INIFileMgr()
		{}
		#endregion

		#region����Ĺ��з�������
		/// <summary>
		/// дini�ļ�
		/// </summary>
		/// <param name="objFile">ini�ļ�ʵ����</param>
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
					throw new Exception("����Ϣд��INI�ļ�ʱ��������\n"+objFile.svSection+"__"+objFile.svKeyWord+"__"+objFile.svValue+"\n\n"+Err.Message);
			}
		}
		
		/// <summary>
		/// ��ini�ļ���Ϣ
		/// </summary>
		/// <param name="objFile">ini�ļ���Ϣ</param>
		/// <returns>ini�ļ��е�һ����ֵ</returns>
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
					throw new Exception("��ȡINI�ļ���Ϣ����\n"+objFile.svSection+"__"+objFile.svKeyWord+"\n\n"+Err.Message);
			}
			return objFile.svValue;
		}		
		#endregion
	}
	#endregion

}
