# Talabat Backend Project

## Overview
Talabat is a backend project built using ASP.NET Core, following the Onion Architecture pattern. The project is designed to be modular, maintainable, and scalable, with a clear separation of concerns between layers. It provides a robust API for e-commerce functionalities such as product management, user authentication, cart operations, order processing, and payment integration.

## Table of Contents
- [Project Structure](#project-structure)
- [Onion Architecture](#onion-architecture)
- [Features](#features)
- [Getting Started](#getting-started)
- [Configuration](#configuration)
- [API Endpoints](#api-endpoints)
- [Contributing](#contributing)
- [License](#license)

## Project Structure
```
Talabat/
│   Talabat.sln
│
├── Talabat/                # API Layer (Presentation)
├── Talabat.Core/           # Domain Layer (Entities, Interfaces)
├── Talabat.Repository/     # Data Access Layer (Repositories)
├── Talabat.Service/        # Service Layer (Business Logic)
└── ...
```

### Layer Descriptions
- **API Layer (`Talabat/`)**: Handles HTTP requests, controllers, and middleware.
- **Core Layer (`Talabat.Core/`)**: Contains domain entities, interfaces, and specifications.
- **Repository Layer (`Talabat.Repository/`)**: Implements data access and repository patterns.
- **Service Layer (`Talabat.Service/`)**: Contains business logic and service implementations.

## Onion Architecture
The project is structured according to Onion Architecture principles:
- **Core Domain** at the center (Entities, Interfaces)
- **Service Layer** for business logic
- **Repository Layer** for data access
- **API Layer** for presentation and external communication

This architecture ensures:
- Loose coupling between layers
- Testability and maintainability
- Clear separation of concerns

## Features
- User registration and authentication (JWT-based)
- Product catalog management
- Shopping cart operations
- Order placement and tracking
- Payment processing
- Error handling middleware
- Pagination and filtering

## Getting Started
### Prerequisites
- [.NET 6 SDK or later](https://dotnet.microsoft.com/download)
- SQL Server (or update connection string for your DB)

### Setup
1. **Clone the repository:**
   ```sh
   git clone <your-repo-url>
   cd Talabat
   ```
2. **Restore dependencies:**
   ```sh
   dotnet restore
   ```
3. **Update configuration:**
   - Edit `Talabat/appsettings.json` and `appsettings.Development.json` for your environment (DB connection, JWT secret, etc).
4. **Run database migrations (if any):**
   ```sh
   dotnet ef database update --project Talabat.Repository
   ```
5. **Run the API:**
   ```sh
   dotnet run --project Talabat
   ```
6. **API will be available at:**
   - `https://localhost:5001` (default)

## Configuration
- **appsettings.json**: Main configuration file for connection strings, JWT settings, etc.
- **wwwroot/images/**: Static files/images directory.

## API Endpoints
Example endpoints (see controllers for full list):
- `POST /api/account/register` — Register a new user
- `POST /api/account/login` — User login
- `GET /api/products` — List products
- `GET /api/products/{id}` — Get product details
- `POST /api/carts` — Add to cart
- `POST /api/orders` — Place order
- `POST /api/payments` — Process payment

## Contributing
1. Fork the repository
2. Create a new branch (`git checkout -b feature/your-feature`)
3. Commit your changes
4. Push to the branch (`git push origin feature/your-feature`)
5. Open a Pull Request

## License
This project is licensed under the MIT License.
