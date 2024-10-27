Library9API

This project is a comprehensive library management system designed to help librarians, employees, and members manage library resources effectively. The project includes features such as book management, member management, loan management, and more. It is built with a focus on scalability, security, and ease of use.

Technologies Used

This project utilizes a variety of technologies to ensure a robust and efficient system. Below is a detailed explanation of the key technologies used:

ASP.NET Core

ASP.NET Core is a cross-platform, high-performance framework for building modern, cloud-based, internet-connected applications. It is used to build the RESTful API that powers the LibraryAPI project.

Entity Framework Core

Entity Framework Core (EF Core) is an open-source ORM (Object-Relational Mapper) for .NET. It allows developers to work with a database using .NET objects, eliminating the need for most data-access code.

Identity Framework

The ASP.NET Core Identity framework is used to manage users, passwords, roles, and claims. It provides a complete, customizable authentication and authorization system. It integrates seamlessly with EF Core to handle the storage and retrieval of user-related data.

JWT (JSON Web Tokens)

JWT is used for securely transmitting information between parties as a JSON object. It is used for authentication and authorization in the LibraryAPI project.

SQL Server

SQL Server is a relational database management system developed by Microsoft. It is used as the database for the LibraryAPI project to store all the library data.

Swagger

Swagger is an open-source tool for documenting APIs. It provides a user-friendly interface to explore and test API endpoints. The LibraryAPI project includes Swagger for API documentation and testing.
    


Local Development:
    Clone the repository:

    bash

    git clone https://github.com/yourusername/Library9API.git

    Update the appsettings.json file.
    
Apply the database migrations:

    bash
    
    dotnet ef migrations add Initial  

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
