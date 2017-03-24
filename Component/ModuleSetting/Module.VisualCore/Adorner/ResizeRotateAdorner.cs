using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Media;

namespace Module.VisualCore
{
    /// <summary>
    /// 调整旋转装饰器
    /// </summary>
    public class ResizeRotateAdorner : Adorner
    {
        /// <summary>
        /// 可视化元素集
        /// </summary>
        private VisualCollection visuals;

        /// <summary>
        /// 默认外观
        /// </summary>
        private ResizeRotateChrome chrome;


        /// <summary>
        /// 获取此元素内的可视化子元素的数目。
        /// </summary>
        protected override int VisualChildrenCount
        {
            get
            {
                return this.visuals.Count;
            }
        }

        /// <summary>
        /// 初始化 <see cref="ResizeRotateAdorner"/>类的新实例。
        /// </summary>
        /// <param name="designerItem">The designer item.</param>
        public ResizeRotateAdorner(ContentControl designerItem)
            : base(designerItem)
        {
            SnapsToDevicePixels = true;
            this.chrome = new ResizeRotateChrome();
            this.chrome.DataContext = designerItem;
            this.visuals = new VisualCollection(this);
            this.visuals.Add(this.chrome);
        }

        /// <summary>
        /// 定位子元素并确定大小。
        /// </summary>
        /// <param name="arrangeBounds">父级中此元素应用来排列自身及其子元素的最终区域。</param>
        /// <returns>
        /// 返回值
        /// </returns>
        protected override Size ArrangeOverride(Size arrangeBounds)
        {
            this.chrome.Arrange(new Rect(arrangeBounds));
            return arrangeBounds;
        }

        /// <summary>
        /// 重写 <see cref="M:System.Windows.Media.Visual.GetVisualChild(System.Int32)" />，然后从子元素集合返回指定索引处的子级。
        /// </summary>
        /// <param name="index">集合中所请求子元素从零开始的索引。</param>
        /// <returns>
        /// 所请求的子元素。它不应返回 null；如果提供的索引超出范围，将引发异常。
        /// </returns>
        protected override Visual GetVisualChild(int index)
        {
            return this.visuals[index];
        }
    }
}
