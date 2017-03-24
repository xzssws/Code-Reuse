using System;
using System.Windows.Data;
using System.Windows.Media;

namespace CC_Converter
{
    /// <summary>
    /// 系统字体转换器 [WPF转换器]
    /// </summary>
    public class Convert_StringToFont : IValueConverter
    {
        /// <summary>
        /// 字符串类型的系统字体名称转换为字体类型
        /// </summary>
        /// <param name="value">绑定源生成的值</param>
        /// <param name="targetType">绑定目标属性的类型</param>
        /// <param name="parameter">要使用的转换器参数</param>
        /// <param name="culture">要用在转换器中的区域性</param>
        /// <returns> 转换后的值。如果该方法返回 null，则使用有效的 null 值。</returns>
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (value == null) return null;
            System.Windows.Media.FontFamilyConverter d = new System.Windows.Media.FontFamilyConverter();
            return d.ConvertFromString(value.ToString());
        }

        /// <summary>
        /// 将字体类型转换为字符串类型的字体名称
        /// </summary>
        /// <param name="value">绑定源生成的值</param>
        /// <param name="targetType">绑定目标属性的类型</param>
        /// <param name="parameter">要使用的转换器参数</param>
        /// <param name="culture">要用在转换器中的区域性</param>
        /// <returns> 转换后的值。如果该方法返回 null，则使用有效的 null 值。</returns>
        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (value == null) return null;
            return (value as FontFamily).Source;
        }
    }
}