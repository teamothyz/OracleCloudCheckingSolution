using OpenQA.Selenium.Chrome;
using SeleniumUndetectedChromeDriver;

namespace ChromeDriverLibrary
{
    public class ChromeDriverInstance
    {
        public static UndetectedChromeDriver? GetInstance(string? proxy = null, bool isHeadless = true, CancellationToken? token = null)
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
                if (proxyInfo.Count == 4)
                {
                    var basePath = AppDomain.CurrentDomain.BaseDirectory;
                    options.AddArgument($"--load-extension={basePath}/chromedriver/proxyauth");
                }

                driver = UndetectedChromeDriver.Create(
                    driverExecutablePath: "chromedriver/chromedriver.exe",
                    headless: isHeadless,
                    hideCommandPromptWindow: true,
                    options: options);

                if (proxyInfo.Count == 4)
                {
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
                if (driver != null) try { driver.Quit(); } catch { };
                throw;
            }
        }
    }
}
