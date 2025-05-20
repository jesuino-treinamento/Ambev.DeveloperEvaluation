
# Developer Evaluation - Sales API

## 📌 Overview

This project implements an API to manage sales records for the DeveloperStore team. The project follows Domain-Driven Design (DDD) principles, and leverages PostgreSQL as the database. The API supports CRUD operations for sales, along with business rules for discounts and product quantities. 

### Business Rules:
1. Purchases above 4 identical items get a 10% discount.
2. Purchases between 10 and 20 identical items get a 20% discount.
3. Maximum quantity of 20 identical items per product.
4. No discounts for quantities below 4 items.

Events are logged for:
- SaleCreated
- SaleModified
- SaleCancelled
- ItemCancelled

## 🛠 Tech Stack

- **Backend**: ASP.NET Core 6
- **Database**: PostgreSQL (via EF Core)
- **Event Logging**: Custom log-based events (for SaleCreated, SaleModified, etc.)
- **Validation**: FluentValidation
- **Unit Testing**: xUnit
- **Dependency Injection**: Microsoft.Extensions.DependencyInjection

## ⚙️ Setup Instructions

### 1. Clone the Repository

```bash
git clone https://github.com/jesuino-treinamento/ambev.developerevaluation.git
cd ambev.developerevaluation
```

### 2. Configure PostgreSQL

#### **Option 1: Using Local PostgreSQL**

