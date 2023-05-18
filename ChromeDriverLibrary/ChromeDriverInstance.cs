using OpenQA.Selenium.Chrome;
using SeleniumUndetectedChromeDriver;

namespace ChromeDriverLibrary
{
    public class ChromeDriverInstance
    {
        public static UndetectedChromeDriver GetInstance(string? proxy = null, bool isHeadless = true)
        {
            var options = new ChromeOptions();
            if (!string.IsNullOrEmpty(proxy)) options.AddArgument($"--proxy-server={proxy}");

            return UndetectedChromeDriver.Create(
                driverExecutablePath: "chromedriver/chromedriver.exe",
                headless: isHeadless,
                hideCommandPromptWindow: true,
                options: options);
        }
    }
}
