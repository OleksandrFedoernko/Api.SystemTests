Feature: UpdateTask

Background:
    Given requesting user id which will be used for creating task for upd is "testId"
    And header user id which will be used for creating task for upd is is "id"
    And requesting user type which will be used for creating task for upd is "BACKOFFICE_PROFESSIONAL"
    And storage id which will be used for creating task for upd is "testid"
    When post task request for upd is sent
    And I save task id

@ignore
Scenario: Update task using BACKOFFICE PROFESSIONAL and CLIENT SYSTEM user types
    Given requesting user id which will be used for updating task is "testId"
    And header user id which will be used for updating task is "id"
    And requesting user type which will be used for updating task is <requesting_user_type>
    And comment which will be added to history table of the task operation is <comment>
    And task id which will be used for updating task is <task_id>
    And storage id which will be used for updating task is <storage_id>
    And reference id which will be used for updating task is <reference_id>
    And correlation id which will be used for updating task is <correlation_id>
    And causation id which will be used for updating task is <causation_id>
    And name of the process which will be used for updating task is <origin>
    And title which will be used for updating task is <title>
    And description which will be used updating task is <description>
    And icon which will be used for updating task is <icon>
    And related entity which will be used for updating task is <related_entity>
    And priority which will be used for updating task is <priority>
    And percent complete which will be used for updating task is <percent_complete>
    And possible outcomes which will be used for updating task are <possible_outcome1>, <possible_outcome2>
    And chosen outcome which will be used for updating task is <chosen_outcome>
    And assigned to which will be used for updating task task is <assigned_to>
    And start date time which will be used for updating task is <start_date_time>
    And due date time which will be used for updating task is <due_date_time>
    When update task request is sent
    Then status code should be 200
    And response body from update task should be valid according to the JSON schema
Examples:
    | requesting_user_type    | comment            | task_id                              | storage_id             | reference_id | correlation_id | causation_id | origin    | title    | description        | icon    | related_entity | priority | percent_complete | possible_outcome1 | possible_outcome2 | chosen_outcome | assigned_to | start_date_time              | due_date_time                |
    | CLIENT_SYSTEM           | changed whole task | f6300e7a-50e5-4ced-a75f-b4aa38a715e5 | storagetestidsecond848 | updRefId     | updCorId       | updCausId    | updOrigin | updTitle | upd desc of a task | updIcon | https://updEnt | 85       | 12               | good              | ok                | good           | upduser1    | 2023-08-15T12:50:14.6155887Z | 2023-08-25T12:50:14.6155887Z |
    | CLIENT_SYSTEM           | newcomment         | f6300e7a-50e5-4ced-a75f-b4aa38a715e5 | storagetestidsecond848 | updRefId     | updCorId       | updCausId    | updOrigin | updTitle | upd desc of a task | updIcon | https://updEnt | 11       | 12               | good              | ok                | ok             | upduser1    | 2023-08-15T12:50:14.6155887Z | 2023-08-25T12:50:14.6155887Z |
    | BACKOFFICE_PROFESSIONAL |                    | 5fe1b2a3-161b-4450-8b9f-98a14d30a2bf | storagetestidfirst733  | updRefId     | updCorId       | updCausId    | updOrigin | updTitle | upd desc of a task | updIcon | https://updEnt | 80       | 10               | bad               | allright          | bad            | upduser2    | 2023-08-15T12:50:14.6155887Z | 2023-08-11T12:50:14.6155887Z |
    | BACKOFFICE_PROFESSIONAL |                    | 5fe1b2a3-161b-4450-8b9f-98a14d30a2bf | storagetestidfirst733  | updRefId     | updCorId       | updCausId    | updOrigin | updTitle | upd desc of a task | updIcon | https://updEnt | 25       | 100              | excellent         | horrific          | horrific       | upduser3    | 2023-08-15T12:50:14.6155887Z | 2023-08-25T12:50:14.6155887Z |

