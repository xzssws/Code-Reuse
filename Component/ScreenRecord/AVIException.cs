using System;

namespace ScreenRecord
{
    /// <summary>
    /// 异常类
    /// </summary>
    public class AVIException : ApplicationException
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="s">错误信息</param>
        public AVIException(string s)
            : base(s)
        {
        }
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="s">错误信息</param>
        /// <param name="hr">错误句柄</param>
        public AVIException(string s, Int32 hr)
            : base(s)
        {
            if (hr == AVIERR_BADPARAM)
            {
                err_msg = "AVIERR_BADPARAM";
            }
            else
            {
                err_msg = "unknown";
            }
        }
        /// <summary>
        /// 获取错误信息
        /// </summary>
        /// <returns>错误信息</returns>
        public string ErrMsg()
        {
            return err_msg;
        }

        private const Int32 AVIERR_BADPARAM = -2147205018;
        private string err_msg;
    }
}