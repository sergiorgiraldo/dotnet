using Microsoft.Office.Interop.Outlook;

namespace ProcessEmailOutlook
{
    public delegate void ProcessEventHandler(object sender, ProcessEventHandlerArgs args);

    public class ProcessEventHandlerArgs
    {
        public string Body;
        public MailItem Mail;

        public ProcessEventHandlerArgs(string body, MailItem mail)
        {
            Body = body;
            Mail = mail;
        }
    }
}