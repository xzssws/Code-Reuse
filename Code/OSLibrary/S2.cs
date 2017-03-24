using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.IO;//文件流
using System.Linq;
using System.Net;//网络编程空间
using System.Net.Mail;
using System.Net.NetworkInformation;
using System.Reflection;
using System.Runtime.InteropServices;//加载DllImport
using System.Runtime.Serialization.Formatters.Binary;
using System.Runtime.Serialization.Formatters.Soap;
using System.Security.Cryptography;//MD5加密空间
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace Trouble_Ze
{
    public class S2
    {
        /// <summary>
        ///  给予字符串获得Byte数组
        /// </summary>
        /// <param name="value">要编码的字符串</param>
        /// <returns>byte数组</returns>
        public byte[] GoBytes(string value)
        {
            char[] chars = value.ToCharArray();
            Encoder utf8 = UTF8Encoding.UTF8.GetEncoder();
            byte[] bytes = new byte[utf8.GetByteCount(chars, 0, chars.Length, true)];
            utf8.GetBytes(chars, 0, chars.Length, bytes, 0, true);
            return bytes;
        }

        /// <summary>
        /// 测试的IP地址是否能Ping成功
        /// </summary>
        /// <param name="InnerIp">要测试的IP地址</param>
        public static bool TestPing(string InnerIp)
        {
            PingReply reply = new Ping().Send(InnerIp);
            return reply.Status == IPStatus.Success ? true : false;
            //修改于2012-10-12 13:01 直接构造函数对象.方法  用三元替换掉if else
        }

        /// <summary>
        /// 操作进程项
        /// </summary>
        /// <param name="temp">进程对象</param>
        /// <param name="ProcessName">进程名称</param>
        /// <param name="ProcessParameter">进程参数</param>
        /// <param name="IsHide">是否隐藏</param>
        public static void OpenProcess(string ProcessName, string ProcessParameter, bool IsHide)
        {
            try
            {
                ProcessStartInfo process_Info = new ProcessStartInfo(ProcessName, ProcessParameter);
                Process process_Temp = new Process();
                process_Temp.StartInfo = process_Info;
                process_Info.WindowStyle = IsHide ? ProcessWindowStyle.Hidden : ProcessWindowStyle.Normal;
                process_Temp.Start();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            //修改于2012-10-12 13:14 简化掉一部分代码 去掉 关闭和返回值
        }

        /// <summary>
        /// 返回一个数据库连接
        /// </summary>
        /// <param name="数据库名称">要连接数据库的名称</param>
        /// <param name="服务器名称">连接的服务器名称</param>
        /// <param name="无密码模式">是否使用Windows模式</param>
        /// <param name="用户名">用户名</param>
        /// <param name="密码">密码</param>
        /// <returns></returns>
        public static SqlConnection 数据库连接(string 数据库名称, string 服务器名称, bool 无密码模式, string 用户名 = "", string 密码 = "")
        {
            SqlConnection conn = new SqlConnection();
            string 连接字符串;
            if (无密码模式)
            {
                连接字符串 = string.Format("Data Source={0};Initial Catalog={1};Integrated Security=True;Pooling=False", 服务器名称, 数据库名称);
            }
            else
            {
                连接字符串 = string.Format("Data Source={0}; initial catalog ={1}; user id={2}; pwd ={3}", 服务器名称, 数据库名称, 用户名, 密码);
            }
            try
            {
                return new SqlConnection();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }
        }

        /// <summary>
        /// 如果数据库关闭则打开如果打开则关闭
        /// </summary>
        /// <param name="数据库">传入数据库连接参数</param>
        public static void 开关数据库(SqlConnection 数据库)
        {
            if (数据库.State == ConnectionState.Closed)//如果传入的数据库连接为关闭
            {
                数据库.Open();
            }
            else if (数据库.State == ConnectionState.Open)
            {
                数据库.Close();
            }
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="查询字符串">需要查询的字符串</param>
        /// <param name="数据库连接">输入数据库连接</param>
        /// <returns>返回一个SqlDataReader对象</returns>
        public static SqlDataReader SqlRead(string 查询字符串, SqlConnection 数据库连接)
        {
            return new SqlCommand(查询字符串, 数据库连接).ExecuteReader();
            //未完成 原本思路 将数据库查询完毕 并把每一项赋值给一个字符串 返回一个字符串数组
        }

        /// <summary>
        /// 返回修改过的行数
        /// </summary>
        /// <param name="更改字符串">需要执行的SQL语句</param>
        /// <param name="数据库连接">输入数据库连接</param>
        /// <returns>返回受影响的行数</returns>
        public static int SqlSet(string 更改字符串, SqlConnection 数据库连接)
        {
            return new SqlCommand(更改字符串, 数据库连接).ExecuteNonQuery();
        }

        /// <summary>
        /// 返回数据库查询单行结果
        /// </summary>
        /// <param name="查询字符串">需要执行的SQL语句</param>
        /// <param name="数据库连接">输入数据库连接</param>
        /// <returns>返回字符串数据</returns>
        public static string SqlResult(string 查询字符串, SqlConnection 数据库连接)
        {
            return new SqlCommand(查询字符串, 数据库连接).ExecuteScalar().ToString();
        }

        /// <summary>
        /// 验证正则表达式
        /// </summary>
        /// <param name="Expression">输入的正则表达式</param>
        /// <param name="Text">需要验证正则表达式的字符串</param>
        /// <returns>返回一个布尔值</returns>
        public static bool RegExp(string Text, string Expression)
        {
            return Regex.IsMatch(Text, Expression);
        }

        /// <summary>
        /// 使用MD5加密字符串
        /// </summary>
        /// <param name="Text">输入需要加密的字符串参数</param>
        /// <returns>如果参数为空返回字符串为空 成功则显示加密后的字符串</returns>
        public static string MD54String(string Text)
        {
            //创建一个MD5储值的变量
            string pwd = "";
            // 加密后是一个字节类型的数组，这里要注意编码UTF8/Unicode等的选择　
            byte[] s = MD5.Create().ComputeHash(Encoding.UTF8.GetBytes(Text));
            // 通过使用循环，将字节类型的数组转换为字符串，此字符串是常规字符格式化所得
            for (int i = 0; i < s.Length; i++)
            {
                // 将得到的字符串使用十六进制类型格式。格式后的字符是小写的字母，如果使用大写（X）则格式后的字符是大写字符
                pwd = pwd + s[i].ToString("X");
            }
            return pwd;
        }

        /// <summary>
        /// 修改文件属性方法
        /// </summary>
        /// <param name="文档路径">传入文档路径 可以是文件夹 也可以是文件</param>
        /// <param name="功能"> 文档功能：1 只读 2 系统 3 存档 4 隐藏 5 正常</param>
        public static void 修改文件属性(string 文档路径, string 功能)
        {
            /*
             * 文档功能：1 只读 2 系统 3 存档 4 隐藏 5 正常 需要名称空间 System.IO;
             */
            FileInfo FileObject = new FileInfo(文档路径);
            switch (功能)
            {
                case "1":
                    FileObject.Attributes = FileAttributes.ReadOnly;//文件属性设置为只读
                    break;

                case "2":
                    FileObject.Attributes = FileAttributes.System;//文件属性设置为系统文件
                    break;

                case "3":
                    FileObject.Attributes = FileAttributes.Archive;//文件属性设置为存档
                    break;

                case "4":
                    FileObject.Attributes = FileAttributes.Hidden;//文件属性设置为隐藏
                    break;

                case "5":
                    FileObject.Attributes = FileAttributes.Normal;//文件属性设置为正常 仅对单个文件生效
                    break;
            }
        }

        
        /// <summary>
        /// 在ListView控件中查询数据有则返回True 无返回False
        /// </summary>
        /// <param name="ListViewControl">控件对象</param>
        /// <param name="Text">需要查询的关键字</param>
        /// <returns>返回一个布尔值</returns>
        public static bool FindItemViewItem(ListView ListViewControl, string Text)
        {
            ListViewItem founditem = ListViewControl.FindItemWithText(Text);
            return founditem == null ? false : true;
        }

        /// <summary>
        /// 在TextBox控件中选择起始位置到选择长度
        /// </summary>
        /// <param name="TextBoxControl">TextBox对象</param>
        /// <param name="StartLength">光标起始位置</param>
        /// <param name="EndLength">选择文本的长度</param>
        public static void SelectTextBoxText(TextBox TextBoxControl, int StartLength, int EndLength)
        {
            TextBoxControl.SelectionStart = StartLength;
            TextBoxControl.SelectionLength = EndLength;
        }

        /// <summary>
        /// 查看类中方法并返回字符串数组
        /// </summary>
        /// <param name="tempClass">类</param>
        /// <returns>返回类中方法的字符串数组</returns>
        public static string[] ShowClass(object tempClass)
        {
            int i = 0;
            Type typeObject = tempClass.GetType();//类型无效！！ 问题？？
            MethodInfo[] methodInfoObject = typeObject.GetMethods();//遍历类中所有方法
            string[] temp = new string[methodInfoObject.Length];//定义字符串数组长度与类方法集合的长度相同
            foreach (MethodInfo item in methodInfoObject)//开始遍历
            {
                temp[i] = item.ToString();
                i++;
            }
            return temp;
        }

        /// <summary>
        /// 窗体内获取拖放文件的路径
        /// </summary>
        /// <param name="frm">要拖放到得窗体</param>
        /// <param name="e">拖放的文件</param>
        public static string 拖放控件(Form frm, DragEventArgs e)
        {
            frm.AllowDrop = true;//开启拖放功能
            e.Effect = DragDropEffects.Copy;//拖放的效果
            string[] str = (string[])e.Data.GetData(DataFormats.FileDrop, true);//获取拖放文件的路径 输出的数组[0]是文件路径
            return str[0];//返回第一个
            //已经获取到文件的路径了 下列就是自由想象空间了
        }

        /*
         * 加载该方法来实现用鼠标来拖动窗体
         * 代码如下
         *  private void Form1_MouseDown(object sender, MouseEventArgs e)  用窗体鼠标按下事件来实现此方法
            {
                 new S2().移动窗体(this.Handle);
            }
         */

        [DllImport("user32.dll")]
        public static extern bool ReleaseCapture();//加载外部文件User32.dll中的方法 ReleaseCapture（）

        [DllImport("user32.dll")]
        public static extern bool SendMessage(IntPtr hwnd, int wMsg, int wParam, int IParam);//加载外部文件User32.dll文件中的SendMessage方法

        public static void 移动窗体(IntPtr 当前窗体句柄)
        {
            ReleaseCapture();//调用方法 未知？？
            SendMessage(当前窗体句柄, 0x0112, 0xf010 + 0x0002, 0);//实现移动 未知？？
        }

        /// <summary>
        /// 参数传入数字返回该数值的中文大写
        /// </summary>
        /// <param name="数值">传入Double参数数据</param>
        /// <returns>返回传入数值的中文大写形式</returns>
        //由窗体标题或窗体类来获取窗体句柄
        [DllImport("user32.dll")]
        private static extern IntPtr FindWindow(String 窗体类名, String 窗体名称);

        /// <summary>
        /// 输入窗体名称,或窗体类名返回窗体句柄
        /// </summary>
        /// <param name="窗体类名">可选！窗体类名</param>
        /// <param name="窗体名称">可选！窗体标题</param>
        /// <returns></returns>
        public IntPtr 获取窗体句柄(String 窗体类名 = null, String 窗体名称 = null)
        {
            return FindWindow(窗体类名, 窗体名称);
        }

        //返回子对象句柄
        [DllImport("User32.dll", EntryPoint = "FindWindowEx")]
        private static extern IntPtr FindWindowEx(IntPtr 父句柄, IntPtr 子窗体句柄0, string 对象类名, string 对象标题);

        /// <summary>
        /// 获取子对象句柄 返回0则为空
        /// </summary>
        /// <param name="窗体类名或名称">输入窗体类名或名称可自动判断</param>
        /// <param name="对象类名">通过其他方式获取对象类名</param>
        /// <returns>返回窗体句柄</returns>
        public IntPtr 获取子对象句柄(String 窗体类名或名称, String 对象类名)
        {
            IntPtr 窗体句柄 = FindWindow(窗体类名或名称, null).ToInt32() == 0 ? FindWindow(窗体类名或名称, null) : FindWindow(null, 窗体类名或名称);
            if (窗体句柄.ToInt32() != 0)
            {
                return FindWindowEx(窗体句柄, new IntPtr(0), 对象类名, null);
            }
            return new IntPtr(0);
        }

        //通过句柄设置窗体状态
        [DllImport("user32.dll")]
        private static extern bool ShowWindow(IntPtr hWnd, int nCmdShow);

        /// <summary>
        /// 设置窗体状态 是窗体为当前窗体
        /// </summary>
        /// <param name="窗体句柄"></param>
        /// <param name="状态值">设置状态值</param>
        /// <returns>返回是否成功</returns>
        public bool 设置窗体状态(IntPtr 窗体句柄, int 状态值)
        {
            return ShowWindow(窗体句柄, 状态值);
        }

        //设置当前窗体
        [DllImport("user32.dll")]
        private static extern bool SetForegroundWindow(IntPtr hWnd);

        private static bool 设置当前窗体(IntPtr 窗体句柄)
        {
            return SetForegroundWindow(窗体句柄);
        }

        //根据句柄获得类名
        [DllImport("user32.dll")]
        private static extern void GetClassName(IntPtr hwnd, StringBuilder lpClassName, int nMaxCount);

        public void 根据句柄获得类名(IntPtr 窗体句柄)
        {
            StringBuilder lpClassName = new StringBuilder(256);
            GetClassName(窗体句柄, new StringBuilder(256), 256);
        }

        //向窗体发送信息
        [DllImport("user32.dll")]
        private static extern int SendMessage(IntPtr hWnd, int Msg, int wParam, string lParam);

        private const int WM_SETTEXT = 0x000C;

        public void 向窗体发送信息(IntPtr 对象句柄, string 信息)
        {
            SendMessage(对象句柄, 0x000C, 0, 信息);
        }

        [DllImport("user32.dll")]
        public static extern IntPtr LoadCursorFromFile(string fileName);//获取指定光标文件的句柄

        [DllImport("user32", EntryPoint = "LoadCursorFromFile")]
        public static extern int IntLoadCursorFromFile(string lpFileName);//加光标路径

        [DllImport("user32", EntryPoint = "SetSystemCursor")]
        public static extern void SetSystemCursor(int hcur, int i);  //设置系统光标

        /// <summary>
        /// 返回指定光标图标的光标对象
        /// </summary>
        /// <param name="光标路径">输入光标所在路径</param>
        /// <returns>返回Cursor对象</returns>
        public Cursor ChangeCur(string 光标路径)
        {
            Cursor myCursor = new Cursor(Cursor.Current.Handle);//创建一个新的光标对象
            IntPtr colorCursorHandle = LoadCursorFromFile(光标路径);//鼠标图标路径
            myCursor.GetType().InvokeMember("handle", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.SetField, null, myCursor, new object[] { colorCursorHandle });//不可少的项
            return myCursor;
        }

        /// <summary>
        /// 利用反射返回类
        /// </summary>
        /// <param name="src">可执行文件的路径</param>
        /// <param name="ClassName">返回类的类名</param>
        /// <returns>返回该程序集的指定类</returns>
        public static string 输入数字返回大写(double 数值)
        {
            string[] 单位 = { "分", "角", "元", "拾", "佰", "仟", "万", "拾", "佰", "仟", "亿", "拾", "佰", "仟", "兆", "拾", "佰", "仟" };
            string[] 数字大写 = { "零", "壹", "贰", "叁", "肆", "伍", "陆", "柒", "捌", "玖" };
            string 临时 = 数值.ToString().Replace(".", "");//把小数点替换掉
            string 大写字符串 = "";
            for (int i = 临时.Length; i > 0; i--)
            {
                大写字符串 += 数字大写[Convert.ToInt32(临时[临时.Length - i].ToString())];
                大写字符串 += 单位[i - 1];
            }
            return 大写字符串;
        }

        public static object 反射类(string src, string ClassName)
        {
            try
            {
                Assembly assembly = Assembly.LoadFile(src);
                Type[] types = assembly.GetTypes();
                Type Class = types.First<Type>(t => t.Name == ClassName);
                return Activator.CreateInstance(Class);
            }
            catch
            {
                return null;
            }
        }

        /// <summary>
        /// 对对象进行二进制序列化
        /// </summary>
        /// <param name="输出路径">输入输出路径</param>
        /// <param name="对象">输入对象</param>
        /// <returns>成功true 错误false </returns>
        public static bool 二进制序列化(string 输出路径, object 对象)
        {
            using (FileStream fs = new FileStream(输出路径, FileMode.Create))
            {
                try
                {
                    BinaryFormatter bf = new BinaryFormatter();
                    bf.Serialize(fs, 对象);
                    return true;
                }
                catch
                {
                    return false;
                }
            }
        }

        /// <summary>
        /// 二进制式反序列化返回对象
        /// </summary>
        /// <param name="文件路径">输入文件路径</param>
        /// <returns>返回对象，成功</returns>
        public static object 二进制反序列化(string 文件路径)
        {
            using (FileStream fs = new FileStream(文件路径, FileMode.Open))
            {
                try
                {
                    BinaryFormatter bf = new BinaryFormatter();
                    return bf.Deserialize(fs);
                }
                catch
                {
                    return null;
                }
            }
        }

        /// <summary>
        /// 只输入数字的输入框 Press事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void 只输入数字输入框(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && e.KeyChar != '\b')//\b为退格符
            {
                e.Handled = true;
            }
        }

        /// <summary>
        /// 在注册表中编辑项的方法
        /// </summary>
        public void SetRegeditItem()
        {
            RegistryKey RootKey = Registry.CurrentConfig; //创建键代理根节点
            /*           寻找根键
             * 声明RegistryKey对象
             * 映射注册表中CurrentConfig根键
             */
            RegistryKey CreateKey = RootKey.CreateSubKey("Folder");
            /*           创建
             * 注册表中创建项方法 CreateSubKey（）方法中
             * 参数为地址 \\Software\\....\\项名称
             * 若是在根目录中创建则不需要写路径
             */
            RegistryKey OpenSonKey = RootKey.OpenSubKey("Folder", true);
            /*           打开
             * 由于第一条语句中Registry 命令只能访问注册表
             * 的根目录，如果要访问子目录需要 写N多路径
             * 我们需要创建一个子路径目录 用到在根目录对象.OpenSubKey对象
             * 来创建 第一个参数为路径，第二个参数为是否有权限
             */
            RootKey.DeleteSubKey("Folder");
            /*           删除
             *根本思想 ：每一个RegistryKey声明对象都代表着注册表中的一个路径
             *          所有首参数也均是路径形式
             *本语句则是在RootKey对象对应的路径中删除Folder项 也可以使路径
             */
        }

        /// <summary>
        /// 在注册表中编辑键值的方法
        /// </summary>
        public void SetRegeditKey()
        {
            RegistryKey RootKey = Registry.CurrentConfig;
            //创建根对象
            RegistryKey SonKey = RootKey.OpenSubKey("Folder", true);
            //打开子项目录 贯穿整个方法
            SonKey.SetValue("Name", "Key", RegistryValueKind.String);
            /*           创建键值
             * 在SonKey引用的路径下 使用SetValue方法
             * 创建键值 首参为名称 第二参为数据 第三参为数据类型
             */
            //SonKey.DeleteValue("Name");
            /*           删除键值
             * 在SonKey引用的路径下 使用DeleteValue方法
             * 删除键值 首参为要删除键值名称
             */
            string KeyInfo = SonKey.GetValue("Name").ToString();
            /*           获取键值
             * 在SonKey引用的路径下 使用GetValue方法
             * 查看数据 参数为键值名称
             */
            string[] KeyNames = SonKey.GetValueNames();
            /*           获取SonKey目录下所有键的名称
             * 在SonKEy引用路径下 使用GetValueNames方法
             * 返回数组数据 定义一个数组接收每个键的名称
             */
            string[] ItemNames = SonKey.GetSubKeyNames();
            /*
             *           获取目录下所有项的名称
             * 在SonKey引用路径下 使用GetSubKeyNames方法
             * 返回数组数据 定义数组接收每个键值名称
             */
        }

        //使用Soap进行序列化 输入创建参数 和对象 注意要用通用类型
        public static void SaveSoap(string src, object o)
        {
            using (FileStream fs = new FileStream(src, FileMode.Create))
            {
                new SoapFormatter().Serialize(fs, o);
            }
        }

        //使用Soap进行反序列化 输入地址参数
        public static object OpenSoap(string src)
        {
            using (FileStream fs = new FileStream(src, FileMode.Open))
            {
                return new SoapFormatter().Deserialize(fs);
            }
        }
    }

    public class Y2
    {
        public void ShowNetWork()
        {
            NetworkInterface[] adapters = NetworkInterface.GetAllNetworkInterfaces();
            IPInterfaceProperties ip = adapters[0].GetIPProperties();
            foreach (NetworkInterface item in adapters)
            {
                Console.WriteLine("描述信息" + item.Description);
                Console.WriteLine("名称:{0}", item.Name);
                Console.WriteLine("类型:{0}", item.NetworkInterfaceType);
                Console.WriteLine("速度:{0}", item.Speed);
                Console.WriteLine("MAC:{0}", item.GetPhysicalAddress());
                IPAddressCollection dnsServers = item.GetIPProperties().DnsAddresses;
                if (dnsServers.Count > 0)
                {
                    foreach (IPAddress item2 in dnsServers)
                    {
                        Console.WriteLine("DNS服务器IP地址" + item2);
                    }
                }
                else
                {
                    Console.WriteLine("DNS服务器IP地址");
                }
            }
        }

        public string btn_Click(string txtNum, string txtKey)
        {
            int num, key;
            if (int.TryParse(txtNum, out num) && int.TryParse(txtKey, out key))//确定是否输入是数值
            {
                return (num ^ key).ToString();//加密主要步骤
            }
            else
            {
                return "请输入正确的数值";
            }
        }

        public string jbtn_Click(string txtNum, string txtKey)
        {
            int key, encrypt;
            if (int.TryParse(txtNum, out encrypt) && int.TryParse(txtKey, out key))//确定是否输入是数值
            {
                return (encrypt ^ key).ToString();//加密主要步骤
            }
            else
            {
                return "请输入正确的数值";
            }
        }

        private static void 网络流量监测(ref string IP)
        {
            StringBuilder sb = new StringBuilder();
            string ip = IP;
            Ping pingsender = new Ping();
            PingOptions po = new PingOptions();
            po.DontFragment = true;
            byte[] buffer = Encoding.ASCII.GetBytes("test");
            int timeout = 120;

            try
            {
                PingReply reply = pingsender.Send(ip, timeout, buffer, po);

                if (reply.Status == IPStatus.Success)
                {
                    sb.AppendLine("答复主机的时间:" + reply.Address.ToString());
                    sb.AppendLine("往返时间:" + reply.RoundtripTime);
                    sb.AppendLine("生存时间:" + reply.Options.Ttl);
                    sb.AppendLine("是否控制数据包的分段:" + (reply.Options.DontFragment ? "是" : "否"));
                    sb.AppendLine("缓冲区大小:" + reply.Buffer.Length);
                }
                else
                {
                    sb.AppendLine(reply.Status.ToString());
                }
                IP = sb.ToString();
            }
            catch (Exception ex)
            {
                IP = ex.Message;
            }
        }

        public string getMD5(string str)
        {
            string pwd = "";
            MD5 md5 = MD5.Create();//实例化一个md5对像
            // 加密后是一个字节类型的数组，这里要注意编码UTF8/Unicode等的选择　
            byte[] s = md5.ComputeHash(Encoding.UTF8.GetBytes(str));
            // 通过使用循环，将字节类型的数组转换为字符串，此字符串是常规字符格式化所得
            for (int i = 0; i < s.Length; i++)
            {
                // 将得到的字符串使用十六进制类型格式。格式后的字符是小写的字母，如果使用大写（X）则格式后的字符是大写字符
                pwd = pwd + s[i].ToString("X");
            }
            return pwd;
        }

        /// <summary>
        /// 发送邮件代码 示例
        /// </summary>
        public void SendEmail()
        {
            try
            {
                //实例化SmtpClient
                SmtpClient client = new SmtpClient("邮箱STMP服务器");
                //出站方式设置为NetWork
                client.DeliveryMethod = SmtpDeliveryMethod.Network;
                //smtp服务器验证并制定账号密码
                client.Credentials = new NetworkCredential("您的用户名", "你的密码");
                //实例化邮件对象
                MailMessage message = new MailMessage();
                //设置邮件优先级
                message.Priority = MailPriority.Normal;
                //设置收件方看到的邮件来源为:发送方邮件地址、来源标题、编码
                message.From = new MailAddress("发件人", "发件人名称", Encoding.GetEncoding("gb2312"));
                //接收方
                message.To.Add("收件人");
                //标题
                message.Subject = "标题";
                message.SubjectEncoding = Encoding.GetEncoding("gb2312");
                //邮件正文是否支持HTML
                message.IsBodyHtml = true;
                //邮件内容
                message.Body = "";
                //正文编码
                message.BodyEncoding = Encoding.GetEncoding("gb2312");
                //发送
                client.Send(message);
            }
            catch (SmtpException ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// 自动生成实体类代码
        /// </summary>
        /// <param name="classname"> 类名</param>
        /// <param name="dc">字典型 key 属性名称 value 类型</param>
        /// <returns>生成的字符串代码</returns>
        public string AutoClass(string classname, Dictionary<string, string> dc)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("public class " + classname + Environment.NewLine + "{" + Environment.NewLine);
            foreach (var item in dc)
            {
                sb.AppendLine("    private " + item.Value + " _" + item.Key + " ;" + Environment.NewLine);
                sb.AppendLine("    public " + item.Value + " " + item.Key + Environment.NewLine + "    {" + Environment.NewLine + "        get { return " + "_" + item.Key + "; }" + Environment.NewLine + "        set { " + item.Key + " = value; }" + Environment.NewLine + "    }");
            }
            sb.AppendLine(Environment.NewLine + "}");
            return sb.ToString();
        }
    }
}

/*
 * 自定义转换
   public static <implicit隐式|explicit显式> operator 隐式转换类(自身类 Object)
       {
        逻辑
        return 隐式转换类();
       }
*/

/*
 * 运算符重载
  public static operator <运算符> (自身类 对象)
  {
    return 自身类();
  }
*/

/*
  * 扩展方法的使用
  public static <返回值> <方法名称>(this <要扩展的类> 形参)
  {
	业务逻辑的实现，
  }
*/