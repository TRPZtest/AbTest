# AbTest

Hello!

This is my implementation of an A/B testing API for a test task. There are three API methods: one for price, another for button color, and the last one for experiment statistics.

You can run the application without a local SQL database. There is a connection string to my Azure SQL database, but there is also a folder named "Db scripts" with schema and data scripts in the root folder of the repository.

The database structure looks like this:
![image](https://github.com/TRPZtest/AbTest/assets/86252204/f5aa42d4-755d-493e-b872-0a63d0870a0e)

The solution includes unit tests that are used to test the ExperimentService class with an in-memory SQLite database.

To try the API, use Swagger.
