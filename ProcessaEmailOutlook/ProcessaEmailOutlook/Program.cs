using System;
using System.IO;
using System.Reflection;
using Microsoft.Office.Interop.Outlook;
using Exception=System.Exception;

namespace ProcessEmailOutlook
{
    internal class Program
    {
        private static readonly ApplicationClass OutlookApp = new ApplicationClass();

        private static void Main(string[] args)
        {
            string sPathCommands = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location),
                                                "commands");
            try
            {
                if (!Directory.Exists(sPathCommands))
                    Directory.CreateDirectory(sPathCommands);

                OutlookApp.NewMailEx += OutLookAppNewMailEx;

                if (args.Length > 0 && args[0] == "testes")
                {
                    MailItem mailTestes;

                    mailTestes = (MailItem)OutlookApp.CreateItem(OlItemType.olMailItem);
                    mailTestes.Subject = "TestesInternos";
                    mailTestes.Body = "TestesInternos";
                    Processar(mailTestes);

                    mailTestes = (MailItem)OutlookApp.CreateItem(OlItemType.olMailItem);
                    mailTestes.Subject = "TestesExternos";
                    mailTestes.Body = "TestesExternos";
                    Processar(mailTestes);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("ERRO :: " + e);
            }

            Console.Read();

            OutlookApp.Quit();
        }

        private static void OutLookAppNewMailEx(string entryidcollection)
        {
            NameSpace outlookNamespace = OutlookApp.GetNamespace("MAPI");
            MAPIFolder mapiFolder = OutlookApp.Session.GetDefaultFolder(OlDefaultFolders.olFolderInbox);
            var oEmail = (MailItem) outlookNamespace.GetItemFromID(entryidcollection, mapiFolder.StoreID);
            Processar(oEmail);
        }

        private static void Processar(MailItem email)
        {
            try
            {
                string sBody = email.Body;
                string sVerb = email.Subject.ToLower();
                var emailToProcess = new EmailToProcess(sBody);

                switch (sVerb)
                {
                    case "email":
                        emailToProcess.SetProcessStrategy(new SendEmail(sBody, email));
                        break;
                    case "startrb":
                        emailToProcess.SetProcessStrategy(new StartRoboBase(sBody, email));
                        break;
                    case "stoprb":
                        emailToProcess.SetProcessStrategy(new StopRoboBase(sBody, email));
                        break;
                    case "testesinternos":
                        emailToProcess.SetProcessStrategy(new TestesInternos(sBody, email));
                        break;
                    default:
                        if (!sVerb.Contains(" ")) //apenas verbos de uma palavra so 
                            if (!TryLoadStrategy(emailToProcess, sVerb, sBody, email))
                                Console.WriteLine("VERBO NAO ENTENDIDO::{0}", sVerb);
                        break;
                }
                emailToProcess.Process();
            }
            catch (Exception e)
            {
                Console.WriteLine("ERRO :: " + e);
            }
        }

        private static bool TryLoadStrategy(EmailToProcess emailToProcess, string verb, string body, MailItem email)
        {
            string sFileName = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location),
                                            verb + ".dll");
            Assembly oAssemblyCandidate;
            Type oTypeImplementation = typeof (ProcessStrategy);
            bool bOK = false;

            if (File.Exists(sFileName))
            {
                oAssemblyCandidate = Assembly.LoadFile(sFileName);
                Type[] types = oAssemblyCandidate.GetTypes();
                foreach (Type type in types)
                {
                    if (type.BaseType == oTypeImplementation)
                    {
                        object oStrategy = Activator.CreateInstance(type, new object[] {body, email});

                        emailToProcess.SetProcessStrategy(oStrategy as ProcessStrategy);
                        bOK = true;
                        break;
                    }
                }
            }


            return bOK;
        }
    }
}