using System;
using System.IO;
using System.Reflection;
using Microsoft.Office.Interop.Outlook;

namespace ProcessEmailOutlook
{
    public abstract class ProcessStrategy
    {
        private readonly string _sBody;
        private readonly string _sGuid;
        private readonly MailItem _oEmail;

        private readonly string _sPathCommands =
            Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), "commands");

        protected ProcessStrategy(string body, MailItem email)
        {
            _sBody = body;
            _sGuid = Guid.NewGuid().ToString();
            _oEmail = email;
        }

        private ProcessStrategy()
        {
        }

        public event ProcessEventHandler OnBeforeProcess;
        public event ProcessEventHandler OnProcess;
        public event ProcessEventHandler OnAfterProcess;

        private void BeforeProcess()
        {
            using (var sw = new StreamWriter(Path.Combine(_sPathCommands, _sGuid), true))
            {
                sw.WriteLine(DateTime.Now + ">INICIANDO");
            }

            if (OnBeforeProcess != null) OnBeforeProcess(this, new ProcessEventHandlerArgs(_sBody, _oEmail));
        }

        public void Process(string body)
        {
            BeforeProcess();
            if (OnProcess != null) OnProcess(this, new ProcessEventHandlerArgs(_sBody, _oEmail));
            AfterProcess();
        }

        private void AfterProcess()
        {
            using (var sw = new StreamWriter(Path.Combine(_sPathCommands, _sGuid), true))
            {
                sw.WriteLine(DateTime.Now + ">FINALIZADO");
            }

            if (OnAfterProcess != null) OnAfterProcess(this, new ProcessEventHandlerArgs(_sBody, _oEmail));
        }

        public void GravarLog(string log)
        {
            using (var sw = new StreamWriter(Path.Combine(_sPathCommands, _sGuid), true))
            {
                sw.WriteLine(DateTime.Now + ">" + log);
            }
        }
    }
}