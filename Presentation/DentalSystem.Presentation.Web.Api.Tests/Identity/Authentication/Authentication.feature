Feature: Authentication
    Test authentication feature

    Scenario: Login with dental worker
    When Login with dental worker user details
        | userName | password |
        | dentist_01@mail.com | Dentist_01#123456 |
        | dentist_02@mail.com | Dentist_02#123456 |
        | dentistAssistant_01@mail.com | DentistAssistant_01#123456 |
        | dentistAssistant_02@mail.com | DentistAssistant_02#123456 |
    Then Should receive dental worker access token

    Scenario: Register client
    When Register client with user details
        | userName | password |
        | patient_01@mail.com | Patient_01#123456 |
    Then Should receive client access token