using System.Globalization;
using System.Text.RegularExpressions;
using System.Windows.Controls;

namespace CC_ValidateRule
{
    /// <summary>
    /// 正则表达式验证规则
    /// 验证输入值是否满足正则表达式
    /// </summary>
    public class CheckRegularExpresion : ValidationRule
    {
        /// <summary>
        /// 获取或设置正则表达式
        /// </summary>
        /// <value>
        /// 正则表达式
        /// </value>
        public string RegularExpresion { get; set; }

        /// <summary>
        /// 错误提示
        /// </summary>
        private string errorMsg = "此项不能为空";

        /// <summary>
        /// 获取或设置错误信息
        /// </summary>
        /// <value>
        /// 错误信息
        /// </value>
        public string ErrorMsg
        {
            get { return errorMsg; }
            set { errorMsg = value; }
        }

        /// <summary>
        /// 当在派生类中重写时，对值执行验证检查。
        /// </summary>
        /// <param name="value">要检查的来自绑定目标的值。</param>
        /// <param name="cultureInfo">要在此规则中使用的区域性。</param>
        /// <returns>
        /// 一个 <see cref="T:System.Windows.Controls.ValidationResult" /> 对象。
        /// </returns>
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            bool result = Regex.IsMatch(value.ToString(), RegularExpresion) == false && value != "";

            if (result)
            {
                return ValidationResult.ValidResult;
            }
            else
            {
                return new ValidationResult(false, ErrorMsg);
            }
        }
    }
}