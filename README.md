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
Minimal API endpoints.
Tar emot HTTP-anrop och returnerar HTTP-response.
Hanterar global exception handling.

- **Application**  
Use cases och services.
Koordinerar domänlogik genom att anropa domänlagret.
Returnerar resultat via ApplicationResult (Result pattern).

- **Domain**  
Innehåller domänobjekt som entiteter, value objects och aggregat (aggregate roots med tillhörande entiteter).
Definierar affärsregler och domänvalidering.  
Hanterar domänexceptions.

- **Infrastructure**  
Implementerar databasåtkomst. 
Innehåller Entity Framework Core, DbContext och repository-implementationer.  
Hanterar transaktioner och rå SQL.

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
[Öppna ER-diagram](docs/ERD.pdf)

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
