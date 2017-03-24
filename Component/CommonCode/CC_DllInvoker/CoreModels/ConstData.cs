namespace CC_DllInvoker
{
    /// <summary>
    /// SCADA DLL方法标记
    /// </summary>
    public struct ConstData
    {
        #region 常量定义

        /// <summary>
        /// 组合格式
        /// </summary>
        public const string @STR = "{0}{1}{2}";

        /// <summary>
        /// 初始化方法标记
        /// </summary>
        public const string @INIT = "_Initialize_";

        /// <summary>
        /// 运算方法标记
        /// </summary>
        public const string @RUN = "_";

        /// <summary>
        /// 更新可调参数标记
        /// </summary>
        public const string @SETP = "_SetParameter_";

        /// <summary>
        /// 获取缓冲区大小标记
        /// </summary>
        public const string @GETB = "_GetStateBufferSize_";

        /// <summary>
        /// 保存状态
        /// </summary>
        public const string @SAVE = "_SaveState_";

        /// <summary>
        /// 加载状态
        /// </summary>
        public const string @LOAD = "_LoadState_";

        #endregion 常量定义
    }
}