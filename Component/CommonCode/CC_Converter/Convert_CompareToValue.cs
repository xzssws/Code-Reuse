using System;
using System.Globalization;
using System.Windows.Data;

namespace CC_Converter
{
    /// <summary>
    /// 异常图标是否显示[WPF转换器]
    /// </summary>
    public class Convert_CompareToValue : IValueConverter
    {
        /// <summary>
        /// 用作比较的值
        /// </summary>
        public object CompareValue { get; set; }

        /// <summary>
        /// 当Value值不等于比较值时转换的值
        /// </summary>
        public object FalseValue { get; set; }

        /// <summary>
        /// 当Value值等于比较值时转换的值
        /// </summary>
        public object TrueValue { get; set; }

        /// <summary>
        /// 判断Value值与比较值是否相等，然后返回特定的值
        /// </summary>
        /// <param name="value">绑定源生成的值</param>
        /// <param name="targetType">绑定目标属性的类型</param>
        /// <param name="parameter">要使用的转换器参数</param>
        /// <param name="culture">要用在转换器中的区域性</param>
        /// <returns> 转换后的值。如果该方法返回 null，则使用有效的 null 值。</returns>
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null) return null;
            if (value.ToString() != CompareValue.ToString())
            {
                return FalseValue;
            }
            else
            {
                return TrueValue;
            }
        }

        /// <summary>
        /// 逆向转换
        /// </summary>
        /// <param name="value">绑定源生成的值</param>
        /// <param name="targetType">绑定目标属性的类型</param>
        /// <param name="parameter">要使用的转换器参数</param>
        /// <param name="culture">要用在转换器中的区域性</param>
        /// <returns> 转换后的值。如果该方法返回 null，则使用有效的 null 值。</returns>
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}