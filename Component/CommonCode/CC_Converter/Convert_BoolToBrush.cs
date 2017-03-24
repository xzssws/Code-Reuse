using System;
using System.Windows.Data;

namespace CC_Converter
{
    /// <summary>
    /// 布尔值颜色转换器
    /// </summary>
    public class Convert_BoolToBrush : IValueConverter
    {
        /// <summary>
        /// 当值为True时的颜色
        /// </summary>
        private System.Windows.Media.Brush _trueBrush;

        /// <summary>
        /// 当值为True时的颜色
        /// </summary>
        /// <value>
        /// TrueBrush
        /// </value>
        public System.Windows.Media.Brush TrueBrush
        {
            get { return _trueBrush; }
            set { _trueBrush = value; }
        }

        /// <summary>
        /// 当值为False时的颜色
        /// </summary>
        private System.Windows.Media.Brush _falseBrush;

        /// <summary>
        /// 当值为False时的颜色
        /// </summary>
        /// <value>
        /// FalseBrush.
        /// </value>
        public System.Windows.Media.Brush FalseBrush
        {
            get { return _falseBrush; }
            set { _falseBrush = value; }
        }

        /// <summary>
        /// 当值为Null时的颜色
        /// </summary>
        private System.Windows.Media.Brush _nullBrush;

        /// <summary>
        /// 当值为Null时的颜色
        /// </summary>
        /// <value>
        /// NullBrush.
        /// </value>
        public System.Windows.Media.Brush NullBrush
        {
            get { return _nullBrush; }
            set { _nullBrush = value; }
        }

        /// <summary>
        /// 默认的颜色
        /// </summary>
        private System.Windows.Media.Brush _defaultBrush;

        /// <summary>
        /// 默认的颜色
        /// </summary>
        /// <value>
        /// DefaulBrush.
        /// </value>
        public System.Windows.Media.Brush DefaultBrush
        {
            get { return _defaultBrush; }
            set { _defaultBrush = value; }
        }

        /// <summary>
        /// 将布尔值转换成相应的颜色
        /// </summary>
        /// <param name="value">绑定源生成的值</param>
        /// <param name="targetType">绑定目标属性的类型</param>
        /// <param name="parameter">要使用的转换器参数</param>
        /// <param name="culture">要用在转换器中的区域性</param>
        /// <returns> 转换后的值。如果该方法返回 null，则使用有效的 null 值。</returns>
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (value == null) return NullBrush;
            if (value.ToString() == "0" || value.ToString() == "1" || value.ToString().ToLower() == "true" || value.ToString().ToLower() == "false")
            {
                if (value.ToString() == "0" || value.ToString() == "1")
                    value = (value.ToString() == "0" ? "false" : "true");
                if ((bool)value)
                {
                    return TrueBrush;
                }
                else
                {
                    return FalseBrush;
                }
            }
            else
            {
                return DefaultBrush;
            }
        }

        /// <summary>
        /// 将颜色转换为布尔值
        /// </summary>
        /// <param name="value">绑定源生成的值</param>
        /// <param name="targetType">绑定目标属性的类型</param>
        /// <param name="parameter">要使用的转换器参数</param>
        /// <param name="culture">要用在转换器中的区域性</param>
        /// <returns> 转换后的值。如果该方法返回 null，则使用有效的 null 值。</returns>
        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (value == null) return NullBrush;
            System.Windows.Media.Brush brush = value as System.Windows.Media.Brush;
            if (brush != null && brush == TrueBrush)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}