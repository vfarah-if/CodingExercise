# CodingExercise

```
Feature: Student Editing
	As a student admin
        I want to be able to edit students
```
The point of this exercise it to facilitate good domain repository patterns utilising MongoDb, Repository and Unit of work patterns, .net core and MVC ideally, but feel free to utilise another Framework that does the same.

Document what you learn as a point of intererst guiding anyone else down your path of understanding.

1. As a user I would like to edit or create a list of students (DONE)
2. Create a user (DONE)
3. Delete a user (DONE)
4. Verify user been created or deleted (DONE)
5. Update an existing user and verify he exists by some other field (DONE)
6. Get user by id (DONE)
7. Filter list and count (DONE)
8. Create a Student Web API that uses all the repository methods (DONE)
	a. Refactor controller logic into services (Wont do as controller is so simple)
	b. Create a student view model that represents the data with restrictions on the size of the data, the fact that all the data will be required, range validation etc
	c. Create a test suite that represents all the HTTP Data returned to test the way this implementation works
9  Create an integration or acceptance test for the API (DONE)
10. Extend the crud to include Head for efficient check to see if exists (DONE)
10. Create MVC For listing, editing, deleting students (TODO)

#Links of interest while doing project
1. https://docs.microsoft.com/en-us/aspnet/core/tutorials/first-web-api?view=aspnetcore-2.2&tabs=visual-studio
2. https://docs.microsoft.com/en-us/aspnet/core/mvc/models/validation?view=aspnetcore-2.2
3. https://docs.microsoft.com/en-us/aspnet/core/tutorials/first-web-api?view=aspnetcore-2.2&tabs=visual-studio
4. https://docs.microsoft.com/en-us/aspnet/core/fundamentals/dependency-injection?view=aspnetcore-2.2
5. https://fullstackmark.com/post/20/painless-integration-testing-with-aspnet-core-web-api
6. https://andrewlock.net/coming-in-asp-net-core-2-1-top-level-mvc-parameter-validation/
7. https://docs.microsoft.com/en-us/aspnet/core/mvc/controllers/routing?view=aspnetcore-2.2
8. https://docs.microsoft.com/en-us/aspnet/core/fundamentals/routing?view=aspnetcore-2.2#route-template-reference
9. https://weblog.west-wind.com/posts/2018/Feb/18/Accessing-Configuration-in-NET-Core-Test-Projects
10. https://medium.com/bynder-tech/c-why-you-should-use-configureawait-false-in-your-library-code-d7837dce3d7f

#Issues of interest
1. https://medium.com/@joni2nja/gotcha-upgrading-asp-net-core-2-1-to-2-2-api-versioning-and-endpoint-routing-compatibility-fb5ab1c5d952