@ignore
Scenario: update task by inserting only storage id
    Given requesting user id which will be used for updating task is "testId"
    And header user id which will be used for updating task is "id"
    And requesting user type which will be used for updating task is <requesting_user_type>
    And task id which will be used for updating task is <task_id>
    And storage id which will be used for updating task is <storage_id>
    When update task request is sent
    Then status code should be 200
    And response body from update task should be valid according to the JSON schema
Examples:
    | requesting_user_type    | task_id                              | storage_id            |
    | BACKOFFICE_PROFESSIONAL | ad61db25-48be-4cc6-82f5-202b079b01c6 | storagetestidfirst733 |
    | BACKOFFICE_PROFESSIONAL | b12c7aa9-4d5e-48d6-82a8-02a2f22537a1 | newstorageids         |

@ignore
Scenario: Update task using EMPLOYER and PARTICIPANT user types
    Given requesting user id which will be used for updating task with different params is <requesting_user_id>
    And requesting user type which will be used for updating task is <requesting_user_type>
    And task id which will be used for updating task is <task_id>
    And storage id which will be used for updating task is <storage_id>
    And task id which will be used for updating task is <task_id>
    And storage id which will be used for updating task is <storage_id>
    And reference id which will be used for updating task is <reference_id>
    And correlation id which will be used for updating task is <correlation_id>
    And causation id which will be used for updating task is <causation_id>
    And name of the process which will be used for updating task is <origin>
    And title which will be used for updating task is <title>
    And description which will be used updating task is <description>
    And icon which will be used for updating task is <icon>
    And related entity which will be used for updating task is <related_entity>
    And priority which will be used for updating task is <priority>
    And percent complete which will be used for updating task is <percent_complete>
    And possible outcomes which will be used for updating task are <possible_outcome1>, <possible_outcome2>
    And chosen outcome which will be used for updating task is <chosen_outcome>
    And assigned to which will be used for updating task task is <assigned_to>
    And start date time which will be used for updating task is <start_date_time>
    And due date time which will be used for updating task is <due_date_time>
    When update task request is sent
    Then status code should be 201
    And response body from update task should be valid according to the JSON schema
Examples:
    | requesting_user_id | requesting_user_type | task_id                              | storage_id                  | reference_id | correlation_id | causation_id | origin    | title    | description                 | icon    | related_entity     | priority | percent_complete | possible_outcome1 | possible_outcome2 | chosen_outcome | assigned_to | start_date_time              | due_date_time                |
    | testid2            | PARTICIPANT          | e2a5e623-ecac-4b77-809b-0b797064e6af | participant-storage-testid2 | updRefId     | updCorId       | updCausId    | updOrigin | updTitle | upd desc of a task for part | updIcon | https://updEntPart | 85       | 12               | good              | wonderful         | wonderful      | upduserPart | 2023-08-15T12:50:14.6155887Z | 2023-08-25T12:50:14.6155887Z |
    | testid1            | EMPLOYER             | bdc65fc9-ec29-4faa-8e0a-65bbf30a781b | employer-storage-testid1    | updRefId     | updCorId       | updCausId    | updOrigin | updTitle | upd desc of a task for emp  | updIcon | https://updEntEmp  | 85       | 12               | good              | ok                | good           | upduserEmpl | 2023-08-15T12:50:14.6155887Z | 2023-08-25T12:50:14.6155887Z |

Scenario: Update task using EMPLOYER and PARTICIPANT user types which doesn't belong to user
    Given requesting user id which will be used for updating task is "testId"
    And header user id which will be used for updating task is "id"
    And requesting user type which will be used for updating task is <requesting_user_type>
    And wrong task id which will be used for updating task is <task_id>
    And storage id which will be used for updating task is <storage_id>
    When update task request is sent
    Then status code should be 403
    And response body from update task should contain message "Access to this resource is denied."
    And error code should be "INVALID_ACCESS_RIGHTS"
