namespace ProcessEmailOutlook
{
    internal class EmailToProcess
    {
        private readonly string _sBody;
        private ProcessStrategy _oStrategy;

        private EmailToProcess()
        {
        }

        public EmailToProcess(string sBody)
        {
            _sBody = sBody;
        }

        public void SetProcessStrategy(ProcessStrategy processStrategy)
        {
            _oStrategy = processStrategy;
        }

        public void Process()
        {
            if (_oStrategy != null)
                _oStrategy.Process(_sBody);
        }
    }
}