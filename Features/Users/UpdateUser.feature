Feature: UpdateUserById

Background:
	Given header user id which will be used for updating user is "id"
	And requesting user id for updating user is "testId"
	And requesting_user_type which will be created  for upd "BACKOFFICE_PROFESSIONAL"
	And user id which will be created for upd is "userid"
	And user name which will be created for upd is "Name"
	When I send POST user request
	Then I save user id

Scenario: Update user using allowed user types
	Given user id which will be used for updating user is "userid417"
	And user name which will be used for updating user is <name>
	And user icon which will be used for updating user is <description>
	And requesting user type for updating user is <requesting_user_type>
	When update user request is sent
	Then status code should be 200
	And response body from update user should be <name>, <description>
Examples:
	| name    | description | requesting_user_type    |
	| updname | upddesc     | BACKOFFICE_PROFESSIONAL |
	| updname |             | BACKOFFICE_PROFESSIONAL |
	| updname | upddesc     | CLIENT_SYSTEM           |

Scenario: Update user using forbidden user types
	Given user id which will be used for updating user is "userid417"
	And requesting user type for updating user is <requesting_user_type>
	And user name which will be used for updating user is <name>
	When update user request is sent
	Then status code should be 403
	And forbidden request message from update user request should have text "Access to this resource is denied."
	And error code should be "INVALID_ACCESS_RIGHTS"
Examples:
	| requesting_user_type | name |
	| PARTICIPANT          | Name |
	| EMPLOYER             | Name |

Scenario: Update user by inserting wrong data
	Given user id which will be used for updating user is "userid417"
	And user name which will be used for updating user is <name>
	And bad requesting user id for updating user is <requesting_user_id>
	And requesting user type for updating user is <requesting_user_type>
	And bad header user id which will be used for updating user is <header_user_id>
	When update user request is sent
	Then status code should be 400
	And bad request message from update user request should have text <message> in the field <field>
	And error code should be "INVALID_MODEL_RECIEVED_BAD_REQUEST"
Examples:
	| name | requesting_user_id | requesting_user_type    | header_user_id    | message                         | field                |
	|      | testId             | BACKOFFICE_PROFESSIONAL | id                | 'User Name' must not be empty.  | name                 |
	| name |                    | BACKOFFICE_PROFESSIONAL | id                | Field cannot be empty.          | requesting_user_id   |
	| name | testId             |                         | id                | Field cannot be empty.          | requesting_user_type |
	| name | testId             | BACKOFFICE_PROFESSIONAL |                   | Field cannot be empty.          | user_id              |
	| name | testId             | BACKOFFICE_PROFESSIONAL | useriddoesntexist | User with this ID doesnt exist. | user_id              |
@ignore
Scenario: Update user by id that doesn't exist
	Given user id which will be used for updating user is "doesntexist"
	And requesting user type for updating user is <requesting_user_type>
	When update user request is sent
	Then status code should be 404
	And not found request message from update user request should have text "Resource Not Found"
	And error code should be "INVALID_RESOURCE_REQUESTED"
Examples:
	| requesting_user_type    |
	| BACKOFFICE_PROFESSIONAL |
