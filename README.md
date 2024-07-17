# Flashcards

Web app for flashcards


## Technologies

- Frontend - Angular
- Backend - ASP.NET
- Database - MS SQL Express


## Features

- Account system
- CRUD operations on collections
- CRUD operations on cards
- Sharing collections with different users


## Launching

- Initialize MS SQL database
- Update connection string
```CMD
    ..\Flashcards\flashcards-back\api\appsettings.json
```
```json
   "DefaultConnection": "Data Source=<host-name>\\SQLEXPRESS; Initial Catalog=<database name>;..."
```
- Make migrations
```CMD
    ..\Flashcards\flashcards-back\api>dotnet-ef database update
```
- Run backend server
```CMD
   ..\Flashcards\flashcards-back\api>dotnet run 
```
- Run frontend server
```CMD
    ..\Flashcards\flashcards-front>ng serve -o
```


## Demo
![Alt text](/../master/screenshots/login.png?raw=true "Login page")

![Alt text](/../master/screenshots/collections.png?raw=true "Collections page")

![Alt text](/../master/screenshots/cards.png?raw=true "Cards page")


## Endpoints
![Alt text](/../master/screenshots/endpoints.png?raw=true "Endpoints")
