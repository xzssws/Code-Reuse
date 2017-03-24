using System;

namespace CC_DllInvoker
{
    /// <summary>
    /// 数据点类
    /// </summary>
    [Serializable]
    public class DataPoint
    {
        #region 属性定义

        /// <summary>
        /// 点id
        /// </summary>
        public short ID { get; set; }

        /// <summary>
        /// 点名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 点描述
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// 电厂区域
        /// </summary>
        public string PowerPlantRegion { get; set; }

        /// <summary>
        /// 点类型
        /// </summary>
        public short Type { get; set; }

        /// <summary>
        /// 点值（数据库中的点值）
        /// </summary>
        public float Value { get; set; }

        /// <summary>
        /// 质量码
        /// </summary>
        public string Qualitycode { get; set; }

        /// <summary>
        /// 更新时间
        /// </summary>
        public long UpdateTime { get; set; }

        /// <summary>
        /// 质量原因
        /// </summary>
        public int Reason_dcs { get; set; }

        #endregion 属性定义
    }
}