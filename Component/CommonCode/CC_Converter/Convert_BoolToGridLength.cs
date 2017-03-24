using System;
using System.Windows;
using System.Windows.Data;

namespace CC_Converter
{
    /// <summary>
    /// 布尔值转换Grid宽度或高度[WPF转换器]
    /// </summary>
    public class Convert_BoolToGridLength : IValueConverter
    {
        /// <summary>
        /// 是否有星标标记
        /// </summary>
        private bool _isStar = false;

        /// <summary>
        /// 是否有星标标记 (按比例调整大小)
        /// </summary>
        /// <value>
        ///   是/否
        /// </value>
        public bool IsStar
        {
            get { return _isStar; }
            set { _isStar = value; }
        }

        /// <summary>
        /// 将Bool值转成Grid的宽度或高度
        /// </summary>
        /// <param name="value">绑定源生成的值</param>
        /// <param name="targetType">绑定目标属性的类型</param>
        /// <param name="parameter">要使用的转换器参数</param>
        /// <param name="culture">要用在转换器中的区域性</param>
        /// <returns> 转换后的值。如果该方法返回 null，则使用有效的 null 值。</returns>
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (System.Convert.ToBoolean(value))
            {
                return new GridLength(System.Convert.ToDouble(parameter), IsStar ? GridUnitType.Star : GridUnitType.Pixel);
            }
            else
            {
                //隐藏
                return new GridLength(0, GridUnitType.Pixel);
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