using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net.Mail;


namespace MyHelper4Web
{
    public class MyEmailHelper
    {
        private string _SendMailAddress;//发送者邮箱
        private string _SendName;//发送者姓名
        private string _Title;//邮件标题，对应mail的subject
        private string _Content;//邮件内容
        private string _SMTPHost;
        private string _SMTPUser;
        private string _SMTPPassword;
        private List<Attachment> _Attactments = new List<Attachment>();//附件
        private List<string> _ReveiverMailAddresses = new List<string>();//接受者邮箱列表
        private List<string> _CarBonCopy = new List<string>();//抄送的邮箱列表

      
        /// <summary>
        /// 构造邮件,此方法不完善，很多设置项硬编码，例如邮件内容编码方式硬编码为UTF8，SSL硬编码为False。此外仅实现了同步发送模式。
        /// </summary>
        /// <param name="smtpHost">SMTP Host smtp.qq.com</param>
        /// <param name="smtpUser">SMTP账户</param>
        /// <param name="smtpPassword">SMTP密码</param>
        /// <param name="sendName">发信人</param>
        /// <param name="title">题目</param>
        /// <param name="content">内容</param>
        /// <param name="sendMailAddress">发送地址</param>
        public MyEmailHelper(string smtpHost,
            string smtpUser,
            string smtpPassword,
            string sendName,
            string title,
            string content,
            string sendMailAddress)
        {

            //if (string.IsNullOrEmpty(receiverMailAddress))
            //    throw new ArgumentNullException("receiverMailAddress is Null");
            _SMTPHost = smtpHost;
            _SMTPUser = smtpUser;
            _SMTPPassword = smtpPassword;
            _Title = title;
            _Content = content;
            _SendMailAddress = sendMailAddress;
            _SendName = sendName;
        }
        /// <summary>
        /// 添加附件
        /// </summary>
        /// <param name="attachmentFileName">附件名称</param>
        public void AddAttachment(string attachmentFileName)
        {
            _Attactments.Add(new Attachment(attachmentFileName));
        }

        /// <summary>
        /// 添加抄送地址
        /// </summary>
        /// <param name="carbonCopy"></param>
        public void AddCarbonCopy(string carbonCopy)
        {
            _CarBonCopy.Add(carbonCopy);
        }

        /// <summary>
        /// 添加接受地址
        /// </summary>
        /// <param name="carbonCopy"></param>
        public void AddReceiverEmail(string receiverEmail)
        {
            _ReveiverMailAddresses.Add(receiverEmail);
        }


        /// <summary>
        /// 发送邮件
        /// </summary>
        public void Send()
        {
            if (_ReveiverMailAddresses.Count == 0)
            {
                throw new ArgumentNullException("receiverMailAddress is Null");
            }
           
            MailAddress sendMailAddress = new MailAddress(_SendMailAddress, _SendName);
            MailMessage mail = new MailMessage();
            mail.Subject = _Title;
            mail.From = sendMailAddress;
            mail.Body = _Content;
            mail.BodyEncoding = System.Text.Encoding.UTF8;
            mail.IsBodyHtml = true;
            mail.Priority = MailPriority.Normal;            
            mail.DeliveryNotificationOptions = DeliveryNotificationOptions.OnSuccess;
            foreach (var receiverMail in _ReveiverMailAddresses)
            {
                mail.To.Add(new MailAddress(receiverMail));
            }

            foreach (var carbonCopy in _CarBonCopy)
            {
                mail.CC.Add(new MailAddress(carbonCopy));
            }
            foreach (var attach in _Attactments)
            {
                mail.Attachments.Add(attach);
            }

            SmtpClient client = new SmtpClient();
            client.Host = _SMTPHost;
            client.UseDefaultCredentials = false;
            client.Credentials = new System.Net.NetworkCredential(_SMTPUser, _SMTPPassword);
            client.DeliveryMethod = SmtpDeliveryMethod.Network;
            client.EnableSsl = false;
            client.Send(mail);
        }

    }
}
