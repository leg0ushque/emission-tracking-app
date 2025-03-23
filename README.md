# emission-tracking-app
An application complex for emission tracking

## How to:

Install EF tool
```
dotnet tool install --global dotnet-ef
```

Manage your migrations\

create:
```
dotnet ef migrations add InitialUdbCreate -c UserDbContext --project src\EmisTracking.Services.Database\EmisTracking.Services.Database.csproj
```
```
dotnet ef migrations add InitialEdbCreate -c EmissionDbContext --project src\EmisTracking.Services.Database\EmisTracking.Services.Database.csproj
```

use:
```
dotnet ef database update -c UserDbContext --project src\EmisTracking.Services.Database\EmisTracking.Services.Database.csproj
```
```
dotnet ef database update -c EmissionDbContext --project src\EmisTracking.Services.Database\EmisTracking.Services.Database.csproj
```
