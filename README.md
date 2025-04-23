# 🗂️ TaskFlow

A task management system built with **ASP.NET Core MVC**, following the **Clean Architecture** pattern. Designed as a hands-on project for learning backend-focused development in the .NET ecosystem.

---

## 🛠️ Technologies & Tools

- **ASP.NET Core MVC 8**
- **Entity Framework Core** – data access
- **ASP.NET Identity** – authentication & roles
- **AutoMapper** – DTO mapping
- **MediatR** – CQRS (Commands & Queries)
- **FluentValidation** – model validation
- **xUnit + Moq + FluentAssertions** – unit testing
- **Bootstrap** – basic UI styling

---

## 🎯 Features

- 🧑‍💼 User registration & login with **Identity UI**
- 🔐 Role-based access (Admin / Manager / User)
- ✅ Task creation, editing, deletion
- 📊 Filtering & sorting tasks (AssignedTo, CreatedBy, Status, Priority)
- 📨 Tasks can be assigned to users
- 📄 Clean separation of concerns using **CQRS + MediatR**
- 📦 Unit-tested command handlers & validators

---

## 🧱 Architecture

This project follows the **Clean Architecture** approach:

