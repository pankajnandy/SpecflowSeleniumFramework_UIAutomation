using OpenQA.Selenium;
using OpenQA.Selenium.BiDi.Modules.BrowsingContext;
using OpenQA.Selenium.Support.UI;
using Specflow_UIAutomation_TFL.Pages;
using System;
using System.Text.RegularExpressions;
using TechTalk.SpecFlow;

namespace Specflow_UIAutomation_TFL.StepDefinitions
{
    [Binding]
    public sealed class JourneyPlannerStepDefinitions
    {

        private IWebDriver driver;
        ScenarioContext _specflowContext;
        PlanAJourneyPage _planAJourneyPage;
        JourneyResultsPage _journeyResultsPage;

        public JourneyPlannerStepDefinitions(IWebDriver driver, ScenarioContext scenarioContext) 
        { 
            this.driver = driver;
            _specflowContext = scenarioContext;
        }

        [Given(@"the user is on the journey planning widget page")]
        public void GivenTheUserIsOnTheJourneyPlanningWidgetPage()
        {
            driver.Url = "https://tfl.gov.uk/plan-a-journey/";
        }

        [When(@"the user enters ""([^""]*)"" as the starting point")]
        public void WhenTheUserEntersAsTheStartingPoint(string fromStation)
        {
            _planAJourneyPage = new PlanAJourneyPage(driver);
            _planAJourneyPage.AcceptCookies();
            _planAJourneyPage.FromStation(fromStation);
        }

        [When(@"the user enters ""([^""]*)"" as the destination")]
        public void WhenTheUserEntersAsTheDestination(string toStation)
        {
            if (_planAJourneyPage == null)
            {
                _planAJourneyPage = new PlanAJourneyPage(driver);
                _planAJourneyPage.AcceptCookies();
            }
            _planAJourneyPage.ToStation(toStation);
        }

        [When(@"the user clicks on Plan Journey button")]
        public void WhenTheUserClicksOnPlanJourneyButton()
        {
           _journeyResultsPage = _planAJourneyPage.ClickPlanJourneyBtn();
        }


        [Then(@"the widget should display a valid journey plan")]
        public void ThenTheWidgetShouldDisplayAValidJourneyPlan()
        {
            string title = _journeyResultsPage.GetPageTitle();
            Assert.AreEqual("Journey results - Transport for London", title);
            Assert.IsTrue(_journeyResultsPage.IsJourneyOptionsVisible());
        }

        [Then(@"Verify Journey results page is not displayed")]
        public void ThenVerifyJourneyResultsPageIsNotDisplayed()
        {
            string title = _journeyResultsPage.GetPageTitle();
            Assert.AreNotEqual("Journey results - Transport for London", title);
        }


        [Then(@"verify the result for both walking and cycling time")]
        public void ThenVerifyTheResultForBothWalkingAndCyclingTime()
        {

            string walkingtime = _journeyResultsPage.GetJourneyTimeInMin("walking");
            Assert.AreEqual("6mins", walkingtime, "Walking Time");

            string cyclingTime = _journeyResultsPage.GetJourneyTimeInMin("cycling");
            Assert.AreEqual("1mins",cyclingTime, "Cycling Time");
            
        }

        [Then(@"the user click Edit Preferences")]
        public void ThenTheUserClickEditPreferences()
        {
            _journeyResultsPage.EditPrefrencesBtnClick();
        }

        [Then(@"selects ""([^""]*)"" and then click Update Journey")]
        public void ThenSelectsAndThenClickUpdateJourney(string journeyPreference)
        {
            _journeyResultsPage.SelectJourneyPreference(journeyPreference);
            _journeyResultsPage.UpdateJourneyBtnClick();
        }


        [Then(@"verify the journey time")]
        public void ThenVerifyTheJourneyTime()
        {
            List<IWebElement> journeyResult = _journeyResultsPage.JourneyTimeBox();
            foreach (var journey in journeyResult)
            {
                Thread.Sleep(2000);
                string journeyDetails = journey.Text;

                var departTime = Regex.Match(journeyDetails, @"Depart at:\r\n(\d{2}:\d{2})").Groups[1].Value;
                var arrivalTime = Regex.Match(journeyDetails, @"Arrive at:\r\n(\d{2}:\d{2})").Groups[1].Value;
                var totalTime = Regex.Match(journeyDetails, @"Total time:\r\n(\d+mins)").Groups[1].Value;

                // Extracting the additional travel option details if needed
                var travelDetails = Regex.Match(journeyDetails, @"Option 1: (.*?)(?=Depart)").Groups[1].Value;

                // Convert times to TimeSpan for calculation
                TimeSpan depart = TimeSpan.Parse(departTime);
                TimeSpan arrive = TimeSpan.Parse(arrivalTime);
                TimeSpan difference = arrive - depart;

                int totalMinutes = int.Parse(Regex.Match(totalTime, @"\d+").Value);                

                Assert.AreEqual(totalMinutes, difference.TotalMinutes, "Journey Detail text is :" + journeyDetails);

                /*
                List<IWebElement> timeElements = new List<IWebElement>(journey.FindElements(By.ClassName("time")));
                foreach (var ele in timeElements)
                {
                    string attribute1 = ele.GetAttribute("class");
                    string value2 = ele.Text;
                    string attribute3 = ele.GetAttribute("value");
                    string test1 = ele.ToString();                   
                
                }

                List<IWebElement> durationsElements = new List<IWebElement>(journey.FindElements(By.ClassName("journey-time")));
                foreach (var ele in durationsElements)
                {
                    string attribute1 = ele.GetAttribute("class");
                    string value2 = ele.Text;
                    string attribute3 = ele.GetAttribute("value");
                    string test1 = ele.ToString();

                }*/

            }
          
        }

        [Then(@"click View Details")]
        public void ThenClickViewDetails()
        {
            _journeyResultsPage.ViewDetailsBtnClick();
        }

        [Then(@"verify complete access information at ""([^""]*)""")]
        public void ThenVerifyCompleteAccessInformationAt(string stationName)
        {
            List<IWebElement> accessInformation=  _journeyResultsPage.FindStationAccessElement(stationName);
            Assert.AreEqual(3, accessInformation.Count, "number of station access info");
            Assert.AreEqual("Up stairs", accessInformation[0].GetAttribute("aria-label"), "first access info is Up stairs ");
            Assert.AreEqual("Up lift", accessInformation[1].GetAttribute("aria-label"), "second access info is Up lift ");
            Assert.AreEqual("Level walkway", accessInformation[2].GetAttribute("aria-label"), "first access info is Level walkway");
        }


        [Then(@"Verify the widget should not provide journey options")]
        public void ThenVerifyTheWidgetShouldNotProvideJourneyOptions()
        {
            //Assert.IsFalse(_journeyResultsPage.IsJourneyOptionsVisible(),"Journey results not displayed for Invalid journey ");

            try
            {
                var element = driver.FindElement(By.CssSelector(".extra-journey-options"));
                Assert.Fail("Element is found, but it should not be.");
            }
            catch (NoSuchElementException)
            {
                // Element is not found, which is the expected outcome 
                Assert.IsFalse(false,"Journey is not found as expected.");
            }
        }

    }

    

}
