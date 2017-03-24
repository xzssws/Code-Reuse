using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;

namespace OSLibrary.系统
{
    public class WindowsManager
    {
        #region 引用API
        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        internal struct TokPriv1Luid
        {
            public int Count;
            public long Luid;
            public int Attr;
        }

        [DllImport("kernel32.dll", ExactSpelling = true)]
        internal static extern IntPtr GetCurrentProcess();

        [DllImport("advapi32.dll", ExactSpelling = true, SetLastError = true)]
        internal static extern bool OpenProcessToken(IntPtr h, int acc, ref IntPtr phtok);

        [DllImport("advapi32.dll", SetLastError = true)]
        internal static extern bool LookupPrivilegeValue(string host, string name, ref long pluid);

        [DllImport("advapi32.dll", ExactSpelling = true, SetLastError = true)]
        internal static extern bool AdjustTokenPrivileges(IntPtr htok, bool disall,
            ref TokPriv1Luid newst, int len, IntPtr prev, IntPtr relen);

        [DllImport("user32.dll", ExactSpelling = true, SetLastError = true)]
        internal static extern bool ExitWindowsEx(int flg, int rea);
        #endregion

        #region 常量定义
        internal const int SE_PRIVILEGE_ENABLED = 0x00000002;
        internal const int TOKEN_QUERY = 0x00000008;
        internal const int TOKEN_ADJUST_PRIVILEGES = 0x00000020;
        internal const string SE_SHUTDOWN_NAME = "SeShutdownPrivilege";

        /// <summary>
        /// 注销
        /// </summary>
        internal const int EWX_LOGOFF = 0x00000000;

        /// <summary>
        /// 关机
        /// </summary>
        internal const int EWX_SHUTDOWN = 0x00000001;

        /// <summary>
        /// 重启
        /// </summary>
        internal const int EWX_REBOOT = 0x00000002;

        internal const int EWX_FORCE = 0x00000004;
        internal const int EWX_POWEROFF = 0x00000008;
        internal const int EWX_FORCEIFHUNG = 0x00000010;
        #endregion

        #region 内部方法
        /// <summary>
        /// 退出Windows
        /// </summary>
        /// <param name="flg">标志</param>
        private static void DoExitWin(int flg)
        {
            bool ok;
            TokPriv1Luid tp;
            IntPtr hproc = GetCurrentProcess();
            IntPtr htok = IntPtr.Zero;
            ok = OpenProcessToken(hproc, TOKEN_ADJUST_PRIVILEGES | TOKEN_QUERY, ref htok);
            tp.Count = 1;
            tp.Luid = 0;
            tp.Attr = SE_PRIVILEGE_ENABLED;
            ok = LookupPrivilegeValue(null, SE_SHUTDOWN_NAME, ref tp.Luid);
            ok = AdjustTokenPrivileges(htok, false, ref tp, 0, IntPtr.Zero, IntPtr.Zero);
            ok = ExitWindowsEx(flg, 0);
        }
        /// <summary>
        /// 设置启动项
        /// </summary>
        /// <param name="cmd">true 启动 false 取消启动</param>
        /// <param name="KeyName">启动项名称</param>
        /// <param name="argPath">文件路径</param>
        private static bool RegCompStartRun(bool cmd, string KeyName, string argPath)
        {
            string starupPath = argPath;
            if (string.IsNullOrEmpty(argPath) || !System.IO.File.Exists(argPath)) return false;
            //表示Window注册表中项级节点,读取 Windows 注册表基项HKEY_LOCAL_MACHINE
            Microsoft.Win32.RegistryKey loca = Microsoft.Win32.Registry.LocalMachine;
            Microsoft.Win32.RegistryKey run = loca.CreateSubKey(@"SOFTWARE\Microsoft\Windows\CurrentVersion\Run");

            try
            {
                if (cmd)
                {
                    if (run.GetValue(KeyName) == null)
                    {
                        run.SetValue(KeyName, starupPath);//加入注册，参数一为注册节点名称(随意)
                    }
                }
                else
                {
                    if (run.GetValue(KeyName) != null)
                    {
                        run.DeleteValue(KeyName, false);//删除该注册节点
                    }
                }
                loca.Close();
                return true;
            }
            catch
            {
                return false;
            }
        }
        /// <summary>
        /// 设置启动项
        /// </summary>
        /// <param name="cmd">true 启动 false 取消启动</param>
        /// <param name="KeyName">启动项名称</param>
        /// <param name="argPath">文件路径</param>
        private static bool RegCompStartRun(bool cmd, string argPath)
        {
            FileInfo fi = new FileInfo(argPath);
            return RegCompStartRun(cmd, fi.Name, argPath);
        }

        #endregion

