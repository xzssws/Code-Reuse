using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Media;

namespace Module.VisualCore
{
    /// <summary>
    /// 设计装饰器
    /// </summary>
    public class DesignerItemDecorator : Control
    {
        /// <summary>
        /// 装饰器
        /// </summary>
        private Adorner adorner;

        /// <summary>
        /// 初始化 <see cref="DesignerItemDecorator"/>类的新实例。
        /// </summary>
        public DesignerItemDecorator()
        {
            Unloaded += new RoutedEventHandler(this.DesignerItemDecorator_Unloaded);
        }

        /// <summary>
        /// 是否显示装饰器
        /// </summary>
        /// <value>
        /// True:  False:
        /// </value>
        public bool ShowDecorator
        {
            get { return (bool)GetValue(ShowDecoratorProperty); }
            set { SetValue(ShowDecoratorProperty, value); }
        }

        /// <summary>
        /// The show decorator property
        /// </summary>
        public static readonly DependencyProperty ShowDecoratorProperty =
            DependencyProperty.Register("ShowDecorator", typeof(bool), typeof(DesignerItemDecorator),
            new FrameworkPropertyMetadata(false, new PropertyChangedCallback(ShowDecoratorProperty_Changed)));
        /// <summary>
        /// 显示装饰器属性变更事件
        /// </summary>
        /// <param name="d">The d.</param>
        /// <param name="e"><see cref="DependencyPropertyChangedEventArgs"/> 事件参数</param>
        private static void ShowDecoratorProperty_Changed(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            DesignerItemDecorator decorator = (DesignerItemDecorator)d;
            bool showDecorator = (bool)e.NewValue;

            if (showDecorator)
            {
                decorator.ShowAdorner();
            }
            else
            {
                decorator.HideAdorner();
            }
        }

        /// <summary>
        /// 隐藏装饰器
        /// </summary>
        private void HideAdorner()
        {
            if (this.adorner != null)
            {
                this.adorner.Visibility = Visibility.Hidden;
            }
        }

        /// <summary>
        /// 显示装饰器
        /// </summary>
        private void ShowAdorner()
        {
            if (this.adorner == null)
            {
                AdornerLayer adornerLayer = AdornerLayer.GetAdornerLayer(this);

                if (adornerLayer != null)
                {
                    ContentControl designerItem = this.DataContext as ContentControl;
                    Canvas canvas = VisualTreeHelper.GetParent(designerItem) as Canvas;
                    this.adorner = new ResizeRotateAdorner(designerItem);
                    adornerLayer.Add(this.adorner);

                    if (this.ShowDecorator)
                    {
                        this.adorner.Visibility = Visibility.Visible;
                    }
                    else
                    {
                        this.adorner.Visibility = Visibility.Hidden;
                    }
                }
            }
            else
            {
                this.adorner.Visibility = Visibility.Visible;
            }
        }

        /// <summary>
        /// 卸载元素
        /// </summary>
        /// <param name="sender">事件源</param>
        /// <param name="e"><see cref="RoutedEventArgs"/> 事件参数</param>
        private void DesignerItemDecorator_Unloaded(object sender, RoutedEventArgs e)
        {
            if (this.adorner != null)
            {
                AdornerLayer adornerLayer = AdornerLayer.GetAdornerLayer(this);
                if (adornerLayer != null)
                {
                    adornerLayer.Remove(this.adorner);
                }
                this.adorner = null;
            }
        }


    }
}