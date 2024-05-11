Feature: DeleteTaskByItsId

Background: Create task for future get tests
	Given requesting user id which will be used for creating task before deleting it is "testId"
	And requesting user type which will be used for creating task before deleting it is "BACKOFFICE_PROFESSIONAL"
	And header user id which will be used for creating task before deleting it is "test_user_id"
	And storage id which will be used for creating task before deleting it is "idfordelete"
	When post task request before deleting it is sent
	Then I save task id

Scenario: Delete task by providing allowed user types
	Given id which will be used for deleting task is "task id placeholder"
	And requesting user type for deleting task is <requesting_user_type>
	And query reason is "test purposes"
	And delete mode is "DELETE"
	When delete task request is sent
	Then status code should be 204
Examples:
	| requesting_user_type    |
	| BACKOFFICE_PROFESSIONAL |
	| CLIENT_SYSTEM           |

Scenario: Delete task by providing forbidden user types
	Given id which will be used for deleting task is "task id placeholder"
	And requesting user type for deleting task is <requesting_user_type>
	And query reason is "test purposes"
	When delete task request is sent
	Then status code should be 403
	And forbidden request message from delete task request should have text "Access to this resource is denied."
	And error code should be "TASK_MGMT_INVALID_ACCESS_RIGHTS"
	And I delete task which was created
Examples:
	| requesting_user_type |
	| EMPLOYER             |
	| PARTICIPANT          |

Scenario: Delete task by providing incorrect data
	Given id which will be used for deleting task is "task_id_placeholder"
	And bad requesting user id for deleting task is <requesting_user_id>
	And requesting user type for deleting task is <requesting_user_type>
	And bad header user id which will be used for deleting task is <user_id>
	And query reason is "test purposes"
	And delete mode is "DELETE"
	When delete task request is sent
	Then status code should be 400
	And bad request message from delete task request should have text <message> in the field <field>
	And error code should be "INVALID_MODEL_RECIEVED_BAD_REQUEST"
	And I delete task which was created
Examples:
	| requesting_user_id | requesting_user_type    | user_id      | field              | message                          |
	|                    | BACKOFFICE_PROFESSIONAL | test_user_id | requesting_user_id | Field cannot be empty.           |
	| testId             | BACKOFFICE_PROFESSIONAL |              | user_id            | Field cannot be empty.           |
	| testId             | BACKOFFICE_PROFESSIONAL | doesntexist  | doesntexist        | User with this ID doesn't exist. |
    
Scenario: Delete task with id that doesn't exist or with another user id
	Given non-existent id which will be used for deleting task is "wrongtaskid"
	And requesting user type for deleting task is <requesting_user_type>
	And query reason is "test purposes"
	When delete task request with non-existent id is sent
	Then status code should be 404
	And not found message from delete task request should have message "Resource Not Found"
	And error code should be "INVALID_RESOURCE_REQUESTED"
	And I delete task which was created
Examples:
	| requesting_user_type    |
	| BACKOFFICE_PROFESSIONAL |
   
 
