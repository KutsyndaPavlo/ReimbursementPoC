Feature: customerSubmission

A short summary of the feature

#@tag1
Scenario: Add a customerSubmission
	#Given I am a client
	Given program  was created  
	Given service  was created2
	Given vendorSubmision was created2
	When I make a POST request in order to create a customerSubmission
	Then the response status code is '201' and the customerSubmission response data are valid
    Then customerSubmission was canceled	
	Then vendorSubmision was canceled2
	Then service  was deleted2
	Then program was deleted

Scenario: Cancel a customerSubmission
	#Given I am a client
	Given program  was created
	Given service  was created2
	Given vendorSubmision was created2
	Given customerSubmission was created
	When I make a Cancel request in order to cancel an existing customerSubmission
    Then the response status code of cancel  is '200' and the customerSubmission response data are valid
	Then vendorSubmision was canceled2
	Then service  was deleted2
	Then program was deleted

Scenario: Get a customerSubmission by id
	#Given I am a client
	Given program  was created
	Given service  was created2
	Given vendorSubmision was created2
	Given customerSubmission was created
	When I make a Get by id request in order to get an existing customerSubmission
    Then the response status code of get is '200' and the get customerSubmission by id response data are valid2
	Then customerSubmission was canceled	
	Then vendorSubmision was canceled2
	Then service  was deleted2
	Then program was deleted

