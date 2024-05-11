Feature: UpdateStorageById

Background:
	Given user id from header parameter which will be used for updating storage is "id"
	And requesting user id for updating storage is "testId"
	And id which will be created for upd is "storage_id"
	And name which will be created for upd is "Name"
	And requesting user type which will be used for upd "BACKOFFICE_PROFESSIONAL"
	When I send request
	Then I save storage id

Scenario: Update storage by its id with allowed user types
	Given storage name which will be used as new value is <name>
	And storage icon which will be used as new values is <icon>
	And requesting user type for updating storage is <requesting_user_type>
	When update storage request is sent
	Then status code should be 200
	And response body from update storage equals <name>, <icon>
Examples:
	| name                   | icon                        | requesting_user_type    |
	| updatedBackofficeName  | updatedBackofficeIcon.png   | BACKOFFICE_PROFESSIONAL |
	| updatedClienSystemName | updatedClientSystemIcon.png | CLIENT_SYSTEM           |

Scenario: Update storage by inserting only name
	Given id which will be used for updating storage is <storage_id>
	And storage name which will be used as new value is <name>
	And requesting user type for updating storage is <requesting_user_type>
	When update storage request is sent
	Then status code should be 200
	And response body from update storage equals <name>, <icon>
Examples:
	| storage_id    | name | requesting_user_type    |
	| id-for-update | Name | BACKOFFICE_PROFESSIONAL |

Scenario: Update storage by inserting only icon
	Given id which will be used for updating storage is <storage_id>
	And storage icon which will be used as new values is <icon>
	And requesting user type for updating storage is <requesting_user_type>
	When update storage request is sent
	Then status code should be 200
Examples:
	| storage_id    | icon                 | requesting_user_type    |
	| id-for-update | WithoutName-Icon.png | BACKOFFICE_PROFESSIONAL |

Scenario: Update storage by its id with forbidden user types
	Given id which will be used for updating storage is <storage_id>
	And requesting user type for updating storage is <requesting_user_type>
	When update storage request is sent
	Then status code should be 403
	And forbidden request message from update storage request should have text "Access to this resource is denied."
	And error code should be "INVALID_ACCESS_RIGHTS"
Examples:
	| storage_id    | requesting_user_type |
	| id-for-update | PARTICIPANT          |
	| id-for-update | EMPLOYER             |

Scenario: Updating storage by insert wrong data
	Given id which will be used for updating storage is <storage_id>
	And bad requesting user id for updating storage is <requesting_user_id>
	And requesting user type for updating storage is <requesting_user_type>
	And bad user id from header parameter which will be used for updating storage is <user_id>
	When update storage request is sent
	Then status code should be 400
	And bad request message from update storage request should have text <message> in the field <field>
	And error code should be "INVALID_MODEL_RECIEVED_BAD_REQUEST"
Examples:
	| storage_id         | requesting_user_id | requesting_user_type    | user_id    | message                        | field                |
	| bad-request-id-212 |                    | BACKOFFICE_PROFESSIONAL | id         | Field cannot be empty          | requesting_user_id   |
	| bad-request-id-212 | testId             |                         | id         | Field cannot be empty          | requesting_user_type |
	| bad-request-id-212 | testId             | CLIENT_SYSTEM           |            | Field cannot be empty          | user_id              |
	| bad-request-id-212 | testId             | CLIENT_SYSTEM           | doesnexist | user with this ID doesnt exist | user_id              |
@ignore
Scenario: Update storage with id that does not exist
	Given id which will be used for updating storage is <storage_id>
	And requesting user type for updating storage is <requesting_user_type>
	When update storage request is sent
	Then status code should be 404
	And not found request message from update storage request should have text "Resource Not Found"
	And error code should be "INVALID_RESOURCE_REQUESTED"
Examples:
	| storage_id          | requesting_user_type    |
	| id-doesnt-exist-323 | BACKOFFICE_PROFESSIONAL |

Scenario: Send empty request body
	Given id which will be used for updating storage is <storage_id>
	And requesting user type for updating storage is <requesting_user_type>
	When update storage request is sent
	Then status code should be 200
Examples:
	| storage_id    | requesting_user_type    |
	| id-for-update | BACKOFFICE_PROFESSIONAL |
