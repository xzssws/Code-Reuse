using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace OSLibrary.浪潮
{
    /// <summary>
    /// 金额格式化、输入验证、字符串处理
    /// </summary>
    public class StringEx
    {
        public StringEx()
        { }

        /// <summary>
        /// 把金额转化为大写
        /// </summary>
        /// <param name="amount"></param>
        /// <returns></returns>
        public static string JeToBig(string amount)
        {
            if (amount == null || amount.Length == 0)
                return "";

            string result = string.Empty;
            if (Convert.ToDouble(amount) == 0)
            {
                result = "零元整";
            }
            else
            {
                Decimal dValue = Convert.ToDecimal(amount);
                string Je = dValue.ToString("#.##");
                string vsJe = Convert.ToString(Convert.ToDouble(Je) * 100);
                result = NumToUpper(vsJe);
            }
            return result;
        }
        ///<summary>
        /// 金额转换为大写
        /// <param name="psValue">金额</param>
        /// <returns>大写金额</returns>
        public static string NumToUpper(string amount)
        {
            string vs_amount = amount;
            vs_amount = vs_amount.Replace(",", "");
            vs_amount = vs_amount.Replace("-", "");
            char[] tmpBuf;
            string tmpStr = null;
            int len, i, k, m, n, l;
            string[] DigitFigure = new string[] { "零", "壹", "贰", "叁", "肆", "伍", "陆", "柒", "捌", "玖" };
            string[] DigitUnit = new string[] { "分", "角", "元", "拾", "佰", "仟", "万", "拾", "佰", "仟", "亿", "拾", "佰", "仟", "整" };  //1028
            tmpBuf = vs_amount.ToString().ToCharArray();
            len = vs_amount.Length;
            if (len >= 15) //超过千亿视为无意义 111111111111
                return "";
            m = len;
            n = l = 0;
            for (i = 0; i < len; i++)
            {
                if ((tmpBuf[m - 1]) - 0x30 == 0)
                {
                    n++;
                    m--;
                }
                else
                {
                    break;
                }
            }
            for (i = 0; i < m; i++)
            {
                if ((tmpBuf[i] - 0x30) != 0)
                {
                    k = tmpBuf[i] - 0x30;
                    tmpStr += DigitFigure[k];

                    k = len - i - 1;
                    tmpStr += DigitUnit[k];
                    l = 0;

                }
                else if ((len - i) == 7)//10000100
                {
                    if ((tmpBuf[i - 1] - 0x30) != 0 || (tmpBuf[i - 2] - 0x30) != 0 || (tmpBuf[i - 3] - 0x30) != 0)
                    {
                        if (l == 1)
                        {
                            tmpStr = tmpStr.Remove(tmpStr.Length - 1, 1);
                        }
                        tmpStr += DigitUnit[6];
                        tmpStr += DigitFigure[0];
                        l = 1;
                    }
                }
                else if ((len - i) == 11)
                {
                    if (l == 1)
                    {
                        tmpStr = tmpStr.Remove(tmpStr.Length - 1, 1);
                    }
                    tmpStr += DigitUnit[10];
                    tmpStr += DigitFigure[0];
                    l = 1;
                }
                else if ((len - i) == 3)
                {
                    if (l == 1)
                    {
                        tmpStr = tmpStr.Remove(tmpStr.Length - 1, 1);
                    }
                    tmpStr += DigitUnit[2];
                    tmpStr += DigitFigure[0];
                    l = 1;
                }
                else if (l == 0)
                {
                    l = 1;
                    tmpStr += DigitFigure[0];
                    continue;
                }
            }
            if (n > 10)
            {
                tmpStr += DigitUnit[10];
                tmpStr += DigitUnit[2];
            }
            else if (n > 6 && n < 10)
            {
                tmpStr += DigitUnit[6];
                tmpStr += DigitUnit[2];
            }
            else if (n > 2)
            {
                tmpStr += DigitUnit[2];
            }
            tmpStr += DigitUnit[14];
            tmpStr += null;

            if (tmpStr.Substring(tmpStr.Length - 2, 1) == "分")
            {
                tmpStr = tmpStr.Remove(tmpStr.Length - 1, 1);
            }
            return tmpStr;
        }
        /// <summary>
        /// 得到几个字符组成的字符串
        /// </summary>
        /// <param name="piNum">字符个数</param>
        /// <param name="psStr">特使字符</param>
        /// <returns>几个字符组成的字符串,如6个a:aaaaaa</returns>
        public static string StringFormat(int piNum, string psStr)
        {
            string vsValue = "";
            int i;
            for (i = 1; i < piNum + 1; i++)
            {
                vsValue = vsValue + psStr;
            }
            return vsValue;
        }
        /// <summary>
        /// 用来格式化数值
        /// </summary>
        /// <param name="psValue">需要格式化的字符串（应该是合法的数字字符串）</param>
        /// <param name="psPrecision">需要格式化的精度</param>
        /// <returns>格式化好的数据</returns>
        public static string FormatNumber(object poValue, object poPrecision)
        {
            string psValue = "0";
            decimal pdValue = 0.0M;
            string psPrecision = "20";//默认
            try
            {
                if (poValue == null) poValue = "0";
                pdValue = Convert.ToDecimal(poValue);
            }
            catch
            {
                poValue = "0";
                pdValue = 0.0M;
            }
            try
            {
                if (poPrecision == null) poPrecision = "8";
                Convert.ToInt16(poPrecision);
            }
            catch
            {
                poPrecision = "8";
            }
            psPrecision = Convert.ToString(poPrecision);
            if (poValue.GetType().ToString() == "System.Int16" || poValue.GetType().ToString() == "System.Int32")
                pdValue = Convert.ToDecimal(poValue) + 0.0M;
            psValue = String.Format(StringEx.FormatNumberFormat(psPrecision), pdValue);
            return psValue;
        }
        /// <summary>
        /// 得到数据格式化的格式化模板{0:f20}
        /// </summary>
        /// <param name="psPrecision"></param>
        /// <returns></returns>
        public static string FormatNumberFormat(object psPrecision)
        {
            try
            {
                Convert.ToInt32(psPrecision);
            }
            catch
            {
                return "{0:f8}";
            }
            return "{0:f" + Convert.ToInt16(psPrecision) + "}";
        }

        ///<summary>
        /// 格式化金额
        /// </summary>
        /// <param name="psValue">金额</param>
        /// <param name="psPrecision">精度</param>
        /// <returns>格式化后的金额</returns>
        public static string FormatNumberEx(string psValue, string psPrecision)
        {
            double num;
            double f_value;
            string s_value;

            if (psPrecision == null || psPrecision.Length == 0)
            {
                psPrecision = "2";
            }
            if (psValue.Trim() == "")
                psValue = "0";

            int viPrecision = int.Parse(psPrecision);
            if (viPrecision == -1)
                return psValue;
            num = Math.Pow(10, Convert.ToInt16(psPrecision));
            f_value = Math.Round(Convert.ToDouble(psValue) * num, viPrecision) / num;
            //**************************************
            //修改因精度过大导致的出现科学计数法的问题
            string format_string;
            format_string = "f" + psPrecision;
            //**************************************
            s_value = f_value.ToString(format_string);
            s_value = s_value + ".";
            string[] vArr = s_value.Split(((".").ToCharArray())[0]);
            while (vArr[1].Length < Convert.ToInt16(psPrecision))
            {
                vArr[1] = vArr[1] + "0";
            }
            if (Convert.ToInt16(psPrecision) != 0)
                s_value = vArr[0] + "." + vArr[1];
            else
                s_value = vArr[0];
            int viMod = 3 - vArr[0].Length % 3;
            string vsRet = "";
            for (int i = 0; i < vArr[0].Length; i++)
            {
                vsRet = vsRet + vArr[0].Substring(i, 1);
                if (((i + viMod) % 3 == 2) && (i != vArr[0].Length - 1))
                    vsRet += ",";
            }
            if (vArr[0].Length == 0) vsRet = "0";
            int piPrec = Convert.ToInt32(psPrecision);
            if (piPrec > 0) vsRet += ".";
            for (int i = 0; i < piPrec; i++)
            {
                if (i >= vArr[1].Length)
                    vsRet += "0";
                else
                    vsRet += vArr[1].Substring(i, 1);
            }
            if (vsRet.Length > 2 && vsRet.Substring(0, 1).Equals("-") && vsRet.Substring(1, 1).Equals(","))
            {
                vsRet = vsRet.Remove(1, 1);
            }
            return vsRet;
        }

        /// <summary>
        /// 得到字符串的字符长度，一个汉字的长度为2
        /// </summary>
        /// <param name="psStr">需要得到长度的字符串</param>
        /// <returns>字符串的长度</returns>
        public static int GetStrLen(string psStr)
        {
            return System.Text.UnicodeEncoding.Default.GetByteCount(psStr);
        }
        /// <summary>
        /// 验证字符串是否超出规定范围
        /// </summary>
        /// <param name="psStr">字符串</param>
        /// <param name="minLen">最小长度</param>
        /// <param name="maxLen">最大长度</param>
        /// <returns>false:超出范围</returns>
        public static bool IsOverLen(string psStr, int minLen, int maxLen)
        {
            int len = GetStrLen(psStr);
            if (len < minLen || len > maxLen)
                return false;
            return true;
        }
        /// <summary>
        /// 验证字符串是否超长
        /// </summary>
        /// <param name="psStr"></param>
        /// <param name="piLen"></param>
        /// <param name="isNull"></param>
        /// <returns>返回信息，如果为""通过</returns>
        public static string CheckStrLen(string psStr, int piLen, bool isNull)
        {
            if (!isNull && string.IsNullOrEmpty(psStr))
                return "不能为空";
            if (GetStrLen(psStr) > piLen)
                return "长度不能超过" + piLen.ToString();
            return "";
        }
        /// <summary>
        /// 利用正则表达式验证字符串
        /// </summary>
        /// <param name="psStr">字符串</param>
        /// <param name="reg">正则表达式</param>
        /// <returns></returns>
        public static bool CheckStr(string psStr, string reg)
        {
            Regex myReg = new Regex(reg);
            Match m = myReg.Match(psStr);
            return m.Success;
        }
        /// <summary>
        /// 检查是否包含特殊字符
        /// by map
        /// 2003/07/09任丽霞整理
        /// </summary>
        /// <param name="psStr">要验证的字符串</param>
        /// <returns></returns>
        public static string CheckSpecialChar(string psStr)
        {
            //string reg = "['=<>%*()\"]|[-]{2}";
            string reg = "['=<>*\"]|[-]{2}";
            if (CheckStr(psStr, reg))
                return psStr + "中包含不合法字符串";
            else
                return "";
        }
        /// <summary>
        /// 检查是否包含特殊字符  包含小括号
        /// by map
        /// 2003/07/09任丽霞整理
        /// </summary>
        /// <param name="psStr">要验证的字符串</param>
        /// <returns></returns>
        public static string CheckSpecialCharEx(string psStr)
        {
            //string reg = "['=<>%*()\"]|[-]{2}";
            string reg = "['=<>*\"]|[-]{2}";
            if (CheckStr(psStr, reg))
                return psStr + "中包含不合法字符串";
            else
                return "";
        }
        public static string CheckInputStr(string psStr, int piLen)
        {
            string flag1 = CheckSpecialChar(psStr);
            if (flag1 == "")
            {
                string flag2 = CheckStrLen(psStr, piLen, false);
                return flag2;
            }
            else
                return flag1;
        }
        public static string CheckInputStr(string psStr, int piLen, bool isNull)
        {
            string flag1 = CheckSpecialChar(psStr);
            if (flag1 == "")
            {
                string flag2 = CheckStrLen(psStr, piLen, isNull);
                return flag2;
            }
            else
                return flag1;
        }
        /// <summary>
        /// 综合验证字符串
        /// 包括特殊字符，是否为空，是否超过规定长度等
        /// </summary>
        /// <param name="psStr"></param>
        /// <param name="piLen"></param>
        /// <param name="isNull"></param>
        /// <returns>返回为""，则符合；返回不为""，就是不符合的信息。</returns>
        public static string CheckDictStr(string psStr, int piLen, bool isNull)
        {
            string flag1 = CheckSpecialChar(psStr);
            if (flag1 == "")
            {
                string flag2 = CheckStrLen(psStr, piLen, isNull);
                return flag2;
            }
            else
                return flag1;
        }
        /// <summary>
        /// 是否是浮点数 
        /// </summary>
        /// <param name="psStr"></param>
        /// <returns></returns>
        public static bool IsFloat(string psStr)
        {
            double resNum;
            bool isNum = Double.TryParse(
                psStr, System.Globalization.NumberStyles.Float,
                System.Globalization.NumberFormatInfo.InvariantInfo,
                out resNum
                );
            return isNum;
        }
        /// <summary>
        /// 验证邮箱
        /// </summary>
        /// <returns></returns>
        public static bool CheckEmail(string str)
        {
            if (str.Trim() == "")
                return true;
            string reg = "^[a-zA-Z][a-zA-Z0-9._]*@[a-zA-Z0-9.]+[.]+[a-zA-Z0-9]+$";
            return CheckStr(str, reg);
        }
        /// <summary>
        /// 验证邮编
        /// </summary>
        /// <returns></returns>
        public static bool CheckPostalcode(string str)
        {
            if (str.Trim() == "")
                return true;
            string reg = "^[0-9]{6}$";
            return CheckStr(str, reg);
        }
        /// <summary>
        /// 验证主页，网址
        /// </summary>
        /// <returns></returns>
        public static bool CheckHomePage(string str)
        {
            if (str.Trim() == "")
                return true;
            return Regex.IsMatch(str, @"^(http://){0,1}www.(\w)+.(com|net|org|com.cn|net.cn|org.cn|gov.cn|info|biz|tv|cc|cn)$");
        }
        /// <summary>
        /// 验证身份证
        /// </summary>
        /// <param name="psStr"></param>
        /// <returns></returns>
        public static bool CheckIDCard(string psStr)
        {
            if (psStr.Trim() == "")
                return false;
            string reg = "^[0-9]{15,16}$";
            return CheckStr(psStr, reg);
        }
        /// <summary>
        /// [李强 2007.12.16]
        /// 按字节长度截取字符串,一个汉字长度为2Bytes,
        /// 且避免出现汉字乱码  
        /// </summary>
        /// <param name="sSourceContent">被截取的字符串</param>
        /// <param name="iMaxLength">需要的长度</param>
        /// <returns>截取后的字符串</returns>
        public static string SubStr(string sSourceContent, int iMaxLength)
        {
            /// <summary>
            /// 实现思路:
            /// 1、先截取要求的字符串,取最后一位的ASCII码
            /// 2、用substring取最后一位，汉字和字符都是1
            /// 3、判断最后一位的长度，汉字2B，字符1B
            /// 4、若是2，则是完整的汉字，结束
            /// 5、若是1，则判断是否为半个汉字（与127比较）
            /// 6、若是半个汉字，则截取的长度-1，结束
            /// 7、若是字符，结束
            /// [李强 inspurerp@gmail.com]
            /// </summary>
            string sResult = "";
            sSourceContent = sSourceContent.Trim();
            if (sSourceContent == "" || iMaxLength < 1)
            {
                return "";
            }
            string _txt = sSourceContent.Trim();
            byte[] buff = System.Text.Encoding.Default.GetBytes(_txt);
            if (iMaxLength > buff.Length)//若长度超长则用实际的最大值
            {
                iMaxLength = buff.Length;
            }
            byte[] buff_temp = new byte[iMaxLength];
            try
            {
                int iASCIICode = 0;
                for (int j = 0; j < iMaxLength; j++)
                {
                    buff_temp[j] = buff[j];
                    if (j == iMaxLength - 1)
                        iASCIICode = buff[j];
                }
                string _SelText = System.Text.Encoding.Default.GetString(buff_temp);
                sResult = _SelText;
                string _LastSelText = _SelText.Substring(_SelText.Length - 1, 1);//取最后一位
                byte[] buff_last = System.Text.Encoding.Default.GetBytes(_LastSelText);//将最后一位转为字符数组                
                if (buff_last.Length == 1)//最后位置是单个字符
                {
                    if (iASCIICode > 127)//半个汉字
                    {
                        buff_temp = new byte[iMaxLength - 1];
                        for (int p = 0; p < iMaxLength - 1; p++)
                        {
                            buff_temp[p] = buff[p];
                        }
                        sResult = System.Text.Encoding.Default.GetString(buff_temp);
                    }
                }
                buff_last = null;
            }
            catch
            {
                sResult = "";
            }
            finally
            {
                buff = null;
                buff_temp = null;
            }
            return sResult;
        }
        /// <summary>
        /// 10进制->16进制
        /// [李强 2007.12.18]
        /// </summary>
        /// <param name="ten">10进制字符串</param>
        /// <returns></returns>
        public static string Ten2Hex(string ten)
        {
            ulong tenValue = Convert.ToUInt64(ten);
            ulong divValue, resValue;
            string hex = "";
            double dbVal;
            do
            {
                dbVal = tenValue / 16;
                divValue = (ulong)Math.Floor(dbVal);
                resValue = tenValue % 16;
                hex = tenValue2Char(resValue) + hex;
                tenValue = divValue;
            }
            while (tenValue >= 16);
            if (tenValue != 0)
                hex = tenValue2Char(tenValue) + hex;
            return hex;
        }
        private static string tenValue2Char(ulong ten)
        {
            switch (ten)
            {
                case 0:
                case 1:
                case 2:
                case 3:
                case 4:
                case 5:
                case 6:
                case 7:
                case 8:
                case 9:
                    return ten.ToString();
                case 10:
                    return "A";
                case 11:
                    return "B";
                case 12:
                    return "C";
                case 13:
                    return "D";
                case 14:
                    return "E";
                case 15:
                    return "F";
                default:
                    return "";
            }
        }
        /// <summary>
        /// 16进制->10进制
        /// [李强 2007.12.18]
        /// </summary>
        /// <param name="hex">16进制字符串</param>
        /// <returns></returns>
        public static string Hex2Ten(string hex)
        {
            int ten = 0;
            for (int i = 0, j = hex.Length - 1; i < hex.Length; i++)
            {
                ten += HexChar2Value(hex.Substring(i, 1)) * ((int)Math.Pow(16, j));
                j--;
            }
            return ten.ToString();
        }
        private static int HexChar2Value(string hexChar)
        {
            switch (hexChar)
            {
                case "0":
                case "1":
                case "2":
                case "3":
                case "4":
                case "5":
                case "6":
                case "7":
                case "8":
                case "9":
                    return Convert.ToInt32(hexChar);
                case "a":
                case "A":
                    return 10;
                case "b":
                case "B":
                    return 11;
                case "c":
                case "C":
                    return 12;
                case "d":
                case "D":
                    return 13;
                case "e":
                case "E":
                    return 14;
                case "f":
                case "F":
                    return 15;
                default:
                    return 0;
            }
        }
        /// <summary>
        /// 开发者:温兴宝
        /// 开发日期:2008-12-25
        /// 函数描述:显示日期
        /// </summary>
        /// <param name="vsRQ">日期</param>
        /// <param name="vsRQFGF">分隔符</param>
        /// <returns></returns>
        public static string GetShowRQ(string vsRQ, string vsRQFGF)
        {
            if (vsRQ.Length != 8)
            {
                return "日期不是8位字符串";
               
            }
            return vsRQ.Substring(0, 4) + vsRQFGF + vsRQ.Substring(4, 2) + vsRQFGF + vsRQ.Substring(6, 2);
        }

        /// <summary>
        /// 是否银行账户
        /// by whs
        /// </summary>
        /// <param name="psStr">要验证的字符串</param>
        /// <returns>false:不是整数true:是整数</returns>
        public static bool IsYHZH(string psStr)
        {
            if (psStr.Length == 0)
                return false;
            string reg = ("^[0-9\\-_]{0,30}$");
            return CheckStr(psStr, reg);
        }


        #region 得到字符串长度，一个汉字长度为2

        /// <summary>
        /// 得到字符串长度，一个汉字长度为2
        /// </summary>
        /// <param name="inputString">参数字符串</param>
        /// <returns></returns>
        public static int StrLength(string inputString)
        {
            System.Text.ASCIIEncoding ascii = new System.Text.ASCIIEncoding();
            int tempLen = 0;
            byte[] s = ascii.GetBytes(inputString);
            for (int i = 0; i < s.Length; i++)
            {
                if ((int)s[i] == 63)
                    tempLen += 2;
                else
                    tempLen += 1;
            }
            return tempLen;
        }

        #endregion 得到字符串长度，一个汉字长度为2

        #region 截取指定长度字符串

        /// <summary>
        /// 截取指定长度字符串
        /// </summary>
        /// <param name="inputString">要处理的字符串</param>
        /// <param name="len">指定长度</param>
        /// <returns>返回处理后的字符串</returns>
        public static string ClipString(string inputString, int len)
        {
            bool isShowFix = false;
            if (len % 2 == 1)
            {
                isShowFix = true;
                len--;
            }
            System.Text.ASCIIEncoding ascii = new System.Text.ASCIIEncoding();
            int tempLen = 0;
            string tempString = "";
            byte[] s = ascii.GetBytes(inputString);
            for (int i = 0; i < s.Length; i++)
            {
                if ((int)s[i] == 63)
                    tempLen += 2;
                else
                    tempLen += 1;

                try
                {
                    tempString += inputString.Substring(i, 1);
                }
                catch
                {
                    break;
                }

                if (tempLen > len)
                    break;
            }

            byte[] mybyte = System.Text.Encoding.Default.GetBytes(inputString);
            if (isShowFix && mybyte.Length > len)
                tempString += "…";
            return tempString;
        }

        #endregion 截取指定长度字符串

        #region 获得两个日期的间隔

        /// <summary>
        /// 获得两个日期的间隔
        /// </summary>
        /// <param name="DateTime1">日期一。</param>
        /// <param name="DateTime2">日期二。</param>
        /// <returns>日期间隔TimeSpan。</returns>
        public static TimeSpan DateDiff(DateTime DateTime1, DateTime DateTime2)
        {
            TimeSpan ts1 = new TimeSpan(DateTime1.Ticks);
            TimeSpan ts2 = new TimeSpan(DateTime2.Ticks);
            TimeSpan ts = ts1.Subtract(ts2).Duration();
            return ts;
        }

        #endregion 获得两个日期的间隔

        #region 格式化日期时间

        /// <summary>
        /// 格式化日期时间
        /// </summary>
        /// <param name="dateTime1">日期时间</param>
        /// <param name="dateMode">显示模式</param>
        /// <returns>0-9种模式的日期</returns>
        public static string FormatDate(DateTime dateTime1, string dateMode)
        {
            switch (dateMode)
            {
                case "0":
                    return dateTime1.ToString("yyyy-MM-dd");

                case "1":
                    return dateTime1.ToString("yyyy-MM-dd HH:mm:ss");

                case "2":
                    return dateTime1.ToString("yyyy/MM/dd");

                case "3":
                    return dateTime1.ToString("yyyy年MM月dd日");

                case "4":
                    return dateTime1.ToString("MM-dd");

                case "5":
                    return dateTime1.ToString("MM/dd");

                case "6":
                    return dateTime1.ToString("MM月dd日");

                case "7":
                    return dateTime1.ToString("yyyy-MM");

                case "8":
                    return dateTime1.ToString("yyyy/MM");

                case "9":
                    return dateTime1.ToString("yyyy年MM月");

                default:
                    return dateTime1.ToString();
            }
        }

        #endregion 格式化日期时间

        #region 得到随机日期

        /// <summary>
        /// 得到随机日期
        /// </summary>
        /// <param name="time1">起始日期</param>
        /// <param name="time2">结束日期</param>
        /// <returns>间隔日期之间的 随机日期</returns>
        public static DateTime GetRandomTime(DateTime time1, DateTime time2)
        {
            Random random = new Random();
            DateTime minTime = new DateTime();
            DateTime maxTime = new DateTime();

            System.TimeSpan ts = new System.TimeSpan(time1.Ticks - time2.Ticks);

            // 获取两个时间相隔的秒数
            double dTotalSecontds = ts.TotalSeconds;
            int iTotalSecontds = 0;

            if (dTotalSecontds > System.Int32.MaxValue)
            {
                iTotalSecontds = System.Int32.MaxValue;
            }
            else if (dTotalSecontds < System.Int32.MinValue)
            {
                iTotalSecontds = System.Int32.MinValue;
            }
            else
            {
                iTotalSecontds = (int)dTotalSecontds;
            }

            if (iTotalSecontds > 0)
            {
                minTime = time2;
                maxTime = time1;
            }
            else if (iTotalSecontds < 0)
            {
                minTime = time1;
                maxTime = time2;
            }
            else
            {
                return time1;
            }

            int maxValue = iTotalSecontds;

            if (iTotalSecontds <= System.Int32.MinValue)
                maxValue = System.Int32.MinValue + 1;

            int i = random.Next(System.Math.Abs(maxValue));

            return minTime.AddSeconds(i);
        }

        #endregion 得到随机日期

        #region HTML转行成TEXT

        /// <summary>
        /// HTML转行成TEXT
        /// </summary>
        /// <param name="strHtml"></param>
        /// <returns></returns>
        public static string HtmlToTxt(string strHtml)
        {
            string[] aryReg ={
            @"<script[^>]*?>.*?</script>",
            @"<(\/\s*)?!?((\w+:)?\w+)(\w+(\s*=?\s*(([""'])(\\[""'tbnr]|[^\7])*?\7|\w+)|.{0})|\s)*?(\/\s*)?>",
            @"([\r\n])[\s]+",
            @"&(quot|#34);",
            @"&(amp|#38);",
            @"&(lt|#60);",
            @"&(gt|#62);",
            @"&(nbsp|#160);",
            @"&(iexcl|#161);",
            @"&(cent|#162);",
            @"&(pound|#163);",
            @"&(copy|#169);",
            @"&#(\d+);",
            @"-->",
            @"<!--.*\n"
            };

            string newReg = aryReg[0];
            string strOutput = strHtml;
            for (int i = 0; i < aryReg.Length; i++)
            {
                Regex regex = new Regex(aryReg[i], RegexOptions.IgnoreCase);
                strOutput = regex.Replace(strOutput, string.Empty);
            }

            strOutput.Replace("<", "");
            strOutput.Replace(">", "");
            strOutput.Replace("\r\n", "");

            return strOutput;
        }

        #endregion HTML转行成TEXT

        #region 判断对象是否为空

        /// <summary>
        /// 判断对象是否为空，为空返回true
        /// </summary>
        /// <typeparam name="T">要验证的对象的类型</typeparam>
        /// <param name="data">要验证的对象</param>
        public static bool IsNullOrEmpty<T>(T data)
        {
            //如果为null
            if (data == null)
            {
                return true;
            }

            //如果为""
            if (data.GetType() == typeof(String))
            {
                if (string.IsNullOrEmpty(data.ToString().Trim()))
                {
                    return true;
                }
            }

            //如果为DBNull
            if (data.GetType() == typeof(DBNull))
            {
                return true;
            }

            //不为空
            return false;
        }

        /// <summary>
        /// 判断对象是否为空，为空返回true
        /// </summary>
        /// <param name="data">要验证的对象</param>
        public static bool IsNullOrEmpty(object data)
        {
            //如果为null
            if (data == null)
            {
                return true;
            }

            //如果为""
            if (data.GetType() == typeof(String))
            {
                if (string.IsNullOrEmpty(data.ToString().Trim()))
                {
                    return true;
                }
            }

            //如果为DBNull
            if (data.GetType() == typeof(DBNull))
            {
                return true;
            }

            //不为空
            return false;
        }

        #endregion 判断对象是否为空

        #region 验证IP地址是否合法

        /// <summary>
        /// 验证IP地址是否合法
        /// </summary>
        /// <param name="ip">要验证的IP地址</param>
        public static bool IsIP(string ip)
        {
            //如果为空，认为验证合格
            if (IsNullOrEmpty(ip))
            {
                return true;
            }

            //清除要验证字符串中的空格
            ip = ip.Trim();

            //模式字符串
            string pattern = @"^((2[0-4]\d|25[0-5]|[01]?\d\d?)\.){3}(2[0-4]\d|25[0-5]|[01]?\d\d?)$";

            //验证
            return RegexHelper.IsMatch(ip, pattern);
        }

        #endregion 验证IP地址是否合法

        #region 验证EMail是否合法

        /// <summary>
        /// 验证EMail是否合法
        /// </summary>
        /// <param name="email">要验证的Email</param>
        public static bool IsEmail(string email)
        {
            //如果为空，认为验证不合格
            if (IsNullOrEmpty(email))
            {
                return false;
            }

            //清除要验证字符串中的空格
            email = email.Trim();

            //模式字符串
            string pattern = @"^([0-9a-zA-Z]([-.\w]*[0-9a-zA-Z])*@([0-9a-zA-Z][-\w]*[0-9a-zA-Z]\.)+[a-zA-Z]{2,9})$";

            //验证
            return RegexHelper.IsMatch(email, pattern);
        }

        #endregion 验证EMail是否合法

        #region 验证是否为整数

        /// <summary>
        /// 验证是否为整数 如果为空，认为验证不合格 返回false
        /// </summary>
        /// <param name="number">要验证的整数</param>
        public static bool IsInt(string number)
        {
            //如果为空，认为验证不合格
            if (IsNullOrEmpty(number))
            {
                return false;
            }

            //清除要验证字符串中的空格
            number = number.Trim();

            //模式字符串
            string pattern = @"^[0-9]+[0-9]*$";
            
            //验证
            return RegexHelper.IsMatch(number, pattern);
        }

        #endregion 验证是否为整数

        #region 验证是否为数字

        /// <summary>
        /// 验证是否为数字
        /// </summary>
        /// <param name="number">要验证的数字</param>
        public static bool IsNumber(string number)
        {
            //如果为空，认为验证不合格
            if (IsNullOrEmpty(number))
            {
                return false;
            }

            //清除要验证字符串中的空格
            number = number.Trim();

            //模式字符串
            string pattern = @"^[0-9]+[0-9]*[.]?[0-9]*$";

            //验证
            return RegexHelper.IsMatch(number, pattern);
        }

        #endregion 验证是否为数字

        #region 验证日期是否合法

        /// <summary>
        /// 验证日期是否合法,对不规则的作了简单处理
        /// </summary>
        /// <param name="date">日期</param>
        public static bool IsDate(ref string date)
        {
            //如果为空，认为验证合格
            if (IsNullOrEmpty(date))
            {
                return true;
            }

            //清除要验证字符串中的空格
            date = date.Trim();

            //替换\
            date = date.Replace(@"\", "-");
            //替换/
            date = date.Replace(@"/", "-");

            //如果查找到汉字"今",则认为是当前日期
            if (date.IndexOf("今") != -1)
            {
                date = DateTime.Now.ToString();
            }

            try
            {
                //用转换测试是否为规则的日期字符
                date = Convert.ToDateTime(date).ToString("d");
                return true;
            }
            catch
            {
                //如果日期字符串中存在非数字，则返回false
                if (!IsInt(date))
                {
                    return false;
                }

                #region 对纯数字进行解析

                //对8位纯数字进行解析
                if (date.Length == 8)
                {
                    //获取年月日
                    string year = date.Substring(0, 4);
                    string month = date.Substring(4, 2);
                    string day = date.Substring(6, 2);

                    //验证合法性
                    if (Convert.ToInt32(year) < 1900 || Convert.ToInt32(year) > 2100)
                    {
                        return false;
                    }
                    if (Convert.ToInt32(month) > 12 || Convert.ToInt32(day) > 31)
                    {
                        return false;
                    }

                    //拼接日期
                    date = Convert.ToDateTime(year + "-" + month + "-" + day).ToString("d");
                    return true;
                }

                //对6位纯数字进行解析
                if (date.Length == 6)
                {
                    //获取年月
                    string year = date.Substring(0, 4);
                    string month = date.Substring(4, 2);

                    //验证合法性
                    if (Convert.ToInt32(year) < 1900 || Convert.ToInt32(year) > 2100)
                    {
                        return false;
                    }
                    if (Convert.ToInt32(month) > 12)
                    {
                        return false;
                    }

                    //拼接日期
                    date = Convert.ToDateTime(year + "-" + month).ToString("d");
                    return true;
                }

                //对5位纯数字进行解析
                if (date.Length == 5)
                {
                    //获取年月
                    string year = date.Substring(0, 4);
                    string month = date.Substring(4, 1);

                    //验证合法性
                    if (Convert.ToInt32(year) < 1900 || Convert.ToInt32(year) > 2100)
                    {
                        return false;
                    }

                    //拼接日期
                    date = year + "-" + month;
                    return true;
                }

                //对4位纯数字进行解析
                if (date.Length == 4)
                {
                    //获取年
                    string year = date.Substring(0, 4);

                    //验证合法性
                    if (Convert.ToInt32(year) < 1900 || Convert.ToInt32(year) > 2100)
                    {
                        return false;
                    }

                    //拼接日期
                    date = Convert.ToDateTime(year).ToString("d");
                    return true;
                }

                #endregion 对纯数字进行解析

                return false;
            }
        }

        #endregion 验证日期是否合法

        #region 验证身份证是否合法

        /// <summary>
        /// 验证身份证是否合法
        /// </summary>
        /// <param name="idCard">要验证的身份证</param>
        public static bool IsIdCard(string idCard)
        {
            //如果为空，认为验证合格
            if (IsNullOrEmpty(idCard))
            {
                return true;
            }

            //清除要验证字符串中的空格
            idCard = idCard.Trim();

            //模式字符串
            StringBuilder pattern = new StringBuilder();
            pattern.Append(@"^(11|12|13|14|15|21|22|23|31|32|33|34|35|36|37|41|42|43|44|45|46|");
            pattern.Append(@"50|51|52|53|54|61|62|63|64|65|71|81|82|91)");
            pattern.Append(@"(\d{13}|\d{15}[\dx])$");

            //验证
            return RegexHelper.IsMatch(idCard, pattern.ToString());
        }

        #endregion 验证身份证是否合法

        #region 检测客户的输入中是否有危险字符串

        /// <summary>
        /// 检测客户输入的字符串是否有效,并将原始字符串修改为有效字符串或空字符串。
        /// 当检测到客户的输入中有攻击性危险字符串,则返回false,有效返回true。
        /// </summary>
        /// <param name="input">要检测的字符串</param>
        public static bool IsValidInput(ref string input)
        {
            try
            {
                if (IsNullOrEmpty(input))
                {
                    //如果是空值,则跳出
                    return true;
                }
                else
                {
                    //替换单引号
                    input = input.Replace("'", "''").Trim();

                    //检测攻击性危险字符串
                    string testString = "and |or |exec |insert |select |delete |update |count |chr |mid |master |truncate |char |declare ";
                    string[] testArray = testString.Split('|');
                    foreach (string testStr in testArray)
                    {
                        if (input.ToLower().IndexOf(testStr) != -1)
                        {
                            //检测到攻击字符串,清空传入的值
                            input = "";
                            return false;
                        }
                    }

                    //未检测到攻击字符串
                    return true;
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        #endregion 检测客户的输入中是否有危险字符串
    }
}
