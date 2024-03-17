namespace ThermisClient
{
    internal static class Program
    {
        [STAThread]
        static void Main()
        {
            ApplicationConfiguration.Initialize();
            LogicAbstraction logicAbstraction = new LogicAbstraction();
            var Form = new ParentForm();
            Application.Run(Form);

            bool NeedToReinject = true;
            while(true)
            {
                if (logicAbstraction.isLoggedIn())
                {
                    bool isTargetRunning = logicAbstraction.isProcessRunning("WINWORD");
                    if(NeedToReinject && isTargetRunning)
                    {
                        logicAbstraction.Inject("WINWORD", "C:\\Users\\Cappe\\OneDrive\\Skrivbord\\HP Fusket v2\\hpfuskhemsida\\Themis\\ThemisDll\\x64\\Debug\\ThemisDll.dll");
                        NeedToReinject = false;
                    }
                    else if(!isTargetRunning)
                    {
                        NeedToReinject = true;
                    }
                    else
                    {
                        // Add tracking status to the child form
                    }
                }
                else
                {
                    // Add login page
                }
                Thread.Sleep(1000);
            }
        }
    }
}