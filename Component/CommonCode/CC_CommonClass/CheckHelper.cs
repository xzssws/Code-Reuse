using System;
using System.IO;
using System.Management;
using System.Text;
using System.Text.RegularExpressions;

namespace CC_CommonClass
{
    /// <summary>
    /// 检查输入帮助
    /// 此方法配合Exception是所有检测正确不显示问题
    /// 如果发现错误会跑出CheckExcepiton异常
    /// </summary>
    public class CheckHelper
    {

        #region 字段

        /// <summary>
        /// 服务器类型
        /// </summary>
        public static readonly string ServerType = "1";

        /// <summary>
        /// 中文
        /// </summary>
        public static readonly string LanguageChinese = "Chinese";

        /// <summary>
        /// 英文
        /// </summary>
        public static readonly string LanguageEngLish = "English";

        /// <summary>
        /// 全时间格式
        /// </summary>
        public static readonly string ProcessDateFormate = "yyyy-MM-dd HH:mm:ss";

        /// <summary>
        /// 日期格式
        /// </summary>
        public static readonly string BusinessDateFormate = "yyyy-MM-dd";

        /// <summary>
        /// 身份证正则表达式(15位)
        /// </summary>
        public static readonly string IDCardNO15 = @"/^[1-9]\d{7}((0\d)|(1[0-2]))(([0|1|2]\d)|3[0-1])\d{3}$/";

        /// <summary>
        /// 身份证正则表达式(18位)
        /// </summary>
        public static readonly string IDCardNO18 = @"/^[1-9]\d{5}[1-9]\d{3}((0\d)|(1[0-2]))(([0|1|2]\d)|3[0-1])\d{4}$/";

        /// <summary>
        /// ID正则表达式（15位）
        /// </summary>
        public static readonly Regex RegexID15 = new Regex("^[1-9]\\d{7}((0\\d)|(1[0-2]))(([0|1|2]\\d)|3[0-1])\\d{3}$");

        /// <summary>
        /// ID正则表达式（18位）
        /// </summary>
        public static readonly Regex RegexID18 = new Regex("^[1-9][0-9]{5}(19[0-9]{2}|200[0-9]|2010)(0[1-9]|1[0-2])(0[1-9]|[12][0-9]|3[01])[0-9]{3}[0-9xX]$");

        /// <summary>
        /// 转档清单
        /// </summary>
        public static readonly Regex ZDQD = new Regex("^([A-Z]|[a-z]|[0-9]){2,8}[1-2]{1}[0-9]{3}[0-1]{1}[0-9]{1}[0-3]{1}[0-9]{1}[0-9]{4}$");

        /// <summary>
        /// The ywzh
        /// </summary>
        public static readonly Regex YWZH = new Regex("^([1-2]{1}[0-9]{3})(([0-1]{1}[0-9]{1}[0-3]{1}[0-9]{1}[0-9]{4})|([0-9]{3}[0-9]{6}[0-9]{0,2}))$");

        #endregion 字段

        #region 公开方法

        /// <summary>
        /// 身份证号码格式验证
        /// </summary>
        /// <param name="cardno">身份证号码</param>
        /// <returns>是/否</returns>
        public bool PersonCard(string cardno)
        {
            Regex personcardno = new Regex("\\d{15}|\\d{18}");
            if (personcardno.IsMatch(cardno))
            {
                return true;
            }
            return false;
        }

        /// <summary>
        /// 身份证号码格式验证
        /// </summary>
        /// <param name="cardno">身份证号码</param>
        /// <returns>是/否</returns>
        public bool PersonCardLenthCheck(string cardno)
        {
            Regex personcardno = new Regex(@"^\d{15}$|^\d{18}$");
            if (personcardno.IsMatch(cardno))
            {
                return true;
            }
            return false;
        }

        /// <summary>
        /// 固定电话号码格式验证
        /// </summary>
        /// <param name="phoneno">电话号码</param>
        /// <returns>是/否</returns>
        public bool TelePhone(string phoneno)
        {
            Regex Telephone = new Regex("\\d{3}-\\d{8}|\\d{4}-\\d{7}");
            if (Telephone.IsMatch(phoneno))
            {
                return true;
            }
            return false;
        }

