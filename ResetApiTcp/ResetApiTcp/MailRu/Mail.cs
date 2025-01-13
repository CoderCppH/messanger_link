using System;
using System.Net;
using System.Net.Mail;

namespace ResetApiTcp.MailRu
{
    public class StreamMail
    {
        private string _pwd_from_address = "sqxTXuZbmgA2Qtm6vFYb";
        private string _from_address = "pargev.na@mail.ru";
        private string _smtp_address = "smtp.mail.ru";
        private int _smtp_port = 587;
        public bool SendMessage(string messageText, string messageHead, string toAddress) 
        {
            bool r_s_msg = false;

            using (SmtpClient smtp = new SmtpClient(_smtp_address, _smtp_port))
            {
                smtp.Credentials = new NetworkCredential(_from_address, _pwd_from_address);
                smtp.EnableSsl = true;
                using (MailMessage mailMessage = new MailMessage())
                {
                    mailMessage.From = new MailAddress(_from_address);
                    mailMessage.To.Add(toAddress);
                    mailMessage.Subject = messageHead;
                    mailMessage.Body = messageText;
                    try
                    {
                        smtp.Send(mailMessage);
                        r_s_msg = true;
                        Console.WriteLine("send message succesfull");
                    }
                    catch
                    {
                        r_s_msg = false;
                        Console.WriteLine("error send message");
                    }
                }
            }
            return r_s_msg;
        }
    }
}
