Feature: DeleteStorageByItsId

Background:
	Given storage id which will be used for delete is "storageid"
	And requesting user id for creating storage before delete is "testId"
	And requesting user type for creating storage before delete is "BACKOFFICE_PROFESSIONAL"
	And user id from header parameter for creating storage before delete is "id"
	And name for storage is "delete"
	And icon for storage "delete.png"
	When post storage before delete request is sent

Scenario: Delete storage by providing allowed user types
	Given id which will be used for deleting storage is "id placeholder"
	And requesting user type for deleting storage is <requesting_user_type>
	When delete storage request is sent
	Then status code should be 204
	And response body from delete storage is empty
Examples:
	| requesting_user_type    |
	| CLIENT_SYSTEM           |
	| BACKOFFICE_PROFESSIONAL |

Scenario: Delete storage by providing forbidden user types
	Given id which will be used for deleting storage is "id placeholder"
	And requesting user type for deleting storage is <requesting_user_type>
	When delete storage request is sent
	Then status code should be 403
	And forbidden message from delete storage request should have text "Access to this resource is denied."
	And error code should be "INVALID_ACCESS_RIGHTS"
	And I delete storage which was created
Examples:
	| requesting_user_type |
	| PARTICIPANT          |
	| EMPLOYER             |

Scenario: Delete storage by inserting bad data
	Given id which will be used for deleting storage is " id placeholder"
	And bad requesting user id for deleting storage is <requesting_user_id>
	And requesting user type for deleting storage is <requesting_user_type>
	And bad user id from header parameter is <user_id>
	When delete storage request is sent
	Then status code should be 400
	And bad request message from delete storage request should have text <message> in the field <field>
	And error code should be "INVALID_MODEL_RECIEVED_BAD_REQUEST"
	And I delete storage which was created
Examples:
	| requesting_user_id | requesting_user_type    | user_id | field                | message               |
	|                    | CLIENT_SYSTEM           | id      | requesting_user_id   | Field cannot be empty |
	| testId             |                         | id      | requesting_user_type | Field cannot be empty |
	| testId             | BACKOFFICE_PROFESSIONAL |         | user_id            | Field cannot be empty |

Scenario: Delete storage by id that does not exist
	Given nonexisting id which will be used for deleting storageis "iddoesnotexist"
	And requesting user type for deleting storage is <requesting_user_type>
	When delete storage request with nonexisting id is sent
	Then status code should be 404
	And not found message from delete storage request should have text "Resource Not Found"
	And error code should be "INVALID_RESOURCE_REQUESTED"
	And I delete storage which was created
Examples:
	| requesting_user_type    |
	| BACKOFFICE_PROFESSIONAL |
