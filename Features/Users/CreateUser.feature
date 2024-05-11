Feature: CreateUser

Scenario: Create user with allowed user types
	Given id which will be used for creating user is <user_id>
	And name which will be used for creating user is <name>
	And description which will be used for creating user is <description>
	And requesting_user_type is "<requesting_user_type>"
	And header user_id is "user_id"
	And requesting_user_id is "testId"
	When post user request is sent
	Then status code should be 201
	And response body from post user equals <user_id>, <name>, <description>
Examples:
	| user_id    | name                | description     | requesting_user_type    |
	| user_id_   | userName            | userDescription | BACKOFFICE_PROFESSIONAL |
	| user_id_   | userName2           | userDescription | CLIENT_SYSTEM           |
	| user_id_ _ | userNameWithoutDesc |                 | BACKOFFICE_PROFESSIONAL |

Scenario: Create user with forbidden user types
	Given id which will be used for creating user is <user_id>
	And name which will be used for creating user is <name>
	And description which will be used for creating user is <description>
	And requesting_user_type is "<requesting_user_type>"
	And header user_id is "id"
	And requesting_user_id is "testId"
	When post user request is sent
	Then status code should be 403
	And forbidden request message from create user request should have text "Access to this resource is denied"
	And error code should be "INVALID_ACCESS_RIGHTS"
Examples:
	| user_id     | requesting_user_type |
	| forbiddenid | EMPLOYER             |
	| forbiddenid | PATRICIPANT          |

Scenario: Create user by inserting wrong data
	Given id which will be used for creating bad user is <user_id>
	And name which will be used for creating user is <name>
	And requesting_user_type is "<requesting_user_type>"
	And header_user_id is "<header_user_id>"
	And requesting_user_id is "<requesting_user_id>"
	When post user request is sent
	Then status code should be 400
	And bad request message from create user request should have text <message> in the field <field>
	And error code should be "INVALID_MODEL_RECIEVED_BAD_REQUEST"
Examples:
	| user_id    | name           | requesting_user_id | requesting_user_type    | header_user_id  | field                | message                           |
	| testUserId | badrequestname | testId             | BACKOFFICE_PROFESSIONAL | id              | user_id              | UserId is not valid               |
	|            | badrequestname | testId             | BACKOFFICE_PROFESSIONAL | id              | user_id              | Required query parameter is empty |
	| test_id_25 |                | testId             | BACKOFFICE_PROFESSIONAL | id              | name                 | 'User Name' must not be empty.    |
	| test_id_25 | badrequestname |                    | BACKOFFICE_PROFESSIONAL | id              | requesting_user_id   | Field cannot be empty.            |
	| test_id_25 | badrequestname | testId             |                         | id              | requesting_user_type | Field cannot be empty.            |
	| test_id_25 | badrequestname | testId             | BACKOFFICE_PROFESSIONAL |                 | user_id              | Field cannot be empty.            |
	| test_id_25 | badrequestname | testId             | BACKOFFICE_PROFESSIONAL | userdoesntexist | user_id              | User with this ID doesn't exist.  |

Scenario: Send empty request body
	Given requesting_user_type is "<requesting_user_type>"
	And header_user_id is "<header_user_id>"
	And requesting_user_id is "<requesting_user_id>"
	When post user request is sent
	Then status code should be 400
	And bad request message from create user request should have text <message> in the field <field>
	And error code should be "INVALID_MODEL_RECIEVED_BAD_REQUEST"
Examples:
	| requesting_user_type    | field   | message                           |
	| BACKOFFICE_PROFESSIONAL | user_id | Required query parameter is empty |

Scenario: Create user with same user_id
	Given id which will be used for creating user with same id is "id"
	And name which will be used for creating user is <name>
	Given requesting_user_type is "BACKOFFICE_PROFESSIONAL"
	And header user_id is "id"
	And requesting_user_id is "testId"
	When post user request is sent
	Then status code should be 409
	And error code should be "RESOURCE_ALREADY_EXISTS"
Examples:
	| user_id      | name |
	| test_user_id | name |
