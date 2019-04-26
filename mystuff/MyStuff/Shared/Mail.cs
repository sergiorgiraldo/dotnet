using System;
using System.Net.Mail;

namespace MyStuff.Shared
{
    public class Mail
    {
        public static bool Send(string subject, string body, string to = "TO@EXAMPLE.COM")
        {
            var client = new SmtpClient
            {
                DeliveryMethod = SmtpDeliveryMethod.Network,
                UseDefaultCredentials = false,
                Credentials = new System.Net.NetworkCredential("USER", "PASSWORD"),
                Host = "SMTP SERVER",
                Port = 587,
                EnableSsl = false
            };
            var mail = new MailMessage("FROM@EXAMPLE.COM", to)
            {
                Subject = subject,
                Body = body
            };
            try
            {
                client.Send(mail);
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }
    }
}
