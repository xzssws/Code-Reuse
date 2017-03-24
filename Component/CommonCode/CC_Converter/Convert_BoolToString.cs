using System;
using System.Windows.Data;

namespace CC_Converter
{
    /// <summary>
    /// 布尔值转换为自定义值
    /// </summary>
    public class Convert_BoolToString : IValueConverter
    {
        /// <summary>
        /// 当值为True的时候转换的值
        /// </summary>
        public object TrueValue { get; set; }

        /// <summary>
        /// 当值为False转换的值
        /// </summary>
        public object FalseValue { get; set; }

        /// <summary>
        /// 类型转换失败时转换的值
        /// </summary>
        public object DefaultValue { get; set; }

        /// <summary>
        /// 当Value值为Null时转换的值
        /// </summary>
        public object NullValue { set; get; }

        /// <summary>
        /// 将布尔值转换为设定的值
        /// </summary>
        /// <param name="value">绑定源生成的值</param>
        /// <param name="targetType">绑定目标属性的类型</param>
        /// <param name="parameter">要使用的转换器参数</param>
        /// <param name="culture">要用在转换器中的区域性</param>
        /// <returns> 转换后的值。如果该方法返回 null，则使用有效的 null 值。</returns>
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (value == null) return NullValue;
            if (value.ToString() == "0" || value.ToString() == "1" || value.ToString().ToLower() == "true" || value.ToString().ToLower() == "false")
            {
                if (value.ToString() == "0" || value.ToString() == "1")
                    value = (value.ToString() == "0" ? "false" : "true");
                bool result = System.Convert.ToBoolean(value);
                return result ? TrueValue : FalseValue;
            }
            else
            {
                return DefaultValue;
            }
        }

        /// <summary>
        /// 将设定的值转换为布尔值
        /// </summary>
        /// <param name="value">绑定源生成的值</param>
        /// <param name="targetType">绑定目标属性的类型</param>
        /// <param name="parameter">要使用的转换器参数</param>
        /// <param name="culture">要用在转换器中的区域性</param>
        /// <returns> 转换后的值。如果该方法返回 null，则使用有效的 null 值。</returns>
        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (value == null) return NullValue;
            return value.Equals(TrueValue);
        }
    }
}