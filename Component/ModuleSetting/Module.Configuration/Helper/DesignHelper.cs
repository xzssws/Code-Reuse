using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;

namespace Module.Configuration
{
    public class DesignHelper
    {
        public static T FindAnchestor<T>(DependencyObject current) where T : DependencyObject
        {
            do
            {
                if (current is T)
                {
                    return (T)current;
                }

                current = VisualTreeHelper.GetParent(current);
            }

            while (current != null);

            return null;
        }

    }  /// <summary>
    /// 拖拽行为
    /// </summary>
    public static class DragBehaviour
    {
        #region 依赖属性

        /// <summary>
        /// 是否启用拖拽
        /// </summary>
        public static readonly DependencyProperty IsDragEnabledProperty = DependencyProperty.RegisterAttached("IsDragEnabled", typeof(bool), typeof(DragBehaviour), new UIPropertyMetadata(false, OnIsDragEnabledPropertyChanged));

        /// <summary>
        /// 是否拖拽中
        /// </summary>
        public static readonly DependencyProperty IsDraggingProperty = DependencyProperty.RegisterAttached("IsDragging", typeof(bool), typeof(DragBehaviour), new UIPropertyMetadata(false));

        /// <summary>
        /// X轴 新位置
        /// </summary>
        public static readonly DependencyProperty XProperty = DependencyProperty.RegisterAttached("X", typeof(double), typeof(DragBehaviour), new FrameworkPropertyMetadata(0.0, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault));

        /// <summary>
        /// Y轴 新位置
        /// </summary>
        public static readonly DependencyProperty YProperty = DependencyProperty.RegisterAttached("Y", typeof(double), typeof(DragBehaviour), new FrameworkPropertyMetadata(0.0, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault));

        /// <summary>
        /// X轴 原始位置
        /// </summary>
        private static readonly DependencyPropertyKey OriginalXPropertyKey = DependencyProperty.RegisterAttachedReadOnly("OriginalX", typeof(double), typeof(DragBehaviour), new UIPropertyMetadata(0.0));

        /// <summary>
        /// Y轴 原始位置
        /// </summary>
        private static readonly DependencyPropertyKey OriginalYPropertyKey = DependencyProperty.RegisterAttachedReadOnly("OriginalY", typeof(double), typeof(DragBehaviour), new UIPropertyMetadata(0.0));

        #endregion 依赖属性

        #region 依赖属性访问方法

        /// <summary>
        /// 获得是否启用拖拽属性
        /// </summary>
        /// <param name="obj">拖拽对象</param>
        /// <returns>true 开启 false 未开启</returns>
        public static bool GetIsDragEnabled(DependencyObject obj)
        {
            return (bool)obj.GetValue(IsDragEnabledProperty);
        }

        /// <summary>
        /// 设置是否启用拖拽属性
        /// </summary>
        /// <param name="obj">拖拽对象</param>
        /// <param name="value">true 开启 false 禁用</param>
        public static void SetIsDragEnabled(DependencyObject obj, bool value)
        {
            obj.SetValue(IsDragEnabledProperty, value);
        }

        /// <summary>
        /// 获取控件是否处于拖拽中
        /// </summary>
        /// <param name="obj">拖拽对象</param>
        /// <returns>true 是 false 否</returns>
        public static bool GetIsDragging(DependencyObject obj)
        {
            return (bool)obj.GetValue(IsDraggingProperty);
        }

        /// <summary>
        /// 设置是否处于拖拽中
        /// </summary>
        /// <param name="obj">拖拽对象</param>
        /// <param name="value">true 是 false 否</param>
        public static void SetIsDragging(DependencyObject obj, bool value)
        {
            obj.SetValue(IsDraggingProperty, value);
        }

        /// <summary>
        /// 获取X轴值
        /// </summary>
        /// <param name="obj">拖拽对象</param>
        /// <returns>左边距</returns>
        public static double GetX(DependencyObject obj)
        {
            return (double)obj.GetValue(XProperty);
        }

        /// <summary>
        /// 设置X轴值
        /// </summary>
        /// <param name="obj">拖拽对象</param>
        /// <param name="value">值</param>
        public static void SetX(DependencyObject obj, double value)
        {
            obj.SetValue(XProperty, value);
        }

        /// <summary>
        /// 获取Y轴值
        /// </summary>
        /// <param name="obj">拖拽对象</param>
        /// <returns></returns>
        public static double GetY(DependencyObject obj)
        {
            return (double)obj.GetValue(YProperty);
        }

        /// <summary>
        ///  设置Y轴值
        /// </summary>
        /// <param name="obj">拖拽对象</param>
        /// <param name="value">值</param>
        public static void SetY(DependencyObject obj, double value)
        {
            obj.SetValue(YProperty, value);
        }

        /// <summary>
        /// 获取起始X轴值
        /// </summary>
        /// <param name="obj">拖拽对象</param>
        /// <returns></returns>
        private static double GetOriginalX(DependencyObject obj)
        {
            return (double)obj.GetValue(OriginalXPropertyKey.DependencyProperty);
        }

        /// <summary>
        ///  设置起始X轴值
        /// </summary>
        /// <param name="obj">拖拽对象</param>
        /// <param name="value">值</param>
        private static void SetOriginalX(DependencyObject obj, double value)
        {
            obj.SetValue(OriginalXPropertyKey, value);
        }

