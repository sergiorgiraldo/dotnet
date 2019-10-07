using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Mail;

namespace journey
{
    class Program
    {
        static void Main(string[] args)
        {
            if (args.Length == 0 || args[0] == "/h" || args[0] == "-h"){
                Console.WriteLine("/h: this help");
                Console.WriteLine("/done {taskId}: mark task with id {taskId} as done. Interacts with myJournal");
                Console.WriteLine("/my {task}: register activity in myJournal");
                Console.WriteLine("{task}: register activity in Journey");
                Environment.Exit(0);
            }

            if (args[0] == "/done"){
                var pathTodo = @"C:\Users\sgiraldo\OneDrive\Documentos\journal\todo.txt";
                var taskId = args[1] + ">";
                var lines = File.ReadAllLines(pathTodo);
                var newContent = new List<string>();
                foreach (var line in lines){
                    if (line.StartsWith(taskId)) continue;
                    newContent.Add(line);
                }
                File.WriteAllLines(pathTodo, newContent);
                Environment.Exit(0);
            }

            string body = "";
            if (args[0] == "/my"){
                body = string.Join(" ", args).Replace("/my", "");
            }
            else{
                body = string.Join(" ", args);
            }

            if (args[0] == "/my"){
                var pathMyJournal = @"C:\Users\sgiraldo\OneDrive\Documentos\journal\{0}";
                pathMyJournal = pathMyJournal.Replace("{0}", DateTime.Now.ToString("dd.MM.yyyy") + ".txt");
                
                var myJournalEntry = DateTime.Now.ToString("yyyyMMdd HHmmss") + " " + body + Environment.NewLine;
                
                File.AppendAllText(pathMyJournal, myJournalEntry);
                
                Environment.Exit(0);
            }

            var fromAddress = new MailAddress("sergiorgiraldo@gmail.com", "Sergio RG");
            var toAddress = new MailAddress("e233017fa4603b52@user.journey.cloud", "journey");
            const string subject = "";

            //get an app password from https://myaccount.google.com/apppasswords and store in a file elsewhere
            //last pass: Gmail apps Console apps
            string userPath = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile);
            string fromPassword = File.ReadAllText(Path.Combine(userPath, "gmail.key"));

            if (args.Length == 0){
                Console.WriteLine("Provide something to post :)");
                Environment.Exit(0);
            }

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
                Body = body + Environment.NewLine + "#work"
            })
            {
                smtp.Send(message);
            }
        }
    }
}