Examples:
    | requesting_user_id | requesting_user_type | task_id                              | storage_id                  |
    | testid1            | PARTICIPANT          | 002fc92d-9555-4950-85ac-79dc7a35695f | participant-storage-testid2 |
    | testid2            | EMPLOYER             | 000278f0-da25-42b7-837b-e3f5795e0a8d | employer-storage-testid1    |
    | testId             | EMPLOYER             | 07280625-8fce-40b4-a338-560929a7d0fd | storagetestidfirst733       |
    | testid2            | PARTICIPANT          | 002fc92d-9555-4950-85ac-79dc7a35695f | changenameofstorageid       |
    | testid1            | EMPLOYER             | 000278f0-da25-42b7-837b-e3f5795e0a8d | changenameofstorageid       |

Scenario: Update task using incorrect data
    Given requesting user id which will be used for updating task with different params is <requesting_user_id>
    And requesting user type which will be used for updating task is <requesting_user_type>
    And bad header user id which will be used for updating task is <header_user_id>
    And wrong task id which will be used for updating task is <task_id>
    And storage id which will be used for updating task is <storage_id>
    And priority which will be used for updating task is <priority>
    And percent complete which will be used for updating task is <percent_complete>
    And possible outcomes which will be used for updating task are <possible_outcome1>, <possible_outcome2>
    And chosen outcome which will be used for updating task is <chosen_outcome>
    And start date time which will be used for updating task is <start_date_time>
    And due date time which will be used for updating task is <due_date_time>
    When update task request is sent
    Then status code should be 400
    And response body from update task should contain message <message> in the field <field>
    And error code should be "INVALID_MODEL_RECIEVED_BAD_REQUEST"
