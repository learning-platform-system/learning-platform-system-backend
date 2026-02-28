# Learning Platform System

Backend-system för ett utbildningsföretag utvecklat inom utbildningen Webbutvecklare .NET (Nackademin). Projektet är utvecklat med ASP.NET Core Minimal API, Entity Framework Core samt enligt principerna för Domain-Driven Design och Clean Architecture. 

## Översikt
Systemet hanterar:
- Kurser
- Kurstillfällen 
- Kurssessioner
- Studenter
- Lärare
- Campus
- Klassrum
- Kategorier och underkategorier
- Registreringar
- Närvaro
- Recensioner

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
Minimal API endpoints
Global exception handling
HTTP-anrop

- **Application**  
Use cases
Applikationstjänster
ApplicationResult-mönster
Koordinering av domänlogik

- **Domain**  
Aggregat (aggregate roots)
Entiteter (child entities)
Value Objects
Affärsregler
Domänvalidering och domänexceptions

- **Infrastructure**  
Entity Framework Core
DbContext
Repository-implementationer
Rå SQL
Transaktioner
Caching

- **Testning**
Projektet innehåller:
- Enhetstester för samtliga domänmodeller och affärsregler
- Enhetstester för applikationstjänster (use cases)
- Integrationstester för Infrastructure-lagret och databasåtkomst
Samtliga lager (Domain, Application och Infrastructure) är testade.

## Databas design
Databasen är modellerad enligt tredje normalformen (3NF) och innehåller 13 tabeller med tydliga relationer mellan aggregat och child entities.
- Code First med Entity Framework Core
- Owned types för Value Objects
- Konfigurationer via IEntityTypeConfiguration

### ER-diagram
ER-diagrammet visar den relationella databasens struktur samt hur domänens aggregat och relationer är modellerade.
[Öppna ER-diagram](docs/erd.pdf)

## Domänmodell
Domain-lagret innehåller:
**Aggregat Roots**
- Category
- Course
- CoursePeriod
- CourseSession
- Student
- Teacher
- Campus
- Classroom

**Child Entities**
- Subcategory
- CourseSessionAttendance
- CoursePeriodReview
- CoursePeriodResource

**Association Entities**
- CoursePeriodEnrollment
- CourseSessionAttendance

**Value Objects**
- PersonName
- ContactInformation
- Address

## Tekniker
- .NET 10
- C# 14
- ASP.NET Core Minimal API
- Entity Framework Core (Code First)
- SQL Server
- SQLite (för tester)
- xUnit
- Moq

## Vad projektet demonstrerar
- Datamodellering i relationsdatabas
- Tydliga aggregate boundaries
- Separation of concerns
- Repository-mönster
- Strukturerad versionshantering med feature-branches
