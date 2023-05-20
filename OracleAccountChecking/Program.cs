using ChromeDriverLibrary;
using OracleAccountChecking.Services;

namespace OracleAccountChecking
{
    internal static class Program
    {
        [STAThread]
        static void Main()
        {
            ApplicationConfiguration.Initialize();
            Application.Run(new FrmMain());
        }
    }
}