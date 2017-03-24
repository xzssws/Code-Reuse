using System.Windows;
using System.Windows.Controls;

namespace Module.VisualCore
{
    /// <summary>
    /// 调整旋转默认外观
    /// </summary>
    public class ResizeRotateChrome : Control
    {
        /// <summary>
        /// 初始化 <see cref="ResizeRotateChrome"/>类的新实例。
        /// </summary>
        static ResizeRotateChrome()
        {
            FrameworkElement.DefaultStyleKeyProperty.OverrideMetadata(typeof(ResizeRotateChrome), new FrameworkPropertyMetadata(typeof(ResizeRotateChrome)));
        }
    }
}