using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Windows.Controls;

namespace CC_ValidateRule
{
    /// <summary>
    /// 包含项验证规则
    /// 验证输入的值是否在项集合中存在
    /// </summary>
    public class CheckContains : ValidationRule
    {
        /// <summary>
        /// 是否包含项
        /// </summary>
        private bool isContains = true;

        /// <summary>
        /// 获取或设置是否包含项
        /// </summary>
        /// <value>
        /// true 包含 false 不包含
        /// </value>
        [Description("包含或非包含")]
        public bool IsContains
        {
            get { return isContains; }
            set { isContains = value; }
        }

        /// <summary>
        /// 获取或设置项集合
        /// </summary>
        /// <value>
        /// 项集合 用+表示分割
        /// </value>
        public string Items { get; set; }

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
            bool result = Items.Split('+').Contains(value.ToString());

            if (IsContains)
            {
                return result ? ValidationResult.ValidResult : new ValidationResult(false, ErrorMsg);
            }
            else
            {
                return !result ? ValidationResult.ValidResult : new ValidationResult(false, ErrorMsg);
            }
        }
    }
}