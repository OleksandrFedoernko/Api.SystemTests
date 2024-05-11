Feature: GetAllUsers

Scenario: Get all users using allowed user types
	Given header user_id is "id"
	And requesting_user_id is "testId"
	And requesting_user_type is "<requesting_user_type>"
	When get all users request is sent
	Then status code should be 200
	And amount of users should be equal to the total value
Examples:
	| requesting_user_type    |
	| BACKOFFICE_PROFESSIONAL |
	| CLIENT_SYSTEM           |

Scenario: Get all users using forbidden user types
	Given header user_id is "id"
	And requesting_user_id is "testId"
	And requesting_user_type is "<requesting_user_type>"
	When get all users request is sent
	Then status code should be 403
	And message from get all users should be "Access to this resource is denied."
	And error code should be "INVALID_ACCESS_RIGHTS"
Examples:
	| requesting_user_type |
	| PARTICIPANT          |
	| EMPLOYER             |

Scenario: Get all users by providing wrong data
	Given header user_id is "<header_user_id>"
	And requesting_user_id is "<requesting_user_id>"
	And requesting_user_type is "<requesting_user_type>"
	When get all users request is sent
	Then status code should be 400
	And message from get all users should be <message> in the field <field>
	And error code should be "INVALID_MODEL_RECIEVED_BAD_REQUEST"
Examples:
	| requesting_user_id | requesting_user_type    | header_user_id  | message                         | field                |
	|                    | BACKOFFICE_PROFESSIONAL | id              | Field cannot be empty.          | requesting_user_id   |
	| testId             |                         | id              | Field cannot be empty.          | requesting_user_type |
	| testId             | BACKOFFICE_PROFESSIONAL |                 | Field cannot be empty.          | user_id              |
	| testId             | BACKOFFICE_PROFESSIONAL | userdoesntexist | User with this ID doesnt exist. | user_id              |