        /// <summary>
        ///  获取起始Y轴值
        /// </summary>
        /// <param name="obj">拖拽对象</param>
        /// <returns></returns>
        private static double GetOriginalY(DependencyObject obj)
        {
            return (double)obj.GetValue(OriginalYPropertyKey.DependencyProperty);
        }

        /// <summary>
        ///  设置起始Y轴值
        /// </summary>
        /// <param name="obj">拖拽对象</param>
        /// <param name="value">值</param>
        private static void SetOriginalY(DependencyObject obj, double value)
        {
            obj.SetValue(OriginalYPropertyKey, value);
        }

        #endregion 依赖属性访问方法

        #region 事件处理器

        /// <summary>
        /// 更改是否启用拖拽属性触发事件
        /// </summary>
        private static void OnIsDragEnabledPropertyChanged(DependencyObject obj, DependencyPropertyChangedEventArgs e)
        {
            //获取对象 验证.
            var element = obj as FrameworkElement;
            FrameworkContentElement contentElement = null;
            if (element == null)
            {
                contentElement = obj as FrameworkContentElement;
                if (contentElement == null)
                    return;
            }
            //如果非bool则直接退出
            if (e.NewValue is bool == false)
                return;

            //注册
            if ((bool)e.NewValue)
            {
                if (element != null)
                {
                    element.MouseLeftButtonDown += MouseDownHandler;
                    element.MouseLeftButtonUp += MouseUpHandler;
                }
                else
                {
                    contentElement.MouseLeftButtonDown += MouseDownHandler;
                    contentElement.MouseLeftButtonUp += MouseUpHandler;
                }
                Debug.WriteLine("拖拽行为注册成功", "NAPS.Control");
            }
            //注销
            else
            {
                if (element != null)
                {
                    element.MouseLeftButtonDown -= MouseDownHandler;
                    element.MouseLeftButtonUp -= MouseUpHandler;
                }
                else
                {
                    contentElement.MouseLeftButtonDown -= MouseDownHandler;
                    contentElement.MouseLeftButtonUp -= MouseUpHandler;
                }
                Debug.WriteLine("拖拽行为注销成功", "NAPS.Control");
            }
        }

        /// <summary>
        /// 鼠标按下处理器
        /// </summary>
        private static void MouseDownHandler(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            if (e.ClickCount == 1)
            {
                var obj = sender as DependencyObject;
                //设置状态:拖拽中
                SetIsDragging(obj, true);
                //获得拖拽对象位置
                Point pos = e.GetPosition(obj as IInputElement);
                //设置拖拽对象起始位置
                SetOriginalX(obj, pos.X);
                SetOriginalY(obj, pos.Y);

                Debug.WriteLine("开始拖拽 对象:" + obj, "NAPS.Control");

                //捕获鼠标到对象并且注册鼠标移动事件
                var element = obj as FrameworkElement;
                if (element != null)
                {
                    element.CaptureMouse();
                    element.MouseMove += MouserMoveHandler;
                }
                else
                {
                    var contentElement = obj as FrameworkContentElement;
                    if (contentElement == null)
                        throw new ArgumentException("控件必须继承自FrameworkElement或FrameworkContentElement!");
                    contentElement.CaptureMouse();
                    contentElement.MouseMove += MouserMoveHandler;
                }
            }
        }

        /// <summary>
        /// 鼠标弹起处理器
        /// </summary>
        private static void MouseUpHandler(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            var obj = (DependencyObject)sender;
            //设置状态:非拖拽中
            SetIsDragging(obj, false);
            //清除起始位置
            obj.ClearValue(OriginalXPropertyKey);
            obj.ClearValue(OriginalYPropertyKey);

            Debug.WriteLine("完成拖拽 对象:" + obj, "NAPS.Control");

            //完成拖拽 注销事件
            var element = sender as FrameworkElement;

            if (element != null)
            {
                element.MouseMove -= MouserMoveHandler;
                element.ReleaseMouseCapture();
            }
            else
            {
                var contentElement = sender as FrameworkContentElement;
                if (contentElement == null)
                    throw new ArgumentException("控件必须继承自FrameworkElement或FrameworkContentElement!");
                contentElement.MouseMove -= MouserMoveHandler;
                contentElement.ReleaseMouseCapture();
            }
            e.Handled = true;
        }

        /// <summary>
        /// 鼠标移动处理器
        /// </summary>
        private static void MouserMoveHandler(object sender, System.Windows.Input.MouseEventArgs e)
        {
            var obj = sender as DependencyObject;
            if (!GetIsDragging(obj))
                return;
            //获得当前拖拽对象位置
            Point pos = e.GetPosition(obj as IInputElement);
            //获得位置偏差
            double horizontalChange = pos.X - GetOriginalX(obj);
            double verticalChange = pos.Y - GetOriginalY(obj);
            //验证如果错误则重置为0
            if (double.IsNaN(GetX(obj)))
                SetX(obj, 0);
            if (double.IsNaN(GetY(obj)))
                SetY(obj, 0);

            //设置该控件所在位置
            SetX(obj, GetX(obj) + horizontalChange);
            SetY(obj, GetY(obj) + verticalChange);
            e.Handled = true;

        }

        #endregion 事件处理器
    }
}
