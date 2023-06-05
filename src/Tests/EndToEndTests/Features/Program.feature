Feature: Feature1

A short summary of the feature

@tag1
Scenario: [scenario name]
	Given [context]
	When [action]
	Then [outcome]

Scenario: Add a program
	Given I am a client
	When I make a POST request with 'weather.json' to 'weatherforecast'
	Then the response status code is '201'
	And the response json should be 'weather.json'
