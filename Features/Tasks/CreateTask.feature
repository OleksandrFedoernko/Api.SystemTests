Feature: CreateTask

Background:
    Given header user id which will be used for creating task is "id"
    And requesting user id which will be used for creating task is "testId"

Scenario: Create task using correct user types and inserting valid inputs
    And requesting user type which will be used for creating task is <requesting_user_type>
    And storage id which will be used for creating task is <storage_id>
    And reference id which will be used for creating task is <reference_id>
    And correlation id which will be used for creating task is <correlation_id>
    And causation id which will be used for creating task is <causation_id>
    And name of the process which will be used for creating task is <origin>
    And title which will be used for creating task is <title>
    And description which will be used creating task is <description>
    And icon which will be used for creating task is <icon>
    And related entity which will be used for creating task is <related_entity>
    And priority which will be used for creating task is <priority>
    And percent complete which will be used for creating task is <percent_complete>
    And possible outcomes which will be used for creating task are <possible_outcome1> <possible_outcome2>
    And chosen outcome which will be used for creating task is <chosen_outcome>
    And assigned to which will be used for creating task is <assigned_to>
    And start date time which will be used for creating task is <start_date_time>
    And due date time which will be used for creating task is <due_date_time>
    And require chosen outcome is <require_chosen_outcome>
    And reviewers are <reviewer1> <reviewer2>
    And number of minimum reviewers is <minimum_reviewers>
    When post task request is sent
    Then status code should be 201
    And response body from create task should be valid according to json schema
    And I delete task
Examples:
    | requesting_user_type    | storage_id             | reference_id     | correlation_id   | causation_id      | origin            | title               | description        | icon               | related_entity     | priority | percent_complete | possible_outcome1 | possible_outcome2 | chosen_outcome | assigned_to | start_date_time              | due_date_time                | require_chosen_outcome | reviewer1 | reviewer2 | minimum_reviewers |
    | BACKOFFICE_PROFESSIONAL | storagetestidfirst733  | backOfficeRef-id | backOfficeCor-id | backOfficeCaus-id | backOfficeProcess | backofficeTaskTitle | backofficeTaskDesc | backofficeTaskIcon | https://backoffice | 25       | 85               | good              | string            | good           | me          | 2023-06-29T12:50:14.6155887Z |                              | true                   | 121212    | 212121    | 2                 |
    | BACKOFFICE_PROFESSIONAL | storagetestidfirst733  | backOfficeRef-id | backOfficeCor-id | backOfficeCaus-id | backOfficeProcess | backofficeTaskTitle | backofficeTaskDesc | backofficeTaskIcon | https://backoffice | 25       | 85               | good              | string            | good           | me          | 2023-06-29T12:50:14.6155887Z | 2023-07-04T12:50:14.6155887Z | true                   | 121212    | 212121    | 2                 |
    | CLIENT_SYSTEM           | storagetestidsecond848 | clientSysRef-id  | clientSysCor-id  | clientSysCaus-id  | clientSysProcess  | clientSysTaskTitle  | clientSysTaskDesc  | clientSysTaskIcon  | https://clientSys  | 10       | 30               | excellent         | SAASAS            | excellent      | someone     | 2023-07-05T12:50:14.6155887Z | 2023-07-07T12:50:14.6155887Z | true                   | 343534    | 545454    | 2                 |
    | BACKOFFICE_PROFESSIONAL | storagetestidfirst733  | backOfficeRef-id | backOfficeCor-id | backOfficeCaus-id | backOfficeProcess | backofficeTaskTitle | backofficeTaskDesc | backofficeTaskIcon | https://backoffice | 10       | 100              | excellent         | SAASAS            | excellent      | someone     | 2023-07-05T12:50:14.6155887Z | 2023-07-07T12:50:14.6155887Z | false                  | 686767    | 65667     | 2                 |

Scenario: Create task inserting date offset
    And requesting user type which will be used for creating task is <requesting_user_type>
    And storage id which will be used for creating task is <storage_id>
    And number of days in due_date_offset field is <number_of_days>
    And offset type in due_date_offset field is <offset_type>
    When post task request is sent
    Then status code should be 201
    And response body from create task should be valid according to json schema
    And I delete task
Examples:
    | requesting_user_type    | storage_id            | number_of_days | offset_type   |
    | BACKOFFICE_PROFESSIONAL | storagetestidfirst356 | 5              | BUSINESS_DAYS |
    | BACKOFFICE_PROFESSIONAL | storageiddoesntexist  | 10             | CALENDAR_DAYS |
    | BACKOFFICE_PROFESSIONAL | idalreadycreated     | 1              | BUSINESS_DAYS |

