using System;
using System.Windows.Data;

namespace CC_Converter
{
    /// <summary>
    /// double类型的数字向Log10类型数字的转换
    /// </summary>
    public class Convert_DoubleToLog10 : IValueConverter
    {
        /// <summary>
        /// 类型转换失败时转换后的值
        /// </summary>
        public object DefaultValue { get; set; }

        /// <summary>
        /// 当Value值为Null时转换的值
        /// </summary>
        public object NullValue { get; set; }

        /// <summary>
        /// double类型的数字向Log10类型数字的转换
        /// </summary>
        /// <param name="value">绑定源生成的值</param>
        /// <param name="targetType">绑定目标属性的类型</param>
        /// <param name="parameter">要使用的转换器参数</param>
        /// <param name="culture">要用在转换器中的区域性</param>
        /// <returns> 转换后的值。如果该方法返回 null，则使用有效的 null 值。</returns>
        public object Convert(object value, System.Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (value == null) return NullValue;
            double d = 0;
            if (double.TryParse(value.ToString(), out d))
            {
                double val = (double)value;
                return Math.Log10(val);
            }
            else
            {
                return DefaultValue;
            }
        }

        /// <summary>
        /// 将Log10类型的值转换为Double类型的数据
        /// </summary>
        /// <param name="value">绑定源生成的值</param>
        /// <param name="targetType">绑定目标属性的类型</param>
        /// <param name="parameter">要使用的转换器参数</param>
        /// <param name="culture">要用在转换器中的区域性</param>
        /// <returns> 转换后的值。如果该方法返回 null，则使用有效的 null 值。</returns>
        public object ConvertBack(object value, System.Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (value == null) return NullValue;
            double d = 0;
            if (double.TryParse(value.ToString(), out d))
            {
                double val = (double)value;
                return Math.Pow(10, val);
            }
            else
            {
                return DefaultValue;
            }
        }
    }
}