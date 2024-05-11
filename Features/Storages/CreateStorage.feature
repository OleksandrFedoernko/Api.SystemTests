Feature: CreateStorage

Scenario: Create storage with allowed user types
	Given id which will be used for creating storage is <storage_id>
	And name which will be used for creating storage is <name>
	And icon which will be used for creating storage is <icon>
	And requesting_user_type is "<requesting_user_type>"
	And header user_id is "user_id"
	And requesting_user_id is "testId"
	When post storage request is sent
	Then status code should be 201
	And response body from post storage equals <storage_id>, <name>, <icon>
	And I delete storage
Examples:
	| storage_id | name | icon     | requesting_user_type    |
	| id_first_  | Name | Icon.png | BACKOFFICE_PROFESSIONAL |
	| id_second_ | Name | Icon.png | CLIENT_SYSTEM           |

Scenario: Create storage inserting only name
	Given id which will be used for creating storage is <storage_id>
	And name which will be used for creating storage is <name>
	And requesting_user_type is "<requesting_user_type>"
	And header user_id is "visma_idella_pd"
	And requesting_user_id is "testId"
	When post storage request is sent
	Then status code should be 201
	And response body from post storage equals <storage_id>, <name>, <icon>
	And I delete storage
Examples:
	| storage_id | name | requesting_user_type    |
	| id_first_  | Name | BACKOFFICE_PROFESSIONAL |

Scenario: Create storage inserting only icon
	Given id which will be used for creating storage is <storage_id>
	And icon which will be used for creating storage is <icon>
	And requesting_user_type is "<requesting_user_type>"
	And header user_id is "visma_idella_pd"
	And requesting_user_id is "testId"
	When post storage request is sent
	Then status code should be 201
	And response body from post storage equals <storage_id>, <name>, <icon>
	And I delete storage
Examples:
	| storage_id | icon     | requesting_user_type    |
	| id_first_  | Icon.png | BACKOFFICE_PROFESSIONAL |

Scenario: Create storage with user types that are not allowed
	Given id which will be used for creating storage is <storage_id>
	And requesting_user_type is "<requesting_user_type>"
	And header user_id is "user_id"
	And requesting_user_id is "testId"
	When post storage request is sent
	Then status code should be 403
	And forbidden request message from create storage request should have text "Access to this resource is denied"
	And error code should be "INVALID_ACCESS_RIGHTS"
Examples:
	| storage_id  | requesting_user_type |
	| forbiddenid | EMPLOYER             |
	| forbiddenid | PATRICIPANT          |

Scenario: Create storage by inserting wrong data
	Given id which will be used for creating bad storage is <storage_id>
	And name which will be used for creating storage is <name>
	And icon which will be used for creating storage is <icon>
	And requesting_user_type is "<requesting_user_type>"
	And header user_id is "<user_id>"
	And requesting_user_id is "<requesting_user_id>"
	When post storage request is sent
	Then status code should be 400
	And bad request message from create storage request should have text <message> in field <field>
	And error code should be "INVALID_MODEL_RECIEVED_BAD_REQUEST"
Examples:
	| storage_id   | name           | icon               | requesting_user_id | requesting_user_type    | user_id      | field                | message                          |
	| badrequestid | badRequestName | badRequestIcon.png |                    | BACKOFFICE_PROFESSIONAL | user_id      | requesting_user_id   | Field cannot be empty.           |
	| badrequestid | badRequestName | badRequestIcon.png | testId             |                         | user_id      | requesting_user_type | Field cannot be empty.           |
	| badrequestid | badRequestName | badRequestIcon.png | testId             | BACKOFFICE_PROFESSIONAL |              | user_id              | Field cannot be empty.           |
	| badrequestid | badRequestName | badRequestIcon.png | testId             | BACKOFFICE_PROFESSIONAL | doesnt_exist | user_id              | User with this ID doesn't exist. |

Scenario: Send empty request body
	Given id which will be used for creating storage is <storage_id>
	And requesting_user_type is "BACKOFFICE_PROFESSIONAL"
	And header user_id is "visma_idella_pd"
	And requesting_user_id is "testId"
	When post storage request is sent
	Then status code should be 201
Examples:
	| storage_id |
	| id_first_  |

Scenario: Create storage with same id
	Given id which will be used for creating storage with same id is "alreadyexists"
	And requesting_user_type is "BACKOFFICE_PROFESSIONAL"
	And header user_id is "user_id"
	And requesting_user_id is "testId"
	When post storage request is sent
	Then status code should be 409