1. Install PostgreSQL locally if not done already: [PostgreSQL Downloads](https://www.postgresql.org/download/)
2. Create a new database:

```sql
CREATE DATABASE salesdb;
```

3. Update `appsettings.json` with your PostgreSQL connection string:

```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Host=ambev_developer_evaluation_database;Port=5432;Database=developerevaluation;Username=developer;Password=ev@luAt10n",
    "Redis": "redis://developer:ev@luAt10n@ambev_developer_evaluation_cache:6379"
  },
  "Jwt": {
    "SecretKey": "YourSuperSecretKeyForJwtTokenGenerationThatShouldBeAtLeast32BytesLong"
  },
  "Logging": {
    "LogLevel": {
      "Default": "Information",
      "Microsoft": "Warning",
      "Microsoft.Hosting.Lifetime": "Information"
    }
  },
  "Serilog": {
    "MinimumLevel": "Information",
    "WriteTo": [
      {
        "Name": "Console",
        "Args": {
          "outputTemplate": "[{Timestamp:yyyy-MM-dd HH:mm:ss.fff zzz} [{Level:u3}] {SourceContext}] {Message:lj}{NewLine}{Exception}"
        }
      },
      {
        "Name": "File",
        "Args": {
          "path": "logs/log-.txt",
          "rollingInterval": "Day",
          "outputTemplate": "{Timestamp:yyyy-MM-dd HH:mm:ss.fff zzz} [{Level:u3}] {SourceContext} {Message:lj}{NewLine}{Exception}"
        }
      }
    ],
    "Enrich": [ "FromLogContext", "WithMachineName" ]
  },
  "AllowedHosts": "*"
}
```

#### **Option 2: Using Docker Compose**

To run PostgreSQL in Docker, use the provided `docker-compose.yml`.

1. Create and configure the `docker-compose.yml` file:

```yaml
version: '3.8'

services:
  database:
    container_name: ambev_developer_evaluation_database
    image: postgres:13
    environment:
      POSTGRES_DB: developerevaluation
      POSTGRES_USER: developer
      POSTGRES_PASSWORD: ev@luAt10n
    ports:
      - "5432:5432"
    volumes:
      - db-data:/var/lib/postgresql/data
    restart: unless-stopped
    healthcheck:
      test: ["CMD", "pg_isready", "-U", "developer", "-d", "developerevaluation"]
      interval: 5s
      timeout: 5s
      retries: 5

  nosql:
    container_name: ambev_developer_evaluation_nosql
    image: mongo:7.0
    environment:
      MONGO_INITDB_ROOT_USERNAME: developer
      MONGO_INITDB_ROOT_PASSWORD: ev@luAt10n
    ports:
      - "27017:27017"
    restart: unless-stopped
    healthcheck:
      test: echo 'db.runCommand("ping").ok' | mongosh localhost:27017/test --quiet
      interval: 10s
      timeout: 5s
      retries: 5

  cache:
    container_name: ambev_developer_evaluation_cache
    image: redis:7.4.1-alpine
    command: redis-server --requirepass ev@luAt10n
    ports:
      - "6379:6379"
    restart: unless-stopped
    healthcheck:
      test: ["CMD", "redis-cli", "-a", "ev@luAt10n", "ping"]
      interval: 10s
      timeout: 5s
      retries: 5

  webapi:
    container_name: ambev_developer_evaluation_webapi
    image: ambevdeveloperevaluationwebapi
    build:
      context: .
      dockerfile: src/Ambev.DeveloperEvaluation.WebApi/Dockerfile
    environment:
      - ASPNETCORE_ENVIRONMENT=Docker
    ports:
      - "8080:8080"
      - "8081:8081"
    depends_on:
      - database
      - nosql
      - cache
    restart: unless-stopped

volumes:
  db-data:
    driver: local
```
2. Run PostgreSQL container:

```bash
docker-compose up -d
```

3. Update `appsettings.json` to use the Docker container:

```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Host=localhost;Port=5432;Database=developerevaluation;User Id=developer;Password=ev@luAt10n;"
  },
  "Jwt": {
    "SecretKey": "YourSuperSecretKeyForJwtTokenGenerationThatShouldBeAtLeast32BytesLong"
  },
  "Logging": {
    "LogLevel": {
      "Default": "Information",
      "Microsoft": "Warning",
      "Microsoft.Hosting.Lifetime": "Information"
    }
  },

  "Serilog": {
    "MinimumLevel": "Information",
    "WriteTo": [
      {
        "Name": "Console",
        "Args": {
          "outputTemplate": "[{Timestamp:yyyy-MM-dd HH:mm:ss.fff zzz} [{Level:u3}] {SourceContext}] {Message:lj}{NewLine}{Exception}"
        }
      },
      {
        "Name": "File",
        "Args": {
          "path": "logs/log-.txt",
          "rollingInterval": "Day",
          "outputTemplate": "{Timestamp:yyyy-MM-dd HH:mm:ss.fff zzz} [{Level:u3}] {SourceContext} {Message:lj}{NewLine}{Exception}"
        }
      }
    ],
    "Enrich": [ "FromLogContext", "WithMachineName" ]
  },
  "AllowedHosts": "*"
}
```

### 3. Build and Run the Project

To build and run the application locally:

```bash
dotnet build
dotnet run
```

### 4. Run the Application in Docker

To run the application in a Docker container, create a `Dockerfile` for the app:

```dockerfile
# Base image
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

# Build image
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src

# Copy csproj files
COPY ["src/Ambev.DeveloperEvaluation.WebApi/Ambev.DeveloperEvaluation.WebApi.csproj", "src/Ambev.DeveloperEvaluation.WebApi/"]
COPY ["src/Ambev.DeveloperEvaluation.IoC/Ambev.DeveloperEvaluation.IoC.csproj", "src/Ambev.DeveloperEvaluation.IoC/"]
COPY ["src/Ambev.DeveloperEvaluation.Domain/Ambev.DeveloperEvaluation.Domain.csproj", "src/Ambev.DeveloperEvaluation.Domain/"]
COPY ["src/Ambev.DeveloperEvaluation.Common/Ambev.DeveloperEvaluation.Common.csproj", "src/Ambev.DeveloperEvaluation.Common/"]
COPY ["src/Ambev.DeveloperEvaluation.Application/Ambev.DeveloperEvaluation.Application.csproj", "src/Ambev.DeveloperEvaluation.Application/"]
COPY ["src/Ambev.DeveloperEvaluation.ORM/Ambev.DeveloperEvaluation.ORM.csproj", "src/Ambev.DeveloperEvaluation.ORM/"]

# Restore packages
RUN dotnet restore "src/Ambev.DeveloperEvaluation.WebApi/Ambev.DeveloperEvaluation.WebApi.csproj"

# Copy the rest of the code
COPY . .

WORKDIR "/src/src/Ambev.DeveloperEvaluation.WebApi"
RUN dotnet build "Ambev.DeveloperEvaluation.WebApi.csproj" -c $BUILD_CONFIGURATION -o /app/build

# Publish image
#FROM build AS publish
#ARG BUILD_CONFIGURATION=Release
#RUN dotnet publish "Ambev.DeveloperEvaluation.WebApi.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

# Final image
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Ambev.DeveloperEvaluation.WebApi.dll"]

```

1. Build the Docker image:

```bash
docker build -t developer-evaluation-webapi .
```

2. Run the Docker container:

```bash
docker run -d -p 8080:8080 --name ambev_developer_evaluation_webapi
```

### 5. Migrations and Database Setup

After the application is running, run the database migrations:

```bash
dotnet ef database update --connection "Host=localhost;Port=5432;Database=developerevaluation;User Id=developer;Password=ev@luAt10n;"
```

This will apply the database schema to your PostgreSQL database.

### 6. Testing the API

Use a tool like [Postman](https://www.postman.com/) or [curl](https://curl.se/) to test the endpoints.

#### Example API Request:

1. **Create Sale**:
   - Method: `POST`
   - URL: `http://localhost:8080/api/sales`
   - Body:
   ```json
   {
	  "customerId": "3fa85f64-5717-4562-b3fc-2c963f66afa6",
	  "branchId": "3fa85f64-5717-4562-b3fc-2c963f66afa6",
	  "saleDate": "2025-05-20T02:59:48.700Z",
	  "products": [
		{
		  "productId": "3fa85f64-5717-4562-b3fc-2c963f66afa6",
		  "quantity": 0
		}
	  ]
	}
   ```

2. **Get Sale by ID**:
   - Method: `GET`
   - URL: `http://localhost:8080/api/sales/{sale-id}`

3. **Cancel Sale**:
   - Method: `DELETE`
   - URL: `http://localhost:8080/api/sales/{sale-id}`
   
4. **GetAll Sales**
   - Method: `Get`
   - URL: `http://localhost:8080/api/sales`
   - Description: Retrieve a list of all carts
   - Query Parameters:
   - `_page` : Page number for pagination (default: 1)
   - `_size` : Number of items per page (default: 10)
   - `_order`: Ordering of results (e.g., "saledate")
   - Response: 
  ```json
  {
	  "currentPage": 1,
	  "pageSize": 10,
	  "totalPages": 1,
	  "totalCount": 2,
	  "hasPrevious": false,
	  "hasNext": false,
	  "data": [
		{
		  "id": "f4612be0-1ac3-47c0-9610-047b9d081eb3",
		  "saleNumber": "SALE-20250520024126533",
		  "saleDate": "2025-05-20T02:41:26.533933Z",
		  "customerId": "3fa85f64-5717-4562-b3fc-2c963f66afa7",
		  "customerName": "Marcelo Jesuino",
		  "branchId": "3fa85f64-5717-4562-b3fc-2c963f66afa7",
		  "branchName": "Filial Rio",
		  "items": [
			{
			  "id": "1e2d569f-f3cf-4604-a6f5-06f24a26e188",
			  "productName": "description 2",
			  "quantity": 18,
			  "unitPrice": 59.99,
			  "discount": 215.96,
			  "totalPrice": 863.86,
			  "isCancelled": true
			},
			{
			  "id": "bd35a71a-2037-458e-a659-21f76ee79ccc",
			  "productName": "description 1",
			  "quantity": 15,
			  "unitPrice": 19.99,
			  "discount": 59.97,
			  "totalPrice": 239.88,
			  "isCancelled": true
			}
		  ],
		  "totalAmount": 1103.74,
		  "totalDiscount": 275.93,
		  "isCancelled": true
		},
		{
		  "id": "5ebdca72-01c5-4caf-8267-7e21fc8173be",
		  "saleNumber": "SALE-20250520024040308",
		  "saleDate": "2025-05-20T02:40:40.308649Z",
		  "customerId": "3fa85f64-5717-4562-b3fc-2c963f66afa6",
		  "customerName": "Marcio Jesuino",
		  "branchId": "3fa85f64-5717-4562-b3fc-2c963f66afa7",
		  "branchName": "Filial Rio",
		  "items": [
			{
			  "id": "62ae5f46-1ee5-4f76-85bb-1e9887809368",
			  "productName": "description 2",
			  "quantity": 20,
			  "unitPrice": 59.99,
			  "discount": 239.96,
			  "totalPrice": 959.84,
			  "isCancelled": false
			}
		  ],
		  "totalAmount": 959.84,
		  "totalDiscount": 239.96,
		  "isCancelled": false
		}
	  ],
	  "success": true,
	  "message": "",
	  "errors": []
	}
  ```

---

## 🧪 Running Tests

To run the tests:

1. **Unit Tests**:

```bash
dotnet test
```

2. **Integration Tests**:

```bash
dotnet test --filter Integration
```

---

## 🚀 Deployment

For production environments, it's recommended to deploy using Docker containers and orchestrate with Kubernetes or any container orchestration system.

---

## 📄 License

This project is licensed under the MIT License - see the [LICENSE](LICENSE) file for details.
