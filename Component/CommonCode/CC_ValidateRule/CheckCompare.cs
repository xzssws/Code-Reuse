using System;
using System.Globalization;
using System.Windows.Controls;

namespace CC_ValidateRule
{
    /// <summary>
    /// 比较验证规则
    /// 可以对整数 时间 字符串长度 字符串进行比较
    /// </summary>
    public class CheckCompare : ValidationRule
    {
        /// <summary>
        /// 进行何种类型的验证
        /// </summary>
        private string validationValueType = "string";

        /// <summary>
        /// 获取或设置验证类型
        /// </summary>
        /// <value>
        /// int 整型  datetime 时间 stringlength 字符串长度 string 字符串 (默认)
        /// </value>
        public string ValidationValueType
        {
            get { return validationValueType; }
            set { validationValueType = value; }
        }

        /// <summary>
        /// 获取或设置提示的错误信息
        /// </summary>
        /// <value>
        /// 提示的错误信息
        /// </value>
        public string ErrorMsg { get; set; }

        /// <summary>
        /// 获取或设置比较值
        /// </summary>
        /// <value>
        /// 比较的值
        /// </value>
        public object CompareValue { get; set; }

        /// <summary>
        /// 获取或设置默认的提示信息
        /// </summary>
        /// <value>
        /// 默认提示信息
        /// </value>
        public string DefaultMsg { get; set; }

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
            switch (ValidationValueType)
            {
                case "int":
                    if (Convert.ToInt32(value) == Convert.ToInt32(CompareValue))
                    {
                        return ValidationResult.ValidResult;
                    }
                    else
                    {
                        return new ValidationResult(false, ErrorMsg);
                    }
                case "datetime":
                    if (Convert.ToDateTime(value) == Convert.ToDateTime(CompareValue))
                    {
                        return ValidationResult.ValidResult;
                    }
                    else
                    {
                        return new ValidationResult(false, ErrorMsg);
                    }
                case "stringlength":
                    if (Convert.ToInt32(value) == value.ToString().Length)
                    {
                        return ValidationResult.ValidResult;
                    }
                    else
                    {
                        return new ValidationResult(false, ErrorMsg);
                    }
                case "string":
                    if (value.ToString() == value.ToString())
                    {
                        return ValidationResult.ValidResult;
                    }
                    else
                    {
                        return new ValidationResult(false, ErrorMsg);
                    }
                default:
                    return new ValidationResult(false, DefaultMsg);
            }
        }
    }
}