Library9API

Library9API is a comprehensive RESTful API designed for library management systems. This project enables effective management of library resources.
Features:
    Book Management: Add, update, and delete books.
    Member Management: Register, update, and manage member statuses.
    Loan Management: Borrow books, return, and calculate penalties.
    User Roles: Different user roles (admin, employee, member) with specific permissions.
    Security with JWT: User authentication and authorization.

Technologies:
    ASP.NET Core: For building the RESTful API.
    Entity Framework Core: For database interactions.
    SQL Server: For database management.
    Swagger: For API documentation.
    


Local Development:
    Clone the repository:

    bash

git clone https://github.com/yourusername/Library9API.git

Update the appsettings.json file.
Apply the database migrations:

bash

dotnet ef database update

Start the application:

bash

dotnet run

API Usage
Login

Use the following endpoint for user login:

   POST /api/authentication/login

Example JSON

json

{
    "email": "admin@admin.com",
    "password": "Admin1234!"
}

Key Endpoints

  Books
        GET /api/books - Retrieve all books
        POST /api/books - Add a new book

  Members
        GET /api/members - Retrieve all members
        POST /api/members - Add a new member

Testing

You can use Swagger UI to test the project. Access the Swagger interface to explore and test API endpoints.
