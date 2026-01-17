# 📄 .NET 10 Web API — Full-Stack Backend

This is the backend API for a full-stack web application demonstrating:

- JWT authentication with role-based access (Admin/User)  
- User CRUD operations (Create, Read, Update, Delete)  
- Password hashing using BCrypt  
- In-memory EF Core database for fast local development  
- Swagger UI for testing endpoints  
- CORS configured for frontend requests  

---

## 🔹 Features

- **Authentication**: `/api/auth/login` returns JWT token with role claims  
- **Role-Based Access**: Admin users can access User CRUD endpoints  
- **User Management**:
  - Create new users with hashed passwords  
  - Update user info and password  
  - Delete users  
  - Get user list (Admin only)  
- **Security**: Passwords hashed using BCrypt  
- **Swagger**: Full documentation and interactive testing  
- **CORS**: Allows requests from `http://localhost:3000`  

---

## 🔹 Prerequisites

- [.NET 10 SDK](https://dotnet.microsoft.com/en-us/download/dotnet/10.0)  
- IDE: Visual Studio 2022, VS Code, or Rider  

---

## 🔹 Setup & Run

1. Clone the repository:

```bash
git clone <repo-url>
cd backend
dotnet restore
dotnet run
https://localhost:44377/swagger
| Username | Password | Role  |
| -------- | -------- | ----- |
| admin    | 123456   | Admin |
| user     | !234567  | User  |

| Step             | Command                           |
| ---------------- | --------------------------------- |
| Restore packages | `dotnet restore`                  |
| Run API          | `dotnet run`                      |
| Swagger UI       | `https://localhost:44377/swagger` |

