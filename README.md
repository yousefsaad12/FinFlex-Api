# FinFlex API

![FinFlex_Api 💻 (1)](https://github.com/yousefsaad12/FinFlex-Api/assets/99688907/6c658614-8a43-4ad6-9391-3158d4c1704c)

![GitHub last commit](https://img.shields.io/github/last-commit/yousefsaad12/FinFlex-Api)
![GitHub language count](https://img.shields.io/github/languages/count/yousefsaad12/FinFlex-Api)

## Description

**FinFlex API** is a comprehensive RESTful web API built with ASP.NET Core that provides backend services for stock portfolio management and investment tracking. This robust API enables users to manage their stock investments, create personalized portfolios, and engage with a community through stock comments and reviews.

The API is designed for financial applications, investment platforms, and portfolio management systems that require reliable stock data management, user authentication, and social features for investment discussions.

## Table of Contents
- [Features](#features)
- [Tech Stack](#tech-stack)
- [Prerequisites](#prerequisites)
- [Installation](#installation)
- [Usage](#usage)
- [API Endpoints](#api-endpoints)
- [Contributing](#contributing)
- [License](#license)

## Features

The FinFlex API offers the following features:

- **📈 Stock Management**: Complete CRUD operations for stock data including symbol, company name, purchase price, dividends, industry, and market cap
- **💼 Portfolio Tracking**: Personal portfolio management allowing users to add/remove stocks and track their investments
- **💬 Community Comments**: Social features enabling users to share insights, reviews, and experiences about specific stocks
- **🔐 JWT Authentication**: Secure user authentication and authorization using JSON Web Tokens
- **📚 API Documentation**: Interactive Swagger UI documentation for easy API exploration and testing
- **🔍 Advanced Filtering**: Support for filtering and pagination on stock listings
- **👤 User Management**: Complete user registration and login system
## Tech Stack

- **Framework**: ASP.NET Core 7.0
- **Database**: SQL Server with Entity Framework Core
- **Authentication**: JWT (JSON Web Tokens)
- **Documentation**: Swagger/OpenAPI
- **Serialization**: Newtonsoft.Json
- **Architecture**: RESTful API with MVC pattern

## Prerequisites

Before running this application, make sure you have the following installed:

- [.NET 7.0 SDK](https://dotnet.microsoft.com/download/dotnet/7.0)
- [SQL Server](https://www.microsoft.com/en-us/sql-server/sql-server-downloads) (LocalDB, Express, or full version)
- [Visual Studio 2022](https://visualstudio.microsoft.com/) or [Visual Studio Code](https://code.visualstudio.com/) (optional but recommended)

## Installation

To run the FinFlex API locally, follow these steps:

1. Clone this repository:

    ```bash
    git clone https://github.com/yousefsaad12/FinFlex-Api.git
    ```

2. Navigate to the project directory:

    ```bash
    cd FinFlex-Api
    ```

3. Navigate to the API project directory:

    ```bash
    cd api
    ```

4. Restore NuGet packages:

    ```bash
    dotnet restore
    ```

5. Update the database connection string in `appsettings.json` if needed

6. Apply database migrations:

    ```bash
    dotnet ef database update
    ```

7. Start the server:

    ```bash
    dotnet watch run
    ```

The server will start running at `https://localhost:7xxx` (HTTPS) or `http://localhost:5xxx` (HTTP) by default. The exact ports will be displayed in the console.

## Usage

To use the FinFlex API, follow these guidelines:

1. **Access Swagger Documentation**: Navigate to `https://localhost:7xxx/swagger` to view interactive API documentation
2. **Authentication**: Register a new account or login using existing credentials to obtain a JWT token
3. **Authorization**: Include the JWT token in the Authorization header as `Bearer {your-token}` for protected endpoints
4. **Stock Management**: Use stock endpoints to create, read, update, and delete stock information
5. **Portfolio Management**: Build and manage your personal stock portfolio
6. **Community Engagement**: Add comments and reviews for stocks to share insights with other users



## API Endpoints


### Authentication

- **POST /api/Account/register**
  - Description: Register a new user account and generate a JWT token for accessing protected endpoints.
  - Request Body:
    ```json
    {
        "userName": "string",
        "email": "user@example.com",
        "password": "string"
    }
    ```
  - Response:
    ```json
    { 
        "UserName": "userexample",
        "Email": "user@example.com",
        "token": "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9..."
    }
    ```

- **POST /api/Account/login**
  - Description: Authenticate a user and generate a JWT token for accessing protected endpoints.
  - Request Body:
    ```json
    {
        "email": "user@example.com",
        "password": "password123"
    }
    ```
  - Response:
    ```json
    { 
        "UserName": "userexample",
        "Email": "user@example.com",
        "token": "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9..."
    }
    ```

### Stocks

- **GET /api/Stocks**
  - Description: Retrieve all stocks with optional filtering and pagination support.
  - Authorization: Bearer Token (JWT)
  - Response:
    ```json
    [
      {
        "id": 1,
        "symbol": "TSLA",
        "companyName": "TESLA",
        "purchase": 1000000000,
        "lastDiv": 100,
        "industry": "string",
        "marketCap": 5000000000,
        "comments": [
            {
                "id": 1,
                "title": "Good Stock",
                "content": "string",
                "createdOn": "2024-02-01T16:47:17.435881",
                "createdBy": "User1",
                "stockId": 1
            }
    ]
      },
      {
        "id": 2,
        "symbol": "PLTR",
        "companyName": "Palantir Technologies",
        "purchase": 15000000,
        "lastDiv": 100,
        "industry": "string",
        "marketCap": 190000000,
        "comments": []
      }
    ]
    ```

- **POST /api/Stock**
  - Description: Create a new stock entry.
  - Authorization: Bearer Token (JWT)
  - Request Body:
    ```json
    {
        "symbol": "string",
        "companyName": "string",
        "purchase": 1000000000,
        "lastDiv": 100,
        "industry": "string",
        "marketCap": 5000000000
    }
    ```
  - Response:
    ```json
    {   
        "Id": "1"
        "symbol": "string",
        "companyName": "string",
        "purchase": 1000000000,
        "lastDiv": 100,
        "industry": "string",
        "marketCap": 5000000000
    }
    ```

- **PUT /api/Stock/:id**
  - Description: Update an existing Stock by ID.
  - Authorization: Bearer Token (JWT)
  - Request Body:
    ```json
    {
        "symbol": "string",
        "companyName": "string",
        "purchase": 1000000000,
        "lastDiv": 100,
        "industry": "string",
        "marketCap": 5000000000
    }
    ```
  - Response:
    ```json
    {
        "id": "1",
        "symbol": "string",
        "companyName": "string",
        "purchase": 1000000000,
        "lastDiv": 100,
        "industry": "string",
        "marketCap": 5000000000
    }
    ```



- **GET /api/Stock/:id**
  - Description: Get a Stock by ID.
  - Authorization: Bearer Token (JWT)
  - Response:
    ```json
    {
        "id": "1",
        "symbol": "string",
        "companyName": "string",
        "purchase": 1000000000,
        "lastDiv": 100,
        "industry": "string",
        "marketCap": 5000000000
    }
    ```


- **DELETE /api/Stock/:id**
  - Description: Delete a Stock by ID.
  - Authorization: Bearer Token (JWT)
  - Response:
   No Content

### Comments

- **GET /api/Comment**
  - Description: Retrieve all comments.
  - Authorization: Bearer Token (JWT)
  - Response:
    ```json
    [
      {
        "id": 1,
        "title": "Good Stock",
        "content": "string",
        "createdOn": "2024-02-01T16:47:17.435881",
        "createdBy": "User1",
        "stockId": 1
      },
      
    ]
    ```

- **GET /api/Comment/:id**
  - Description: Retrieve a specific comment by ID.
  - Authorization: Bearer Token (JWT)
  - Response:
    ```json
    [
      {
        "id": 1,
        "title": "Good Stock",
        "content": "string",
        "createdOn": "2024-02-01T16:47:17.435881",
        "createdBy": "User1",
        "stockId": 1
      },
      
    ]
    ```


- **POST /api/Comment/:stockId**
  - Description: Create a new comment for a specific stock.
  - Authorization: Bearer Token (JWT)
  - Request Body:
    ```json
    {
        "title": "string",
        "content": "string"
    }
    ```
  - Response:
    ```json
    {
        "id": 1,
        "title": "Good Stock",
        "content": "string",
        "createdOn": "2024-02-01T16:47:17.435881",
        "createdBy": "User1",
        "stockId": 1
    }
    ```

- **PUT /api/Comment/:id**
  - Description: Update an existing Comment by ID.
  - Authorization: Bearer Token (JWT)
  - Request Body:
    ```json
    {
        "title": "string",
        "content": "string"
    }
    ```
  - Response:
    ```json
    {
        "id": 1,
        "title": "Good Stock",
        "content": "string",
        "createdOn": "2024-02-01T16:47:17.435881",
        "createdBy": "User1",
        "stockId": 1
    }
    ```

- **DELETE /api/Comment/:id**
  - Description: Delete a Comment by ID.
  - Authorization: Bearer Token (JWT)
  - Response:
    ```json
    {
        "id": 1,
        "title": "Good Stock",
        "content": "string",
        "createdOn": "2024-02-01T16:47:17.435881",
        "createdBy": "User1",
        "stockId": 1
    }
    ```

### Portfolio

- **GET /api/Portfolio**
  - Description: Retrieve all portfolio stocks for the authenticated user.
  - Authorization: Bearer Token (JWT)
  - Response:
    ```json
    [
       {
         "id": 1,
         "symbol": "TSLA",
         "companyName": "TESLA",
         "purchase": 1000000000,
         "lastDiv": 100,
         "industry": "string",
         "marketCap": 5000000000,
         "comments": [],
         "portfolios": []
        },
        {
        "id": 2,
        "symbol": "PLTR",
        "companyName": "Palantir Technologies",
        "purchase": 15000000,
        "lastDiv": 100,
        "industry": "string",
        "marketCap": 190000000,
        "comments": [],
        "portfolios": []
        }
    ]
    ```

- **POST /api/Portfolio/:StockSymbol**
  - Description: Add a stock to the authenticated user's portfolio by stock symbol.
  - Authorization: Bearer Token (JWT)
  - Response:
    ```json
    {
        "message": "Stock has been added"
    }
    ```

- **DELETE /api/Portfolio/:StockSymbol**
  - Description: Remove a stock from the authenticated user's portfolio by stock symbol.
  - Authorization: Bearer Token (JWT)
  

---



## Contributing

Contributions are welcome! If you have any suggestions, feature requests, or bug reports, please open an issue or submit a pull request.

### How to Contribute

1. Fork the repository
2. Create a feature branch (`git checkout -b feature/amazing-feature`)
3. Commit your changes (`git commit -m 'Add some amazing feature'`)
4. Push to the branch (`git push origin feature/amazing-feature`)
5. Open a Pull Request

## License

This project is licensed under the MIT License - see the [LICENSE](LICENSE) file for details.

---

**Built with ❤️ by [yousefsaad12](https://github.com/yousefsaad12)**



