namespace LearningPlatformSystem.Domain.Shared.ValueObjects.PersonNames;

public class PersonName : ValueObject
{
    public const int FirstNameMaxLength = 50;
    public const int LastNameMaxLength = 50;

    public string FirstName { get; }
    public string LastName { get; }

    private PersonName(string firstName, string lastName)
    {
        FirstName = firstName;
        LastName = lastName;
    }

    public static PersonName Create(string firstName, string lastName)
    {
        string normalizedFirstName = firstName?.Trim() ?? string.Empty;
        string normalizedLastName = lastName?.Trim() ?? string.Empty;

        ValidateFirstName(normalizedFirstName);
        ValidateLastName(normalizedLastName);

        PersonName personName = new(normalizedFirstName, normalizedLastName);
        return personName;
    }

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return FirstName;
        yield return LastName;
    }

    // Validering
    private static void ValidateFirstName(string normalizedFirstName)
    {
        if (string.IsNullOrWhiteSpace(normalizedFirstName))
        {
            throw new PersonFirstNameIsRequired();
        }

        if (normalizedFirstName.Length > FirstNameMaxLength)
        {
            throw new PersonFirstNameTooLong(FirstNameMaxLength);
        }
    }

    private static void ValidateLastName(string normalizedLastName)
    {
        if (string.IsNullOrWhiteSpace(normalizedLastName))
        {
            throw new PersonLastNameIsRequired();
        }

        if (normalizedLastName.Length > LastNameMaxLength)
        {
            throw new PersonLastNameTooLong(LastNameMaxLength);
        }
    }
}
