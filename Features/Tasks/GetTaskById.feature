Feature: GetTaskById

Background:
	Given storage ids which will be used for creating task before getting it are "participant-storage-testid2", "employer-storage-testid1"
	And requesting user id which will be used for creating task before getting it is "testId"
	And requesting user type which will be used for creating task before getting is "BACKOFFICE_PROFESSIONAL"
	And header user id which will used for creating task before getting it is "id"
	When post task for get request with first id is sent
	And post task for get request with second id is sent
	Then I save the id of storage and task

Scenario: Get task by id using backoffice and client system user types
	Given id which will be used for getting task is "task id placeholder"
	And requesting user type which will be used for getting task is <requesting_user_type>
	When get task by id is sent
	Then status code should be 200
	And response body from get task should contain <storage_id>, <created_by>
	And I delete created task
Examples:
	| requesting_user_type    | created_by | storage_id                  |
	| BACKOFFICE_PROFESSIONAL | testId     | participant-storage-testid2 |
	| CLIENT_SYSTEM           | testId     | participant-storage-testid2 |

Scenario: Get task by id using participant user type
	Given id which will be used for getting task with participant type is "task id placeholder"
	And requesting user id which will be used for getting task with participant type is an end of storage_id
	And requesting user type which will be used for getting task with participant type is "PARTICIPANT"
	When get task by id with participant type is sent
	Then status code should be 200
	And response body from get task should contain <storage_id>, <created_by>
	And I delete created task
Examples:
	| created_by | storage_id                  |
	| testId     | participant-storage-testid2 |

Scenario: Get task by id using employer user type
	Given id which will be used for getting task with employer type is "task id placeholder"
	And requesting user id which will be used for getting task with employer type is an end of storage_id
	And requesting user type which will be used for getting task with employer type is "EMPLOYER"
	When get task by id with employer type is sent
	Then status code should be 200
	And response body from get task should contain <storage_id>, <created_by>
	And I delete created task
Examples:
	| created_by | storage_id               |
	| testId     | employer-storage-testid1 |

Scenario: Get task by id using task ids of other user types
	Given id which will be used for getting task is "task id placeholder"
	And requesting user type which will be used for getting task is <requesting_user_type>
	When get task by id is sent
	Then status code should be 403
	And forbidden request message from get task request should have text "Access to this resource is denied."
	And error code should be "TASK_MGMT_INVALID_ACCESS_RIGHTS"
	And I delete created task
Examples:
	| requesting_user_type |
	| PARTICIPANT          |
	| EMPLOYER             |

Scenario: Get task by id inserting incorrect data
	Given id which will be used for getting task is "task id placeholder"
	And requesting user id which will be used for getting task with wrong data is <requesting_user_id>
	And requesting user type which will be used for getting task is <requesting_user_type>
	And header user id which will be used for getting task is with wrong data is <header_user_id>
	When get task by id is sent
	Then status code should be 400
	And bad request message from get task request should have text <message> in the field <field>
	And error code should be "TASKMGMT_INVALID_MODEL_RECIEVED_BAD_REQUEST"
	And I delete created task
Examples:
	| requesting_user_id | requesting_user_type    | header_user_id     | message                         | field                |
	|                    | BACKOFFICE_PROFESSIONAL | id                 | Field cannot be empty.          | requesting_user_id   |
	| testId             |                         | id                 | Field cannot be empty.          | requesting_user_type |
	| testId             | BACKOFFICE_PROFESSIONAL |                    | Field cannot be empty.          | user_id              |
	| testId             | BACKOFFICE_PROFESSIONAL | tenatiddoesntexist | User with this ID doesnt exist. | user_id              |

Scenario: Get task with id that doesn't exist
	Given non-existent id which will be used for getting task is "taskiddoesntexist"
	And requesting user type which will be used for getting task is <requesting_user_type>
	When get task by non-existent id is sent
	Then status code should be 404
	And not found request message from get task request should have text "Resource Not Found"
	And error code should be "TASKMGMT_INVALID_RESOURCE_REQUESTED"
	And I delete created task
Examples:
	| requesting_user_type    |
	| BACKOFFICE_PROFESSIONAL |
