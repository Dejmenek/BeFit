# BeFit - Fitness Tracking Application

**Language / Język:** [English](#english) | [Polski (README_PL.md)](README_PL.md)

---

### Table of Contents

1. [General Information](#general-information)
2. [Used Technologies](#used-technologies)
3. [Features](#features)
4. [Installation and Setup](#installation-and-setup)
5. [Examples](#examples)

### General Information

**BeFit** is a comprehensive fitness tracking web application designed to help users monitor their workouts, track progress, and achieve their fitness goals. This is an **academic project** developed as part of coursework, demonstrating modern web development practices using ASP.NET Core.

The application provides a user-friendly interface for managing exercises, creating workout templates, logging workout sessions, and analyzing fitness statistics. BeFit helps fitness enthusiasts stay consistent with their training by offering features like workout templates, session tracking, and progress analytics.
Developed as an academic project.

### Used Technologies

- **.NET 9.0** - Modern .NET framework for building web applications
- **ASP.NET Core MVC** - Web framework for building dynamic web applications
- **Entity Framework Core 9.0** - ORM for database operations
- **SQL Server** - Relational database management system
- **ASP.NET Core Identity** - Authentication and authorization system
- **Bootstrap 5** - Frontend CSS framework for responsive design
- **jQuery** - JavaScript library for DOM manipulation
- **jQuery Validation** - Client-side form validation
- **Razor Pages** - Server-side page rendering engine

### Features

#### User Management
- User registration and authentication
- Secure login with email confirmation
- Role-based access control
- User profile management

#### Exercise Management
- Browse and search exercises
- Exercise details including:
  - Exercise type (Cardio, Strength, Flexibility, etc.)
  - Target muscle groups
  - Difficulty levels
  - Equipment requirements
  - Detailed instructions

#### Workout Templates
- Create custom workout templates
- Define preferred training days
- Add multiple exercises to templates
- Set goals and descriptions for each template
- Edit and delete templates
- View template calendar

#### Workout Sessions
- Log workout sessions with start and end times
- Track detailed exercise performance:
  - Sets and repetitions
  - Weight used
  - Rest time between sets
  - Tempo (for time-based exercises)
  - Duration (for cardio exercises)
  - Distance (for running/cycling)
- Add notes to workout sessions
- View workout history

#### Dashboard & Statistics
- **Workout Statistics:**
  - Total number of workouts completed
  - Current workout streak
  - Total time spent training
- **Exercise Statistics:**
  - Sessions trained per exercise
  - Total repetitions performed
  - Average and maximum weight lifted
- **Training Calendar:**
  - Weekly view of scheduled workout templates
  - Visual representation of training schedule

### Installation and Setup

#### Prerequisites

- **.NET 9.0 SDK** - [Download here](https://dotnet.microsoft.com/download/dotnet/9.0)
- **SQL Server** - SQL Server Express or LocalDB (included with Visual Studio)
- **Visual Studio 2022** or **Visual Studio Code** (recommended)
- **Git** (optional, for cloning the repository)

#### Step 1: Clone the Repository

```bash
git clone <repository-url>
cd BeFit
```

#### Step 2: Configure Database Connection

1. Open `BeFit/appsettings.json`
2. Update the connection string to match your SQL Server instance:

```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=(localdb)\\mssqllocaldb;Database=BeFit;Trusted_Connection=True;MultipleActiveResultSets=true"
  }
}
```

For SQL Server Express, use:
```json
"DefaultConnection": "Server=.\\SQLEXPRESS;Database=BeFit;Trusted_Connection=True;MultipleActiveResultSets=true"
```

#### Step 3: Restore Dependencies

```bash
cd BeFit
dotnet restore
```

#### Step 4: Run Database Migrations

```bash
dotnet ef database update
```

This will create the database schema and apply all migrations.

#### Step 5: Run the Application

```bash
dotnet run
```

#### Step 6: Initial Setup

1. Navigate to the registration page
2. Create a new user account
3. Confirm your email (if email confirmation is enabled)
4. Log in and start using the application

#### Development Notes

- The application uses Entity Framework Core migrations for database schema management
- Initial data seeding is performed automatically on application startup
- For development, the database is created automatically if it doesn't exist

### Examples

#### Home Page

#### Dashboard

#### Workout Templates

#### Workout Session

#### Exercise Statistics
