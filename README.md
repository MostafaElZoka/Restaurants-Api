# Restaurant Management API

A robust, extensible RESTful API for managing restaurants, built with ASP.NET Core (.NET 8), MediatR, and clean architecture principles. This API supports restaurant CRUD operations, authentication, authorization, and file uploads (e.g., restaurant logos).

## Features

- **User Authentication & Authorization**  
  Secure endpoints with JWT-based authentication and role-based access (e.g., Owner role for restaurant creation).

- **Restaurant Management**  
  - Create, read, update, and delete restaurants.
  - Pagination, searching, and sorting support for listing restaurants.
  - Upload and manage restaurant logos.

- **Dishes Management**  
  - Each restaurant can have multiple dishes.
  - Dishes are included in restaurant details.

- **Clean Architecture**  
  - Separation of concerns via Application, Domain, and Infrastructure layers.
  - CQRS with MediatR for command/query handling.

## Technologies Used

- **.NET 8 / C# 12**
- **ASP.NET Core Web API**
- **MediatR** (CQRS pattern)
- **Entity Framework Core** (assumed for data access)
- **JWT Authentication**
- **Swagger/OpenAPI** (recommended for API documentation)

## Getting Started

### Prerequisites

- [.NET 8 SDK](https://dotnet.microsoft.com/download)
- SQL Server or another supported database (if using EF Core)
- Visual Studio 2022 or later

### Setup

1. **Clone the repository:**
   
   
```
   git clone https://github.com/your-org/restaurant-api.git
   cd restaurant-api
   
```

2. **Configure the database:**
   - Update `appsettings.json` and `appsettings.Development.json` with your connection string and other settings.

3. **Apply migrations (if using EF Core):**
   
   
```
   dotnet ef database update
   
```

4. **Run the application:**
   
   
```
   dotnet run
   
```

5. **Access the API:**
   - By default, the API will be available at `https://localhost:5001/api/`.

### API Endpoints

#### Authentication

- `POST /api/auth/register` — Register a new user
- `POST /api/auth/login` — Authenticate and receive a JWT

#### Restaurants

- `GET /api/restaurants` — List all restaurants (supports pagination, search, sort)
- `GET /api/restaurants/{id}` — Get restaurant by ID
- `POST /api/restaurants` — Create a new restaurant (Owner role required)
- `PATCH /api/restaurants/{id}` — Update restaurant details
- `DELETE /api/restaurants/{id}` — Delete a restaurant
- `POST /api/restaurants/{id}/logo` — Upload a restaurant logo

### Example: Create Restaurant


```
POST /api/restaurants
Authorization: Bearer {token}
Content-Type: application/json

{
  "name": "Sample Restaurant",
  "description": "A great place to eat.",
  "category": "Italian",
  "hasDelivery": true,
  "contactEmail": "info@sample.com",
  "contactNumber": "123-456-7890",
  "city": "New York",
  "street": "123 Main St",
  "postalCode": "10001"
}

```

### Error Handling

- Standard HTTP status codes are used.
- Validation and not-found errors return descriptive messages.

## Contributing

1. Fork the repository.
2. Create a feature branch.
3. Commit your changes.
4. Open a pull request.

## License

This project is licensed under the MIT License.

---

**Note:**  
- For more details on endpoints and request/response models, refer to the Swagger UI (`/swagger`) when running the application.
- Update the repository URL and other placeholders as needed.

```

After saving, your project will have a professional and informative README. If you need help with the exact file creation steps in Visual Studio, let me know!