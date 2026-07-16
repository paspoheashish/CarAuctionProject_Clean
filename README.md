# CarAuctionProject_Clean
CarAuctionProject_Clean

# Folder Tree
CarAuctionManagementSystem/
├── CarAuctionManagementSystem.slnx
├── Program.cs
├── README.md
├── DependencyInjection/
│   └── PresentationServiceRegistration.cs
├── Controllers/
│   ├── AuctionsController.cs
│   ├── BidsController.cs
│   └── VehiclesController.cs
├── DTOs/
│   ├── Auctions/
│   │   ├── AuctionResponse.cs
│   │   ├── CloseAuctionRequest.cs
│   │   └── StartAuctionRequest.cs
│   ├── Bids/
│   │   ├── BidResponse.cs
│   │   └── PlaceBidRequest.cs
│   └── Vehicles/
│       ├── CreateVehicleRequest.cs
│       ├── SearchVehiclesRequest.cs
│       └── VehicleResponse.cs
├── Mapping/
│   ├── AuctionMappingProfile .cs
│   ├── BidMappingProfile.cs
│   └── VehicleMappingProfile.cs
├── MiddleWare/
│   └── ExceptionMiddleware.cs
├── CarAuctionManagementSystem.Application/
│   ├── CarAuctionManagementSystem.Application.csproj
│   ├── DependencyInjection/
│   │   └── ApplicationServiceRegistration.cs
│   ├── Interfaces/
│   │   ├── Repositories/
│   │   │   ├── IAuctionRepository.cs
│   │   │   ├── IBidRepository.cs
│   │   │   ├── IUnitOfWork.cs
│   │   │   └── IVehicleRepository.cs
│   │   └── Services/
│   │       ├── IAuctionService.cs
│   │       └── IVehicleService.cs
│   ├── Services/
│   │   ├── AuctionService.cs
│   │   └── VehicleService.cs
│   └── UseCases/
│       ├── Auctions/
│       │   └── Query/
│       │       ├── GetAllActiveAuctionsHandler .cs
│       │       ├── GetAuctionByIdHandler .cs
│       │       └── GetAuctionByVehicleIdHandler .cs
│       └── Bids/
│           ├── Command/
│           │   └── PlaceBidHandler.cs
│           └── Query/
│               └── GetBidsHandler.cs
├── CarAuctionManagementSystem.Domain/
│   ├── CarAuctionManagementSystem.Domain.csproj
│   ├── Entities/
│   │   ├── Auction.cs
│   │   ├── Bid.cs
│   │   ├── Vehicle.cs
│   │   ├── Hatchback.cs
│   │   ├── Sedan.cs
│   │   ├── SUV.cs
│   │   └── Truck.cs
│   └── Enums/
│       └── VehicleType.cs
├── CarAuctionManagementSystem.Infrastructure/
│   ├── CarAuctionManagementSystem.Infrastructure.csproj
│   ├── Database/
│   │   └── InMemoryDatabase.cs
│   └── DependencyInjection/
│       └── InfrastructureServiceRegistration.cs
│   └── Repositories/
│       ├── AuctionRepository.cs
│       ├── BidRepository.cs
│       ├── UnitOfWork.cs
│       └── VehicleRepository.cs
└── .vscode/ (optional local config)


# CarAuctionManagementSystem

A simple in-memory car auction management API built with .NET 8. The solution demonstrates a clean architecture split into Presentation, Application, Domain and Infrastructure layers, with MediatR for use-case handlers and AutoMapper for DTO mappings.

## Project structure

- Controllers/ - ASP.NET Core API controllers (Vehicles, Auctions, Bids)
- DTOs/ - Request and response DTOs used by controllers
- Mapping/ - AutoMapper profiles and converters
- CarAuctionManagementSystem.Application/ - Application services, use cases (handlers), interfaces
- CarAuctionManagementSystem.Domain/ - Domain entities and enums
- CarAuctionManagementSystem.Infrastructure/ - In-memory database and repository implementations
- MiddleWare/ - Centralized exception handling middleware
- Program.cs - application bootstrap and DI registration

## Key technologies

