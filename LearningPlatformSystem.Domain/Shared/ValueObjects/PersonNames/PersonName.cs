using LearningPlatformSystem.Domain.Shared.Validators;

namespace LearningPlatformSystem.Domain.Shared.ValueObjects.PersonNames;

public sealed class PersonName : ValueObject
{
    public const int FirstNameMaxLength = 50;
    public const int LastNameMaxLength = 50;

    public string FirstName { get; }
    public string LastName { get; }

    private PersonName() { } // parameterlös konstruktor som krävs av EF Core
    private PersonName(string firstName, string lastName)
    {
        FirstName = firstName;
        LastName = lastName;
    }

    public static PersonName Create(string firstName, string lastName)
    {
        string normalizedFirstName = DomainValidator.ValidateRequiredString(firstName, FirstNameMaxLength, 
            PersonNameErrors.FirstNameIsRequired, PersonNameErrors.FirstNameIsTooLong(FirstNameMaxLength));

        string normalizedLastName = DomainValidator.ValidateRequiredString(lastName, LastNameMaxLength, 
            PersonNameErrors.LastNameIsRequired, PersonNameErrors.LastNameIsTooLong(LastNameMaxLength));

        PersonName personName = new(normalizedFirstName, normalizedLastName);
        return personName;
    }

    // Definierar vilka egenskaper som avgör om två PersonName-objekt är lika. Används i ValueObjects-klassen för värdebaserad jämförelse (Equals, GetHashCode och operatorer) 
    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return FirstName;
        yield return LastName;
    }
}
