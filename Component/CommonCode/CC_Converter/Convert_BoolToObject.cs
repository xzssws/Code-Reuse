using System;
using System.Windows.Data;

namespace CC_Converter
{
    /// <summary>
    /// 将布尔值转换为相应的对象[WPF转换器]
    /// </summary>
    public class Convert_BoolToObject : IValueConverter
    {
        /// <summary>
        /// 当值为True的返回值 属性字段
        /// </summary>
        private object _trueValue;

        /// <summary>
        /// 当值为True时的返回值
        /// </summary>
        public object TrueValue
        {
            get { return _trueValue; }
            set { _trueValue = value; }
        }

        /// <summary>
        /// 当值为False时的返回值 属性字段
        /// </summary>
        private object _falseValue;

        /// <summary>
        /// 当值为False时的返回值
        /// </summary>
        public object FalseValue
        {
            get { return _falseValue; }
            set { _falseValue = value; }
        }

        /// <summary>
        /// 将布尔值转换为Uri地址
        /// </summary>
        /// <param name="value">绑定源生成的值</param>
        /// <param name="targetType">绑定目标属性的类型</param>
        /// <param name="parameter">要使用的转换器参数</param>
        /// <param name="culture">要用在转换器中的区域性</param>
        /// <returns> 转换后的值。如果该方法返回 null，则使用有效的 null 值。</returns>
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if ((bool)value)
            {
                return TrueValue;
            }
            else
            {
                return FalseValue;
            }
        }

        /// <summary>
        /// 逆向转换
        /// </summary>
        /// <param name="value">绑定目标生成的值。</param>
        /// <param name="targetType">要转换到的类型。</param>
        /// <param name="parameter">要使用的转换器参数。</param>
        /// <param name="culture">要用在转换器中的区域性。</param>
        /// <returns>
        /// 转换后的值。如果该方法返回 null，则使用有效的 null 值。
        /// </returns>
        /// <exception cref="System.NotImplementedException"></exception>
        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}