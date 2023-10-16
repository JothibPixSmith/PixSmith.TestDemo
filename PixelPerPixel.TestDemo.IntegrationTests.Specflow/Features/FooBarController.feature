Feature: FooBarController


Scenario: FooBar Controller Performs Save FooBar
Given FooBarService SaveBoor Method is Mocked
When FooBar SaveFooBar is called
Then An instance of FooBar is returned

Scenario: FooBar Controller Performs Get FooBar
Given FooBarService GetFooBar Method is Mocked
When FooBar GetFooBar is called
Then An instance of FooBar is returned