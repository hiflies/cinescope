# CineScope

A movie discovery and management web application built with ASP.NET Core MVC. Browse movies, search the TMDB database, and manage your movie catalog with an admin panel.

## Features

- Browse and filter movies by genre
- View detailed movie information
- Search and import movies from [TMDB](https://www.themoviedb.org/)
- Admin panel for managing movies (add, edit, delete)
- User authentication (login, registration)
- Dark mode toggle
- Responsive UI with Tailwind CSS

## Tech Stack

- **Framework:** ASP.NET Core (.NET 10)
- **Database:** SQL Server with Entity Framework Core
- **Frontend:** Razor Views, Tailwind CSS
- **External API:** TMDB API

## Project Structure

```
CineScope/
├── Controllers/                # MVC controllers
│   ├── AccountController.cs    #   Login & registration
│   ├── AdminController.cs      #   Admin dashboard
│   ├── AdminMovieController.cs #   Movie CRUD (admin)
│   ├── AdminTmdbController.cs  #   TMDB search & import (admin)
│   ├── ErrorController.cs      #   Error pages
│   ├── HomeController.cs       #   Landing page
│   └── MovieController.cs      #   Movie browsing & details
├── Entities/                   # EF Core entity models
│   ├── Genre.cs
│   ├── Movie.cs
│   └── User.cs
├── Migrations/                 # EF Core migrations
│   └── Seeds/                  #   SQL seed data (genres, movies, users)
├── Models/                     # View models & DTOs
│   ├── CreateAccountModel.cs
│   ├── FilterModel.cs
│   ├── LoginModel.cs
│   ├── PaginationModel.cs
│   ├── TmdbMovieModel.cs
│   └── TmdbMovieSearchModel.cs
├── Services/                   # Business logic & data access
│   ├── CineScopeDbContext.cs   #   EF Core DbContext
│   ├── GenreRepository.cs
│   ├── MovieRepository.cs
│   ├── TmdbApiService.cs       #   TMDB API integration
│   └── UserRepository.cs
├── Utils/                      # Helper utilities
│   ├── CryptoUtils.cs
│   ├── IntExtension.cs
│   └── TmdbModelTransformer.cs
├── Views/                      # Razor views
│   ├── Account/                #   Login & registration pages
│   ├── Admin/                  #   Admin dashboard
│   ├── AdminMovie/             #   Movie management pages
│   ├── AdminTmdb/              #   TMDB search page
│   ├── Error/                  #   Error pages
│   ├── Home/                   #   Landing & 404 pages
│   ├── Movie/                  #   Movie list & details
│   └── Shared/                 #   Layouts, nav, footer, partials
├── wwwroot/                    # Static assets
│   ├── js/                     #   Page-specific JavaScript files
│   ├── app.css                 #   Global styles
│   └── tailwind.config.js      #   Tailwind CSS configuration
├── Program.cs                  # Application entry point
├── appsettings.json            # Configuration (DB connection, API key)
└── CineScope.csproj            # Project file
```

## Prerequisites

- [.NET 10 SDK](https://dotnet.microsoft.com/download)
- SQL Server (local or remote)
- A TMDB API key (read access token)

## Getting Started

### 1. Clone the repository

```bash
git clone https://github.com/hiflies/cinescope.git
cd cinescope
```

### 2. Configure the TMDB API key

Get a free API Read Access Token from [TMDB](https://www.themoviedb.org/settings/api) and paste it into `appsettings.json`:

```json
{
  "App": {
    "TmdbApiKey": "YOUR_TMDB_READ_ACCESS_TOKEN"
  }
}
```

### 3. Configure the database connection

Update the connection string in `appsettings.json` to match your SQL Server instance:

```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Data Source=localhost;Initial Catalog=cinescope;User ID=SA;Password=YourPassword;TrustServerCertificate=True;"
  }
}
```

### 4. Apply migrations

```bash
dotnet ef database update
```

### 5. Run the application

```bash
dotnet run
```

The app will be available at `https://localhost:5001` (or the port shown in the console).
