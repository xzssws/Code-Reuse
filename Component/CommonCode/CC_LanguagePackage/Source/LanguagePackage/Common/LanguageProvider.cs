#region 名称空间定义

using LanguagePackage;
using LanguagePackage.Dynamic;
using System;
using System.Collections.Generic;
using System.IO;
using System.Windows;
using System.Windows.Markup;

#endregion 名称空间定义

/// <summary>
/// 统一语言配置帮助 [静态类]
/// </summary>
public static class LanguageProvider
{
    #region 字段定义
    private static Dictionary<string, string> _others;
    /// <summary>
    /// 语言标记
    /// </summary>
    public const string LFLAG = "@L_";

    #endregion 字段定义

    #region 属性定义

    /// <summary>
    /// 获取或设置初始化完成
    /// </summary>
    public static bool Inited { get; set; }

    /// <summary>
    /// 获取或设置当前系统语言是否动态生成
    /// </summary>
    private static bool IsDynamic { get; set; }

    /// <summary>
    /// 获取或设置语言1
    /// </summary>
    private static ResourceDictionary Lang1 { get; set; }

    /// <summary>
    /// 获取或设置语言2
    /// </summary>
    private static ResourceDictionary Lang2 { get; set; }

    /// <summary>
    /// 获取或设置 附加字典
    /// </summary>
    /// <remarks>
    /// 用于在程序运行时动态定义语言对应项
    /// </remarks>
    public static Dictionary<string, string> Others
    {
        get
        {
            if (_others == null)
            {
                _others = new Flyweight<string>();
            }
            return _others;
        }
    }

    #endregion 属性定义

    #region 方法定义

    /// <summary>
    /// 语言类
    /// </summary>
    private static bool BuildResourceDictionary(out ResourceDictionary langdic1, out  ResourceDictionary langdic2)
    {
        langdic1 = null;
        langdic2 = null;
        try
        {
            string LangPath = Environment.CurrentDirectory + "\\Lang.dic";
            if (File.Exists(LangPath))
            {
                //读取
                var data = File.ReadAllLines(LangPath);
                //创建生成器
                ILanguageBuilder
                    B1 = LanguageBuilderFactory.This.CreateLanguageBuilder(BuilderType.L1),
                    B2 = LanguageBuilderFactory.This.CreateLanguageBuilder(BuilderType.L2);
                //将读取的文件数据赋值给生成器
                B1.Data = data;
                B2.Data = data;
                //生成并获取脚本
                var lang1 = B1.Build();
                var lang2 = B2.Build();
                //生成资源字典（系统可用的语言包）
                langdic1 = XamlReader.Parse(lang1) as ResourceDictionary;
                langdic2 = XamlReader.Parse(lang2) as ResourceDictionary;
                return true;
            }
            else
            {
                return false;
            }
        }
        catch (Exception ex)
        {
            //TODO 写入日志
            return false;
        }
    }

    /// <summary>
    /// 初始化语言包
    /// </summary>
    /// <remarks>
    /// 当输出目录有Lang.dic 文件时根据Lang.dic内容动态生成xaml资源字典读取，如果没有则默认读取程序集内的静态资源字典
    /// </remarks>
    public static void Init()
    {
        ResourceDictionary l1, l2;
        IsDynamic = BuildResourceDictionary(out l1, out l2);
        if (IsDynamic)
        {
            Lang1 = l1; Lang2 = l2;
        }
        else
        {
            Lang1 = new ResourceDictionary() { Source = new Uri("pack://application:,,,/LanguagePackage;component/Static/zh-cn.xaml", UriKind.RelativeOrAbsolute) };
            Lang2 = new ResourceDictionary() { Source = new Uri("pack://application:,,,/LanguagePackage;component/Static/en-us.xaml", UriKind.RelativeOrAbsolute) };
        }
        Inited = true;
    }

    /// <summary>
    /// 返回当前系统设置的语言单词 [扩展方法]
    /// </summary>
    /// <param name="Key">代表语言关键字</param>
    /// <returns>相应的语言</returns>
    public static string l(this string Key)
    {
        string okey = Key;
        if (string.IsNullOrEmpty(Key)) return "";
        string otherKey = string.Empty;
        if (Others.TryGetValue(Key, out otherKey)) return l(otherKey);
        if (!Key.Contains(LFLAG)) Key = LFLAG + Key;
        object LanguageWord = Application.Current.TryFindResource(Key);
        return LanguageWord == null ? okey : LanguageWord.ToString().Replace(LFLAG.TrimEnd('_'), "");
    }

    /// <summary>
    /// 设置系统语言
    /// </summary>
    /// <param name="language">中文：zh-cn；英文：en-us；默认：英文</param>
    /// <param name="IsAPS">true：APS；false：NAPS</param>
    /// <returns>设置语言是否成功</returns>
    public static bool SetLanguage(string language)
    {
        if (!Inited) Init();
        try
        {
            switch (language)
            {
                case "zh-cn":
                    Application.Current.Resources.MergedDictionaries[0] = Lang1;
                    break;
                case "en-us":
                    Application.Current.Resources.MergedDictionaries[0] = Lang2;
                    break;
                default:
                    Application.Current.Resources.MergedDictionaries[0] = Lang1;
                    break;
            }
            return true;
        }
        catch
        {
            return false;
        }
    }

    #endregion 方法定义
}
