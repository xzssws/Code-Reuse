using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Input;
using System.Windows.Media;

namespace Module.VisualCore
{
    /// <summary>
    /// 旋转 Thumb
    /// </summary>
    public class RotateThumb : Thumb
    {
        /// <summary>
        /// 初始角度
        /// </summary>
        private double initialAngle;
        /// <summary>
        /// 旋转结构
        /// </summary>
        private RotateTransform rotateTransform;
        /// <summary>
        /// 起始置换
        /// </summary>
        private Vector startVector;
        /// <summary>
        /// 中心店
        /// </summary>
        private Point centerPoint;
        /// <summary>
        /// 设计器元素
        /// </summary>
        private ContentControl designerItem;
        /// <summary>
        /// 画布
        /// </summary>
        private Canvas canvas;

        /// <summary>
        /// 初始化 <see cref="RotateThumb"/>类的新实例。
        /// </summary>
        public RotateThumb()
        {
            DragDelta += new DragDeltaEventHandler(this.RotateThumb_DragDelta);
            DragStarted += new DragStartedEventHandler(this.RotateThumb_DragStarted);
        }

        /// <summary>
        ///  开始移动
        /// </summary>
        /// <param name="sender">事件源</param>
        /// <param name="e"><see cref="DragStartedEventArgs"/> 事件参数</param>
        private void RotateThumb_DragStarted(object sender, DragStartedEventArgs e)
        {
            this.designerItem = DataContext as ContentControl;

            if (this.designerItem != null)
            {
                this.canvas = VisualTreeHelper.GetParent(this.designerItem) as Canvas;

                if (this.canvas != null)
                {
                    this.centerPoint = this.designerItem.TranslatePoint(
                        new Point(this.designerItem.Width * this.designerItem.RenderTransformOrigin.X,
                                  this.designerItem.Height * this.designerItem.RenderTransformOrigin.Y),
                                  this.canvas);

                    Point startPoint = Mouse.GetPosition(this.canvas);
                    this.startVector = Point.Subtract(startPoint, this.centerPoint);

                    this.rotateTransform = this.designerItem.RenderTransform as RotateTransform;
                    if (this.rotateTransform == null)
                    {
                        this.designerItem.RenderTransform = new RotateTransform(0);
                        this.initialAngle = 0;
                    }
                    else
                    {
                        this.initialAngle = this.rotateTransform.Angle;
                    }
                }
            }
        }

        /// <summary>
        /// 移动中
        /// </summary>
        /// <param name="sender">事件源</param>
        /// <param name="e"><see cref="DragDeltaEventArgs"/> 事件参数</param>
        private void RotateThumb_DragDelta(object sender, DragDeltaEventArgs e)
        {
            if (this.designerItem != null && this.canvas != null)
            {
                Point currentPoint = Mouse.GetPosition(this.canvas);
                Vector deltaVector = Point.Subtract(currentPoint, this.centerPoint);

                double angle = Vector.AngleBetween(this.startVector, deltaVector);

                RotateTransform rotateTransform = this.designerItem.RenderTransform as RotateTransform;
                rotateTransform.Angle = this.initialAngle + Math.Round(angle, 0);
                this.designerItem.InvalidateMeasure();
            }
        }
    }
}
