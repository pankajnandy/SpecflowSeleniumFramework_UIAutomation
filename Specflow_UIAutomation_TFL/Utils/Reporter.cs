using AventStack.ExtentReports.Gherkin.Model;
using AventStack.ExtentReports;
using AventStack.ExtentReports.Reporter;

namespace Specflow_UIAutomation_TFL.Utils
{
    public static class Reporter
    {
        private static ExtentReports extentReport;
        private static ExtentHtmlReporter htmlReporter;
        //private static ExtentSparkReporter htmlReporter; // from 5.0 version
        private static ExtentTest extentTest;
        private static ExtentTest _feature;
        private static ExtentTest _scenario;

        public static void SetUpReport(dynamic path, string documentTitle, string reportName)
        {
            htmlReporter = new ExtentHtmlReporter(path);
            //htmlReporter = new ExtentSparkReporter(path);
            htmlReporter.Config.Theme = AventStack.ExtentReports.Reporter.Configuration.Theme.Dark;
            htmlReporter.Config.DocumentTitle = documentTitle;
            htmlReporter.Config.ReportName = reportName;

            extentReport = new ExtentReports();
            extentReport.AttachReporter(htmlReporter);
        }

        public static void LogToReport(Status status, string message)
        {
            extentTest.Log(status, message);
        }

        public static void CreateTest(string testName)
        {
            extentTest = extentReport.CreateTest(testName);
        }

        public static void CreateFeature(string featureName)
        {
            _feature = extentReport.CreateTest<Feature>(featureName);
        }

        public static void CreateScenario(string scenarioName)
        {
            _scenario = extentReport.CreateTest<Feature>(scenarioName);
        }

        public static ExtentTest CreateGivenStep(string step)
        {
            return _scenario.CreateNode<Given>(step);
        }

        public static ExtentTest CreateWhenStep(string step)
        {
            return _scenario.CreateNode<When>(step);
        }

        public static ExtentTest CreateThenStep(string step)
        {
            return _scenario.CreateNode<Then>(step);
        }

        public static ExtentTest CreateAndStep(string step)
        {
            return _scenario.CreateNode<And>(step);
        }

        public static void FlushReport()
        {
            extentReport.Flush();
        }

        public static void TestStatus(string status)
        {
            if (status.Equals("Pass"))
            {
                extentTest.Pass("Test case passed");
            }
            else
            {
                extentTest.Fail("Test case failed");
            }
        }
    }
}
