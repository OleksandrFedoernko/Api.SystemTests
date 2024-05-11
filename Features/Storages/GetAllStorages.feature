Feature: GetAllStorages


Background:
	Given header user id which will be used for getting all storages is "id"
	And requesting user id which will be used for getting all storages is "testId"

Scenario: Get all storages using CLIENT_SYSTEM and BACKOFFICE_PROFESSIONAL user types
	And requesting user type which will be used for getting all storages is <requesting_user_type>
	And limit which will be used for getting all storages is <limit>
	And name which will be used for getting all storages is <name>
	When get all storages request is sent
	Then status code should be 200
	And response body from get all storages from backoffice and client system user types should be equal to  <name> <icon>
Examples:
	| requesting_user_type    | limit | name             | icon     |
	| BACKOFFICE_PROFESSIONAL | 2     | BackofficeName   | Icon.png |
	| CLIENT_SYSTEM           | 2     | ClientSystemName | Icon.png |
	| BACKOFFICE_PROFESSIONAL | 0     | BackofficeName   | Icon.png |
	| CLIENT_SYSTEM           | 0     | ClientSystemName | Icon.png |
	| BACKOFFICE_PROFESSIONAL | 2     |                  | Icon.png |
	| CLIENT_SYSTEM           | 2     |                  | Icon.png |
	| BACKOFFICE_PROFESSIONAL | 0     |                  | Icon.png |
	| CLIENT_SYSTEM           | 0     |                  | Icon.png |

Scenario: Get all storages using PARTICIPANT and EMPLOYER user types
	Given requesting user id which will be used for getting all storages with other user types <requesting_user_id>
	And requesting user type which will be used for getting all storages is <requesting_user_type>
	And limit which will be used for getting all storages is <limit>
	And name which will be used for getting all storages is <name>
	When get all storages request is sent
	Then status code should be 200
	And response body from get all storages from employer and participant user types should be equal to <name> <icon>
Examples:
	| requesting_user_id | requesting_user_type | limit | name                | icon         |
	| participant_123    | PARTICIPANT          | 2     | articipantTestName  | TestIcon.png |
	| employer_321       | EMPLOYER             | 2     | employerTestName    | TestIcon.png |
	| participant_123    | PARTICIPANT          | 0     | participantTestName | TestIcon.png |
	| employer_321       | EMPLOYER             | 0     | employerTestName    | TestIcon.png |
	| participant_123    | PARTICIPANT          | 2     |                     | TestIcon.png |
	| employer_321       | EMPLOYER             | 2     |                     | TestIcon.png |
	| participant_123    | PARTICIPANT          | 0     |                     | TestIcon.png |
	| employer_321       | EMPLOYER             | 0     |                     | TestIcon.png |

Scenario: Get all storages returns epmty list
	And requesting user type which will be used for getting all storages is <requesting_user_type>
	And limit which will be used for getting all storages is <limit>
	When get all storages request is sent
	Then status code should be 200
	And response body from get all storages from with incorrect user IDs should have empty list of storages
Examples:
	| requesting_user_type | limit |
	| PARTICIPANT          | 0     |
	| EMPLOYER             | 0     |
    
Scenario: Get all storages using wrong data
	Given requesting user id which will be used for getting all storages with other user types <requesting_user_id>
	And requesting user type which will be used for getting all storages is <requesting_user_type>
	And wrong header user id which will be used for getting all storages is <header_user_id>
	And limit which will be used for getting all storages is <limit>
	When get all storages request is sent
	Then status code should be 400
	And bad request message from get all storages request should have text <message> in the field <field>
	And error code should be "TASKMGMT_INVALID_MODEL_RECIEVED_BAD_REQUEST"

Examples:
	| requesting_user_id | requesting_user_type    | user_id     | limit | message                                       | field                |
	|                    | BACKOFFICE_PROFESSIONAL | id          | 0     | Field cannot be empty                         | requesting_user_id   |
	| testId             |                         | id          | 0     | Field cannot be empty                         | requesting_user_type |
	| testId             | BACKOFFICE_PROFESSIONAL |             | 0     | Field cannot be empty                         | user_id              |
	| testId             | BACKOFFICE_PROFESSIONAL | doesntexist | 0     | User with this ID doesnt exist                | user_id              |
	| testId             | BACKOFFICE_PROFESSIONAL | id          | -1    | 'Limit' must be greater than or equal to '1'. | limit                |
	| testId             | BACKOFFICE_PROFESSIONAL | id          | 10001 | 'Limit' must be less than or equal to '1000'. | limit                |

