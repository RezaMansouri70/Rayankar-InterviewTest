Feature: Create Read Edit Delete Customer

    Background:
        Given system error codes are following
          | Key | Value                                                      |
          | 101 | Invalid Mobile Number                                      |
          | 102 | Invalid Email address                                      |
          | 103 | Invalid Bank Account Number                                |
          | 201 | Duplicate customer by First-name, Last-name, Date-of-Birth |
          | 202 | Duplicate customer by Email address                        |

    Scenario Outline: Create Read Edit Delete Customer
        When user creates a customer with below data
        And Lastname of <LastName>
        And Date of birth of <DateOfBirth>
        And Email of <Email>
        And Phone number of <PhoneNumber>
        And Bank Account Number of <BankAccountNumber>
        Then user can lookup customer by Email of <Email> and get "1" records
        When user edit customer with new email of "new@email.com"
        Then Then user can lookup customer by Email of <Email> and get "0" records
        And user can lookup customer by Email of "new@email.com" and get "1" records
        When user delete customer by Email of "new@email.com"
        Then user can lookup customer by Email of "new@email.com" and get "0" records
        And user can lookup customer by Email of <Email> and get "0" records

        Examples:
          | ID | FirstName | LastName | Email        | PhoneNumber   | DateOfBirth | BankAccountNumber |
          | 1  | John      | Doe      | john@doe.com | +989121234567 | 01-JAN-2000 | IR000000000000001 |

    Scenario: Create a duplicate customer by firstname, lastname and date of birth
        Given system has existing customer
          | ID | FirstName | LastName | Email        | DateOfBirth | BankAccountNumber |
          | 1  | JOHN      | DOE      | john@doe.com | 01-JAN-2000 | IR000000000000001 |
        When user creates customer with <FirstName>
        And lastname of <LastName>
        And date of birth of <DateOfBirth>
        And email of <Email>
        Then system responds with "201"  error

    Examples:
      | ID | FirstName | LastName | Email        | DateOfBirth | BankAccountNumber |
      | 1  | John      | Doe      | john@doe.com | 01-JAN-2000 | IR000000000000001 |

    Scenario: Mobile Validation Criteria in the system

        When user tries to submit <MobileNumber>
        Then user receives <ExpectedResult>