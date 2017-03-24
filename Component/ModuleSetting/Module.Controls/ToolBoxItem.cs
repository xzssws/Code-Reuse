using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Module.Controls
{
    /// <summary>
    /// 按照步骤 1a 或 1b 操作，然后执行步骤 2 以在 XAML 文件中使用此自定义控件。
    ///
    /// 步骤 1a) 在当前项目中存在的 XAML 文件中使用该自定义控件。
    /// 将此 XmlNamespace 特性添加到要使用该特性的标记文件的根 
    /// 元素中: 
    ///
    ///     xmlns:MyNamespace="clr-namespace:Module.Controls"
    ///
    ///
    /// 步骤 1b) 在其他项目中存在的 XAML 文件中使用该自定义控件。
    /// 将此 XmlNamespace 特性添加到要使用该特性的标记文件的根 
    /// 元素中: 
    ///
    ///     xmlns:MyNamespace="clr-namespace:Module.Controls;assembly=Module.Controls"
    ///
    /// 您还需要添加一个从 XAML 文件所在的项目到此项目的项目引用，
    /// 并重新生成以避免编译错误: 
    ///
    ///     在解决方案资源管理器中右击目标项目，然后依次单击
    ///     “添加引用”->“项目”->[选择此项目]
    ///
    ///
    /// 步骤 2)
    /// 继续操作并在 XAML 文件中使用控件。
    ///
    ///     <MyNamespace:CustomControl1/>
    ///
    /// </summary>
    public class ToolBoxItem : Control
    {
        static ToolBoxItem()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(ToolBoxItem), new FrameworkPropertyMetadata(typeof(ToolBoxItem)));
        }

        /// <summary>
        /// 获取或设置显示名称
        /// </summary>
        /// <value>
        /// 显示名称
        /// </value>
        public string DisplayName
        {
            get { return (string)GetValue(DisplayNameProperty); }
            set { SetValue(DisplayNameProperty, value); }
        }

        /// <summary>
        /// 显示名称依赖属性
        /// </summary>
        public static readonly DependencyProperty DisplayNameProperty =
            DependencyProperty.Register("DisplayName", typeof(string), typeof(ToolBoxItem), new PropertyMetadata("工具项"));

        /// <summary>
        /// 获取或设置图标
        /// </summary>
        /// <value>
        /// 图标路径
        /// </value>
        public ImageSource Image
        {
            get { return (ImageSource)GetValue(ImageProperty); }
            set { SetValue(ImageProperty, value); }
        }

        /// <summary>
        /// 图标路径依赖属性
        /// </summary>
        public static readonly DependencyProperty ImageProperty =
            DependencyProperty.Register("Image", typeof(ImageSource), typeof(ToolBoxItem), new PropertyMetadata(null));

        /// <summary>
        /// 获取或设置控件类型
        /// </summary>
        /// <value>
        /// 控件枚举
        /// </value>
        public ControlEnum ControlType
        {
            get { return (ControlEnum)GetValue(ControlTypeProperty); }
            set { SetValue(ControlTypeProperty, value); }
        }

        /// <summary>
        /// 控件枚举依赖属性
        /// </summary>
        public static readonly DependencyProperty ControlTypeProperty =
            DependencyProperty.Register("ControlType", typeof(ControlEnum), typeof(ToolBoxItem), new PropertyMetadata(ControlEnum.Button));

        protected override void OnPreviewMouseMove(MouseEventArgs e)
        {
            base.OnPreviewMouseMove(e);
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                //创建数据对象  将模块对象添加到数据对象内
                DataObject data = new DataObject("ToolBoxItem", this.ControlType);
                //启动拖放操作
                DragDrop.DoDragDrop(this, data, DragDropEffects.Copy | DragDropEffects.Move);
            }
        }
    }
}
