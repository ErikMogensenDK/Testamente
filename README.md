Firstly this API handles database access, and implements a service which calculates how your inheritance will be split among your next of kin under danish law.

Some highlights:
- The project was developed in a test-driven fashion, and especially the App layer has extensive testing to ensure the inheritance is split as expected.
- The abstractions used to test in Testamente.QueryTests of IDbConnectionProvider and a IQueryExecutor ensures that even though the dependencies are quite complex when using dapper, that the unit-test is indeed atomic.
- The project utilizes the open-source harpi CLI project (https://github.com/teslae1/harpi) to integration-test the api endpoints, providing concise documentation/verification of endpoints, all within an .yml file (See the tests.harpi.yml file in the Testamente.Web project folder if interested!)

A brief description
The API has 5 sub-projects, which were made to adhere to SOLID principles of single responsibility, etc.
- The .Domain class library
  This contains the domain objects and the interfaces for the repositories to handle these domain objects.
- The .DataAccess class library
  The dataAccess layer contains the DTO-entities stored in the database which is managed through entity framework migrations.
  It also contains the implimentation of the repositories for the CUD part of CRUD operations.
- The .Query class library
  The query layer contains separate DTO-entities for mapping to when reading, implementing command/query segregation, and using dapper instead of entity framework, to     potentially optimize the sql written for the queries.
- The .App class library
  The app layer contains the implementation for the service calculating how inheritance should be split among inheritants, if any.
- The .Web project
  The .Web project contains the actual API, with controllers for accessing the relevant parts of the application.

The structure was certainly overkill and bloated for this application, but was created as an exercise in SOLID, to prepare for projects of a complexity needing more rigorous testing and segregation of responsibility.
