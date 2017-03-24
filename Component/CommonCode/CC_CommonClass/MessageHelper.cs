namespace CC_CommonClass
{
    /// <summary>
    /// 提示信息帮助
    /// </summary>
    public class MessageHelper
    {
        /// <summary>
        /// 显示信息通知对话框
        /// </summary>
        /// <param name="msg">提示的信息对象</param>
        public static void Msg(string msg)
        {
            System.Windows.MessageBox.Show(msg, "信息提示", System.Windows.MessageBoxButton.OK, System.Windows.MessageBoxImage.Information);
        }

        /// <summary>
        /// 错误信息提示
        /// </summary>
        /// <param name="error">错误信息</param>
        public static void Error(string error)
        {
            System.Windows.MessageBox.Show(error, "错误提示", System.Windows.MessageBoxButton.OK, System.Windows.MessageBoxImage.Error);
        }

        /// <summary>
        /// 疑问信息提示
        /// </summary>
        /// <param name="Question">疑问信息</param>
        /// <returns>是/否</returns>
        public static bool Ask(string Question)
        {
            return System.Windows.MessageBox.Show(Question, "系统提示", System.Windows.MessageBoxButton.YesNo, System.Windows.MessageBoxImage.Question) == System.Windows.MessageBoxResult.Yes;
        }
    }
}