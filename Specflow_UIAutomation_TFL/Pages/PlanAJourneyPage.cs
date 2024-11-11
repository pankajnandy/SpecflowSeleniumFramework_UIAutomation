using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace Specflow_UIAutomation_TFL.Pages
{
    public class PlanAJourneyPage
    {
        private IWebDriver driver;

        public PlanAJourneyPage(IWebDriver driver)
        {
            this.driver = driver;
        }


        By acceptAllCookiesBtn = By.XPath("//*[@id='CybotCookiebotDialogBodyLevelButtonLevelOptinAllowAll']");
        /// <summary>
        /// To Accept all cookies when the Cookie pop up appears
        /// </summary>
        public void AcceptCookies()
        {
            if (driver.FindElement(acceptAllCookiesBtn).Displayed)

                driver.FindElement(acceptAllCookiesBtn).Click();
        }


        By planAJourneyWidget = By.Id("#plan-a-journey");

        By fromPlace = By.Id("InputFrom");

        By inputFromDropdown = By.CssSelector("#InputFrom-dropdown");

        /// <summary>
        /// To select the from station in the Plan a Journey . Provide the full name of the station as parameter 
        /// so that it can search the name from the auto complete suggestions
        /// </summary>
        /// <param name="place"></param>
        public void FromStation(string fromStationName)
        {
            driver.FindElement(fromPlace).Clear();
            driver.FindElement(fromPlace).SendKeys(fromStationName);
            
            IWebElement dropdown = driver.FindElement(inputFromDropdown);
            if (dropdown.Displayed)
            {
                WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
                wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(inputFromDropdown));

                List<IWebElement> stopElement = new List<IWebElement>(dropdown.FindElements(By.ClassName("stop-name")));


                foreach (IWebElement element in stopElement)
                {
                    string cssvalue = element.GetAttribute("class");

                    if (element.Text.Contains(fromStationName))
                        element.Click();
                    Thread.Sleep(2000);
                    break;
                }
            } 
        }


        By toPlace = By.Id("InputTo");


        By inputToDropdown = By.CssSelector("#InputTo-dropdown");
        /// <summary>
        /// To select the To station in the Plan a Journey . Provide the full name of the station as parameter 
        /// so that it can search the name from the auto complete suggestions
        /// </summary>
        /// <param name="place"></param>
        public void ToStation(string toStationName)
        {
            driver.FindElement(toPlace).Clear();
            driver.FindElement(toPlace).SendKeys(toStationName);
            Thread.Sleep(2000);
            IWebElement dropdown = driver.FindElement(inputToDropdown);
            if (dropdown.Displayed)
            {
                WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
                wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible(inputToDropdown));

                List<IWebElement> stopElement = new List<IWebElement>(dropdown.FindElements(By.ClassName("stop-name")));
               

                foreach (IWebElement element in stopElement)
                {
                    string cssvalue = element.GetAttribute("class");
                    string placeName = element.Text;
                    if (element.Text.Contains(toStationName) & element.Enabled)
                        element.Click();
                    
                    break;
                }
            }            

        }


        By planJourneyBtn = By.CssSelector("#plan-journey-button");
        /// <summary>
        /// To click on 'Plan Journey' button
        /// </summary>
        /// <returns></returns>
        public JourneyResultsPage ClickPlanJourneyBtn()
        {
            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
            wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(planJourneyBtn));
            driver.FindElement(planJourneyBtn).Click();
            return new JourneyResultsPage(driver);
        }

    }
}
