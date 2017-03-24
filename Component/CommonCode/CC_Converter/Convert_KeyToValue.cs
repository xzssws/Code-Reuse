using System;
using System.Collections.Generic;
using System.Windows.Data;

namespace CC_Converter
{
    /// <summary>
    /// 枚举值转换成字符串[WPF转换器]
    /// </summary>
    public class Convert_KeyToValue : IValueConverter
    {
        /// <summary>
        /// 表达式 Key=Value;Key=Value
        /// </summary>
        public string Expression { get; set; }


        /// <summary>
        /// 枚举字典 属性字段
        /// </summary>
        private Dictionary<string, string> _enumDict;

        /// <summary>
        /// 枚举字典
        /// </summary>
        public Dictionary<string, string> EnumDict
        {
            get
            {
                if (_enumDict == null)
                {
                    _enumDict = new Dictionary<string, string>();
                    string[] dd = Expression.Trim(';').Split(';');
                    foreach (var item in dd)
                    {
                        string[] r = item.Split('=');
                        _enumDict.Add(r[0], r[1]);
                    }
                }
                return _enumDict;
            }
            set { _enumDict = value; }
        }


        /// <summary>
        /// 转换值。
        /// </summary>
        /// <param name="value">绑定源生成的值。</param>
        /// <param name="targetType">绑定目标属性的类型。</param>
        /// <param name="parameter">要使用的转换器参数。</param>
        /// <param name="culture">要用在转换器中的区域性。</param>
        /// <returns>
        /// 转换后的值。如果该方法返回 null，则使用有效的 null 值。
        /// </returns>
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            string result = string.Empty;
            if (value == null || value.ToString() == string.Empty)
            {
                return null;
            }
            if (EnumDict.TryGetValue(value.ToString(), out result))
            {
                return result;
            }
            return result;
        }


        /// <summary>
        /// 转换值。
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