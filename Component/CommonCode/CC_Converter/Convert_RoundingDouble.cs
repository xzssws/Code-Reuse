using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Data;

namespace CC_Converter
{
    /// <summary>
    /// 四舍五入 双精度小数
    /// </summary>
    public class Convert_RoundingDouble : IValueConverter
    {
        /// <summary>
        /// <para> 方法描述：正向转换 </para>
        /// <para> 方法说明：方法说明内容 </para>
        /// <para> 最后编辑人：ss </para> 
        /// <para> 最后编辑时间：2015/5/18 15:25:56 </para>
        /// <para> 编辑原因：编辑原因</para>
        /// </summary>
        /// <param name="value">绑定源生成的值</param>
        /// <param name="targetType">绑定目标属性的类型</param>
        /// <param name="parameter">要使用的转换器参数</param>
        /// <param name="culture">要用在转换器中的区域性</param>
        /// <returns> 转换后的值。如果该方法返回 null，则使用有效的 null 值。</returns>
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (value == null || value.ToString() == string.Empty)
            {
                return System.Windows.DependencyProperty.UnsetValue;
            }
            double dvalue = (double)value;
            return Math.Round(dvalue);
        }

        /// <summary>
        /// <para> 方法描述：逆向转换 </para>
        /// <para> 方法说明：方法说明内容 </para>
        /// <para> 最后编辑人：ss </para> 
        /// <para> 最后编辑时间：2015/5/18 15:25:56 </para>
        /// <para> 编辑原因：编辑原因</para>
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