Scenario: Create task inserting only storage id
    And requesting user type which will be used for creating task is <requesting_user_type>
    And storage id which will be used for creating task is <storage_id>
    When post task request is sent
    Then status code should be 201
    And response body from create task should be valid according to json schema
    And I delete task
Examples:
    | requesting_user_type    | storage_id            |
    | BACKOFFICE_PROFESSIONAL | storagetestidfirst356 |
    | BACKOFFICE_PROFESSIONAL | storageiddoesntexist  |
    | BACKOFFICE_PROFESSIONAL | idalreadycreated     |

Scenario: Create task using forbidden user types
    And requesting user type which will be used for creating task is <requesting_user_type>
    And storage id which will be used for creating task is <storage_id>
    When post task request is sent
    Then status code should be 403
    And response body from forbidden create task request should has message "Access to this resource is denied."
    And error code should be "TASK_MGMT_INVALID_ACCESS_RIGHTS"
Examples:
    | requesting_user_type | storage_id             |
    | PARTICIPANT          | storagetestidfirst733  |
    | EMPLOYER             | storagetestidsecond848 |

Scenario: Create task inserting incorrect data
    Given bad requesting user id which will be used for creating task is <requesting_user_id>
    And requesting user type which will be used for creating task is <requesting_user_type>
    And bad header user id which will be used for creating task is <header_user_id>
    And storage id which will be used for creating task is <storage_id>
    And priority which will be used for creating task is <priority>
    And percent complete which will be used for creating task is <percent_complete>
    And possible outcomes which will be used for creating task are <possible_outcome1> <possible_outcome2>
    And chosen outcome which will be used for creating task is <chosen_outcome>
    And start date time which will be used for creating task is <start_date_time>
    And due date time which will be used for creating task is <due_date_time>
    And require chosen outcome is <require_chosen_outcome>
    And reviewers are <reviewer1> <reviewer2>
    And number of minimum reviewers is <minimum_reviewers>
    And number of days in due_date_offset field is <number_of_days>
    And offset type in due_date_offset field is <offset_type>
    When post task request is sent
    Then status code should be 400
    And response body from bad create task request should has message <message> in the field <field>
    And error code should be "TASKMGMT_INVALID_MODEL_RECIEVED_BAD_REQUEST"
