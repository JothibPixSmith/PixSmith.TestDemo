Feature: FooBarRepository

Background: 
Given Configuration is setup up
And Database is registered

@AfterScenario.Database.CleanDatabase
Scenario: FooBar Repository Performs SaveFooBar
	Given FooBar Repository is initialized
	When FooBar Repository SaveFooBar method is called
	Then FooBar Repository returns FooBar with non-null id field

@AfterScenario.Database.CleanDatabase
Scenario: FooBar Repository Performs GetFooBar
	Given FooBar Repository is initialized
	And A default instance of FooBar is present in the Db
	When FooBar Repository GetFooBar method is called
	Then FooBar Repository returns FooBar with non-null id field