- .NET 8
- ASP.NET Core minimal host
- MediatR (request/handler pattern)
- AutoMapper for DTO ↔ Domain conversions
- In-memory datastore (InMemoryDatabase) for demo purposes
- Swagger/OpenAPI exposed by default

## Getting started

Prerequisites:
- .NET 8 SDK
- Visual Studio 2022/2026 or VS Code (or `dotnet` CLI)

Run with Visual Studio:
- Open `CarAuctionManagementSystem.slnx` and run (F5) or Start Without Debugging (Ctrl+F5).

Run with dotnet CLI:

- From repository root:
  - `dotnet run --project CarAuctionManagementSystem.csproj`
  - The API will start and Swagger UI is enabled at `/swagger`.

## Configuration & DI

- Program.cs registers 3 service registrations:
  - Presentation (controllers, AutoMapper, Swagger)
  - Application (services, MediatR handlers)
  - Infrastructure (repositories, unit of work)

- Repositories are registered as singletons and use `InMemoryDatabase` which is seeded with sample vehicles.

- Exception handling is centralized via `MiddleWare/ExceptionMiddleware.cs` which converts ApplicationException to 400 and other exceptions to 500 with Problem Details.

## Seed data

The in-memory database is seeded with 20 vehicles across Hatchback, Sedan, SUV and Truck types in `Infrastructure/Database/InMemoryDatabase.cs`.
Auctions start empty; use the API to create/start auctions and place bids.

## API Endpoints

Base path: `/api`

Vehicles
- POST /api/vehicles
  - Description: Add a vehicle. Body: `CreateVehicleRequest` (see DTOs)
  - Requires header `X-User-Id` (controllers currently override header with a hardcoded value; see TODO).
  - Example:

  curl -X POST "http://localhost:5000/api/vehicles" -H "Content-Type: application/json" -d \
  '{
    "id": 21,
    "type": "hatchback",
    "manufacturer": "Toyota",
    "model": "Yaris",
    "year": 2022,
    "startingBid": 4500,
    "numberOfDoors": 4
  }'

- GET /api/vehicles?Type={type}&Manufacturer={man}&Model={model}&Year={year}
  - Description: Search vehicles. All query parameters optional.
  - Example: `GET /api/vehicles?Type=sedan&Manufacturer=Toyota`

Auctions
- GET /api/auctions/all
  - Returns all auctions.

- GET /api/auctions/allactive
  - Returns active auctions.

- GET /api/auctions/{auctionId}
  - Returns a single auction by id.

- GET /api/auctions/vehicle/{vehicleId}
  - Returns auction associated with a vehicle.

- POST /api/auctions/start
  - Body: `StartAuctionRequest` { "vehicleId": <id> }
  - Starts auction for vehicle. Requires `X-User-Id` header (controller currently sets owner to `owner1`).
  - Example:
    curl -X POST "http://localhost:5000/api/auctions/start" -H "Content-Type: application/json" -d '{"vehicleId":1}'

- POST /api/auctions/close
  - Body: `CloseAuctionRequest` { "auctionId": <id> }
  - Closes the auction. Requires `X-User-Id` header (controller currently sets owner to `owner1`).

Bids
- POST /api/bids/place
  - Body: `PlaceBidRequest` { "auctionId": <id>, "bidderUserId": "userX", "amount": 5000 }
  - Example:
    curl -X POST "http://localhost:5000/api/bids/place" -H "Content-Type: application/json" -d '{"auctionId":1,"bidderUserId":"bidder1","amount":3600}'

- GET /api/bids/{auctionId}
  - Returns bids for a specific auction.

## Design notes

- The application follows a layered approach:
  - Controllers act as presentation layer and coordinate with Application services or MediatR requests.
  - Application layer defines service interfaces, MediatR handlers, DTOs, and mapping profiles.
  - Infrastructure layer contains repository implementations and the in-memory database.
  - Domain layer contains entities and enums.

- MediatR is used for bids and auction queries; Application Services (IAuctionService / IVehicleService) contain business workflows for starting/closing auctions and adding vehicles.

## Known issues / TODOs

- Controllers currently read `X-User-Id` from headers but then override it with a hardcoded value (`owner1`). Update controllers to trust the header value and validate it properly.
- In-memory persistence is for demo only. Replace with EF Core or another persistent store for production.
