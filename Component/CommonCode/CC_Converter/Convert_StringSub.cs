using System;
using System.Windows.Data;

namespace CC_Converter
{
    /// <summary>
    /// 字符串截取转换器
    /// </summary>
    public class Convert_StringSub : IValueConverter
    {
        /// <summary>
        /// 截取字符长度
        /// </summary>
        public int SubLength { get; set; }

        /// <summary>
        /// 开始位数
        /// </summary>
        public int StartLength { get; set; }

        /// <summary>
        /// 当字符串为空时转换的值
        /// </summary>
        public object NullValue { get; set; }

        /// <summary>
        /// 根据字符串的起始位置和截取长度截取字符串
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
            return value.ToString().Substring(StartLength, SubLength);
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