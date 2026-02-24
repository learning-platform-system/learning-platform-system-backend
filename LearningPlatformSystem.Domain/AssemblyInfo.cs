using System.Runtime.CompilerServices;
// Gör så att Infrastructure kan anropa internal metoder i Domain som annars bara är synliga inom Domain-projektet.
[assembly: InternalsVisibleTo("LearningPlatformSystem.Infrastructure")]