        /// <summary>
        /// 移动电话号码格式验证
        /// </summary>
        /// <param name="inputno">电话号码</param>
        /// <returns>是/否</returns>
        public bool MobilePhone(string phonemo)
        {
            Regex phonenumberno = new Regex("@^(13[0-9]|15[0|3|6|7|8|9]|18[8|9])\\d{8}$");
            if (phonenumberno.IsMatch(phonemo))
            {
                return true;
            }
            return false;
        }

        /// <summary>
        /// IP地址格式验证
        /// </summary>
        /// <param name="ip">ip地址</param>
        /// <returns></returns>
        public bool ComputerIp(string ip)
        {
            Regex computerip = new Regex("\\d+\\.\\d+\\.\\d+\\.\\d+");
            if (computerip.IsMatch(ip))
            {
                return true;
            }
            return false;
        }

        /// <summary>
        /// 数字字母组合格式验证
        /// </summary>
        /// <param name="inputstr">要验证的字符串</param>
        /// <returns>是/否</returns>
        public bool Alphanumeric(string inputstr)
        {
            Regex alphanumericno = new Regex("^[A-Za-z0-9]+$");
            if (alphanumericno.IsMatch(inputstr))
            {
                return true;
            }
            return false;
        }

        /// <summary>
        /// 字母格式验证
        /// </summary>
        /// <param name="inputstr">要验证的字符串</param>
        /// <returns>是/否</returns>
        public bool Alphabet(string inputstr)
        {
            Regex Alphabetno = new Regex("^[A-Za-z]+$");
            if (Alphabetno.IsMatch(inputstr))
            {
                return true;
            }
            return false;
        }

        /// <summary>
        /// 正浮点数格式验证
        /// </summary>
        /// <param name="inputnum">要验证的字符串</param>
        /// <returns>是/否</returns>
        public bool AreFloatingpoint(string inputnum)
        {
            Regex arefloatingpointno = new Regex("^[1-9]\\d*\\.\\d*|0\\.\\d*[1-9]\\d*$");
            if (arefloatingpointno.IsMatch(inputnum))
            {
                return true;
            }
            return false;
        }

        /// <summary>
        /// 整数格式验证
        /// </summary>
        /// <param name="inputnum">要验证的字符串</param>
        /// <returns>是/否</returns>
        public bool InterNum(string inputnum)
        {
            Regex InterNumno = new Regex("^[1-9]\\d*$");
            if (InterNumno.IsMatch(inputnum))
            {
                return true;
            }
            return false;
        }

        /// <summary>
        /// 邮政编码格式验证
        /// </summary>
        /// <param name="inputstr">邮政编码号码</param>
        /// <returns>是/否</returns>
        public bool PostalCoding(string inputstr)
        {
            Regex postalcodingdno = new Regex("[1-9]\\d{5}(?!\\d)");
            if (postalcodingdno.IsMatch(inputstr))
            {
                return true;
            }
            return false;
        }

        /// <summary>
        /// email地址格式验证
        /// </summary>
        /// <param name="inputno">email号码</param>
        /// <returns>是/否</returns>
        public bool Emailadress(string inputno)
        {
            Regex Emailadressno = new Regex("^[/w-]+(/.[/w-]+)*@[/w-]+(/.[/w-]+)+$");
            if (Emailadressno.IsMatch(inputno))
            {
                return true;
            }
            return false;
        }

        /// <summary>
        /// 年_月_日格式验证
        /// </summary>
        /// <param name="inputtime">日期字符串</param>
        /// <returns>是/否</returns>
        public bool Yearmonthday(string inputtime)
        {
            Regex yearmonthdayno = new Regex("/^(d{2}|d{4})-((0([1-9]{1}))|(1[1|2]))-(([0-2]([1-9]{1}))|(3[0|1]))$");
            if (yearmonthdayno.IsMatch(inputtime))
            {
                return true;
            }
            return false;
        }

        /// <summary>
        /// 月/日/年/格式验证
        /// </summary>
        /// <param name="inputtime">日期字符串</param>
        /// <returns>是/否</returns>
        public bool Monthdayyear(string inputtime)
        {
            Regex monthdayyearno = new Regex("/^((0([1-9]{1}))|(1[1|2]))/(([0-2]([1-9]{1}))|(3[0|1]))/(d{2}|d{4})$");
            if (monthdayyearno.IsMatch(inputtime))
            {
                return true;
            }
            return false;
        }

