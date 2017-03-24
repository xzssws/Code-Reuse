using System;

namespace CC_DllInvoker
{
    /// <summary>
    /// 运行日志实体类
    /// </summary>
    [Serializable]
    public class SystemLogModel
    {
        #region 属性

        /// <summary>
        /// 运行日志id
        /// </summary>
        public Guid RunLog_ID { get; set; }

        /// <summary>
        /// 运行时间
        /// </summary>
        public DateTime RunTime { get; set; }

        /// <summary>
        /// 来源
        /// </summary>
        public string Source { get; set; }

        /// <summary>
        /// 级别
        /// </summary>
        public string Level { get; set; }

        /// <summary>
        /// 日志来源
        /// </summary>
        public string Type { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        public string Remark { get; set; }

        /// <summary>
        /// 内容
        /// </summary>
        public string Code { get; set; }

        #endregion 属性
    }
}