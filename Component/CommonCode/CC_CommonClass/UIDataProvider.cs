using System.Windows;

namespace CC_CommonClass
{
    /// <summary>
    /// 附加数据类 用于给一些UI控件附加其他属性用于保存重要数据
    /// </summary>
    public class ObjectData : DependencyObject
    {
        /// <summary>
        /// 获取数据值
        /// </summary>
        /// <param name="obj">依赖对象</param>
        /// <returns></returns>
        public static string GetDataValue(DependencyObject obj)
        {
            return (string)obj.GetValue(DataValueProperty);
        }

        /// <summary>
        /// 设置数据值
        /// </summary>
        /// <param name="obj">依赖对象</param>
        /// <param name="value">值</param>
        public static void SetDataValue(DependencyObject obj, string value)
        {
            obj.SetValue(DataValueProperty, value);
        }

        /// <summary>
        /// 数据值依赖属性
        /// </summary>
        public static readonly DependencyProperty DataValueProperty =
            DependencyProperty.RegisterAttached("DataValue", typeof(string), typeof(ObjectData), new PropertyMetadata(""));
    }
}