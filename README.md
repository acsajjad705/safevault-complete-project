# SafeVault: Secure ASP.NET Core app with RBAC, validation, and tests

## Prerequisites
- .NET 8 SDK

## Configuration
- Set `ConnectionStrings:DefaultConnection` and `Jwt` values in `src/SafeVault.Api/appsettings.json`.
- Seed roles (`Admin`, `Manager`, `User`) at startup or via a simple seeding script.

## Setup
```bash
dotnet restore
dotnet ef database update --project src/SafeVault.Api --startup-project src/SafeVault.Api
