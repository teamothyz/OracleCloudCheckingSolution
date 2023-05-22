using ChromeDriverLibrary;

namespace OracleAccountChecking
{
    internal static class Program
    {
        [STAThread]
        static void Main()
        {
            ApplicationConfiguration.Initialize();
            var frmMain = new FrmMain
            {
                TopMost = true
            };
            Application.Run(frmMain);
        }
    }
}