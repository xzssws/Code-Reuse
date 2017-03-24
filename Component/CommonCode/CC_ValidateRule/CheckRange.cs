using System;
using System.Globalization;
using System.Windows.Controls;

namespace CC_ValidateRule
{
    /// <summary>
    /// 范围验证规则
    /// 可以定制 数字范围 时间范围 和字符串长度范围的验证
    /// </summary>
    public class CheckRange : ValidationRule
    {
        /// <summary>
        /// 进行何种类型的验证
        /// </summary>
        private string validationValueType = "int";

        /// <summary>
        /// 获取或设置验证类型
        /// </summary>
        /// <value>
        /// int 整型 (默认)  datetime 时间 stringlength 字符串长度
        /// </value>
        public string ValidationValueType
        {
            get { return validationValueType; }
            set { validationValueType = value; }
        }

        /// <summary>
        /// 最小值
        /// </summary>
        private object minValue = 0;

        /// <summary>
        /// 获取或设置最小值
        /// </summary>
        /// <value>
        /// 最小值
        /// </value>
        public object MinValue
        {
            get { return minValue; }
            set { minValue = value; }
        }

        /// <summary>
        /// 最大值
        /// </summary>
        private object maxValue = 1;

        /// <summary>
        /// 获取或设置最大值
        /// </summary>
        /// <value>
        /// 最大值
        /// </value>
        public object MaxValue
        {
            get { return maxValue; }
            set { maxValue = value; }
        }

        /// <summary>
        /// 获取或设置错误信息
        /// </summary>
        /// <value>
        /// 错误信息
        /// </value>
        public string ErrorMsg { get; set; }

        /// <summary>
        /// 获取或设置默认的提示信息
        /// </summary>
        /// <value>
        /// 默认提示信息
        /// </value>
        public string DefaultMsg { get; set; }

        /// <summary>
        /// <para> 方法描述：验证值是否在预制的范围内 </para>
        /// <para> 方法说明：方法说明内容 </para>
        /// </summary>
        /// <param name="value">要检查的来自绑定目标的值。</param>
        /// <param name="cultureInfo">要在此规则中使用的区域性</param>
        /// <remarks>
        ///  备注内容
        /// </remarks>
        /// <returns>验证结果</returns>
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            switch (ValidationValueType)
            {
                case "int":
                    if (Convert.ToInt32(MinValue) < Convert.ToInt32(value) && Convert.ToInt32(value) < Convert.ToInt32(MaxValue))
                    {
                        return ValidationResult.ValidResult;
                    }
                    else
                    {
                        return new ValidationResult(false, ErrorMsg);
                    }
                case "datetime":
                    if (Convert.ToDateTime(MinValue) < Convert.ToDateTime(value) && Convert.ToDateTime(value) < Convert.ToDateTime(MaxValue))
                    {
                        return ValidationResult.ValidResult;
                    }
                    else
                    {
                        return new ValidationResult(false, ErrorMsg);
                    }
                case "stringlength":
                    if (Convert.ToInt32(MinValue) < value.ToString().Length && value.ToString().Length < Convert.ToInt32(MaxValue))
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