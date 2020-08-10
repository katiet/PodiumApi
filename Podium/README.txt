The in memory database is preloaded with one applicant.

--------------------------------

To add a new applicant

url: https://localhost:44393/api/applicants (POST)
example json:
{
    "FirstName" : "test",
    "LastName" : "gremlin",
    "DateOfBirth" : "1944-04-23T18:25:43.511Z",
    "Email" : "test@test.com"
}

--------------------------------

to search lenders based on an applicant, deposit and property value

url: https://localhost:44393/api/lenders/search (POST)
example json:
{
    "ApplicantId" : 3,
    "PropertyValue" : 1000,
    "DepositAmount" : 600
}