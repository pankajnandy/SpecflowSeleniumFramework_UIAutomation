Feature: JourneyPlanner

As a user I want to plan a journey from Leicester Square Underground Station
to Covent Garden Underground Station 
So that I can verify the widget's functionality


Scenario: Valid journey planning 
	Given the user is on the journey planning widget page
    When the user enters "Leicester Square Underground Station" as the starting point
    And the user enters "Covent Garden Underground Station" as the destination
    And the user clicks on Plan Journey button
    Then the widget should display a valid journey plan
    And verify the result for both walking and cycling time


Scenario: Valid Journey with least walking 
	Given the user is on the journey planning widget page
    When the user enters "Leicester Square Underground Station" as the starting point
    And the user enters "Covent Garden Underground Station" as the destination
    And the user clicks on Plan Journey button
    Then the widget should display a valid journey plan
    And the user click Edit Preferences
    Then selects "leastwalking" and then click Update Journey
    And verify the journey time


Scenario: Valid Journey and verify complete access Information of Covent Garden Underground Station
	Given the user is on the journey planning widget page
    When the user enters "Leicester Square Underground Station" as the starting point
    And the user enters "Covent Garden Underground Station" as the destination
    And the user clicks on Plan Journey button
    Then the widget should display a valid journey plan
    And the user click Edit Preferences
    Then selects "leastwalking" and then click Update Journey
    And click View Details
    And verify complete access information at "Covent Garden Underground Station"


Scenario: Journey Planning Invalid Values 
	Given the user is on the journey planning widget page
    When the user enters "abc" as the starting point
    And the user enters "xyz" as the destination
    And the user clicks on Plan Journey button
    Then Verify the widget should not provide journey options


Scenario: Journey Planning with No Locations entered
	Given the user is on the journey planning widget page
    When the user enters "" as the destination
    And the user enters "" as the starting point
    And the user clicks on Plan Journey button
    Then Verify Journey results page is not displayed


