using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Media;

namespace Module.VisualCore
{
    /// <summary>
    /// 移动 Thumb
    /// </summary>
    public class MoveThumb : Thumb
    {
        /// <summary>
        /// 旋转结构
        /// </summary>
        private RotateTransform rotateTransform;
        /// <summary>
        /// 设计器元素
        /// </summary>
        private ContentControl designerItem;

        /// <summary>
        /// 初始化 <see cref="MoveThumb"/>类的新实例。
        /// </summary>
        public MoveThumb()
        {
            DragStarted += new DragStartedEventHandler(this.MoveThumb_DragStarted);
            DragDelta += new DragDeltaEventHandler(this.MoveThumb_DragDelta);
        }
        /// <summary>
        /// 开始移动
        /// </summary>
        /// <param name="sender">事件源</param>
        /// <param name="e"><see cref="DragStartedEventArgs"/> 事件参数</param>
        private void MoveThumb_DragStarted(object sender, DragStartedEventArgs e)
        {
            this.designerItem = DataContext as ContentControl;

            if (this.designerItem != null)
            {
                this.rotateTransform = this.designerItem.RenderTransform as RotateTransform;
            }
        }

        /// <summary>
        /// Handles the DragDelta event of the MoveThumb control.
        /// </summary>
        /// <param name="sender">事件源</param>
        /// <param name="e"><see cref="DragDeltaEventArgs"/> 事件参数</param>
        private void MoveThumb_DragDelta(object sender, DragDeltaEventArgs e)
        {
            if (this.designerItem != null)
            {
                Point dragDelta = new Point(e.HorizontalChange, e.VerticalChange);

                if (this.rotateTransform != null)
                {
                    dragDelta = this.rotateTransform.Transform(dragDelta);
                }

                Canvas.SetLeft(this.designerItem, Canvas.GetLeft(this.designerItem) + dragDelta.X);
                Canvas.SetTop(this.designerItem, Canvas.GetTop(this.designerItem) + dragDelta.Y);
            }
        }
    }
}
