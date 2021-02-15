Feature: GetConditions
	Get Valid User ID - 200 Response - OK
	Get No User Matching ID - 404 - Not Found
	Get Incorrect User ID Format - 400 - Not Able to Create SKipped
	

Scenario: Get Valid User ID
	Given that a Get Request for user '1' is created
    When the Get Request is made
	Then the return status code is 'OK'
    And assert that return body with id set to '1'

Scenario: Get Request for User ID that is not exists
	Given that a Get Request for user '99999999' is created
    When the Get Request is made
	Then the return status code is 'Not Found'
