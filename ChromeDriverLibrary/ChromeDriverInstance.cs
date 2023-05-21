using OpenQA.Selenium.Chrome;
using SeleniumUndetectedChromeDriver;
using System.Diagnostics;

namespace ChromeDriverLibrary
{
    public class ChromeDriverInstance
    {
        public static UndetectedChromeDriver? GetInstance(int positionX, int positionY,
            string? proxy = null, bool isHeadless = true,
            List<string>? extensionPaths = null, CancellationToken? token = null)
        {
            UndetectedChromeDriver? driver = null;
            try
            {
                token ??= CancellationToken.None;
                var proxyInfo = new List<string>();

                var options = new ChromeOptions();
                if (!string.IsNullOrEmpty(proxy))
                {
                    var prefix = string.Empty;
                    if (proxy.Contains("http://") || proxy.Contains("https://"))
                    {
                        proxy = proxy.Replace("http://", "");
                        prefix = "http://";
                    }
                    else if (proxy.Contains("socks5://"))
                    {
                        proxy = proxy.Replace("socks5://", "");
                        prefix = "socks5://";
                    }
                    else throw new Exception("unsupported proxy type");

                    proxyInfo = proxy.Split(":").ToList();
                    if (proxyInfo.Count != 2 && proxyInfo.Count != 4) throw new Exception("invalid proxy format");

                    options.AddArgument($"--proxy-server={prefix}{proxyInfo[0]}:{proxyInfo[1]}");
                }

                var basePath = AppDomain.CurrentDomain.BaseDirectory;
                //options.AddArgument($"--load-extension={basePath}/chromedriver/bypasscookie");
                extensionPaths?.ForEach(path => options.AddArgument($"--load-extension={path}"));

                if (proxyInfo.Count == 4) options.AddArgument($"--load-extension={basePath}/chromedriver/proxyauth");

                driver = UndetectedChromeDriver.Create(driverExecutablePath: "chromedriver/chromedriver.exe",
                    headless: isHeadless,
                    hideCommandPromptWindow: true,
                    options: options);
                driver.Manage().Window.Position = new System.Drawing.Point(positionX, positionY);
                driver.Manage().Window.Size = new System.Drawing.Size(375, 500);
                if (proxyInfo.Count == 4)
                {
                    driver.SwitchTo().Window(driver.WindowHandles.First());
                    driver.GoToUrl("chrome://extensions");
                    Thread.Sleep(1000);
                    var findIdScript = "var done = arguments[0];" +
                        "chrome.management.getAll().then((res) => {" +
                        "var ext = res.find(item => item.name = 'Proxy Auto Auth');" +
                        "var extId = ext ? ext.id : '';" +
                        "return done(extId);" +
                        "});";
                    var id = (string)driver.ExecuteAsyncScript(findIdScript);

                    driver.GoToUrl($"chrome-extension://{id}/options.html");
                    driver.FindElement("#login", 30, token.Value);
                    driver.ExecuteScript($"localStorage['proxy_login'] = '{proxyInfo[2]}';" +
                    $"localStorage['proxy_password'] = '{proxyInfo[3]}';" +
                    $"localStorage['proxy_retry'] = '2'");
                }
                return driver;
            }
            catch
            {
                if (driver != null) try { driver.Quit(); } catch { }
                return null;
            }
        }

        public static void ForceKillAll()
        {
            try
            {
                var process = new Process();
                process.StartInfo.FileName = "chromedriver/kill.bat";
                process.StartInfo.UseShellExecute = false;
                process.StartInfo.RedirectStandardOutput = true;
                process.StartInfo.CreateNoWindow = true;
                process.Start();
                process.WaitForExit();
            }
            catch { }
        }
    }
}
