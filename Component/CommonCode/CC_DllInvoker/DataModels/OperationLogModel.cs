using System;

namespace CC_DllInvoker
{
    /// <summary>
    /// 操作日志实体类
    /// </summary>
    [Serializable]
    public class OperationLogModel
    {
        #region 属性定义

        /// <summary>
        /// 日志编号
        /// </summary>
        public Guid OperateLogID { get; set; }

        /// <summary>
        /// 操作时间
        /// </summary>
        public DateTime OperateTime { get; set; }

        /// <summary>
        /// 操作名称
        /// </summary>
        public string OperateName { get; set; }

        /// <summary>
        /// 操作模块
        /// </summary>
        public string OperateModule { get; set; }

        /// <summary>
        /// 操作内容
        /// </summary>
        public string OperateContent { get; set; }

        /// <summary>
        /// 操作站IP
        /// </summary>
        public string OperateStationIP { get; set; }

        /// <summary>
        /// 操作站编号
        /// </summary>
        public string OperateStationNo { get; set; }

        /// <summary>
        /// 操作人
        /// </summary>
        public string Operator { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        public string Remark { get; set; }

        /// <summary>
        /// 日志来源
        /// </summary>
        public string Type { get; set; }

        /// <summary>
        /// 日志来源  0操作日志 1登录日志
        /// </summary>
        public int Source { get; set; }

        #endregion 属性定义
    }
}