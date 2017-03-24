using System;
using System.Globalization;
using System.Windows.Data;

namespace CC_Converter
{
    /// <summary>
    /// 判断两个对象是否相等
    /// </summary>
    public class Convert_EqualityToBoolean : IValueConverter
    {
        /// <summary>
        /// 当Value值为False时转换的值
        /// </summary>
        public object FalseValue { get; set; }

        /// <summary>
        /// 当Value值转换失败时转换的值
        /// </summary>
        public object DefaultValue { get; set; }

        /// <summary>
        /// 当Value值为Null时转换的值
        /// </summary>
        public object NullValue { get; set; }

        /// <summary>
        /// 判断两个对象是否相等
        /// </summary>
        /// <param name="value">绑定源生成的值</param>
        /// <param name="targetType">绑定目标属性的类型</param>
        /// <param name="parameter">要使用的转换器参数</param>
        /// <param name="culture">要用在转换器中的区域性</param>
        /// <returns> 转换后的值。如果该方法返回 null，则使用有效的 null 值。</returns>
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null || parameter == null) return NullValue;
            return object.Equals(value, parameter);
        }

        /// <summary>
        /// 根据Value值转换后的bool值返回指定的值
        /// </summary>
        /// <param name="value">绑定源生成的值</param>
        /// <param name="targetType">绑定目标属性的类型</param>
        /// <param name="parameter">要使用的转换器参数</param>
        /// <param name="culture">要用在转换器中的区域性</param>
        /// <returns> 转换后的值。如果该方法返回 null，则使用有效的 null 值。</returns>
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null) return NullValue;
            if (value.ToString() == "0" || value.ToString() == "1" || value.ToString().ToLower() == "true" || value.ToString().ToLower() == "false")
            {
                if (value.ToString() == "0" || value.ToString() == "1")
                    value = (value.ToString() == "0" ? "false" : "true");
                if ((bool)value)
                    return parameter;
                else
                    return FalseValue;
            }
            else
            {
                return DefaultValue;
            }
        }
    }
}