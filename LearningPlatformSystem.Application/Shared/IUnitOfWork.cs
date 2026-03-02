namespace LearningPlatformSystem.Application.Shared;

public interface IUnitOfWork
{
    Task<int> SaveChangesAsync(CancellationToken ct);
}

/*
Skapa CoursePeriod, lägg till: sessions, resources etc.
Repot anropas vid varje action men ingenting sparas - flera actions buntas ihop i en transaktion, antingen lyckas alla eller misslyckas
Ingen enskild metod ska kopplas till SaveChangesAsync.
Returnerar int som indikerar hur många ändringar som har sparats i databasen, kan användas för att avgöra om ändringarna har sparats eller inte
*/ 