Examples:
    | requesting_user_id | requesting_user_type    | header_user_id | task_id                              | storage_id            | priority | percent_complete | possible_outcome1 | possible_outcome2 | chosen_outcome | start_date_time              | due_date_time                | message                                                       | field                |
    |                    | BACKOFFICE_PROFESSIONAL | id  | 07280625-8fce-40b4-a338-560929a7d0fd | storagetestidfirst733 | 80       | 10               | bad               | allright          | bad            | 2023-08-15T12:50:14.6155887Z | 2023-08-11T12:50:14.6155887Z | Field cannot be empty.                                        | requesting_user_id   |
    | testId             |                         | id  | 059d27f1-4134-476d-abd6-f028759cad18 | storagetestidfirst733 | 25       | 100              | excellent         | horrific          | horrific       | 2023-08-15T12:50:14.6155887Z | 2023-08-25T12:50:14.6155887Z | Field cannot be empty.                                        | requesting_user_type |
    | testId             | BACKOFFICE_PROFESSIONAL |                  | 059d27f1-4134-476d-abd6-f028759cad18 | storagetestidfirst733 | 25       | 100              | excellent         | horrific          | horrific       | 2023-08-15T12:50:14.6155887Z | 2023-08-25T12:50:14.6155887Z | Field cannot be empty.                                        | user_id            |
    | testId             | BACKOFFICE_PROFESSIONAL | notcreated       | 059d27f1-4134-476d-abd6-f028759cad18 | storagetestidfirst733 | 25       | 100              | excellent         | horrific          | horrific       | 2023-08-15T12:50:14.6155887Z | 2023-08-25T12:50:14.6155887Z | User with this ID doesnt exist.                             | user_id            |
    | testId             | BACKOFFICE_PROFESSIONAL | id  | 059d27f1-4134-476d-abd6-f028759cad18 |                      | 25       | 100              | excellent         | horrific          | horrific       | 2023-08-15T12:50:14.6155887Z | 2023-08-25T12:50:14.6155887Z | 'Task Storage Id' must not be empty.                           | storage_id            |
    | testId             | BACKOFFICE_PROFESSIONAL | id  | 059d27f1-4134-476d-abd6-f028759cad18 | storagetestidfirst733 | -100     | 100              | excellent         | horrific          | horrific       | 2023-08-15T12:50:14.6155887Z | 2023-08-25T12:50:14.6155887Z | 'Task Priority' must be greater than or equal to '0'.         | priority            |
    | testId             | BACKOFFICE_PROFESSIONAL | id  | 059d27f1-4134-476d-abd6-f028759cad18 | storagetestidfirst733 | 1000     | 100              | excellent         | horrific          | horrific       | 2023-08-15T12:50:14.6155887Z | 2023-08-25T12:50:14.6155887Z | Field is out of range of allowed values.                      | priority             |
    | testId             | BACKOFFICE_PROFESSIONAL | id  | 059d27f1-4134-476d-abd6-f028759cad18 | storagetestidfirst733 | 10       | -100             | excellent         | horrific          | horrific       | 2023-08-15T12:50:14.6155887Z | 2023-08-25T12:50:14.6155887Z | 'Task Percent Complete' must be greater than or equal to '0'. | percent_complete     |
    | testId             | BACKOFFICE_PROFESSIONAL | id  | 059d27f1-4134-476d-abd6-f028759cad18 | storagetestidfirst733 | 10       | 1000             | excellent         | horrific          | horrific       | 2023-08-15T12:50:14.6155887Z | 2023-08-25T12:50:14.6155887Z | Field is out of range of allowed values.                      | percent_complete     |
    | testId             | BACKOFFICE_PROFESSIONAL | id  | 059d27f1-4134-476d-abd6-f028759cad18 | storagetestidfirst733 | 10       | 10               | excellent         | horrific          | string         | 2023-08-15T12:50:14.6155887Z | 2023-08-25T12:50:14.6155887Z | Chosen Outcome must be from Possible Ouctcomes.               |                      |
    | testId             | BACKOFFICE_PROFESSIONAL | id  | 059d27f1-4134-476d-abd6-f028759cad18 | storagetestidfirst733 | 10       | 10               | excellent         | horrific          | excellent      | 20:23                        | 2023-08-25T12:50:14.6155887Z | Request body cannot be parsed.                                | Body                 |
    | testId             | BACKOFFICE_PROFESSIONAL | id  | 059d27f1-4134-476d-abd6-f028759cad18 | storagetestidfirst733 | 10       | 10               | excellent         | horrific          | excellent      | 2023-08-15T12:50:14.6155887Z | 20:23                        | Request body cannot be parsed.                                | Body                 |
    | testId             | BACKOFFICE_PROFESSIONAL | id  | 059d27f1-4134-476d-abd6-f028759cad18 | storagetestidfirst733 | 10       | 10               | excellent         | horrific          | excellent      | 2023-08-15T12:50:14          | 2023-08-15T12:50:14.6155887Z | Date time value must be in UTC format.                        | start_date_time      |
    | testId             | BACKOFFICE_PROFESSIONAL | id  | 059d27f1-4134-476d-abd6-f028759cad18 | storagetestidfirst733 | 10       | 10               | excellent         | horrific          | excellent      | 2023-08-15T12:50:14.6155887Z | 2023-08-15T12:50:14          | Date time value must be in UTC format.                        | due_date_time        |

Scenario: Update task which doesn't exist
    Given requesting user id which will be used for updating task is "testId"
    And header user id which will be used for updating task is "id"
    And requesting user type which will be used for updating task is <requesting_user_type>
    And wrong task id which will be used for updating task is <task_id>
    And storage id which will be used for updating task is <storage_id>
    When update task request is sent
    Then status code should be 404
    And response body from update task should contain not found message "Resource Not Found"
    And error code should be "INVALID_RESOURCE_REQUESTED"
Examples:
    | requesting_user_type    | task_id     | storage_id            |
    | BACKOFFICE_PROFESSIONAL | doesntexist | storagetestidfirst733 |
