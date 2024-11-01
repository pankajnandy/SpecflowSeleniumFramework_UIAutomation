using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Edge;
using OpenQA.Selenium.Firefox;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Specflow_UIAutomation_TFL.Utils
{
    /// <summary>
    /// this is where the required driver is created.
    /// </summary>
    internal class WebDriverFactory
    {
        public IWebDriver StartBrower(BrowserTypes browserType)
        {
            switch (browserType)
            {
                case BrowserTypes.Firefox:
                    return new FirefoxDriver();
                case BrowserTypes.Chrome:
                    return new ChromeDriver();
                case BrowserTypes.Edge:
                    return new EdgeDriver();
                default:
                    throw new ArgumentOutOfRangeException("Browser not defined");

            }
        }

        public IWebDriver StartBrowser(string browserName)
        {
            switch (browserName.ToLower())
            {
                case "firefox":
                    return new FirefoxDriver();
                case "chrome":
                    return new ChromeDriver();
                case "edge":
                    return new EdgeDriver();
                default:
                    throw new ArgumentOutOfRangeException("Browser not supported ");

            }
        }
    }


}
