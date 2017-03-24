using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Markup;
using System.Xaml;

namespace LanguagePackage
{
    /// <summary>
    /// 语言 可扩展标记
    /// </summary>
    public class LanguageExtension : DynamicResourceExtension
    {
        /// <summary>
        /// 获取或设置 原始键
        /// </summary>
        public string OriginalKey { get; set; }
        /// <summary>
        /// 创建<see cref="LanguageExtension"/>的新实例
        /// </summary>
        /// <param name="Key">原始键</param>
        public LanguageExtension(string Key)
        {
            this.OriginalKey = Key;
#if Release
            //设置原始键值
            base.ResourceKey = LanguageProvider.LFLAG+ this.OriginalKey;
#endif 

        }
        /// <summary>
        /// 返回一个应在此扩展应用的属性上设置的对象。 对于 <see cref="T:System.Windows.DynamicResourceExtension" />，这是当前父级链的资源字典中的对象，该对象的键由 <see cref="P:System.Windows.DynamicResourceExtension.ResourceKey" /> 指定。
        /// </summary>
        /// <param name="serviceProvider">可以为标记扩展提供服务的对象。</param>
        /// <returns>要在应用了扩展的属性上设置的对象。 这是一个将在稍后计算的表达式，而不是实际值。</returns>
        public override object ProvideValue(IServiceProvider serviceProvider)
        {
            try
            {
                if (Application.Current.FindResource(ResourceKey) != null)
                {
                    return base.ProvideValue(serviceProvider);
                }
                else
                {
                    //若无则返回原始键值
                    return OriginalKey;
                }
            }
            catch
            {
                
                //发生错误返回原始键值
                return OriginalKey;
            }
        }
    }
}


