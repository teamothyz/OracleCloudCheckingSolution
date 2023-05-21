using SeleniumUndetectedChromeDriver;
using ChromeDriverLibrary;
using OpenQA.Selenium;
using System.Text;

namespace OracleAccountChecking.Services
{
    public class WebDriverService
    {
        public static int DefaultTimeout { get; set; } = 30;

        public static async Task<bool> CheckTenant(UndetectedChromeDriver driver, string accountName, CancellationToken token)
        {
            driver.GoToUrl("https://www.oracle.com/cloud/sign-in.html");
            await Task.Delay(3000, token).ConfigureAwait(false);

            await HandleCookieAccept(driver, token).ConfigureAwait(false);

            var accountNameElm = driver.FindElement("#cloudAccountName", DefaultTimeout, token);
            driver.Sendkeys(accountNameElm, accountName, true, 5, token);
            await Task.Delay(3000, token).ConfigureAwait(false);

            var button = driver.FindElement("#cloudAccountButton", DefaultTimeout, token);
            driver.Click(button, 5, token);

            await Task.Delay(5, token).ConfigureAwait(false);
            if (!driver.Url.Contains("cloud/sign-in.html")) return true;

            try
            {
                var errorElm = driver.FindElement(@"div[class=""rc63error icn-error-s opcSignInErrorContainer""][style=""""]", 10, token);
                return false;
            }
            catch
            {
                return !driver.Url.Contains("cloud/sign-in.html");
            }
        }

        private static async Task HandleCookieAccept(UndetectedChromeDriver driver, CancellationToken token)
        {
            try
            {
                var iframe = driver.FindElement(@"iframe[name=""trustarc_cm""]", DefaultTimeout / 2, token);
                driver.SwitchTo().Frame(iframe);
                await Task.Delay(3000, token).ConfigureAwait(false);

                var acceptBtn = driver.FindElement("a.call", DefaultTimeout, token);
                driver.Click(acceptBtn, 5, token);
                await Task.Delay(3000, token).ConfigureAwait(false);

                driver.SwitchTo().DefaultContent();
            }
            catch (Exception ex)
            {
                DataHandler.WriteLog(ex);
            }
        }

        public static async Task<Tuple<bool, string>> EnterTenant(UndetectedChromeDriver driver, string accountName, CancellationToken token)
        {
            try
            {
                var url = $"https://cloud.oracle.com/?tenant={accountName}";
                driver.GoToUrl(url);

                //check the existance of submit button (3mins)
                for (var loop = 1; loop <= 6; loop++)
                {
                    try
                    {
                        var submitFederationBtn = driver.FindElement("#submit-federation", DefaultTimeout / 6, token);
                        await Task.Delay(3000, token).ConfigureAwait(false);

                        driver.Click(submitFederationBtn, 5, token);
                        await Task.Delay(3000, token).ConfigureAwait(false);

                        //wait for redirect to login page (3mins)
                        for (var innerLoop = 1; innerLoop <= 3; innerLoop++)
                        {
                            var endTime = DateTime.Now.AddSeconds(DefaultTimeout / 3);
                            while (driver.Url.Contains("oraclecloud.com/v1/oauth2") && endTime > DateTime.Now)
                                await Task.Delay(3000, token).ConfigureAwait(false);

                            if (driver.Url.Contains("identity.oraclecloud.com/ui/v1/signin"))
                                return Tuple.Create(true, string.Empty);
                            try { driver.Click(submitFederationBtn, 1, token); } catch { }
                        }

                        if (!driver.Url.Contains("identity.oraclecloud.com/ui/v1/signin")) continue;
                    }
                    catch
                    {
                        var isValid = CheckInvalidTenant(driver, token);
                        if (!isValid) return Tuple.Create(false, "invalid tenant");

                        if (driver.Url.Contains("identity.oraclecloud.com/ui/v1/signin"))
                            return Tuple.Create(true, string.Empty);
                        else continue;
                    }
                }
                var success = driver.Url.Contains("identity.oraclecloud.com/ui/v1/signin");
                if (success) return Tuple.Create(true, string.Empty);
                else return Tuple.Create(success, "can't load login url");
            }
            catch (Exception ex)
            {
                DataHandler.WriteLog(ex);
                return Tuple.Create(false, ex.Message);
            }
        }

        private static bool CheckInvalidTenant(UndetectedChromeDriver driver, CancellationToken token)
        {
            try
            {
                _ = driver.FindElement("img.error-icon", 10, token);
                return false;
            }
            catch { return true; }
        }

        public static async Task<Tuple<bool, string>> Login(UndetectedChromeDriver driver, string email, string password, CancellationToken token)
        {
            try
            {
                var emailInputElm = driver.FindElement("#idcs-signin-basic-signin-form-username", DefaultTimeout, token);
                driver.Sendkeys(emailInputElm, email, true, DefaultTimeout, token);
                await Task.Delay(1000, token).ConfigureAwait(false);

                var passInputElm = driver.FindElement(@"input[id=""idcs-signin-basic-signin-form-password|input""]", DefaultTimeout, token);
                driver.Sendkeys(passInputElm, password, true, DefaultTimeout, token);
                await Task.Delay(1000, token).ConfigureAwait(false);

                var button = driver.FindElement("#idcs-signin-basic-signin-form-submit > button > div", DefaultTimeout, token);
                driver.Click(button, 5, token);

                for (var i = 1; i <= 3; i++)
                {
                    var loginEndTime = DateTime.Now.AddSeconds(DefaultTimeout / 3);
                    while (driver.Url.Contains("oraclecloud.com/ui/v1/signin") && loginEndTime > DateTime.Now) 
                        await Task.Delay(3000, token).ConfigureAwait(false);

                    if (driver.Url.Contains("cloud.oracle.com/?region"))
                        return Tuple.Create(true, string.Empty);

                    if (driver.Url.Contains("ui/v1/pwdmustchange"))
                        return Tuple.Create(true, "pwdmustchange");

                    var loginSuccess = CheckValidLogin(driver, token);
                    if (!loginSuccess) return Tuple.Create(false, "invalid email or password");

                    try { driver.Click(button, 1, token); } catch { }
                }
                var success = driver.Url.Contains("cloud.oracle.com/?region");
                if (success) return Tuple.Create(true, string.Empty);
                else return Tuple.Create(success, "can't load login success url");
            }
            catch (Exception ex)
            {
                DataHandler.WriteLog(ex);
                return Tuple.Create(false, ex.Message); ;
            }
        }

        private static bool CheckValidLogin(UndetectedChromeDriver driver, CancellationToken token)
        {
            try
            {
                _ = driver.FindElement("span.idcs-icon.idcs-signin-Error-Warning.idcs-text-danger", 10, token);
                return false;
            }
            catch { return true; }
        }

        public static async Task<List<string>> GetBilling(UndetectedChromeDriver driver, CancellationToken token)
        {
            var result = new List<string>();
            try
            {
                var url = "https://cloud.oracle.com/billing/subscriptions";
                driver.GoToUrl(url);
                await Task.Delay(1000, token).ConfigureAwait(false);

                var iframe = driver.FindElement("#sandbox-billing-and-payments-container", DefaultTimeout, token);
                driver.SwitchTo().Frame(iframe);
                await Task.Delay(1000, token).ConfigureAwait(false);

                var table = driver.FindElement(@"div[data-test-id=""subscriptionsTable-id""]", DefaultTimeout, token);
                var trs = table.FindElements(By.CssSelector("tbody > tr"));
                if (trs == null)
                {
                    result.Add("Not found table");
                    return result;
                }

                foreach (var tr in trs)
                {
                    var tds = tr.FindElements(By.TagName("td"));

                    var index = 1;
                    var content = new StringBuilder();
                    foreach (var td in tds)
                    {
                        var tdContent = string.Empty;
                        if (index == 1)
                        {
                            var innerElm = td.FindElement(By.TagName("a"));
                            tdContent = (string)driver.ExecuteScript("return arguments[0].innerText;", innerElm);
                        }
                        else if (index == 3)
                        {
                            var innerElm = td.FindElement(By.TagName("span"));
                            tdContent = (string)driver.ExecuteScript("return arguments[0].innerText;", innerElm);
                        }
                        else tdContent = (string)driver.ExecuteScript("return arguments[0].innerText;", td);
                        content.Append($"|{tdContent}");
                        index++;
                    }
                    content.Remove(0, 1);
                    result.Add(content.ToString());
                }
                return result;
            }
            catch (Exception ex)
            {
                DataHandler.WriteLog(ex);
                result.Add($"exception {ex.Message}");
                return result;
            }
        }
    }
}
