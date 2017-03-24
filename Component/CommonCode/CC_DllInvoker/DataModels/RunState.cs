namespace CC_DllInvoker
{
    /// <summary>
    /// 运行状态
    /// </summary>
    public enum RunState
    {
        /// <summary>
        /// 未运行
        /// </summary>
        UnRun,

        /// <summary>
        /// 启动中
        /// </summary>
        Starting,

        /// <summary>
        /// 初始化中
        /// </summary>
        Initing,

        /// <summary>
        /// 运行中
        /// </summary>
        Running,

        /// <summary>
        /// 停止中
        /// </summary>
        Stopping,

        /// <summary>
        /// 加载中
        /// </summary>
        Loading,

        /// <summary>
        /// 更新中
        /// </summary>
        Updating
    }
}