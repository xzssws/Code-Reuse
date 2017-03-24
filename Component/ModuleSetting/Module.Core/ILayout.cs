using System.Collections.Generic;

namespace Module.Core
{

    /// <summary>
    /// 
    /// </summary>
    public interface ILayout
    {
        /// <summary>
        /// 获取或设置 the height.
        /// </summary>
        /// <value>
        /// The height.
        /// </value>
        double Height { get; set; }

        /// <summary>
        /// 获取或设置 the width.
        /// </summary>
        /// <value>
        /// The width.
        /// </value>
        double Width { get; set; }

        /// <summary>
        /// 获取或设置 the modules.
        /// </summary>
        /// <value>
        /// The modules.
        /// </value>
        List<IModule> Modules { get; set; }
    }
    /// <summary>
    /// 
    /// </summary>
    public interface IGridLayout:ILayout
    {
        /// <summary>
        /// 获取或设置 the rows.
        /// </summary>
        /// <value>
        /// The rows.
        /// </value>
        int Rows { get; set; }
        /// <summary>
        /// 获取或设置 the columns.
        /// </summary>
        /// <value>
        /// The columns.
        /// </value>
        int Columns { get; set; }
    }
}