using System;
using System.Windows;

/// <summary>
/// 统一语言配置帮助 [静态类]
/// </summary>
public static class LanguageProvider
{
    /// <summary>
    /// 语言标记
    /// </summary>
    public const string LFLAG = "@L_";

    /// <summary>
    /// 返回当前系统设置的语言单词 [扩展方法]
    /// </summary>
    /// <param name="Key">代表语言关键字</param>
    /// <returns>相应的语言</returns>
    public static string l(this string Key)
    {
        if (!Key.Contains(LFLAG)) Key = LFLAG + Key;
        object LanguageWord = Application.Current.TryFindResource(Key);
        return LanguageWord == null ? Key : LanguageWord.ToString().Replace(LFLAG.TrimEnd('_'), "");
    }

    /// <summary>
    /// 设置系统语言
    /// </summary>
    /// <param name="language">中文：zh-cn；英文：en-us；默认：英文</param>
    /// <returns>设置语言是否成功</returns>
    public static bool SetLanguage(string language)
    {
        try
        {
            switch (language)
            {
                case "zh-cn":
                    Application.Current.Resources.MergedDictionaries[0] =
                      new ResourceDictionary()
                      {
                          Source = new Uri("pack://application:,,,/CC_LanguageDictionary;component/zh-cn.xaml", UriKind.RelativeOrAbsolute)
                      };
                    break;

                case "en-us":
                default:
                    Application.Current.Resources.MergedDictionaries[0] =
                      new ResourceDictionary()
                      {
                          Source = new Uri("pack://application:,,,/CC_LanguageDictionary;component/en-us.xaml", UriKind.RelativeOrAbsolute)
                      };
                    break;
            }
            return true;
        }
        catch
        {
            return false;
        }
    }
}