# ☕ Coffee Management System

A full-stack **Coffee Shop Management System** built with **ASP.NET Core**, **Clean Architecture**, and **Angular**.  
It handles everything from user authentication to managing coffee items, categories, inventory, and orders.  
Designed for scalability, modularity, and clean separation of concerns.

---

## 📚 Table of Contents

- [✨ Key Features](#-key-features)
- [🛠️ Tech Stack](#️-tech-stack)
- [⚙️ Getting Started](#-getting-started)

---

## ✨ Key Features

✅ **User Authentication & Role-based Authorization**  
→ Roles: Admin, Staff, Customer

✅ **CRUD Operations**  
→ Coffee Items, Categories, Inventory, Orders

✅ **Secure Login with JWT + ASP.NET Identity**  
✅ **Clean Architecture** (Application, Domain, Infrastructure, API)  
✅ **RESTful API + DTO-based data transfer**  
✅ **Swagger UI** for easy API testing  
✅ **Angular Frontend with Tailwind CSS + DaisyUI**  
✅ **Route Protection using Auth Guards**  

---

## 🛠️ Tech Stack

| Layer       | Tech                            |
|-------------|----------------------------------|
| Backend     | ASP.NET Core 8, EF Core, C#     |
| Frontend    | Angular 19, Tailwind CSS, DaisyUI |
| Database    | SQL Server                      |
| Security    | JWT, ASP.NET Identity Roles     |
| API Testing | Swagger                         |

---

## ⚙️ Getting Started

### 🧩 Backend Setup

```bash
# 1. Clone the repo
git clone https://github.com/your-username/coffee-management-system.git

# 2. Navigate to backend project
cd coffee-management-system/CoffeeManagementSystem.API

# 3. Add your SQL Server connection string in appsettings.json

### 🖼️ Frontend Setup
bash
Copy
Edit
# 1. Navigate to the Angular frontend
cd coffee-management-system/coffee-frontend

# 2. Install dependencies
npm install

# 3. Run the development server
ng serve

# App runs at: http://localhost:4200

# 4. Apply DB migrations
dotnet ef database update

# 5. Run the API
dotnet run
