using System.Collections.Generic;

namespace Module.Core
{
    /// <summary>
    /// 
    /// </summary>
    [System.Serializable]
    public class CanvasLayout : ILayout
    {
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
        /// 获取或设置 the modules.
        /// </summary>
        /// <value>
        /// The modules.
        /// </value>
        public List<IModule> Modules
        {
            get;
            set;
        }
    }
}