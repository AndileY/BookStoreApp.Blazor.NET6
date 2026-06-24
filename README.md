# BookStore - Blazor and API Application

A fully data-driven REST API built with **.NET 6** and consumed by **Blazor Server** and **Blazor WebAssembly** applications.

---

## 📚 Project Overview

**Type:** Full-stack Web Application  
**API:** RESTful API with .NET 6  
**Frontend:** Blazor Server + Blazor WebAssembly (WASM)  
**Database:** SQL Server (Entity Framework Core)  
**Authentication:** JWT (Json Web Token)  

### Description

This project is a complete book store management system. It includes a RESTful API built with .NET 6 that handles all data operations, and two frontend applications (Blazor Server and Blazor WebAssembly) that consume the API.

---

## 🚀 Technologies Used

| Technology | Purpose |
|------------|---------|
| **.NET 6** | Backend framework |
| **Entity Framework Core** | ORM for database operations |
| **SQL Server** | Database |
| **Blazor Server** | Server-side frontend |
| **Blazor WebAssembly (WASM)** | Client-side frontend |
| **JWT** | User authentication |
| **Serilog** | Logging |
| **AutoMapper** | Object mapping (Models ↔ DTOs) |
| **NSwag** | HTTP client generation |
| **NuGet** | Package management |

---

## 🛠️ Key Features

| Feature | Description |
|---------|-------------|
| **REST API** | Fully functional RESTful API following REST design principles |
| **CRUD Operations** | Create, Read, Update, Delete books and related data |
| **JWT Authentication** | Secure user authentication with JSON Web Tokens |
| **Repository Pattern** | Clean separation of concerns with Repository Pattern |
| **Dependency Injection** | Built-in DI for loose coupling and testability |
| **AutoMapper** | Automatic mapping between Models and DTOs |
| **Serilog Logging** | Structured logging for debugging and monitoring |
| **NSwag Client** | Auto-generated HTTP client for API consumption |
| **Blazor Server** | Server-side Blazor application |
| **Blazor WASM** | Client-side Blazor WebAssembly application |

---

## 📁 Project Structure

```
BookStore/
├── BookStore.API/              # REST API (backend)
│   ├── Controllers/            # API endpoints
│   ├── Models/                 # Entity models
│   ├── DTOs/                   # Data Transfer Objects
│   ├── Data/                   # Database context
│   ├── Repositories/           # Repository Pattern
│   ├── Services/               # Business logic
│   ├── Helpers/                # Helper classes (JWT, etc.)
│   ├── Middleware/             # Custom middleware
│   ├── appsettings.json        # Configuration
│   └── Program.cs              # Application entry point
│
├── BookStore.BlazorServer/     # Blazor Server frontend
│   ├── Pages/                  # Razor pages
│   ├── Shared/                 # Shared components
│   ├── Services/               # API client services
│   ├── wwwroot/                # Static files
│   └── Program.cs              # Application entry point
│
├── BookStore.BlazorWASM/       # Blazor WebAssembly frontend
│   ├── Pages/                  # Razor pages
│   ├── Shared/                 # Shared components
│   ├── Services/               # API client services
│   ├── wwwroot/                # Static files
│   └── Program.cs              # Application entry point
│
└── README.md                   # This file
```

---

## 🏗️ Architecture

### REST API (Backend)

```
[Client] → [API Controller] → [Service Layer] → [Repository] → [Database]
                                    ↓
                              [DTO + AutoMapper]
```

### Design Patterns Used

| Pattern | Purpose |
|---------|---------|
| **Repository Pattern** | Abstracts data access logic |
| **Dependency Injection** | Manages dependencies and promotes testability |
| **DTO Pattern** | Separates data contracts from domain models |
| **AutoMapper** | Automates object-to-object mapping |

### Authentication Flow

```
1. User logs in with credentials
2. API validates and generates JWT token
3. Token returned to client
4. Client includes token in subsequent requests
5. API validates token on each request
```

---

## 🔧 How to Run the Project

### Prerequisites

