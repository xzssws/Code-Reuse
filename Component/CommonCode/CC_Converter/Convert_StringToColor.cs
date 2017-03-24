using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Windows.Data;
using System.Windows.Media;

namespace CC_Converter
{
    /// <summary>
    /// 系统颜色转换器
    /// </summary>
    [ValueConversion(typeof(KeyValuePair<string, Brush>), typeof(String))]
    public class Convert_StringToColor : IValueConverter
    {
        /// <summary>
        /// 字符串类型的颜色值[#000000]转换为颜色类型
        /// </summary>
        /// <param name="value">绑定源生成的值</param>
        /// <param name="targetType">绑定目标属性的类型</param>
        /// <param name="parameter">要使用的转换器参数</param>
        /// <param name="culture">要用在转换器中的区域性</param>
        /// <returns> 转换后的值。如果该方法返回 null，则使用有效的 null 值。</returns>
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (value == null) return null;
            var d = ColorCollection.FirstOrDefault(t => t.Key == value.ToString());
            return d;
        }

        /// <summary>
        /// 颜色字典集合 属性字段
        /// </summary>
        private Dictionary<string, Brush> _colorCollection;

        /// <summary>
        /// 颜色字典集合
        /// </summary>
        public Dictionary<string, Brush> ColorCollection
        {
            get
            {
                if (_colorCollection == null)
                {
                    _colorCollection = new Dictionary<string, Brush>();
                    Type colorsType = typeof(Brushes);
                    PropertyInfo[] pis = colorsType.GetProperties();
                    foreach (PropertyInfo pi in pis)
                        _colorCollection.Add(pi.Name, (Brush)pi.GetValue(null, null));
                }
                return _colorCollection;
            }
            set
            {
                _colorCollection = value;
            }
        }

        /// <summary>
        /// 逆向转换，颜色类型转换为字符串表示的颜色值
        /// </summary>
        /// <param name="value">绑定源生成的值</param>
        /// <param name="targetType">绑定目标属性的类型</param>
        /// <param name="parameter">要使用的转换器参数</param>
        /// <param name="culture">要用在转换器中的区域性</param>
        /// <returns> 转换后的值。如果该方法返回 null，则使用有效的 null 值。</returns>
        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (value == null) return null;
            KeyValuePair<string, Brush> color;
            color = (KeyValuePair<string, Brush>)value;
            return color.Key;
        }
    }
}