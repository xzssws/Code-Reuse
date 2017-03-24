namespace Module.Core
{
    /// <summary>
    /// 
    /// </summary>
    [System.Serializable]
    public class CanvasModule : ICanvasModule
    {
        /// <summary>
        /// 获取或设置一个值，该值指示是否[bind top].
        /// </summary>
        /// <value>
        /// True:  False:
        /// </value>
        public bool BindTop
        {
            get;
            set;
        }

        /// <summary>
        /// 获取或设置一个值，该值指示是否[bind left].
        /// </summary>
        /// <value>
        /// True:  False:
        /// </value>
        public bool BindLeft
        {
            get;
            set;
        }

        /// <summary>
        /// 获取或设置一个值，该值指示是否[bind right].
        /// </summary>
        /// <value>
        /// True:  False:
        /// </value>
        public bool BindRight
        {
            get;
            set;
        }

        /// <summary>
        /// 获取或设置一个值，该值指示是否[bind down].
        /// </summary>
        /// <value>
        /// True:  False:
        /// </value>
        public bool BindDown
        {
            get;
            set;
        }

        /// <summary>
        /// 获取或设置 the height.
        /// </summary>
        /// <value>
        /// The height.
        /// </value>
        public double Height
        {
            get;
            set;
        }

        /// <summary>
        /// 获取或设置 the width.
        /// </summary>
        /// <value>
        /// The width.
        /// </value>
        public double Width
        {
            get;
            set;
        }

        /// <summary>
        /// 获取或设置 the left.
        /// </summary>
        /// <value>
        /// The left.
        /// </value>
        public double Left
        {
            get;
            set;
        }

        /// <summary>
        /// 获取或设置 the top.
        /// </summary>
        /// <value>
        /// The top.
        /// </value>
        public double Top
        {
            get;
            set;
        }

        /// <summary>
        /// 获取或设置 the type.
        /// </summary>
        /// <value>
        /// The type.
        /// </value>
        public ModuleType Type
        {
            get;
            set;
        }
    }
}