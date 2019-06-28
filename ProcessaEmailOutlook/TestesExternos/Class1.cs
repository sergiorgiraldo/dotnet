using System;
using Microsoft.Office.Interop.Outlook;
using ProcessEmailOutlook;

namespace TestesExternos
{
    public class TestesExternos : ProcessStrategy
    {
        public TestesExternos(string body, MailItem email)
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
            GravarLog("Processing TESTESEXTERNOS!");
        }
    }
}