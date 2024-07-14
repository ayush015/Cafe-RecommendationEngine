# Recommendation Engine

## Overview

This repository contains two main applications: `RecommendationEngineClient` and `RecommendationEngineServer`. The Recommendation Engine is designed to provide restaurant menu recommendations and collect feedback to improve menu items.

## Table of Contents

- [Overview](#overview)
- [Architecture](#architecture)
- [Entry Point](#Entry_Point)

## Architecture

### Client-Side: RecommendationEngineClient

The `RecommendationEngineClient` application is responsible for interacting with users (e.g., chefs, employees) through a console interface. It sends requests to the server and handles the responses.

- **Namespaces:**
  - `10 Common`
  - `20 ClientOperations` (Admin, Base, Chef, Employee, Login)
  - `30 ConsoleHandler` (AdminConsole, ChefConsole, EmployeeConsole, LoginConsole)
  - `DataStore.cs`
  - `Program.cs`
  - `RequestServices.cs`

### Server-Side: RecommendationEngineServer

The `RecommendationEngineServer` application handles the business logic, data persistence, and communication with the client. It is built using .NET and follows a layered architecture with separate folders for data access, services, and controllers.

- **Namespaces:**
  - `10 Common`
  - `20 DAL` (Migrations, Models, Repository, UnitOfWork, RecommendationEngineDBContext)
  - `30 Service` (Admin, Chef, Employee, Login, Notification)
  - `40 Controller`
  - `50 Host` (ClientHandler, Program, SocketSetup)
  - `60 Test`

### Prerequisites

- .NET SDK
- Entity Framework Core
- SQL Server (or any other database supported by EF Core)

### Entry Point
- Server Host/Program.cs
- Client Program.cs

