# SpecflowSeleniumFramework_UIAutomation
UI Automation Framework with Specflow and C# Selenium


1. Set Up Project:
Created a New Specflow MSTEST Project.
Added Dependencies: Included Dependencies like Selenium WebDriver, Selenium Support, DotNetSeleniumExtras, Extent Reports

2. Project Structure:
Package Structure: Organized project in folder, based on functionality (e.g., pages, tests, utilities, Features, Stepdefinitions, Hooks).
Added App.config for adding browser info or can me manipulated for CI/CD set up.

3. WebDriver Management:
Driver Initialization: Setup class for WebDriver initialization (e.g., DriverFactory). Created Config file to manage Driver config.
Driver Teardown: Implemented mechanism to close or quit the WebDriver after test execution. (e.g., using annotations in Hooks.cs).

4. Page Object Model (POM):
Created Page Classes to represent each web page as a class.
Defined page elements and methods to interact with those elements. Methods in each page should be ideally generic re-usable methods. If a scepific logic particular to the test should be written in the test .
Page Initialization: Implemented mechanism to initialize pages and navigate between them .

5. Hooks:
Hooks class that contains common setup and teardown logic. Initialize WebDriver, set up logging, set up extent reports and handle other common functionalities.

6. Utilities:
Setup Enum for Browser selection from WebDriverFactory.
Created Reporting Class for reporting.
Included WebDriver Factory Implementation

7. Test Scripts:
Write Test Scripts: Create test Scripts using the Specflow feature file in Gherkin language and the step implementaion in the Step Definition files. Use the Page Object Model to interact with page elements.


8. App.config
Added App.config to add browser info. Can be used to manipulate for CI/CD .

9. Test Results:
After Test Execution and automation test execution report is generated inside the Test Results Folder created in the Project Location.
