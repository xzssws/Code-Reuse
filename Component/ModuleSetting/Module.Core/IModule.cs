using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.Core
{
    /// <summary>
    /// 
    /// </summary>
    [System.Serializable]
    public enum ModuleType
    {
        /// <summary>
        /// NI
        /// </summary>
        NIAA,
        /// <summary>
        /// CI
        /// </summary>
        CIAA,
        /// <summary>
        /// Tool
        /// </summary>
        ATA,
        /// <summary>
        /// List
        /// </summary>
        ALA,
        /// <summary>
        /// State
        /// </summary>
        ASA
    }
    /// <summary>
    /// 
    /// </summary>
    public interface IModule
    {
        /// <summary>
        /// 获取或设置 the type.
        /// </summary>
        /// <value>
        /// The type.
        /// </value>
        ModuleType Type { get; set; }
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
        /// 获取或设置 the left.
        /// </summary>
        /// <value>
        /// The left.
        /// </value>
        double Left { get; set; }
        /// <summary>
        /// 获取或设置 the top.
        /// </summary>
        /// <value>
        /// The top.
        /// </value>
        double Top { get; set; }
    }
    /// <summary>
    /// 
    /// </summary>
    public interface IGridModule : IModule
    {
        /// <summary>
        /// 获取或设置跨行数
        /// </summary>
        /// <value>
        /// 跨越几行
        /// </value>
        int RowSpan { get; set; }
        /// <summary>
        /// 获取或设置 the column span.
        /// </summary>
        /// <value>
        /// The column span.
        /// </value>
        int ColumnSpan { get; set; }
    }
    /// <summary>
    /// 
    /// </summary>
    public interface ICanvasModule : IModule
    {
        /// <summary>
        /// 获取或设置一个值，该值指示是否[bind top].
        /// </summary>
        /// <value>
        /// True:  False:
        /// </value>
        bool BindTop { get; set; }
        /// <summary>
        /// 获取或设置一个值，该值指示是否[bind left].
        /// </summary>
        /// <value>
        /// True:  False:
        /// </value>
        bool BindLeft { get; set; }
        /// <summary>
        /// 获取或设置一个值，该值指示是否[bind right].
        /// </summary>
        /// <value>
        /// True:  False:
        /// </value>
        bool BindRight { get; set; }
        /// <summary>
        /// 获取或设置一个值，该值指示是否[bind down].
        /// </summary>
        /// <value>
        /// True:  False:
        /// </value>
        bool BindDown { get; set; }
    }
}
