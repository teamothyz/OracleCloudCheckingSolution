using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using SeleniumUndetectedChromeDriver;

namespace ChromeDriverLibrary
{
    public static class ChromeDriverExtension
    {
        public static IWebElement FindElement(this UndetectedChromeDriver driver, string selector, int timeout, CancellationToken token)
        {
            var waiter = GetWaiter(driver, timeout);
            var element = waiter.Until(webdriver => webdriver.FindElement(By.CssSelector(selector)), token);
            return element;
        }

        public static void Sendkeys(this UndetectedChromeDriver driver, IWebElement element, string content, int timeout, CancellationToken token)
        {
            var waiter = GetWaiter(driver, timeout);
            waiter.Until(webdriver =>
            {
                element.Clear();
                Thread.Sleep(500);
                element.SendKeys(content);
                return CompareContent(driver, element, content);
            }, token);
        }

        public static void Click(this UndetectedChromeDriver driver, IWebElement element, int timeout, CancellationToken token)
        {
            var waiter = GetWaiter(driver, timeout);
            waiter.Until(webdriver =>
            {
                element.Click();
                return true;
            }, token);
        }

        private static bool CompareContent(UndetectedChromeDriver driver, IWebElement element, string content)
        {
            try
            {
                var value = element.GetAttribute("value");
                if (value == content) return true;

                var innerHtml = (string)driver.ExecuteScript("return arguments[0].innerHTML;", element);
                if (innerHtml == content) return true;

                throw new WebDriverException("content does not match");
            }
            catch (Exception ex) 
            {
                throw new WebDriverException(ex.Message);
            }
        }

        private static WebDriverWait GetWaiter(UndetectedChromeDriver driver, int timeout)
        {
            var wait = new WebDriverWait(driver, TimeSpan.FromSeconds(timeout));
            wait.IgnoreExceptionTypes(typeof(NoSuchElementException),
                typeof(ElementNotVisibleException),
                typeof(ElementNotInteractableException),
                typeof(StaleElementReferenceException),
                typeof(WebDriverException),
                typeof(WebDriverTimeoutException));
            return wait;
        }
    }
}
