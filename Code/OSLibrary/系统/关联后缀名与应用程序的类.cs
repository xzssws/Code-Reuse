using Microsoft.Win32;
using System;

/// <summary>
/// 关联后缀名与应用程序的类
/// </summary>
public class AppRelevancy
{
    private static RegistryKey SubKey;//后缀名项
    private static RegistryKey SubIcon;//后缀名图标项
    private static RegistryKey SubCommand;//后缀名关联程序项
    private static Exception Error;//错误信息

    /// <summary>
    /// 返回错误信息
    /// </summary>
    /// <returns>Exception错误信息</returns>
    public static Exception GetError()
    {
        return Error;
    }

    /// <summary>
    /// 设置程序关联扩展名以及程序路径和图标路径
    /// </summary>
    /// <param name="postfix">扩展名称</param>
    /// <param name="App">关联应用程序地址</param>
    /// <param name="icon">关联图标地址</param>
    /// <returns>返回执行结果</returns>
    public static bool SetRelevance(string postfix, string App, string icon)
    {
        try
        {
            if (Registry.ClassesRoot.OpenSubKey(postfix) == null)//如果扩展名中没有此项
            {
                SubKey = Registry.ClassesRoot.CreateSubKey(postfix);//创建键值
                SubIcon = SubKey.CreateSubKey("DefaultIcon");//创建图表项
                SubCommand = SubKey.CreateSubKey("shell").CreateSubKey("open").CreateSubKey("command");//命令项
            }
            else
            {
                if (SubKey.OpenSubKey("DefaultIcon") == null)//如果扩展名中没有图标路径
                    SubIcon = SubKey.CreateSubKey("DefaultIcon");//创建图表项
                if (SubKey.OpenSubKey("shell") == null)//如果其中没有shell项
                    SubKey.CreateSubKey("shell").CreateSubKey("open").CreateSubKey("command");
                else if (SubKey.OpenSubKey("shell").OpenSubKey("open") == null)//如果shell中没有open项
                    SubKey.OpenSubKey("shell").CreateSubKey("open").CreateSubKey("command");
                else if (SubKey.OpenSubKey("shell").OpenSubKey("open").OpenSubKey("command") == null)//如果open项中没有command项
                    SubKey.OpenSubKey("shell").OpenSubKey("open").CreateSubKey("command");
            }
            SubIcon.SetValue("", icon);//设置图标路径
            SubCommand.SetValue("", App + " %1");//设置程序路径
            return true;
        }
        catch (Exception ex)
        {
            Error = ex;//返回错误信息
            return false;
        }
    }

    /// <summary>
    /// 设置程序关联扩展名以及程序路径
    /// </summary>
    /// <param name="postfix">扩展名称</param>
    /// <param name="App">关联应用程序地址</param>
    /// <returns>返回执行结果</returns>
    public static bool SetRelevance(string postfix, string App)
    {
        try
        {
            if (Registry.ClassesRoot.OpenSubKey(postfix) == null)//如果扩展名中没有此项
            {
                SubKey = Registry.ClassesRoot.CreateSubKey(postfix);//创建键值
                SubCommand = SubKey.CreateSubKey("shell").CreateSubKey("open").CreateSubKey("command");//命令项
            }
            else
            {
                if (SubKey.OpenSubKey("shell") == null)//如果其中没有shell项
                    SubKey.CreateSubKey("shell").CreateSubKey("open").CreateSubKey("command");
                else if (SubKey.OpenSubKey("shell").OpenSubKey("open") == null)//如果shell中没有open项
                    SubKey.OpenSubKey("shell").CreateSubKey("open").CreateSubKey("command");
                else if (SubKey.OpenSubKey("shell").OpenSubKey("open").OpenSubKey("command") == null)//如果open项中没有command项
                    SubKey.OpenSubKey("shell").OpenSubKey("open").CreateSubKey("command");
            }
            SubCommand.SetValue("", App + " %1");//设置程序路径
            return true;
        }
        catch (Exception ex)
        {
            Error = ex;//返回错误信息
            return false;
        }
    }

    /// <summary>
    /// 以删除原项的基础上创建应用程序关联
    /// </summary>
    /// <param name="postfix">扩展名称</param>
    /// <param name="App">关联应用程序地址</param>
    /// <returns>返回执行结果</returns>
    public static bool CreateRelevance(string postfix, string App)
    {
        try
        {
            if (Registry.ClassesRoot.OpenSubKey(postfix) != null)
                Registry.ClassesRoot.DeleteSubKey(postfix);
            SubKey = Registry.ClassesRoot.CreateSubKey(postfix);//创建键值
            SubCommand = SubKey.CreateSubKey("shell").CreateSubKey("open").CreateSubKey("command");//命令项
            SubCommand.SetValue("", App + " %1");//设置程序路径
            SubCommand.Close();
            return true;
        }
        catch (Exception ex)
        {
            Error = ex;//返回错误信息
            return false;
        }
    }

    /// <summary>
    /// 以删除原项的基础上创建应用程序关联以及图标
    /// </summary>
    /// <param name="postfix">扩展名称</param>
    /// <param name="App">关联应用程序地址</param>
    /// <param name="icon">关联图标地址</param>
    /// <returns>返回执行结果</returns>
    public static bool CreateRelevance(string postfix, string App, string icon)
    {
        try
        {
            if (Registry.ClassesRoot.OpenSubKey(postfix) != null)
                Registry.ClassesRoot.DeleteSubKey(postfix);
            SubKey = Registry.ClassesRoot.CreateSubKey(postfix);//创建键值
            SubIcon = SubKey.CreateSubKey("DefaultIcon");//创建图表项
            SubCommand = SubKey.CreateSubKey("shell").CreateSubKey("open").CreateSubKey("command");//创建命令项
            SubCommand.SetValue("", App + " %1");//设置程序路径
            SubIcon.SetValue("", icon);//设置图标路径
            SubCommand.Close();
            return true;
        }
        catch (Exception ex)
        {
            Error = ex;//返回错误信息
            return false;
        }
    }
}