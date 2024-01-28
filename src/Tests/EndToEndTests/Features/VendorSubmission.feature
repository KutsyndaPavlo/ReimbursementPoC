Feature: vendorSubmission

A short summary of the feature

#@tag1
Scenario: Add a vendorSubmission
	#Given I am a client
	Given program  was created  
	Given service  was created1
	When I make a POST request in order to create a vendorSubmission
	Then the response status code is '201' and the vendorSubmission response data are valid
    Then vendorSubmission was canceled	
	Then service  was deleted1
	Then program was deleted

Scenario: Delete a vendorSubmission
	#Given I am a client
	Given program  was created
	Given service  was created1
	Given vendorSubmission was created
	When I make a Cancel request in order to cancel an existing vendorSubmission
    Then the response status code of cancel  is '200' and the vendorSubmission response data are valid	
	Then service  was deleted1
	Then program was deleted

Scenario: Get a vendorSubmission by id
	#Given I am a client
	Given program  was created
	Given service  was created1
	Given vendorSubmission was created
	When I make a Get by id request in order to get an existing vendorSubmission
    Then the response status code of get is '200' and the get vendorSubmission by id response data are valid
	Then vendorSubmission was canceled	
	Then service  was deleted1
	Then program was deleted

