Feature: FooBarService

Scenario: As a FooBar Rest Service User, I want to save a new FooBar
	Given A Default Foobar instance is created
	And the FooBarRepository save method is mocked
	When SaveFooBar is called
	Then the resulting FooBar instance bar property must end with 'abc'


Scenario: As a FooBar Rest Service User, I want to save many new FooBars
	Given A FooBar instance is used with Foo of <Foo> and Bar of '<Bar>'
	And the FooBarRepository save method is mocked
	When SaveFooBar is called
	Then the resulting FooBar instance bar property must end with 'abc'

Examples:
	| Foo | Bar |
	| 1   | 1   |
	| 2   | 2   |
	| 3   | 3   |

Scenario: As a FooBar Rest Service User, I want to get an existing FooBar
	Given A Default Foobar instance is created
	And the FooBarRepository get method is mocked
	When GetFooBar is called
	Then the resulting FooBar instance is not null