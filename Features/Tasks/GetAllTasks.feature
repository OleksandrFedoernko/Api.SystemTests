Feature: GetAllTasks

Background:
    Given header user id which will be used for getting all tasks is "id"
    And requesting user id which will be used for getting all tasks is "testId"

Scenario: Get all tasks using BACKOFFICE_PROFESSIONAL & CLIENT_SYSTEM user types
    And requesting user type which will be used for getting all tasks is <requesting_user_type>
    And limit which will be used for getting all tasks is <limit>
    And fields which will be used for getting all tasks is <fields>
    When get all tasks request is sent
    Then status code should be 200
    And response body from get all tasks should be equal to <storage_id> <created_by>
Examples:
    | requesting_user_type    | limit | fields   | created_by |
    | BACKOFFICE_PROFESSIONAL | 2     |          | testId     |
    | CLIENT_SYSTEM           | 2     |          | testId     |
    | BACKOFFICE_PROFESSIONAL | 2     |          | testId     |
    | CLIENT_SYSTEM           | 2     |          | testId     |
    | BACKOFFICE_PROFESSIONAL | 2     | Priority | testId     |
    | CLIENT_SYSTEM           | 2     | Priority | testId     |

Scenario: Get all tasks using PARTICIPANT & EMPLOYER user types
    Given requesting user ids which will be used for getting tasks for another user types <requesting_user_id>
    And requesting user type which will be used for getting all tasks is <requesting_user_type>
    And limit which will be used for getting all tasks is <limit>
    And storage id which will be used for getting all storages is <storage_id>
    When get all tasks request is sent
    Then status code should be 200
    And response body from get all tasks should be equal to <storage_id> <created_by>
Examples:
    | requesting_user_id | requesting_user_type | limit | storage_id                  | created_by |
    | testid2            | PARTICIPANT          | 2     |                            | testid2    |
    | testid1            | EMPLOYER             | 2     |                            | testid1    |
    | testid2            | PARTICIPANT          | 2     | participant-storage-testid2 | testId     |
    | testid1            | EMPLOYER             | 2     | employer-storage-testid1    | testId     |

Scenario: Get all tasks by providing other user types
    And requesting user type which will be used for getting all tasks is <requesting_user_type>
    And limit which will be used for getting all tasks is <limit>
    And storage id which will be used for getting all storages is <storage_id>
    When get all tasks request is sent
    Then status code should be 403
    And forbidden request message from get all tasks request should have text "Access to this resource is denied."
    And error code should be "INVALID_ACCESS_RIGHTS"
Examples:
    | requesting_user_type | limit | storage_id                  |
    | PARTICIPANT          | 0     | employer-storage-testid1    |
    | EMPLOYER             | 0     | participant-storage-testid2 |
    | PARTICIPANT          | 0     | participant-storage-testid2 |
    | EMPLOYER             | 0     | employer-storage-testid1    |

Scenario: Get all tasks by providing wrong data
    Given requesting user ids which will be used for getting tasks for another user types <requesting_user_id>
    And requesting user type which will be used for getting all tasks is <requesting_user_type>
    And wrong header user id which will be used for getting all tasks is <header_user_id>
    And limit which will be used for getting all tasks is <limit>
    And storage id which will be used for getting all storages is <storage_id>
    And fields which will be used for getting all tasks is <fields>
    When get all tasks request is sent
    Then status code should be 400
    And bad request message from get all tasks request should have text <message> in the field <field>
    And error code should be "INVALID_MODEL_RECIEVED_BAD_REQUEST"
Examples:
    | requesting_user_id | requesting_user_type    | header_user_id    | limit | fields           | field                | message                                          |
    |                    | BACKOFFICE_PROFESSIONAL | id     | 0     |                  | requesting_user_id   | Field cannot be empty.                           |
    | testId             |                         | id     | 0     |                  | requesting_user_type | Field cannot be empty.                           |
    | testId             | BACKOFFICE_PROFESSIONAL |                     | 0     |                  | user_id            | Field cannot be empty.                           |
    | testId             | BACKOFFICE_PROFESSIONAL | useriddoesntexist | 0     |                  | user_id            | User with this ID doesn't exist.               |
    | testId             | BACKOFFICE_PROFESSIONAL | id     | -1    |                  | limit                | 'Limit' must be greater than or equal to '1'.    |
    | testId             | BACKOFFICE_PROFESSIONAL | id     | 10001 |                  | limit                | 'Limit' must be less than or equal to '1000'.    |
    | testId             | BACKOFFICE_PROFESSIONAL | id     | 0     | fielddoesntexist | fields[0]            | One or more fields are not valid for this entity |
