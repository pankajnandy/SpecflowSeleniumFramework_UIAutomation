using BoDi;
using OpenQA.Selenium;
using Specflow_UIAutomation_TFL.Utils;
using System.Configuration;
using TechTalk.SpecFlow;

namespace Specflow_UIAutomation_TFL.Hooks
{
    [Binding]
    public sealed class Hooks
    {        
        // For additional details on SpecFlow hooks see http://go.specflow.org/doc-hooks
        IWebDriver driver;
        WebDriverFactory webDriverFactory;
        private readonly IObjectContainer _container;

        public TestContext TestContext { get; set; }

        public Hooks(IObjectContainer container)
        {
            _container = container;
        }

        /// <summary>
        /// SetUp required before running Test. For eg. Data loading, report set up etc
        /// </summary>
        /// <param name="testContext"></param>
        [BeforeTestRun]
        public static void BeforeTestRun(TestContext testContext)
        {
            Console.WriteLine("Before test run");
            var dir = testContext.TestRunDirectory;
            Reporter.SetUpReport(dir, "RegressionTest", "Regression test result");

        }

        [BeforeFeature]
        public static void BeforeFeature(FeatureContext featureContext)
        {
            Console.WriteLine("Running before feature...");
            Reporter.CreateFeature(featureContext.FeatureInfo.Title);
        }


        [BeforeScenario("@tag1")]
        public void BeforeScenarioWithTag()
        {
            // Example of filtering hooks using tags. (in this case, this 'before scenario' hook will execute if the feature/scenario contains the tag '@tag1')
            // See https://docs.specflow.org/projects/specflow/en/latest/Bindings/Hooks.html?highlight=hooks#tag-scoping

            
        }
        /// <summary>
        /// SetUp require before running each scenario.
        /// The Browser info is being picked up from App.config
        /// </summary>
        /// <param name="scenarioContext"></param>
        [BeforeScenario(Order = 1)]
        public void FirstBeforeScenario(ScenarioContext scenarioContext)
        {
            // Example of ordering the execution of hooks
            // See https://docs.specflow.org/projects/specflow/en/latest/Bindings/Hooks.html?highlight=order#hook-execution-order

            Console.WriteLine("Running before scenario...");
            Reporter.CreateScenario(scenarioContext.ScenarioInfo.Title);

            string browserName = ConfigurationManager.AppSettings["browser"];
            webDriverFactory = new WebDriverFactory();

            driver = webDriverFactory.StartBrowser(browserName);

            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(5);
            driver.Manage().Window.Maximize();

            _container.RegisterInstanceAs<IWebDriver>(driver);

        }

        /// <summary>
        /// ACtion to be done after each sten in a Scenario. eg. Logging
        /// </summary>
        /// <param name="scenarioContext"></param>
        [AfterStep]
        public void AfterStep(ScenarioContext scenarioContext)
        {
            Console.WriteLine("Running after step....");
            string stepType = scenarioContext.StepContext.StepInfo.StepDefinitionType.ToString();
            string stepName = scenarioContext.StepContext.StepInfo.Text;


            //When scenario passed
            if (scenarioContext.TestError == null)
            {
                if (stepType == "Given")
                {
                    Reporter.CreateGivenStep(stepName);
                }
                else if (stepType == "When")
                {
                    Reporter.CreateWhenStep(stepName);
                }
                else if (stepType == "Then")
                {
                    Reporter.CreateThenStep(stepName);
                }
                else if (stepType == "And")
                {
                    Reporter.CreateAndStep(stepName);
                }
            }

            //When scenario fails
            if (scenarioContext.TestError != null)
            {

                if (stepType == "Given")
                {
                    Reporter.CreateGivenStep(stepName).Fail(scenarioContext.TestError);
                }
                else if (stepType == "When")
                {
                    Reporter.CreateWhenStep(stepName).Fail(scenarioContext.TestError);
                }
                else if (stepType == "Then")
                {
                    Reporter.CreateThenStep(stepName).Fail(scenarioContext.TestError);
                }
                else if (stepType == "And")
                {
                    Reporter.CreateAndStep(stepName).Fail(scenarioContext.TestError);
                }
            }
        }


        /// <summary>
        /// Clean up for action to be done after each scenario execution.
        /// </summary>
        [AfterScenario]
        public void AfterScenario()
        {
            Console.WriteLine("Running after scenario...");
            var driver = _container.Resolve<IWebDriver>();

            if (driver != null)
            {
                driver.Quit();
            }
        }

        [AfterFeature]
        public static void AfterFeature()
        {
            Console.WriteLine("Running after feature...");
        }

        [AfterTestRun]
        public static void AfterTestRun(TestContext testContext)
        {
            Reporter.FlushReport();
        }
    }
}