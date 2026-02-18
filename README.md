# Learning Platform System

Backend för ett utbildningssystem utvecklat inom utbildningen Webbutvecklare .NET (Nackademin).
Systemet hanterar kurser, kurstillfällen, lärare, deltagare och registreringar via ett REST-baserat API.

Projektets huvudsakliga fokus är databashantering, domänmodellering och backend-arkitektur.
Frontend är implementerad i React och finns i ett separat repository (länk nedan).

## Projektets status
Projektet är under utveckling.

## Arkitektur
Projektet är uppbyggt enligt Domain-Driven-Design (DDD) och Clean Architecture.
```
Presentation (Minimal API)
        ↓
Application (Use Cases)
        ↓
Domain (Affärsregler & Aggregates)
        ↑
Infrastructure (EF Core & Databas)
```

### Lagerstruktur
- **Presentation**  
  ASP.NET Core Minimal API som exponerar endpoints.

- **Application**  
  Use cases och applikationslogik.
  Koordinerar domän och infrastruktur.

- **Domain**  
 Aggregates, entiteter, value objects och affärsregler.
 Repository-interfaces definieras här.

- **Infrastructure**  
  Entity Framework Core, konfigurationer och repository-implementationer.

- **Tests**  
  Enhets- och integrationstester för centrala delar av systemet.

## Databas
- Code First med Entity Framework Core
- Relationsdatabas modellerad enligt 3NF
- Tydliga relationer mellan aggregates
- Owned types för Value Objects
- Konfigurationer via IEntityTypeConfiguration

### Exempel på centrala aggregates:
- Course
- CoursePeriod
- Student
- Teacher
- Campus
- Classroom
- Category

## Teknisk stack
### Backend (huvudfokus)
- C#
- .NET
- ASP.NET Core Minimal API
- Entity Framework Core
- SQL Server

### Frontend (separat repository)
🔗 [Frontend repository](https://github.com/learning-platform-system/learning-platform-system-frontend)

## Vad projektet demonstrerar
- Datamodellering i relationsdatabas
- Tydliga aggregate boundaries
- Separation of concerns
- Repository-mönster
- Strukturerad versionshantering med feature-branches
