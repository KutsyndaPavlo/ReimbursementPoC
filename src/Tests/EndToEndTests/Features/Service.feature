Feature: Service

A short summary of the feature

#@tag1
Scenario: Add a service
	#Given I am a client
	Given program  was created
	When I make a POST request in order to create a service
	Then the response status code is '201' and the service response data are valid
    Then service was deleted
	Then program was deleted

Scenario: Update a service
	#Given I am a client
	Given program  was created
	Given service was created
	When I make a PUT request in order to update an existing service
    Then the response status code of update is '200' and the service response data are valid
	Then service was deleted
	Then program was deleted

Scenario: Delete a service
	#Given I am a client
	Given program  was created
	Given service was created
	When I make a Delete request in order to delete an existing service
    Then the response status code of delete  is '204' and the service response data are valid
	Then program was deleted

Scenario: Get a service by id
	#Given I am a client
	Given program  was created
	Given service was created
	When I make a Get by id request in order to get an existing service
    Then the response status code of get is '200' and the get service by id response data are valid
	Then service was deleted
	Then program was deleted

