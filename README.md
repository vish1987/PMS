To create database follow below steps

1. Go to file PMSContextFactory.cs and change db connectionstring
2. Open package manager console and execute command dotnet-ef database update --project PMS.Infrastructure --startup-project PMS.API to create database

Database Schema

PMS database has below 2 tables.

1. Projects
2. Tasks

Project has one to many relationshiop with tasks.
and since project can have subprojects it has 1 to many relationship with itself for subrpojects and also tasks have one to many relationship as they can have subtasks

Api Documentation

Api documentation integrated in code with the help of swagger.
When you run the project, you will land to swagger or use https://localhost:{port no}/swagger

To create report use date format YYYY-MM-dd e.g. 2020-06-01 as its currently not added to swagger spec.
