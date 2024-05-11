Feature: GetStorageById

Background:
	Given ids which will be used for creating storages are "idfortwotypes","participant-storage-part","employer-storage-empl"
	And name which will be used for creating storages are "name for both", "name for part.", "name for empl."
	And icon which will be used for creating storages are "bothTypesIcon.png", "participantIcon.png", "employerIcon.png"
	And requesting user id which will be used for creating storage is "testId"
	And requesting user type which will be used for creating storage is "BACKOFFICE_PROFESSIONAL"
	And header user id which will be used for creating storage is "id"
	When post storage request with different ids is sent
	Then I save created storage ids

Scenario: Get storage by its ID with backoffice and client system user types
	Given id which will be used for getting storage is "storage id placeholder"
	And requesting user type which is <requesting_user_type>
	When get storage by id request is sent
	Then status code should be 200
	And response body from get storage by id equals <name>, <icon>
	And I delete created storage
Examples:
	| requesting_user_type    | name           | icon             |
	| BACKOFFICE_PROFESSIONAL | name for empl. | employerIcon.png |
	| CLIENT_SYSTEM           | name for empl. | employerIcon.png |

Scenario: Get storage by its ID with employer user type
	Given id which will be used for getting storage with employer type is "storage id placeholder"
	And requesting user id which will be used for getting storage with employer type is the ending of a employer storage id
	And requesting user type which will be used for getting storage is "EMPLOYER"
	When get storage by id request with employer type is sent
	Then status code should be 200
	And response body from get storage by id equals <storage_id>, <name>, <icon>
	And I delete created storage

Examples:
	| storage_id            | name           | icon             |
	| employer-storage-empl | name for empl. | employerIcon.png |

Scenario: Get storage by its ID with participant user type
	Given id which will be used for getting storage with participant type is "storage id placeholder"
	And requesting user id which will be used for getting storage with participant type is the ending of a participant storage id
	And requesting user type which will be used for getting storage is "PARTICIPANT"
	When get storage by id request with participant type is sent
	Then status code should be 200
	And response body from get storage by id equals <storage_id>, <name>, <icon>
	And I delete created storage
    
Examples:
	| storage_id               | name           | icon                |
	| participant-storage-part | name for part. | participantIcon.png |

Scenario: Get storage by id unhappy scenarios
	Given non-existent id which will be used for getting storage is <storage_id>
	And requesting user ids which are <requesting_user_id>
	And requesting user type which is <requesting_user_type>
	When get storage request with non-existent id is sent
	Then status codes after unsuccessful get storage by id should be Not Found and Forbidden
	And error message from get storage by id request should have text <message>
	And error code should be "<error_code>"
	And I delete created storage

Examples:
	| storage_id                   | requesting_user_id | requesting_user_type | message                            | error_code                          |
	| employer-storage-empl8837    | part9939           | PARTICIPANT          | Access to this resource is denied. | TASK_MGMT_INVALID_ACCESS_RIGHTS     |
	| participant-storage-part9939 | empl8837           | EMPLOYER             | Access to this resource is denied. | TASK_MGMT_INVALID_ACCESS_RIGHTS     |
	| iddoesnotexist              | testId             | CLIENT_SYSTEM        | Resource Not Found                 | TASKMGMT_INVALID_RESOURCE_REQUESTED |
