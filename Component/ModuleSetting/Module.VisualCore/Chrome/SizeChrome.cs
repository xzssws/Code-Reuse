using System.Windows;
using System.Windows.Controls;

namespace Module.VisualCore
{
    /// <summary>
    /// 设置大小外观
    /// </summary>
    public class SizeChrome : Control
    {
        /// <summary>
        /// 初始化 <see cref="SizeChrome"/>类的新实例。
        /// </summary>
        static SizeChrome()
        {
            FrameworkElement.DefaultStyleKeyProperty.OverrideMetadata(typeof(SizeChrome), new FrameworkPropertyMetadata(typeof(SizeChrome)));
        }
    }
}