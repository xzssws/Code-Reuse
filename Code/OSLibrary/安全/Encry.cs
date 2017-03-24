﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace OSLibrary.浪潮
{
    /// <summary>
    /// 浪潮密钥
    /// </summary>
    class Encry
    {
        public static string passwordExpand(string strPass)
        {
            /*功能:将指定的口令经一定算法解密
                                   参数:psPass 指定的口令
                                   返回:—1，1成功与否
                                   算法描述:根据gfPassCompress 的加密算法反向解密
                                 */
            /*求出变码差值*/
            string strBegin = strPass.Substring(0, 1);
            string strEnd = strPass.Substring(strPass.Length - 1, 1);
            char chBegin = strBegin[0];
            char chEnd = strEnd[0];
            //			strcpy(chBegin,strBegin.GetBuffer(1));
            //			strcpy(chEnd,strEnd.GetBuffer(1));
            //
            //			strBegin.ReleaseBuffer();
            //			strEnd.ReleaseBuffer();

            int nBegin = (int)chBegin;
            int nEnd = (int)chEnd;
            int nCz = nEnd - nBegin;


            strPass = strPass.Substring(1, strPass.Length - 2);

            string strFirst, strSecond;
            char chFirst, chSecond;
            int nNum = 0;
            int nTmp;
            string strDesPass = "";
            for (int i = strPass.Length - 1; i >= 0; i -= 2)
            {
                nNum++;
                if (nNum % 2 == 1)
                {
                    strFirst = strPass.Substring(i - 1, 1);
                    strSecond = strPass.Substring(i, 1);
                }
                else
                {
                    strFirst = strPass.Substring(i, 1);
                    strSecond = strPass.Substring(i - 1, 1);
                }

                chFirst = strFirst[0];
                chSecond = strSecond[0];
                //				strcpy(chFirst,strFirst.GetBuffer(1));
                //				strcpy(chSecond,strSecond.GetBuffer(1));
                //				strFirst.ReleaseBuffer();
                //				strEnd.ReleaseBuffer();

                nTmp = ((int)chFirst - nBegin) * 26 + ((int)chSecond - 97) - nCz;

                string strTmp = new string((char)nTmp, 1);
                strDesPass += strTmp;
            }
            //_messageResponseBroker.ShowMessage(strDesPass);
            return strDesPass;
        }
        public static string passwordExpress(string password)
        {


            /*检查口令是否合法，如不合法则返回*/
            //Validate password is correct
            Regex Regex_AdminPassword = new Regex(@"^[a-zA-Z][a-zA-Z_0-9]{5,30}$");
            password = password.Trim();
            if (Regex_AdminPassword.Match(password).Success == false)
            {
                throw new ArgumentOutOfRangeException("string password", password, "密码只能由6-30位英文字母，数字和下划线构成。\n而且首位字符只能是英文字母。");
            }

            string strDesPass = "";
            int nLen = password.Length;
            int nAscSum = 0;
            char[] szPass = new char[30];
            szPass = password.ToCharArray();
            int nTmp;

            int i;

            for (i = 0; i < nLen; i++)
            {
                //有效字符
                int nAsc = szPass[i];

                if ((nAsc < 48/*0*/ || nAsc > 122/*z*/)
                    || (nAsc > 57/*9*/ && nAsc < 65/*A*/)
                    || (nAsc > 90/*Z*/ && nAsc < 97/*a*/))
                {
                    throw new Exception("密码无效");
                }

                nTmp = (int)szPass[i];

                nAscSum += nTmp;
            }

            int nSumMod = nAscSum % 26;
            int nMaxMod = nSumMod;

            string strMod = "";
            string strDev = "";
            //将Pass中各位的ASCII值加nSumMod然后分别对26取模，求出商值及余数	
            for (i = 0; i < nLen; i++)
            {
                nTmp = (int)szPass[i] + nSumMod;

                int nDev = nTmp / 26; //商
                int nMod = nTmp - nDev * 26; //余
                if (nDev > nMaxMod)
                {
                    nMaxMod = nDev;
                }

                //将各余数据加97（'a'的ASCII码）转换字母
                strMod += ",";
                strMod += new string((char)(nMod + 97), 1);
                strDev += ",";
                char[] chTmp = new char[3];
                strDev += Convert.ToString(nDev);//+10);//itoa(nDev,chTmp,10);
            }

            //随机求出ASCII码差值能表示nMaxMod的任意两字母strBegin,strEnd
            Random ran = new Random();
            //int nTmp;
            nTmp = ran.Next();//rand();
            nTmp = nTmp % (26 - nMaxMod);
            char strBegin = (char)(nTmp + 97);
            char strEnd = (char)(nTmp + nSumMod + 97);

            //检验一下strBegin,strEnd
            if ((strBegin < 'A' || strBegin > 'z') || (strBegin > 'Z' && strBegin < 'a'))
            {
                string ss = "";
                ss += Convert.ToString(nTmp + 96);
                //itoa(nTmp+96,ss.GetBuffer(1),10);
                //_trace(ss.GetBuffer(1));
                //ss.ReleaseBuffer();
            }
            if ((strEnd < 'A' || strEnd > 'z') || (strEnd > 'Z' && strEnd < 'a'))
            {
                string ss = "";
                ss += Convert.ToString(nTmp + nSumMod + 96);
                //itoa(nTmp+96+nSumMod,ss.GetBuffer(1),10);
                //_trace(ss.GetBuffer(1));
                //ss.ReleaseBuffer();
            }

            //求m_strSrcPass中各字符对应的另一字母,
            //该字母ASCII码为strBegin的ASCII码加
            //各位字符上面求出的商值.
            string strTmp = "";
            int nPos = strDev.IndexOf(",");
            char[] chBegin = new char[1];
            chBegin[0] = strBegin;
            //strcpy(chBegin,strBegin.GetBuffer(1));
            //strBegin.ReleaseBuffer();

            while (nPos != -1)
            {
                string szTmp = strDev.Substring(nPos + 1, 1);
                nTmp = (int)chBegin[0] + Convert.ToInt32(szTmp);//atoi(szTmp.GetBuffer(1));
                //szTmp.ReleaseBuffer();

                strTmp += ",";
                strTmp += (char)nTmp;

                nPos = strDev.IndexOf(",", nPos + 1);
            }
            strDev = strTmp;

            //形成加密口令
            i = 0;
            int nModPos = strMod.IndexOf(",");
            int nDevPos = strDev.IndexOf(",");
            while (nModPos != -1 && nDevPos != -1)
            {
                i++;
                if (i % 2 == 1)
                {
                    strDesPass = strDev.Substring(nDevPos + 1, 1) + strMod.Substring(nModPos + 1, 1) + strDesPass;
                }
                else
                {
                    strDesPass = strMod.Substring(nModPos + 1, 1) + strDev.Substring(nDevPos + 1, 1) + strDesPass;
                }

                nModPos = strMod.IndexOf(",", nModPos + 1);
                nDevPos = strDev.IndexOf(",", nDevPos + 1);
            }

            strDesPass = strBegin + strDesPass + strEnd;

            return strDesPass;
        }
        public static string CheckPass(string sPass)
        {
            string sMsg = string.Empty;
            if (sPass.Length < 6)
            {
                sMsg = "短";
                return sMsg;
            }

            string R1 = @"\d";
            string R2 = @"[a-z]";
            string R3 = @"[A-Z]";
            string R4 = @"\W";

            Regex Rg1 = new Regex(R1);
            Regex Rg2 = new Regex(R2);
            Regex Rg3 = new Regex(R3);
            Regex Rg4 = new Regex(R4);

            int i1, i2, i3, i4;
            if (Rg1.IsMatch(sPass, 0))
                i1 = 1;
            else i1 = 0;

            if (Rg2.IsMatch(sPass, 0))
                i2 = 1;
            else i2 = 0;

            if (Rg3.IsMatch(sPass, 0))
                i3 = 1;
            else i3 = 0;

            if (Rg4.IsMatch(sPass, 0))
                i4 = 1;
            else i4 = 0;
            int i = i1 + i2 + i3 + i4;
            if (i == 1)
            {
                sMsg = "弱";
            }
            else if (i == 2)
            {
                sMsg = "中";
            }
            else
            {
                sMsg = "强";
            }
            return sMsg;
        }
    }
}
