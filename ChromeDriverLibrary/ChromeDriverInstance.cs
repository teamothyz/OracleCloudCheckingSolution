using OpenQA.Selenium.Chrome;
using SeleniumUndetectedChromeDriver;
using System.Diagnostics;

namespace ChromeDriverLibrary
{
    public class ChromeDriverInstance
    {
        private static readonly object _lockUserDir = new();

        public static Tuple<UndetectedChromeDriver?, string> GetInstance(int positionX, int positionY,
            string? proxy = null, bool isHeadless = true,
            List<string>? extensionPaths = null, bool disableImg = true, bool privateMode = true, CancellationToken? token = null)
        {
            UndetectedChromeDriver? driver = null;
            var userDataDir = GetUserDir();
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

                var extensions = new List<string>();
                extensions.AddRange(extensionPaths ?? new List<string>());
                if (proxyInfo.Count == 4) extensions.Add($"{basePath}/chromedriver/proxyauth");
                if (extensions.Count > 0) options.AddArguments($"--load-extension={string.Join(",", extensions)}");

                if (privateMode) options.AddArgument("--incognito");
                if (disableImg) options.AddArgument("--blink-settings=imagesEnabled=false");

                driver = UndetectedChromeDriver.Create(driverExecutablePath: "chromedriver/chromedriver.exe",
                    userDataDir: userDataDir,
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
                return new Tuple<UndetectedChromeDriver?, string>(driver, userDataDir);
            }
            catch
            {
                Close(driver, userDataDir).Wait();
                return new Tuple<UndetectedChromeDriver?, string>(driver, string.Empty);
            }
        }

        private static string GetUserDir()
        {
            var basePath = AppDomain.CurrentDomain.BaseDirectory;
            var folder = Path.Combine(basePath, "profiles");
            var container = Path.Combine(folder, Guid.NewGuid().ToString());
            if (!Directory.Exists(folder))
            {
                lock (_lockUserDir)
                {
                    if (!Directory.Exists(folder)) Directory.CreateDirectory(folder);
                }
            }
            Directory.CreateDirectory(container);
            return container;
        }

        public static async Task Close(UndetectedChromeDriver? driver, string folder)
        {
            try
            {
                driver?.Close();
                await Task.Delay(1000).ConfigureAwait(false);
                driver?.Quit();
                await Task.Delay(1000).ConfigureAwait(false);
                driver?.Dispose();
                await Task.Delay(1000).ConfigureAwait(false);
                Directory.Delete(folder, true);
            }
            catch { }
            try
            {
                Directory.Delete(folder, true);
            }
            catch { }
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
            try
            {
                var basePath = AppDomain.CurrentDomain.BaseDirectory;
                var folder = Path.Combine(basePath, "profiles");
                Directory.Delete(folder, true);
            }
            catch { }
        }
    }
}