- [.NET 6 SDK](https://dotnet.microsoft.com/download/dotnet/6.0) installed
- [SQL Server](https://www.microsoft.com/en-us/sql-server/sql-server-downloads) (or SQL Server Express)
- [Visual Studio 2022](https://visualstudio.microsoft.com/) or later
- [NuGet Package Manager](https://www.nuget.org/)

### Step 1: Clone the Repository

```bash
git clone https://github.com/your-username/bookstore-blazor-api.git
cd bookstore-blazor-api
```

### Step 2: Configure Database

1. Open `appsettings.json` in the API project
2. Update the connection string:

```json
"ConnectionStrings": {
  "DefaultConnection": "Server=localhost;Database=BookStoreDB;Trusted_Connection=True;MultipleActiveResultSets=true"
}
```

### Step 3: Apply Migrations

```bash
cd BookStore.API
dotnet ef database update
```

### Step 4: Run the API

```bash
dotnet run
```

The API will be available at: `https://localhost:5001` (or `http://localhost:5000`)

### Step 5: Run Blazor Server

```bash
cd BookStore.BlazorServer
dotnet run
```

### Step 6: Run Blazor WASM

```bash
cd BookStore.BlazorWASM
dotnet run
```

---

## 📡 API Endpoints

| Method | Endpoint | Description |
|--------|----------|-------------|
| GET | `/api/books` | Get all books |
| GET | `/api/books/{id}` | Get book by ID |
| POST | `/api/books` | Create new book |
| PUT | `/api/books/{id}` | Update book |
| DELETE | `/api/books/{id}` | Delete book |
| POST | `/api/auth/login` | User login (JWT) |
| POST | `/api/auth/register` | User registration |

---

## 📸 Screenshots

### Blazor Applications

*Blazor Server - Book list and management interface*

<img width="1304" height="610" alt="Blazor Server - Book list page showing all books" src="https://github.com/user-attachments/assets/0c3d9f91-5021-41d9-8dfd-74e28e8db732" />

*Blazor Server - Add/Edit book form*

<img width="908" height="717" alt="Blazor Server - Add/Edit book form" src="https://github.com/user-attachments/assets/1ff7b46d-ad89-4596-bd53-7bd544883825" />

*Blazor Server - Book details and management interface*

<img width="1569" height="630" alt="Blazor Server - Book details and management interface" src="https://github.com/user-attachments/assets/76f63a39-8600-42a6-b65e-742f38f52116" />

*Blazor WASM - Login page with JWT authentication*

<img width="691" height="808" alt="Blazor WASM - Login page with JWT authentication" src="https://github.com/user-attachments/assets/a0de017d-89b1-41eb-8cf4-e19188f4073c" />

### Swagger API Documentation

*Swagger UI showing all available API endpoints*

<img width="1570" height="739" alt="Swagger UI - API documentation showing all endpoints" src="https://github.com/user-attachments/assets/e8641840-5870-4316-84a1-87a253f09e7c" />

*Swagger UI - Book endpoints with request/response schemas*

<img width="1540" height="733" alt="Swagger UI - Book endpoints with request/response schemas" src="https://github.com/user-attachments/assets/7f8d29f3-c761-479d-81dd-26724f7cd974" />

---

## 🧪 Testing

### API Testing

You can test the API using:
- [Postman](https://www.postman.com/)
- [Swagger](https://localhost:5001/swagger) 

### Authentication Testing

```bash
# Login
POST /api/auth/login
{
  "username": "user",
  "password": "password"
}

# Response includes JWT token
{
  "token": "eyJhbGciOiJIUzI1NiIs..."
}
```

---

## 📦 NuGet Packages Used

### API Project

- `Microsoft.EntityFrameworkCore.SqlServer`
- `Microsoft.EntityFrameworkCore.Tools`
- `AutoMapper.Extensions.Microsoft.DependencyInjection`
- `Serilog.AspNetCore`
- `Microsoft.AspNetCore.Authentication.JwtBearer`
- `NSwag.AspNetCore`

### Blazor Projects

- `NSwag.MSBuild`
- `Microsoft.AspNetCore.Components.WebAssembly.Authentication`

---

## 📝 Features Checklist

| Feature | Status |
|---------|--------|
| REST API with .NET 6 | ✅ |
| Entity Framework Core + SQL Server | ✅ |
| Repository Pattern | ✅ |
| Dependency Injection | ✅ |
| Serilog Logging | ✅ |
| JWT Authentication | ✅ |
| AutoMapper (Models ↔ DTOs) | ✅ |
| NSwag HTTP Client Generation | ✅ |
| Blazor Server Frontend | ✅ |
| Blazor WebAssembly Frontend | ✅ |
| CRUD Operations | ✅ |

---

**Made with .NET 6 and Blazor**
