Feature: customerSubmission

A short summary of the feature

#@tag1
Scenario: Add a customerSubmission
	#Given I am a client
	Given program  was created  
	Given service  was created1
	Given vendorSubmision was created
	When I make a POST request in order to create a customerSubmission
	Then the response status code is '201' and the customerSubmission response data are valid
    Then customerSubmission was deleted	
	Then vendorSubmision was deleted
	Then service  was deleted1
	Then program was deleted

Scenario: Delete a customerSubmission
	#Given I am a client
	Given program  was created
	Given service  was created1
	Given vendorSubmision was created
	Given customerSubmission was created
	When I make a Delete request in order to delete an existing customerSubmission
    Then the response status code of delete  is '204' and the customerSubmission response data are valid
	Then vendorSubmision was deleted
	Then service  was deleted1
	Then program was deleted

Scenario: Get a customerSubmission by id
	#Given I am a client
	Given program  was created
	Given service  was created1
	Given vendorSubmision was created
	Given customerSubmission was created
	When I make a Get by id request in order to get an existing customerSubmission
    Then the response status code of get is '200' and the get customerSubmission by id response data are valid
	Then customerSubmission was deleted	
	Then vendorSubmision was deleted
	Then service  was deleted1
	Then program was deleted

