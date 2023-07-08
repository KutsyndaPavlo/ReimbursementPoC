Feature: Program

A short summary of the feature

@tag1
Scenario: Add a program
	#Given I am a client
	When I make a POST request in order to create a program
	Then the response status code is '201' and the response data are valid
    Then Program was deleted

Scenario: Update a program
	#Given I am a client
	Given Program was created
	When I make a PUT request in order to update an existing program
    Then the response status code of update is '200' and the response data are valid
	Then Program was deleted

Scenario: Delete a program
	#Given I am a client
	Given Program was created
	When I make a Delete request in order to delete an existing program
    Then the response status code of delete  is '204' and the response data are valid

Scenario: Get a program by id
	#Given I am a client
	Given Program was created
	When I make a Get by id request in order to get an existing program
    Then the response status code of get is '200' and the response data are valid
	Then Program was deleted
