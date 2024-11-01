using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Specflow_UIAutomation_TFL.Pages
{
    public class JourneyResultsPage
    {
        private IWebDriver driver;

        public JourneyResultsPage(IWebDriver driver)
        {
            this.driver = driver;
        }

        By planAJourneyMenu = By.CssSelector(".plan-journey");
        public PlanAJourneyPage ClickPlanJourneyMenu()
        {
            driver.FindElement(planAJourneyMenu).Click();
            return new PlanAJourneyPage(driver);
        }

        By editJourneyTag = By.CssSelector(".edit-journey");
        public void ClickEditJourneyTag()
        {
            driver.FindElement(editJourneyTag).Click();            
        }

        By editPrefrencesBtn = By.CssSelector(".more-options");
        public void EditPrefrencesBtnClick()
        {
            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
            wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible(editPrefrencesBtn));
            driver.FindElement(editPrefrencesBtn).Click();
        }

        By hidePrefrencesBtn = By.CssSelector(".less-options");
        public void HidePrefrencesBtnClick()
        {
            driver.FindElement(hidePrefrencesBtn).Click();
        }


        By leastWalkpreferenceRadioBtn = By.CssSelector("#JourneyPreference_2");
        public void SelectLeastWalkingOption()
        {
            driver.FindElement(leastWalkpreferenceRadioBtn).Click();
        }


        By journeyOptionWalking = By.CssSelector(".walking>div>div.journey-info");

        public string GetJourneyTimeInMin(string journeyOption)
        {
            string journeyOptionCssPath = $".{journeyOption.ToLower()}>div>div.journey-info";            

            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
            wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible(By.CssSelector(".extra-journey-options")));
            IWebElement ele = driver.FindElement(By.CssSelector(journeyOptionCssPath));
            return ele.Text;
        }

        public string GetPageTitle()
        {
            return driver.Title;
        }

        By jourPrefShowMeList = By.CssSelector(".show-me-list>fieldset>div>div>div");
        /// <summary>
        /// To Select the Journey Option prefrence. Need to pass the journey prefrence locator 'value' atttribute details.
        /// for eg. 'leasttime' for fastest route,'leastinterchange' for Routes with fewest change, 'lease walking' for Routes with least walking
        /// </summary>
        /// <param name="preference"></param>
        public void SelectJourneyPreference(string preference)
        {
            IWebElement dropdown = driver.FindElement(jourPrefShowMeList);
            List<IWebElement> journeyPrefElement = new List<IWebElement>(dropdown.FindElements(By.XPath("//input[@name='JourneyPreference']")));

            foreach (IWebElement element in journeyPrefElement)
            {
                string test = element.GetAttribute("value");
                if (element.GetAttribute("value").Contains("leastwalking"))
                {
                    //element.Click();
                    driver.FindElement(By.XPath("//label[@for='JourneyPreference_2']")).Click();

                    break;
                }
            }

        }


        By updateJournetBtn = By.CssSelector("div.update-buttons-container:nth-child(10) > div:nth-child(1) > input:nth-child(2)");

        public void UpdateJourneyBtnClick()
        {
            driver.FindElement(updateJournetBtn).Click();  
        }

        By summaryResults = By.CssSelector(".summary-results");

        /// <summary>
        /// Get the List of Journey TimeBoxes
        /// </summary>
        /// <returns></returns>
        public List<IWebElement> JourneyTimeBox()
        {

            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
            wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible(summaryResults));
            IWebElement ele = driver.FindElement(summaryResults);
            List<IWebElement> jrnyOptions = new List<IWebElement>(ele.FindElements(By.ClassName("time-boxes")));
            return jrnyOptions;
        }

        /// <summary>
        /// Getthe List Journey Time in the page
        /// </summary>
        /// <returns></returns>
        public List<IWebElement> JourneyDuration()
        {

            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
            wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible(summaryResults));
            IWebElement ele = driver.FindElement(summaryResults);
            List<IWebElement> jrnyOptions = new List<IWebElement>(ele.FindElements(By.ClassName("journey-time")));
            return jrnyOptions;
        }

        By viewDetails = By.CssSelector("#option-1-content > div:nth-child(2) > div:nth-child(5) > div:nth-child(2) > button:nth-child(1)");

        public void ViewDetailsBtnClick()
        {
            driver.FindElement(viewDetails).Click();
        }

        By journeyStepHeading = By.CssSelector(".step-heading");

        /// <summary>
        /// To get the access details element list for the required stationName
        /// </summary>
        /// <param name="stationName"></param>
        /// <returns></returns>
        public List<IWebElement> FindStationAccessElement(string stationName)
        {
            List<IWebElement> accessDetails = new List<IWebElement>();
            List<IWebElement> stepHeadings = new List<IWebElement>(driver.FindElements(journeyStepHeading));
            foreach (var step in stepHeadings)
            {
                string attribute = step.Text;
                List<IWebElement> stopLocations = new List<IWebElement>(step.FindElements(By.ClassName("location-name")));
                //IWebElement stopLocations = step.FindElement(By.ClassName("location-name"));
                foreach (IWebElement stopLocation in stopLocations)
                {
                    string stop= stopLocation.Text;
                    if (stopLocation.Text.Equals(stationName))
                    {
                        IWebElement accesseInfo = step.FindElement(By.ClassName("access-information"));
                        List<IWebElement> accesses = new List<IWebElement>(accesseInfo.FindElements(By.TagName("a")));
                        accessDetails = accesses;
                        break;
                    }
                }  

            }
            return accessDetails;
        }

        By journeyOptions = By.CssSelector(".extra-journey-options");

        public bool IsJourneyOptionsVisible()
        {
            return driver.FindElement(journeyOptions).Displayed;
        }

    }
}
