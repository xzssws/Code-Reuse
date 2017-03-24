using System;

namespace CC_DllInvoker
{
    /// <summary>
    /// 开关量信号参数
    /// </summary>
    [Serializable]
    public struct DSIG
    {
        #region 属性定义

        /// <summary>
        /// 只
        /// </summary>
        public bool value { get; set; }

        /// <summary>
        /// 质量码
        /// </summary>
        public int quality { get; set; }

        /// <summary>
        /// 质量原因
        /// </summary>
        public int reason_dcs { get; set; }

        #endregion 属性定义
    }
}