        /// <summary>
        /// 字符串是否为纯数字
        /// </summary>
        /// <param name="inputno">要验证的字符串</param>
        /// <returns>是/否</returns>
        public bool Purenumber(string inputno)
        {
            Regex purenumberno = new Regex("/^\\d+$/");
            if (purenumberno.IsMatch(inputno))
            {
                return true;
            }
            return false;
        }

    
        /// <summary>
        /// 获取MD5值
        /// </summary>
        /// <param name="fileName">文件名称</param>
        /// <returns>MD5值</returns>
        public static string GetMD5HashFromFile(string fileName)
        {
            try
            {
                FileStream file = new FileStream(fileName, FileMode.Open);
                System.Security.Cryptography.MD5 md5 = new System.Security.Cryptography.MD5CryptoServiceProvider();
                byte[] retVal = md5.ComputeHash(file);
                file.Close();

                StringBuilder sb = new StringBuilder();
                for (int i = 0; i < retVal.Length; i++)
                {
                    sb.Append(retVal[i].ToString("x2"));
                }
                return sb.ToString();
            }
            catch (Exception ex)
            {
                throw new Exception("GetMD5HashFromFile() fail,error:" + ex.Message);
            }
        }

        #endregion 公开方法
        /// <summary>
        /// 非空检查
        /// </summary>
        /// <param name="field">字段</param>
        /// <param name="label">标签</param>
        public static void NotNullCheck(string input, string label)
        {
            CustomerCheck(input, label + "内容不能为空", (i) => !string.IsNullOrEmpty(i));
        }

        /// <summary>
        /// 非空检查
        /// </summary>
        /// <param name="field">字段</param>
        /// <param name="label">标签</param>
        public static void NotNullCheck(object input, string label)
        {
            if (input == null) throw new Exception(label + "内容不能为空");
        }

        /// <summary>
        /// 字符串长度验证 内部验证非空
        /// </summary>
        /// <param name="input">验证字段</param>
        /// <param name="label">标记</param>
        /// <param name="Max">最大长度</param>
        /// <param name="Min">最小长度</param>
        public static void StrLengthCheck(string input, string label, int Max, int Min = 0)
        {
            NotNullCheck(input, label);
            CustomerCheck(input, label += "长度不正确", (i) => !(System.Text.UnicodeEncoding.Default.GetByteCount(i) > Max || System.Text.UnicodeEncoding.Default.GetByteCount(i) < Min));
        }

        /// <summary>
        /// 正则表达式验证 内部验证非空
        /// </summary>
        /// <param name="field">验证字段</param>
        /// <param name="label">提示字符串</param>
        /// <param name="pattern">最小长度</param>
        public static void RegexCheck(string input, string label, string pattern)
        {
            NotNullCheck(input, label);
            CustomerCheck(input, label + "输入不正确", (i) => System.Text.RegularExpressions.Regex.IsMatch(input, pattern));
        }

        /// <summary>
        /// 用户自定义验证
        /// </summary>
        /// <typeparam name="T">输入类型</typeparam>
        /// <param name="input">输入</param>
        /// <param name="label">标签</param>
        /// <param name="CheckMethod">验证方法</param>
        public static void CustomerCheck<T>(T input, string label, Func<T, bool> CheckMethod)
        {
            NotNullCheck(input, label);
            if (!CheckMethod(input))
            {
                throw new Exception(label);
            }
        }

        /// <summary>
        /// 用户自定义验证
        /// </summary>
        /// <param name="input">输入</param>
        /// <param name="label">标签</param>
        /// <param name="CheckMethod">验证方法</param>
        public static void CustomerCheck(string input, string label, Func<string, bool> CheckMethod)
        {
            if (!CheckMethod(input)) throw new Exception(label);
        }

        /// <summary>
        /// 数值非零验证
        /// </summary>
        /// <param name="input">输入</param>
        /// <param name="label">标签</param>
        public static void NotZero(int input, string label)
        {
            CustomerCheck<int>(input, label + "不能小于1", (i) => i > 0);
        }
    }
}