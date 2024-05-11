Feature: GetTaskHistoryById


Background:
    Given storage ids which will be used for creating task before getting task history are "participant-storage-testid2", "employer-storage-testid1", "storagetestid"
    And requesting user id which will be used for creating task before getting task history is "testId"
    And requesting user type which will be used for creating task before getting task history "BACKOFFICE_PROFESSIONAL"
    And header user id which will used for creating task before getting task history "id"
    When post task for get task history request with first id is sent
    And post task for get task history request with second id is sent
    And post task for get task history request with third id is sent
    Then I save task ids
@retry
Scenario: Get history of a task after creating it using backoffice & client system user types
    Given task id which will be used for getting history is an id of created task
    And requesting user type which will be used for getting task history is <requesting_user_type>
    When get task history request is sent
    Then status code should be 200
    And response body from get task history should contain <changed_by_user_id>, <changed_by_user>, <changed_by_user_type>
    And I delete unused task
Examples:
    | requesting_user_type    | changed_by_user_id | changed_by_user | changed_by_user_type    |
    | BACKOFFICE_PROFESSIONAL | testId             | testUser        | BACKOFFICE_PROFESSIONAL |
    | CLIENT_SYSTEM           | testId             | testUser        | BACKOFFICE_PROFESSIONAL |

Scenario: Get history of a task after creating it using participant user type
    Given task id which will be used for getting history is an id of created task
    And requesting user id which will be used for getting task history for participant type is the ending of storage id
    And requesting user type which will be used for getting task history for participant type is "PARTICIPANT"
    When get task history request for participant is sent
    Then status code should be 200
    And response body from get task history should contain <changed_by_user_id>, <changed_by_user>, <changed_by_user_type>
    And I delete unused task
Examples:
    | changed_by_user_id | changed_by_user | changed_by_user_type    |
    | testId             | testUser        | BACKOFFICE_PROFESSIONAL |
    | testId             | testUser        | BACKOFFICE_PROFESSIONAL |

Scenario: Get history of a task after creating it using employer user type
    Given task id which will be used for getting history is an id of created task
    And requesting user id which will be used for getting task history for employer type is the ending of storage id
    And requesting user type which will be used for getting task history for employer type is "EMPLOYER"
    When get task history request for employer is sent
    Then status code should be 200
    And response body from get task history should contain <changed_by_user_id>, <changed_by_user>, <changed_by_user_type>
    And I delete unused task
Examples:
    | changed_by_user_id | changed_by_user | changed_by_user_type    |
    | testId             | testUser        | BACKOFFICE_PROFESSIONAL |
    | testId             | testUser        | BACKOFFICE_PROFESSIONAL |

@ignore
Scenario: Get history of updated task using backoffice and client system user types
    Given task id which will be used for getting history is an id of created task
    And requesting user type which will be used for getting task history is <requesting_user_type>
    And description which will be used for updating task is "new desc"
    When update task for getting its history request is sent
    And get task history request is sent
    Then status code should be 200
    And response body from get task history should contain <changed_by_user_id>, <changed_by_user>, <changed_by_user_type>
    And changed items should have old and new values after update
Examples:
    | requesting_user_type    | changed_by_user_id | changed_by_user | changed_by_user_type    |
    | BACKOFFICE_PROFESSIONAL | testId             | testUser        | BACKOFFICE_PROFESSIONAL |
    | CLIENT_SYSTEM           | testId             | testUser        | BACKOFFICE_PROFESSIONAL |

@ignore
Scenario: Get history of deleted task
    Given task id which will be used for getting history is an id of created task
    And requesting user type which will be used for getting task history is <requesting_user_type>
    When delete task for getting its history is sent
    And get task history request is sent
    Then status code should be 200
    And response body from get task history should contain <changed_by_user_id>, <changed_by_user>, <changed_by_user_type>
    And changes field should have null items after getting history after delete
    And I delete unused task
Examples:
    | requesting_user_type    | changed_by_user_id | changed_by_user | changed_by_user_type    |
    | BACKOFFICE_PROFESSIONAL | testId             | testUser        | BACKOFFICE_PROFESSIONAL |
    | CLIENT_SYSTEM           | testId             | testUser        | BACKOFFICE_PROFESSIONAL |

Scenario: Get history of a task with forbidden user types
    Given task id which will be used for getting history is an id of created task
    And requesting user type which will be used for getting task history is <requesting_user_type>
    When get task history request with forbidden task id is sent
    Then status code should be 403
    And forbidden request message from get task history request should have text "Access to this resource is denied."
    And error code should be "TASK_MGMT_INVALID_ACCESS_RIGHTS"
Examples:
    | requesting_user_type |
    | PARTICIPANT          |
    | EMPLOYER             |

Scenario: Get task by id inserting incorrect data
    Given task id which will be used for getting history is "historytaskid"
    And requesting user id which will be used for getting task history with wrong data is <requesting_user_id>
    And requesting user type which will be used for getting task history is <requesting_user_type>
    And header user id which will be used for getting task history is with wrong data is <header_user_id>
    When get task history request is sent
    Then status code should be 400
    And bad request message from get task history request should have text <message> in the field <field>
    And error code should be "TASKMGMT_INVALID_MODEL_RECIEVED_BAD_REQUEST"
Examples:
    | requesting_user_id | requesting_user_type    | header_user_id   | message                           | field                |
    |                    | BACKOFFICE_PROFESSIONAL | id    | Field cannot be empty.            | requesting_user_id   |
    | testId             |                         | id    | Field cannot be empty.            | requesting_user_type |
    | testId             | BACKOFFICE_PROFESSIONAL |                    | Field cannot be empty.            | user_id            |
    | testId             | BACKOFFICE_PROFESSIONAL | tenatiddoesntexist | User with this ID doesnt exist. | user_id            |
