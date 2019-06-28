using System;
using Microsoft.Office.Interop.Outlook;

namespace ProcessEmailOutlook
{
    internal class TestesInternos : ProcessStrategy
    {
        public TestesInternos(string body, MailItem email)
            : base(body, email)
        {
            OnBeforeProcess += TestesOnBeforeProcess;
            OnProcess += TestesOnProcess;
            OnAfterProcess += TestesOnAfterProcess;
        }

        private static void TestesOnBeforeProcess(object sender, ProcessEventHandlerArgs e)
        {
            Console.WriteLine("OnBeforeProcess !");
        }

        private static void TestesOnAfterProcess(object sender, ProcessEventHandlerArgs e)
        {
            Console.WriteLine("OnAfterProcess !");
        }

        public void TestesOnProcess(object sender, ProcessEventHandlerArgs e)
        {
            Console.WriteLine("OnProcess !");
            GravarLog("Processing TESTESINTERNOS!");
        }
    }

    internal class SendEmail : ProcessStrategy
    {
        public SendEmail(string body, MailItem email)
            : base(body, email)
        {
            OnProcess += SendEmailOnProcess;
        }

        public void SendEmailOnProcess(object sender, ProcessEventHandlerArgs args)
        {
            Console.WriteLine("SendEmail");
        }
    }

    internal class StartRoboBase : ProcessStrategy
    {
        public StartRoboBase(string body, MailItem email)
            : base(body, email)
        {
            OnProcess += StartRoboBaseOnProcess;
        }

        public void StartRoboBaseOnProcess(object sender, ProcessEventHandlerArgs args)
        {
            Console.WriteLine("StartRoboBase");
        }
    }

    internal class StopRoboBase : ProcessStrategy
    {
        public StopRoboBase(string body, MailItem email)
            : base(body, email)
        {
            OnProcess += StopRoboBaseOnProcess;
        }

        public void StopRoboBaseOnProcess(object sender, ProcessEventHandlerArgs args)
        {
            Console.WriteLine("StopRoboBase");
        }
    }
}