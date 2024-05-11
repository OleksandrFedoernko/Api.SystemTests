Feature: UnauthorizedRequests

Background:
    Given requesting user id which will be used for unauthorized requests is "testId"
    And requesting user type which will be used for unauthorized requests is "BACKOFFICE_PROFESSIONAL"
    And header user id which will be used for unauthorized requests is "test_user_id"
    And id which will be used as a path parameter in the unauthorized requests is "unauthorizedid"
   
Scenario: send requests without key
    When unauthorized requests are sent
    Then status code should be 401
