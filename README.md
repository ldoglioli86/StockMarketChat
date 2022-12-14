# Stock Market Chat Api	

This is a .Net Core Web Api that provides the user the infraestructure to log in and chat with other users in chatrooms. 

## Api methods
* A method to log in users to authenticate and get a Bearer token.
POST: /api/Authentication/login/{user}/{password}

* A method to get all the users in the database. User needs authorization for this.
GET: /api/ChatUsers

* A public method to create users.
POST: /api/ChatUsers

Request body:

{
  "userName": "string",
  "password": "string",
  "email": "string",
  "firstName": "string",
  "lastName": "string"
}

For more information, API contains a Swagger interface to get the Api documentation.

## System requirements
* Sql Server Database
* RabbitMq

## Features
* Allow users to register and get Bearer token.
* Allow to create new users and get users already created.
* Allow users to connect and send messages into chatrooms.
* Detect stock commands('/stock=stock_code') in chat messages to proccess them in a RabbitMQ queue to get the stock value online from
https://stooq.com/q/l/?s={stock_code}&f=sd2t2ohlcv&h&e=csv .

## How to Run It
* Clone repository.
* Install dotnet 6 ( https://dotnet.microsoft.com/en-us/download/dotnet/6.0 )
* Run ´dotnet tool update -g dotnet-ef´   
* Connect to a SQL Server instance and set the connection string `DefaultConnection` in `app.settings` of `StockMarket.ChatService` project.
* Open a terminal, ensure that dotnet command is installed and move to `StockMarket.ChatService` project folder.
* Run `dotnet ef database update`. This command will create the database and run existing migrations.
* Connect to a RabbitMQ instance and set connection string `RabbitConfig` in `StockMarket.ChatService` and `StockMarket.StockMsgsProcessorService` projects.
* In a terminal, move to `StockMarket.StockMsgsProcessorService` and run `dotnet run`
* In a terminal, move to `StockMarket.ChatService` and run `dotnet run`
* Open https://localhost:7252/swagger/index.html and start creating users to use the application.

## Web Client

* You can clone a basic web based chat to use this Api from https://github.com/ldoglioli86/StockMarketChat.Frontend

## Projects in solution
* StockMarket.ChatService : Is the main project in the API with connection to Database using Entity framework, user authorization using .Net Identity, chatHub client using SignalR and a messages producer for RabbitMq.
* StockMarket.ChatService.Tests : a project to test functionalities StockMarket.ChatService using NUnit and Moq. It requires some more tests because there is only one test class to show examples of tests.
* StockMarket.StockMsgsProcessorService : is the project which processes messages sent to RabbitMQ queue and after processing them, send a new message to chat room with a Bot User as sender.
* StockMarket.Common : Is the project which contains shared resources, classes and services.


## Possible improvements
* Store chat messages to retrieve them when a user join to a chat room.

## Creating a Client to connect to the Chat Hub
* You can connect your own application to send messages into the chat using the url of the server, 'https://<servername>:<port>/chat' . There are differents clients for .Net, Java, TypeScript and JavaScript https://docs.microsoft.com/en-us/aspnet/core/signalr/java-client?view=aspnetcore-6.0