        #region 关机
        /// <summary>
        /// 关机
        /// </summary>
        public static void ShutDown()
        {
            DoExitWin(EWX_SHUTDOWN);
        }

        /// <summary>
        /// 重启
        /// </summary>
        public static void Reboot()
        {
            DoExitWin(EWX_REBOOT);
        }

        /// <summary>
        /// 添加开机启动
        /// </summary>
        /// <param name="AppPath">应用路径</param>
        /// <returns>是否成功</returns>
        public static bool SystemRunAdd(string AppPath)
        {
            return RegCompStartRun(true, AppPath);
        }
        /// <summary>
        /// 取消开机启动
        /// </summary>
        /// <param name="AppPath">应用路径</param>
        /// <returns>是否成功</returns>
        public static bool SystemRunDelete(string AppPath)
        {
            return RegCompStartRun(false, AppPath);
        }

        /// <summary>
        /// 检查当前程序是否在启动项中
        /// </summary>
        /// <returns></returns>
        public static bool RegCompStartCheck(string key)
        {
            bool result = false; ;
            try
            {
                Microsoft.Win32.RegistryKey Reg;
                Reg = Microsoft.Win32.Registry.LocalMachine.OpenSubKey("Software\\Microsoft\\Windows\\CurrentVersion\\Run", true);
                if (Reg == null) Reg = Microsoft.Win32.Registry.LocalMachine.OpenSubKey("Software\\Microsoft\\Windows\\CurrentVersion\\Run");
                foreach (string s in Reg.GetValueNames()) if (s.Equals(key)) result = true;
                return result;
            }
            catch
            {
                return result;
            }
        } 
        #endregion

        #region 释放内存
        /// <summary>
        /// 设置操作系统实际划分给进程使用的内存容量
        /// </summary>
        /// <param name="hProcess">指定一个进程的句柄</param>
        /// <param name="dwMinimumWorkingSetSize">用于装载最小进程容量的一个变量</param>
        /// <param name="dwMaximumWorkingSetSize">用于装载最大进程容量的一个变量</param>
        /// <returns></returns>
        [DllImport("kernel32.dll")]
        public static extern int SetProcessWorkingSetSize(IntPtr hProcess, int dwMinimumWorkingSetSize, int dwMaximumWorkingSetSize);

        private static readonly Version myVersion = new Version(1, 0);
        /// <summary>
        /// 将当前进程的内存占用尺寸设置到最小
        /// </summary>
        /// <returns>0为成功,-1为失败</returns>
        public static int SetProcessMemoryToMin()
        {
            return SetProcessMemoryToMin(System.Diagnostics.Process.GetCurrentProcess().Handle);
        }
        /// <summary>
        /// 将内存占用尺寸设置到最小
        /// </summary>
        /// <param name="SetProcess">需要设置内存使用范围的程序进程句柄，一般为当前进程，如：System.Diagnostics.Process.GetCurrentProcess().Handle</param>
        /// <returns>0为成功,-1为失败</returns>
        public static int SetProcessMemoryToMin(IntPtr SetProcess)
        {
            GC.Collect();
            if (Environment.OSVersion.Platform == PlatformID.Win32NT)
            {
                return SetProcessWorkingSetSize(SetProcess, -1, -1);
            }
            return -1;
        } 
        #endregion

        [DllImport("kernel32.dll", SetLastError = true)]
        private static extern int WriteProfileString(string lpszSection, string lpszKeyName, string lpszString);

        [DllImport("user32.dll")]
        public static extern int SendMessage(int hWnd,
        uint Msg,
        int wParam,
        int lParam
        );

        #region 安装字体
        [DllImport("gdi32")]
        public static extern int AddFontResource(string lpFileName);
        /// <summary>
        /// 安装字体
        /// </summary>
        /// <param name="fontFileName">字体文件名称</param>
        /// <param name="FontName">字体名称</param>
        /// <returns>是否成功</returns>
        public static bool InstallFont(string fontFileName, string FontName)
        {
            //系统FONT目录
            string ToFontPath = string.Format(@"{0}\fonts\{1}", System.Environment.GetEnvironmentVariable("WINDIR"), fontFileName);
            //需要安装的FONT目录
            string FromFontPath = string.Format(@"{0}\Font\{1}", System.Windows.Forms.Application.StartupPath, fontFileName);
            try
            {
                if (!File.Exists(ToFontPath) && File.Exists(FromFontPath))
                {
                    int _nRet;
                    File.Copy(FromFontPath, ToFontPath);
                    _nRet = AddFontResource(ToFontPath);
                    _nRet = WriteProfileString("fonts", FontName + "(TrueType)", fontFileName);
                }
            }
            catch
            {
                return false;
            }
            return true;
        } 
        #endregion

    }
}