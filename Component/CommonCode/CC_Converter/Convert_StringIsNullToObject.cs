using System;
using System.Windows.Data;

namespace CC_Converter
{
    /// <summary>
    /// 判断字符串是否为空，转换成特定值
    /// </summary>
    public class Convert_StringIsNullToObject : IValueConverter
    {
        /// <summary>
        /// 当字符串为Null时转换的值
        /// </summary>
        public object NullValue { get; set; }

        /// <summary>
        /// 当字符串不为Null时转换的值
        /// </summary>
        public object NotNullValue { get; set; }

        /// <summary>
        /// 判断Value值与比较值是否相等，然后返回特定的值
        /// </summary>
        /// <param name="value">绑定源生成的值</param>
        /// <param name="targetType">绑定目标属性的类型</param>
        /// <param name="parameter">要使用的转换器参数</param>
        /// <param name="culture">要用在转换器中的区域性</param>
        /// <returns> 转换后的值。如果该方法返回 null，则使用有效的 null 值。</returns>
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (value == null) return NullValue;
            if (string.IsNullOrEmpty(value.ToString())) return NullValue;
            if (value.ToString().Length < 1) return NullValue;
            return NotNullValue;
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