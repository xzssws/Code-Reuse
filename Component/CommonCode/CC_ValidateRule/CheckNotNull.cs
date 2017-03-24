using System.Globalization;
using System.Windows.Controls;

namespace CC_ValidateRule
{
    /// <summary>
    /// 非空验证规则
    /// </summary>
    public class CheckNotNull : ValidationRule
    {
        /// <summary>
        /// 获取或设置错误信息
        /// </summary>
        /// <value>
        /// 错误信息
        /// </value>
        public string ErrorMsg { get; set; }

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
            if (value == null || string.IsNullOrEmpty(value.ToString()))
            {
                return new ValidationResult(false, ErrorMsg);
            }
            else
            {
                return ValidationResult.ValidResult;
            }
        }
    }
}