Feature: GetUserById

Background:
	Given requesting user id for creating user before getting it is "testId"
	And requesting user type fo creating user before getting it is "BACKOFFICE_PROFESSIONAL"
	And user id from header parameter which will be used creating user before getting it is "id"
	And id which will be used for creating user before getting it is "userid"
	And name which will be used for creating user before getting it is "userName"
	And description which will be used for creating user before getting it is "userDesc"
	When create user for future get request is sent
	Then i save user id for getting it

Scenario: Get user by id using allowed user types
	Given id which will be used in getting user is "user id placeholder"
	And requesting user type for getting user is <requesting_user_type>
	When get user by id request is sent
	Then status code should be 200
	And response body from get user by id equals <user_id>, <name>, <description>
Examples:
	| requesting_user_type    | user_id | name     | description |
	| BACKOFFICE_PROFESSIONAL | userid  | userName | userDesc    |
	| CLIENT_SYSTEM           | userid  | userName | userDesc    |

Scenario: Get user by id using user types that are forbidden
	Given id which will be used in getting user is "user id placeholder"
	And requesting user type for getting user is <requesting_user_type>
	When get user by id request is sent
	Then status code should be 403
	And forbidden request message from get user by id request should have text "Access to this resource is denied."
	And error code should be "TASK_MGMT_INVALID_ACCESS_RIGHTS"
Examples:
	| requesting_user_type |
	| PARTICIPANT          |
	| EMPLOYER             |

Scenario: Get user by id providing incorrect values
	Given id which will be used in getting user is "user id placeholder"
	And bad requesting user id for getting user by id is <requesting_user_id>
	And requesting user type for getting user is <requesting_user_type>
	And bad user id from header parameter which will be used for getting user is <header_user_id>
	When get user by id request is sent
	Then status code should be 400
	And message from bad get user by id request should have text <message> in the field <field>
	And error code should be "TASKMGMT_INVALID_MODEL_RECIEVED_BAD_REQUEST"
Examples:
	| requesting_user_id | requesting_user_type    | header_user_id    | message                          | field                |
	|                    | BACKOFFICE_PROFESSIONAL | id                | Field cannot be empty.           | requesting_user_id   |
	| testId             |                         | id                | Field cannot be empty.           | requesting_user_type |
	| testId             | BACKOFFICE_PROFESSIONAL |                   | Field cannot be empty.           | user_id              |
	| testId             | BACKOFFICE_PROFESSIONAL | userIdDoesntExist | User with this ID doesn't exist. | user_id              |

Scenario: Get user by id that doesnt exist
	Given nonexisting id which will be used in getting user is "wronguserid"
	And requesting user type for getting user is <requesting_user_type>
	When get user with nonexisting id request is sent
	Then status code should be 404
	And message from not found get user by id request should have text "Resource Not Found"
	And error code should be "TASKMGMT_INVALID_RESOURCE_REQUESTED"
Examples:
	| requesting_user_type    |
	| BACKOFFICE_PROFESSIONAL |
