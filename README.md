[Build ASP.NET Core Web API - Scratch To Finish (.NET8 API)](https://globant.udemy.com/course/build-rest-apis-with-aspnet-core-web-api-entity-framework/)

Create a new .NET Core Web API project

```jsx
dotnet new webapi -n InterviewPrepApi
cd InterviewPrepApi
code .
dotnet run
```

Install below packages- 

```jsx
Microsoft.EntityFrameworkCore.SqlServer
Microsoft.EntityFrameworkCore.Tools
```

```jsx
# create folder
mkdir NZWalks.API
cd NZWalks.API

# create solution
dotnet new sln -n NZWalks.API

# create Web API project
dotnet new webapi -n NZWalks.API

# add project to solution
dotnet sln add NZWalks.API

# run
cd MyApi
dotnet run

```

---

# 📘 ASP.NET Core – DbContext (Entity Framework Core)

## 🔑 Core Concept

- **DbContext** = Represents a session with the database.
- Acts as a **bridge between domain models and database tables**.
- Provides APIs for **CRUD operations** (query, insert, update, delete).
- Part of **Entity Framework Core**.

## 🛠 Responsibilities

- Maintain **connection** to the database.
- **Track changes** to entities.
- Execute **database operations**.
- Define **schema** using domain/entity classes → mapped to tables.
- Central class for **controller ↔ database communication**.

## 📂 Implementation Steps

1. **Create folder** (e.g., `Data`).
2. **Add class** (e.g., `NZWalksDbContext.cs`).
3. **Inherit from** `DbContext` (`Microsoft.EntityFrameworkCore`).
4. **Constructor**:
    - Accepts `DbContextOptions`.
    - Passes options to base class.
    - Enables **dependency injection** of connection string via `Program.cs`.

## 📌 DbSet Properties

- **DbSet<T>** = Represents a collection of entities → mapped to a table.
- Example:
    - `DbSet<Difficulty> Difficulties`
    - `DbSet<Region> Regions`
    - `DbSet<Walk> Walks`
- These properties = **tables created during EF Core migrations**.

## ⚡ Workflow

- **Controller → DbContext → Database → Controller**
- DbContext mediates all data access.
- Once migrations run, DbSet properties become **actual tables** in DB.

---

On **macOS with VS Code**, you don’t use the Visual Studio Package Manager Console command `Add-Migration`. Instead, you run the **Entity Framework Core CLI** command via the terminal. Here’s the exact step‑by‑step process for you:

---

## 🛠 Steps to Run `Add-Migration` on VS Code (Mac)

### 1. Install EF Core CLI tools

Open your terminal and install the global EF tool:

```bash
dotnet tool install --global dotnet-ef

```

Verify it’s installed:

```bash
dotnet ef --version

```

You should see an `8.x` version if you’re targeting .NET 8.

---

### 2. Add EF Core packages to your project

Inside your Web API project folder (`NZWalks.API.csproj`):

```bash
dotnet add package Microsoft.EntityFrameworkCore.SqlServer --version 8.0.10
dotnet add package Microsoft.EntityFrameworkCore.Tools --version 8.0.10
dotnet add package Microsoft.EntityFrameworkCore.Design --version 8.0.10

```

---

### 3. Ensure DbContext is registered

In `Program.cs`:

```csharp
builder.Services.AddDbContext<NZWalksDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("NZWalksConnection")));

```

---

### 4. Run the migration command

From the terminal in VS Code, navigate to the project folder:

```bash
cd NZWalks.API
dotnet ef migrations add "InitialMigration"

```

This will create a `Migrations/` folder with your migration files.

---

### 5. Apply the migration to the database

```bash
dotnet ef database update

```

---

## ⚡ Common Fixes

- **Error: No DbContext found** → Ensure your `DbContext` class is public and in the same project, or specify:
    
    ```bash
    dotnet ef migrations add "InitialMigration" --project NZWalks.API --startup-project NZWalks.API
    
    ```
    
- **Error: dotnet-ef not found** → Run `dotnet tool install --global dotnet-ef`.
- **Error: Package incompatible** → Use EF Core 8.x with .NET 8 (not EF Core 10.x).
- **SQL Server not supported on ARM Mac** → Use **Azure SQL Edge in Docker** as your local database.

---

✅ On VS Code Mac, the correct command is always:

```bash
dotnet ef migrations add "InitialMigration"

```

not `Add-Migration` (that’s Visual Studio only).

---

```jsx
//commmands to add migrations and update databse
dotnet ef migrations add "InitialMigration" 
dotnet ef database update "InitialMigration”
```

---
