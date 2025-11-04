
# Employee Hierarchy API (.NET 8)

A production-grade ASP.NET Core 8 Web API that demonstrates a robust, scalable solution for retrieving complex hierarchical data from a relational database. The architecture is a clean, multi-layered design fully containerized with Docker for a consistent and portable development environment.

-   **Core Tech**: .NET 8, C#, EF Core, MS SQL Server.
-   **Features**: High-performance recursive CTE query, Clean Architecture, full Docker/Docker Compose support, centralized exception handling, and integrated Swagger API documentation.

---
# Architecture & Design Notes

### **The Core Problem: Efficient Hierarchy Retrieval**

The main challenge was to avoid the N+1 query problem, where fetching a tree structure results in an unacceptable number of database roundtrips. Instead of loading the entire table into memory or making recursive C# calls to the database, the design leverages the power of the database itself.

-   **Solution: Recursive Common Table Expression (CTE)**
    -   A single, efficient SQL query is used to fetch the root employee and all their descendants. This is the most scalable approach for relational databases.
    -   EF Core's `FromSqlRaw` method is used to execute the CTE, safely passing the root employee ID as a parameter to prevent SQL injection.



# **Quick Start (Docker Compose)**

-   # **Prerequisites**

-   [Docker Desktop](https://www.docker.com/products/docker-desktop/) (Required)
  
This is the recommended method. It builds and runs the entire stack (API + Database) with a single command. **No local SQL Server installation is needed.**

**1. Clone the Repository**
```bash
git clone https://github.com/Saboloz18/EnginiDemoProject
cd EnginiDemoProject
```

**2. Run with Docker Compose**
From the project root (where `docker-compose.yml` is located), run:
```bash
docker compose up -d
```
The command will build the API image, pull the SQL Server image, and start both containers. Database migrations and data seeding are applied automatically on startup.


**3. Access the API**
-   **Swagger UI**: Open your browser to `http://localhost:8080/swagger/index.html`
-   **Database**: Connect from a tool like SSMS using the server name `localhost,1433`, Sql Server Authentication Username = sa, Password = VeryStrongPassword123@.

---
# **Quick Start (Visual Studio)**
  -   # **Prerequisites**   
  -   [.NET 8.0 SDK](https://dotnet.microsoft.com/download/dotnet/8.0)
  -   Visual studio

**1. Open solution with Visual Studio and Run

### **API Endpoint**

The primary endpoint demonstrates the core functionality.

#### ** Initial Data Seeded
                ```
                { Name = "Employee 1", ManagerId = null },
                { Name = "Employee 2", ManagerId = 1 },
                { Name = "Employee 3", ManagerId = 1 },
                { Name = "Employee 4", ManagerId = 2 },
                { Name = "Employee 5", ManagerId = 2 },
                { Name = "Employee 6", ManagerId = 4 }
                ```

**`GET /employees/{id}`**

Retrieves an employee and their entire subordinate tree. Returns a `200 OK` on success, `404 Not Found` if the ID does not exist, and `500 Internal Server Error` on failure.

**Example Response (`GET /employees/1`)**
```json
{
  "id": 1,
  "name": "Employee 1",
  "managerId": null,
  "subordinates": [
    {
      "id": 2,
      "name": "Employee 2",
      "managerId": 1,
      "subordinates": [ /* ...nested subordinates... */ ]
    }
  ]
}
```

---

### **Stack Choices & Design Decisions**

The stack was chosen for performance, maintainability, and a robust development experience.

-   **Clean Architecture**: The core design decision.
    -   **Why**: It creates a clean separation of concerns, making the application testable and independent of external frameworks like EF Core or ASP.NET Core. The `Domain` and `Application` layers contain the core business logic, while `Persistence` and `Presentation` handle external details.

-   **Docker & Docker Compose**: Used for creating a portable, "it just works" development environment.
    -   **Why**: It eliminates the need for developers to install and configure a local SQL Server instance. The `docker-compose.yml` defines the entire application stack as code.
    -   **ARM64 Compatibility**: The `db` service uses a multi-architecture SQL Server image (`azure-sql-edge` or the `mssql-server-arm64` preview) to ensure compatibility on modern hardware like Apple Silicon.

-   **Centralized Exception Handling**: A custom middleware intercepts all unhandled exceptions.
    -   **Why**: This avoids cluttering controllers with repetitive `try/catch` blocks. It provides a single place to log errors and format consistent, user-friendly JSON error responses (`404`, `500`, etc.), preventing stack traces from leaking to the client.