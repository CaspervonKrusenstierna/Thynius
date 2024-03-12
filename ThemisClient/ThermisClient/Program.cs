namespace ThermisClient
{
    internal static class Program
    {
        [STAThread]
        static void Main()
        {
            ApplicationConfiguration.Initialize();
            LogicAbstraction logicAbstraction = new LogicAbstraction();
            var Form = new Form1(logicAbstraction);
            Application.Run(Form);
        }
    }
}