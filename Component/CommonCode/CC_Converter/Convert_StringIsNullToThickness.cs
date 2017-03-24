using System;
using System.Windows;
using System.Windows.Data;

namespace CC_Converter
{
    /// <summary>
    /// 控件周围框架宽度的转换
    /// </summary>
    public class Convert_StringIsNullToThickness : IValueConverter
    {
        /// <summary>
        /// 控件左边框的宽度
        /// </summary>
        public double ThicknessLeft { get; set; }

        /// <summary>
        /// 控件上边框的宽度
        /// </summary>
        public double ThicknessTop { get; set; }

        /// <summary>
        /// 控件右边框的宽度
        /// </summary>
        public double ThicknessRight { get; set; }

        /// <summary>
        /// 控件下边框的宽度
        /// </summary>
        public double ThicknessBottom { get; set; }

        /// <summary>
        /// 当Value值为Null时转换的值
        /// </summary>
        public object NullValue { get; set; }

        /// <summary>
        /// 创建一个边框对象
        /// </summary>
        private Thickness th = new Thickness();

        /// <summary>
        /// 根据Value值转换成控件的边框宽度
        /// </summary>
        /// <param name="value">绑定源生成的值</param>
        /// <param name="targetType">绑定目标属性的类型</param>
        /// <param name="parameter">要使用的转换器参数</param>
        /// <param name="culture">要用在转换器中的区域性</param>
        /// <returns> 转换后的值。如果该方法返回 null，则使用有效的 null 值。</returns>
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (value != null)
            {
                if (string.IsNullOrEmpty(value.ToString()) != null)
                    return new Thickness(ThicknessLeft, ThicknessTop, ThicknessRight, ThicknessBottom);
                else
                    return NullValue;
            }
            else
            {
                return th;
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
        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}