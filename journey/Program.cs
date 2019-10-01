using System;
using System.IO;
using System.Net;
using System.Net.Mail;

namespace journey
{
    class Program
    {
        static void Main(string[] args)
        {
            var fromAddress = new MailAddress("sergiorgiraldo@gmail.com", "Sergio RG");
            var toAddress = new MailAddress("e233017fa4603b52@user.journey.cloud", "journey");
            const string subject = "";

            //get an app password from https://myaccount.google.com/apppasswords and store in a file elsewhere
            string userPath = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile);
            string fromPassword = File.ReadAllText(Path.Combine(userPath, "gmail.key"));

            if (args.Length == 0){
                Console.WriteLine("Provide something to post :)");
                Environment.Exit(0);
            }

            string body = string.Join(" ", args);

            var smtp = new SmtpClient
            {
                Host = "smtp.gmail.com",
                Port = 587,
                EnableSsl = true,
                DeliveryMethod = SmtpDeliveryMethod.Network,
                UseDefaultCredentials = false,
                Credentials = new NetworkCredential(fromAddress.Address, fromPassword)
            };
            using (var message = new MailMessage(fromAddress, toAddress)
            {
                Subject = subject,
                Body = body
            })
            {
                smtp.Send(message);
            }
        }
    }
}
