# JobPilotAI

JobPilotAI is an AI-powered office assistant for tradespeople. It transforms rough job notes into professional documentation, invoices, customer follow-ups and business insights.

The project is currently an early-stage .NET API built around Clean Architecture. Its processing flow and integration boundaries are in place, with deterministic placeholder content standing in for future AI capabilities.

## Features

- Convert job details and rough notes into a structured processing result
- Produce draft professional summaries and customer follow-up messages
- Calculate draft invoices from labour and material costs
- Suggest practical next actions for a job
- Generate placeholder social media copy
- Support multiple trade types with string-based JSON enum values
- Provide interactive API documentation through Swagger
- Store jobs through a thread-safe in-memory repository abstraction

## Current Architecture

JobPilotAI follows Clean Architecture principles. Dependencies point inward toward the Domain and Application layers, keeping business models independent of delivery and infrastructure concerns.

```text
                         HTTP requests
                               |
                               v
+------------------------------+------------------------------+
|                       JobPilotAI.Api                        |
|       Controllers, JSON configuration, Swagger, DI         |
+------------------------------+------------------------------+
                               |
                               v
+------------------------------+------------------------------+
|                  JobPilotAI.Application                    |
|     Commands, result models, interfaces, orchestration     |
+------------------------------+------------------------------+
             |                                 ^
             v                                 |
+----------------------------+   +-------------+--------------+
|     JobPilotAI.Domain      |   | JobPilotAI.Infrastructure |
| Models, enums, core rules  |   | Fake AI, in-memory storage|
+----------------------------+   +----------------------------+

                    +--------------------+
                    |  JobPilotAI.Tests  |
                    |   Automated tests  |
                    +--------------------+
```

| Project | Responsibility |
| --- | --- |
| `JobPilotAI.Domain` | Core job, invoice, invoice item, processing result, and trade models |
| `JobPilotAI.Application` | Job-processing use case, commands, results, and abstraction contracts |
| `JobPilotAI.Infrastructure` | Fake AI content generation, in-memory persistence, and service registration |
| `JobPilotAI.Api` | HTTP endpoints, dependency composition, JSON configuration, and Swagger |
| `JobPilotAI.Tests` | Automated test project |

## Tech Stack

- .NET 8
- C# with nullable reference types enabled
- ASP.NET Core Web API
- System.Text.Json
- Microsoft dependency injection
- Swashbuckle / OpenAPI (Swagger)
- xUnit and coverlet
- In-memory, thread-safe persistence

## Getting Started

### Prerequisites

- [.NET 8 SDK](https://dotnet.microsoft.com/download/dotnet/8.0)

### Run Locally

Clone the repository and move into its directory, then restore and build the solution:

```bash
dotnet restore
dotnet build
```

Start the API:

```bash
dotnet run --project JobPilotAI.Api
```

In the Development environment, Swagger is available at:

```text
http://localhost:5132/swagger
```

Check the API health endpoint:

```bash
curl http://localhost:5132/api/jobs/health
```

Process a sample plumbing job:

```bash
curl -X POST http://localhost:5132/api/jobs/process \
  -H "Content-Type: application/json" \
  -d '{
    "tradeType": "Plumbing",
    "customerName": "John Smith",
    "jobAddress": "14 High Street",
    "rawNotes": "Replaced kitchen mixer tap. Took 2 hours.",
    "labourHours": 2,
    "hourlyRate": 50,
    "materialsCost": 65
  }'
```

Run the automated tests with:

```bash
dotnet test
```

## Current Progress

- [x] Clean Architecture solution structure
- [x] Core job and invoice domain models
- [x] Application command and processing contracts
- [x] Job-processing API endpoint
- [x] Health endpoint and Swagger documentation
- [x] String-based JSON enum support
- [x] Fake AI assistant implementation
- [x] Thread-safe in-memory repository
- [ ] Connect the application workflow to AI-generated content
- [ ] Persist processed jobs through the repository
- [ ] Add production database storage
- [ ] Expand automated test coverage

The current `JobProcessor` validates requests and returns a complete draft result using placeholder content. No external AI provider or database is connected yet.

## Roadmap

1. Integrate the AI assistant into the job-processing workflow.
2. Add robust application and API validation with consistent error responses.
3. Introduce durable database persistence and repository implementations.
4. Expand unit, integration, and end-to-end test coverage.
5. Add authentication and secure multi-user data isolation.
6. Develop business insights, reporting, and workflow automation.
7. Build a user-facing experience for creating and managing jobs.

## Screenshots

Screenshots and product previews will be added as the user interface develops.

## Contributing

Contributions are welcome. Before opening a pull request:

1. Create a focused branch for your change.
2. Follow the existing Clean Architecture boundaries and C# conventions.
3. Add or update tests for changed behaviour.
4. Run `dotnet build` and `dotnet test` locally.
5. Describe the purpose and impact of the change in the pull request.

For larger changes, open an issue first so the proposed design can be discussed before implementation.

## License

This repository does not currently include a license. All rights are reserved by the project owner until a license is added.
