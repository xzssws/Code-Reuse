using System;
using System.Windows;
using System.Windows.Data;

namespace CC_Converter
{
    /// <summary>
    /// 字符串为空转换[WPF转换器]
    /// </summary>
    public class Convert_StringToNull : IValueConverter
    {
        /// <summary>
        /// 特定字符串值
        /// </summary>
        public string Value { get; set; }

        /// <summary>
        /// value为空，转换特定值，不为空，返回原值
        /// </summary>
        /// <param name="value">绑定源生成的值</param>
        /// <param name="targetType">绑定目标属性的类型</param>
        /// <param name="parameter">要使用的转换器参数</param>
        /// <param name="culture">要用在转换器中的区域性</param>
        /// <returns> 转换后的值。如果该方法返回 null，则使用有效的 null 值。</returns>
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (value == null) return DependencyProperty.UnsetValue;
            if (value.ToString() == Value) return DependencyProperty.UnsetValue;
            else return value;
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