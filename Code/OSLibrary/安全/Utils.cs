using System;
using System.Text.RegularExpressions;

namespace Genersoft.ZJGL.Servers.PublicCom
{
	/// <summary>
	/// Utils ��ժҪ˵����
	/// </summary>
	public class Utils
	{
		public Utils()
		{
			//
			// TODO: �ڴ˴���ӹ��캯���߼�
			//
		}
		//-----------------------------------------------------------------------
		//	���ܣ���ָ���Ŀ��һ���㷨����
		//	  ������bsPass ָ���Ŀ���
		//	  ���أ����ܺ������.
		//	  �㷨������
		//		��1��ֻ����bsPass�а�����ĸ����Сд���ɣ�����Ϊ�����ַ����������
		//		��2�����bsPass�и�λ��ASCIIֵ֮�Ͷ�26ȡģ���������nMod��
		//		��3����bsPass�и�λ��ASCIIֵ��nModȻ��ֱ��26ȡģ�������ֵ������
		//		��4�����������ݼ�97��'a'��ASCII�룩ת����ĸ
		//		��5�������������ĸchBegin,chEnd��Ҫ�������ĸASCII֮����ΪnMod
		//		��6����bsPass�и��ַ���Ӧ����һ��ĸ������ĸASCII��ΪchBegin��ASCII���
		//			��λ�ַ������������ֵ
		//		��7����6�������ĸ�ֱ�ӣ�4�������ĸ��ֵ���γ��¿�����м䲿��
		//		��8���γɼ��ܺ����Ϊ��chBegin+��7��+chEnd
		//--------------------------------------------------------------------------
		public static string passwordExpress(string password)
		{
			

			/*�������Ƿ�Ϸ����粻�Ϸ��򷵻�*/
			//Validate password is correct
			Regex Regex_AdminPassword = new Regex(@"^[a-zA-Z][a-zA-Z_0-9]{5,30}$");
			password = password.Trim();
			if(Regex_AdminPassword.Match(password).Success == false)
			{
				throw new ArgumentOutOfRangeException("string password", password, "����ֻ����6-30λӢ����ĸ�����ֺ��»��߹��ɡ�\n������λ�ַ�ֻ����Ӣ����ĸ��");
			}

			string strDesPass="";
			int nLen = password.Length;
			int nAscSum=0;
			char[] szPass=new char[30];
			szPass = password.ToCharArray();
			int nTmp;

			int i;

			for(i=0; i<nLen; i++)
			{
				//��Ч�ַ�
				int nAsc=szPass[i];

				if((nAsc<48/*0*/ || nAsc>122/*z*/) 
					|| (nAsc>57/*9*/ && nAsc<65/*A*/)
					|| (nAsc>90/*Z*/ && nAsc<97/*a*/))
				{
					throw new Exception("������Ч");
				}
		
				nTmp=(int)szPass[i];

				nAscSum+=nTmp;
			}

			int nSumMod=nAscSum%26;
			int nMaxMod=nSumMod;

			string strMod="";
			string strDev="";
            //��Pass�и�λ��ASCIIֵ��nSumModȻ��ֱ��26ȡģ�������ֵ������	
			for(i=0 ; i<nLen; i++)
			{
				nTmp=(int)szPass[i]+nSumMod;
		
				int nDev=nTmp/26; //��
				int nMod=nTmp-nDev*26; //��
				if(nDev>nMaxMod)
				{
					nMaxMod=nDev;
				}

				//���������ݼ�97��'a'��ASCII�룩ת����ĸ
				strMod+=",";
				strMod+=new string((char)(nMod+97),1);
				strDev+=",";
				char[] chTmp=new char[3];
				strDev+=Convert.ToString(nDev);//+10);//itoa(nDev,chTmp,10);
			}

			//������ASCII���ֵ�ܱ�ʾnMaxMod����������ĸstrBegin,strEnd
			Random ran=new Random();
			//int nTmp;
			nTmp=ran.Next();//rand();
			nTmp=nTmp%(26-nMaxMod);
			char strBegin=(char)(nTmp+97);
			char strEnd=(char)(nTmp+nSumMod+97);

			//����һ��strBegin,strEnd
			if((strBegin<'A' || strBegin >'z') || (strBegin>'Z' && strBegin<'a'))
			{
				string ss="";
				ss+=Convert.ToString(nTmp+96);
				//itoa(nTmp+96,ss.GetBuffer(1),10);
				//_trace(ss.GetBuffer(1));
				//ss.ReleaseBuffer();
			}
			if((strEnd<'A' || strEnd >'z') || (strEnd>'Z' && strEnd<'a'))
			{
				string ss="";
				ss+=Convert.ToString(nTmp+nSumMod+96);
				//itoa(nTmp+96+nSumMod,ss.GetBuffer(1),10);
				//_trace(ss.GetBuffer(1));
				//ss.ReleaseBuffer();
			}

			//��m_strSrcPass�и��ַ���Ӧ����һ��ĸ,
			//����ĸASCII��ΪstrBegin��ASCII���
			//��λ�ַ������������ֵ.
			string strTmp = "";
			int nPos=strDev.IndexOf(",");
			char[] chBegin=new char[1];
			chBegin[0]=strBegin;
			//strcpy(chBegin,strBegin.GetBuffer(1));
			//strBegin.ReleaseBuffer();

			while(nPos!=-1)
			{
				string szTmp=strDev.Substring(nPos+1,1);
				nTmp=(int)chBegin[0]+Convert.ToInt32(szTmp);//atoi(szTmp.GetBuffer(1));
				//szTmp.ReleaseBuffer();

				strTmp+=",";
				strTmp+=(char)nTmp;

				nPos=strDev.IndexOf(",",nPos+1);
			}
			strDev=strTmp;

			//�γɼ��ܿ���
			i=0;
			int nModPos=strMod.IndexOf(",");
			int nDevPos=strDev.IndexOf(",");
			while(nModPos!=-1 && nDevPos!=-1)
			{
				i++;
				if(i%2==1)
				{
					strDesPass=strDev.Substring(nDevPos+1,1)+strMod.Substring(nModPos+1,1)+strDesPass;
				}
				else
				{
					strDesPass=strMod.Substring(nModPos+1,1)+strDev.Substring(nDevPos+1,1)+strDesPass;
				}

				nModPos=strMod.IndexOf(",",nModPos+1);
				nDevPos=strDev.IndexOf(",",nDevPos+1);
			}

			strDesPass=strBegin+strDesPass+strEnd;

			return strDesPass;
		}
		public static string  passwordExpand(string strPass)
		{
			/*���ܣ���ָ���Ŀ��һ���㷨����
								   ������psPass ָ���Ŀ���
								   ���أ���1��1�ɹ����
								   �㷨����������gfPassCompress �ļ����㷨�������
								 */
			/*��������ֵ*/
			string strBegin=strPass.Substring(0,1);
			string strEnd=strPass.Substring(strPass.Length - 1,1);
			char chBegin=strBegin[0];
			char chEnd=strEnd[0];
			//			strcpy(chBegin,strBegin.GetBuffer(1));
			//			strcpy(chEnd,strEnd.GetBuffer(1));
			//
			//			strBegin.ReleaseBuffer();
			//			strEnd.ReleaseBuffer();

			int nBegin=(int)chBegin;
			int nEnd=(int)chEnd;
			int nCz=nEnd-nBegin;


			strPass=strPass.Substring(1,strPass.Length-2);

			string strFirst,strSecond;
			char chFirst,chSecond;
			int nNum=0;
			int nTmp;
			string strDesPass="";
			for(int i=strPass.Length-1; i>=0 ; i-=2)
			{
				nNum++;
				if(nNum%2==1)
				{
					strFirst=strPass.Substring(i-1,1);
					strSecond=strPass.Substring(i,1);
				}
				else
				{
					strFirst=strPass.Substring(i,1);
					strSecond=strPass.Substring(i-1,1);
				}

				chFirst=strFirst[0];
				chSecond=strSecond[0];
				//				strcpy(chFirst,strFirst.GetBuffer(1));
				//				strcpy(chSecond,strSecond.GetBuffer(1));
				//				strFirst.ReleaseBuffer();
				//				strEnd.ReleaseBuffer();

				nTmp=((int)chFirst-nBegin)*26+((int)chSecond-97)-nCz; 

				string strTmp=new string((char)nTmp,1);
				strDesPass+=strTmp;
			}
			//_messageResponseBroker.ShowMessage(strDesPass);
			return strDesPass;
		}
	}
}
