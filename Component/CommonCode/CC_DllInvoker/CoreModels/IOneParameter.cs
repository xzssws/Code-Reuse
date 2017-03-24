using System;

namespace CC_DllInvoker
{
    /// <summary>
    /// 参数接口
    ///     参数字段
    /// </summary>
    public interface IOneParameter : ICloneable
    {
        #region 字段定义

        /// <summary>
        /// 类型
        /// </summary>
        string Type { get; set; }

        /// <summary>
        /// 名称
        /// </summary>
        string Name { get; set; }

        /// <summary>
        /// 系统标识
        /// </summary>
        string System { get; set; }

        /// <summary>
        /// 值
        /// </summary>
        object Value { get; set; }

        /// <summary>
        /// 初始化值
        /// </summary>
        object InitValue { get; set; }

        /// <summary>
        /// 描述
        /// </summary>
        string Description { get; set; }

        /// <summary>
        /// 工程单位
        /// </summary>
        string Unit { get; set; }

        /// <summary>
        /// 来源模块
        /// </summary>
        string SourceModule { get; set; }  //Module.Input

        /// <summary>
        /// 输出来源
        /// </summary>
        string SourceData { get; set; }  //Module.Input

        /// <summary>
        /// 参数类型
        /// </summary>
        string ParameterType { get; set; } //Module.Parameter

        /// <summary>
        /// 名称
        /// </summary>
        string SignalName { get; set; }   //Input、Output

        /// <summary>
        /// 来源名称
        /// </summary>
        string SourceName { get; set; }  //MOdule.Parameter.SourceName

        /// <summary>
        /// 质量码
        /// </summary>
        string QualityCode { get; set; }

        /// <summary>
        /// 目标名称
        /// </summary>
        string TargetName { get; set; }

        /// <summary>
        /// 目标来源
        /// </summary>
        string SourceTag { get; set; }

        #endregion 字段定义
    }
}