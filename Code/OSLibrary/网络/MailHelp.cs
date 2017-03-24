using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Mail;
using System.Text;

namespace OSLibrary.网络
{
    /// <summary>
    /// sendEmail 的摘要说明
    /// </summary>
    public static class sendEmail
    {

        static sendEmail()
        {
            //
            // TODO: 在此处添加构造函数逻辑
            //
        }

        /// <summary>
        /// 发送邮件程序
        /// </summary>
        /// <param name="from">发送人邮件地址</param>
        /// <param name="fromname">发送人显示名称</param>
        /// <param name="to">发送给谁（邮件地址）</param>
        /// <param name="subject">标题</param>
        /// <param name="body">内容</param>
        /// <param name="username">邮件登录名</param>
        /// <param name="password">邮件密码</param>
        /// <param name="server">邮件服务器 smtp服务器地址</param>
        /// <param name="fujian">附件</param>
        /// <returns>send ok</returns>
        /// 调用方法 SendMail("abc@126.com", "某某人", "cba@126.com", "你好", "我测试下邮件", "邮箱登录名", "邮箱密码", "smtp.126.com", "");
        /// 
        public static string SendMail(string from, string fromname, string to, string subject, string body, string username, string password, string server, string fujian)
        {
            try
            {
                //邮件发送类
                MailMessage mail = new MailMessage();
                //是谁发送的邮件
                mail.From = new MailAddress(from, fromname);
                //发送给谁
                mail.To.Add(to);
                //标题
                mail.Subject = subject;
                //内容编码
                mail.BodyEncoding = Encoding.Default;
                //发送优先级
                mail.Priority = MailPriority.High;
                //邮件内容
                mail.Body = body;
                //是否HTML形式发送
                mail.IsBodyHtml = true;
                //附件
                if (fujian.Length > 0)
                {
                    mail.Attachments.Add(new Attachment(fujian));
                }
                //邮件服务器和端口
                SmtpClient smtp = new SmtpClient(server, 25);
                smtp.UseDefaultCredentials = true;
                //指定发送方式
                smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
                //指定登录名和密码
                smtp.Credentials = new System.Net.NetworkCredential(username, password);
                //mail.Fields.Add("http://schemas.microsoft.com/cdo/configuration/smtpauthenticate", "1"); //basic authentication 
                //mail.Fields.Add("http://schemas.microsoft.com/cdo/configuration/sendusername", username); //set your username here 
                //mail.Fields.Add("http://schemas.microsoft.com/cdo/configuration/sendpassword", password); //set your password here
                //超时时间
                smtp.EnableSsl = false;
                smtp.Timeout = 10000;
                smtp.Send(mail);
                return "成功发送请注意查收";
            }
            catch (Exception exp)
            {
                return exp.Message;
            }
        }

        //读取指定URL地址的HTML，用来以后发送网页用
        public static string ScreenScrapeHtml(string url)
        {
            //读取stream并且对于中文页面防止乱码
            StreamReader reader = new StreamReader(System.Net.WebRequest.Create(url).GetResponse().GetResponseStream(), System.Text.Encoding.UTF8);
            string str = reader.ReadToEnd();
            reader.Close();
            return str;
        }

        ///   <summary>
        ///   发送邮件
        ///   </summary>
        ///   <param   name= "server "> smtp地址 </param>
        ///   <param   name= "username "> 用户名 </param>
        ///   <param   name= "password "> 密码 </param>
        ///   <param   name= "from "> 发信人地址 </param>
        ///   <param   name= "to "> 收信人地址 </param>
        ///   <param   name= "subject "> 邮件标题 </param>
        ///   <param   name= "body "> 邮件正文 </param>
        ///   <param   name= "IsHtml "> 是否是HTML格式的邮件 </param>
        public static string SendMail(string from, string to, string subject, string body, string server, string username, string password, bool IsHtml)
        {
            try
            {
                //设置SMTP 验证,端口默认为25，如果需要其他请修改
                SmtpClient mailClient = new SmtpClient(server, 25);

                //指定如何发送电子邮件。
                //Network   电子邮件通过网络发送到   SMTP   服务器。    
                //PickupDirectoryFromIis   将电子邮件复制到挑选目录，然后通过本地   Internet   信息服务   (IIS)   传送。    
                //SpecifiedPickupDirectory 将电子邮件复制到 SmtpClient.PickupDirectoryLocation 属性指定的目录，然后由外部应用程序传送。  
                mailClient.DeliveryMethod = System.Net.Mail.SmtpDeliveryMethod.Network;

                //创建邮件对象
                MailMessage mailMessage = new MailMessage(from, to, subject, body);
                //定义邮件正文，主题的编码方式
                mailMessage.BodyEncoding = System.Text.Encoding.GetEncoding("gb2312");
                mailMessage.SubjectEncoding = System.Text.Encoding.GetEncoding("gb2312");
                //mailMessage.BodyEncoding = Encoding.Default;
                //获取或者设置一个值，该值表示电子邮件正文是否为HTML
                mailMessage.IsBodyHtml = IsHtml;
                //指定邮件的优先级
                mailMessage.Priority = MailPriority.High;

                //发件人身份验证,否则163   发不了
                //表示当前登陆用户的默认凭据进行身份验证，并且包含用户名密码
                mailClient.UseDefaultCredentials = true;
                mailClient.Credentials = new System.Net.NetworkCredential(username, password);
                //发送
                mailClient.Send(mailMessage);
                return "发送成功";
            }
            catch (Exception exp)
            {
                return exp.Message;
            }
        }

        //发送plaintxt
        public static void SendText(string from, string to, string subject, string body, string server, string username, string password)
        {
            SendMail(from, to, subject, body, server, username, password, false);
        }

        //发送HTML内容
        public static string SendHtml(string from, string to, string subject, string body, string server, string username, string password)
        {
            return SendMail(from, to, subject, body, server, username, password, true);
        }

        //发送制定网页
        public static string SendWebUrl(string from, string to, string subject, string server, string username, string password, string url)
        {
            //发送制定网页
            return SendHtml(from, to, subject, ScreenScrapeHtml(url), server, username, password);
        }
    }

}