Examples:
    | requesting_user_id | requesting_user_type    | header_user_id | storage_id          | priority | percent_complete | possible_outcome1 | possible_outcome2 | chosen_outcome | assigned_to | start_date_time          | due_date_time            | require_chosen_outcome | reviewer1 | reviewer2 | minimum_reviewers | number_of_days | offset_type   | message                                                                                                       | field                |
    |                    | BACKOFFICE_PROFESSIONAL | visma_idella_pd  | bad-request-id-212 | 25       | 85               | good              | string            | good           | me          | 2023-06-29T13:50:20.767Z | 2023-06-29T13:50:20.767Z | true                   | name1     | name2     | 2                 | 5              | CALENDAR_DAYS | Field cannot be empty                                                                                         | requesting_user_id   |
    | testId             |                         | visma_idella_pd  | bad-request-id-212 | 25       | 85               | good              | string            | good           | me          | 2023-06-29T13:50:20.767Z | 2023-06-29T13:50:20.767Z | true                   | name1     | name2     | 2                 | 5              | CALENDAR_DAYS | Field cannot be empty                                                                                         | requesting_user_type |
    | testId             | BACKOFFICE_PROFESSIONAL |                  | bad-request-id-212 | 25       | 85               | good              | string            | good           | me          | 2023-06-29T13:50:20.767Z | 2023-06-29T13:50:20.767Z | true                   | name1     | name2     | 2                 | 5              | CALENDAR_DAYS | Field cannot be empty                                                                                         | user_id            |
    | testId             | BACKOFFICE_PROFESSIONAL | doesnt_exist     | bad-request-id-212 | 25       | 85               | good              | string            | good           | me          | 2023-06-29T13:50:20.767Z | 2023-06-29T13:50:20.767Z | true                   | name1     | name2     | 2                 | 5              | CALENDAR_DAYS | User with this ID doesnt exist                                                                              | user_id            |
    | testId             | BACKOFFICE_PROFESSIONAL | visma_idella_pd  |                    | 25       | 85               | good              | string            | good           | me          | 2023-06-29T13:50:20.767Z | 2023-06-29T13:50:20.767Z | true                   | name1     | name2     | 2                 | 5              | CALENDAR_DAYS | 'Task Storage Id' must not be empty.                                                                           | storage_id            |
    | testId             | BACKOFFICE_PROFESSIONAL | visma_idella_pd  | bad-request-id-212 | 150      | 85               | good              | string            | good           | me          | 2023-06-29T13:50:20.767Z | 2023-06-29T13:50:20.767Z | true                   | name1     | name2     | 2                 | 5              | CALENDAR_DAYS | Field is out of range of allowed values.                                                                      | priority             |
    | testId             | BACKOFFICE_PROFESSIONAL | visma_idella_pd  | bad-request-id-212 | -150     | 85               | good              | string            | good           | me          | 2023-06-29T13:50:20.767Z | 2023-06-29T13:50:20.767Z | true                   | name1     | name2     | 2                 | 5              | CALENDAR_DAYS | Task Priority' must be greater than or equal to '0'.                                                          | priority             |
    | testId             | BACKOFFICE_PROFESSIONAL | visma_idella_pd  | bad-request-id-212 | 25       | 900              | good              | string            | good           | me          | 2023-06-29T13:50:20.767Z | 2023-06-29T13:50:20.767Z | true                   | name1     | name2     | 2                 | 5              | CALENDAR_DAYS | Field is out of range of allowed values.                                                                      | percent_complete     |
    | testId             | BACKOFFICE_PROFESSIONAL | visma_idella_pd  | bad-request-id-212 | 25       | -900             | good              | string            | good           | me          | 2023-06-29T13:50:20.767Z | 2023-06-29T13:50:20.767Z | true                   | name1     | name2     | 2                 | 5              | CALENDAR_DAYS | 'Task Percent Complete' must be greater than or equal to '0'                                                  | percent_complete     |
    | testId             | BACKOFFICE_PROFESSIONAL | visma_idella_pd  | bad-request-id-212 | 25       | 40               | good              | string            | notexist       | me          | 2023-06-29T13:50:20.767Z | 2023-06-29T13:50:20.767Z | true                   | name1     | name2     | 2                 | 5              | CALENDAR_DAYS | Chosen Outcome must be from Possible Ouctcomes                                                                |                      |
    | testId             | BACKOFFICE_PROFESSIONAL | visma_idella_pd  | bad-request-id-212 | 25       | 40               | good              | string            | good           | me          | 2023-06-29T13:50:20.767  | 2023-06-29T13:50:20.767Z | true                   | name1     | name2     | 2                 | 5              | CALENDAR_DAYS | Date time value must be in UTC format.                                                                        | start_date_time      |
    | testId             | BACKOFFICE_PROFESSIONAL | visma_idella_pd  | bad-request-id-212 | 25       | 40               | good              | string            | good           | me          | 2023-06-29T13:50:20.767Z | 2023-06-29T13:50:20.767  | true                   | name1     | name2     | 2                 | 5              | CALENDAR_DAYS | Date time value must be in UTC format.                                                                        | due_date_time        |
    | testId             | BACKOFFICE_PROFESSIONAL | visma_idella_pd  | bad-request-id-212 | 25       | 40               | good              | string            | good           | me          | 2023-07-17               | 2023-06-29T13:50:20.767Z | true                   | name1     | name2     | 2                 | 5              | CALENDAR_DAYS | Request body cannot be parsed.                                                                                | Body                 |
    | testId             | BACKOFFICE_PROFESSIONAL | visma_idella_pd  | bad-request-id-212 | 25       | 40               | good              | string            | good           | me          | 2023-06-29T13:50:20.767Z | 2023-07-17               | true                   | name1     | name2     | 2                 | 5              | CALENDAR_DAYS | Request body cannot be parsed.                                                                                | Body                 |
    | testId             | BACKOFFICE_PROFESSIONAL | visma_idella_pd  | bad-request-id-212 | 25       | 40               | good              | string            | good           | me          | 10:35                    | 2023-06-29T13:50:20.767Z | true                   | name1     | name2     | 2                 | 5              | CALENDAR_DAYS | Request body cannot be parsed.                                                                                | Body                 |
    | testId             | BACKOFFICE_PROFESSIONAL | visma_idella_pd  | bad-request-id-212 | 25       | 40               | good              | string            | good           | me          | 2023-06-29T13:50:20.767Z | 10:35                    | true                   | name1     | name2     | 2                 | 5              | CALENDAR_DAYS | Request body cannot be parsed.                                                                                | Body                 |
    | testId             | BACKOFFICE_PROFESSIONAL | visma_idella_pd  | bad-request-id-212 | 25       | 40               | good              | string            | good           | me          | 2023‐07‐13T14:24:35Z     | 2023-06-29T13:50:20.767Z | true                   | name1     | name2     | 2                 | 5              | CALENDAR_DAYS | Request body cannot be parsed.                                                                                | Body                 |
    | testId             | BACKOFFICE_PROFESSIONAL | visma_idella_pd  | bad-request-id-212 | 25       | 40               | good              | string            | good           | me          | 2023‐07‐13T14:24:35      | 2023-06-29T13:50:20.767Z | true                   | name1     | name2     | 2                 | 5              | CALENDAR_DAYS | Request body cannot be parsed.                                                                                | Body                 |
    | testId             | BACKOFFICE_PROFESSIONAL | visma_idella_pd  | bad-request-id-212 | 25       | 40               | good              | string            | good           | me          | 2023‐07‐13T14:24:35.123  | 2023-06-29T13:50:20.767Z | true                   | name1     | name2     | 2                 | 5              | CALENDAR_DAYS | Request body cannot be parsed.                                                                                | Body                 |
    | testId             | BACKOFFICE_PROFESSIONAL | visma_idella_pd  | bad-request-id-212 | 25       | 40               | good              | string            | good           | me          | 2023‐07‐13T14:24:35Z+01  | 2023-06-29T13:50:20.767Z | true                   | name1     | name2     | 2                 | 5              | CALENDAR_DAYS | Request body cannot be parsed.                                                                                | Body                 |
    | testId             | BACKOFFICE_PROFESSIONAL | visma_idella_pd  | bad-request-id-212 | 25       | 40               | good              | string            | good           | me          | 2023‐07‐13T14:24:35.123Z | 2023-06-29T13:50:20.767Z | true                   | name1     | name2     | 2                 | 5              | CALENDAR_DAYS | Request body cannot be parsed.                                                                                | Body                 |
    | testId             | BACKOFFICE_PROFESSIONAL | visma_idella_pd  | bad-request-id-212 | 25       | 85               | good              | string            | good           | me          | 2023-06-29T13:50:20.767Z | 2023-06-29T13:50:20.767Z | text                   | name1     | name2     | 2                 | 5              | CALENDAR_DAYS | Request body cannot be parsed.                                                                                | Body                 |
    | testId             | BACKOFFICE_PROFESSIONAL | visma_idella_pd  | bad-request-id-212 | 25       | 85               | good              | string            | good           | me          | 2023-06-29T13:50:20.767Z | 2023-06-29T13:50:20.767Z | true                   | name1     | name2     | 51                | 5              | CALENDAR_DAYS | DueDateTime and DueDateOffset are mutually exclusive. Only one of these values can be present in the request. |                      |
    | testId             | BACKOFFICE_PROFESSIONAL | visma_idella_pd  | bad-request-id-212 | 25       | 85               | good              | string            | good           | me          | 2023-06-29T13:50:20.767Z | 2023-06-29T13:50:20.767Z | true                   | name1     | name2     | -2                | 5              | CALENDAR_DAYS | DueDateTime and DueDateOffset are mutually exclusive. Only one of these values can be present in the request. |                      |
    | testId             | BACKOFFICE_PROFESSIONAL | visma_idella_pd  | bad-request-id-212 | 25       | 85               | good              | string            | good           | me          | 2023-06-29T13:50:20.767Z | 2023-06-29T13:50:20.767Z | true                   | name1     | name2     | 2                 | 5              | TEXT          | Request body cannot be parsed.                                                                                | Body                 |
    | testId             | BACKOFFICE_PROFESSIONAL | visma_idella_pd  | bad-request-id-212 | 25       | 85               | good              | string            | good           | me          | 2023-06-29T13:50:20.767Z | 2023-06-29T13:50:20.767Z | true                   | name1     | name2     | 2                 |                | CALENDAR_DAYS | DueDateTime and DueDateOffset are mutually exclusive. Only one of these values can be present in the request. |                      |


Scenario: Send empty request body
    And requesting user type which will be used for creating task is <requesting_user_type>
    When empty request is sent
    Then status code should be 400
    And response body from bad create task request should has message <message> in the field <field>
Examples:
    | requesting_user_type    | message                        | field |
    | BACKOFFICE_PROFESSIONAL | Request body cannot be parsed. | Body  |
