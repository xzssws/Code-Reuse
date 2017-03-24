using System;
using System.Windows.Data;

namespace CC_Converter
{
    /// <summary>
    /// 字符串属性截取首字母转换器 一般用于以首字符分组的列表  [WPF转换器]
    /// </summary>
    public class Convert_StringToFirstKey : IValueConverter
    {
        /// <summary>
        /// 截取字符串首字母分组
        /// </summary>
        /// <param name="value">绑定源生成的值</param>
        /// <param name="targetType">绑定目标属性的类型</param>
        /// <param name="parameter">要使用的转换器参数</param>
        /// <param name="culture">要用在转换器中的区域性</param>
        /// <returns> 转换后的值。如果该方法返回 null，则使用有效的 null 值。</returns>
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (value == null) return null;
            if (string.IsNullOrEmpty(value.ToString())) return null;
            if (value.ToString().Length < 1) return null;

            return value.ToString().Substring(0, 1);
        }

        /// <summary>
        /// 逆向转换
        /// </summary>
        /// <param name="value">绑定源生成的值</param>
        /// <param name="targetType">绑定目标属性的类型</param>
        /// <param name="parameter">要使用的转换器参数</param>
        /// <param name="culture">要用在转换器中的区域性</param>
        /// <returns> 转换后的值。如果该方法返回 null，则使用有效的 null 值。</returns>
        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}