# API Prototype
A simple API application that uses SQL Database

## How It's Made:

**Tech used:** .Net Core 8, C#, EF, SQL

Uses .net 8 to create a simple api that uses Entity Framework to modify database.

## Endpoints:
* /api/Prototype/GetAll
  * Gets All Threads and Messages posted on them
* /api/Prototype/GetThread
  * Gets a specific Thread 
* /api/Prototype/CreateThread
  * Creates a new Thread 
* /api/Prototype/EditThreadDescription
  * Edits the description of a Thread
* /api/Prototype/DeleteThread
  * Deletes a Thread and all its child Messages
* /api/Prototype/PostMessage
  * Posts a new Message on a Thread
* /api/Prototype/UpdateMessage
  * Updates a Message posted on a Thread
* /api/Prototype/DeleteMessage
  * Deletes a Message 

## How to run:
1. Add a connection string ("DefaultConnection") to a SQL databse
2. Update-Database tables by running Update-Database
3. Run the Application
