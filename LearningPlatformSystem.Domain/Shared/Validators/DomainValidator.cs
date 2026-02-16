using LearningPlatformSystem.Domain.Addresses;
using LearningPlatformSystem.Domain.Shared.Exceptions;

namespace LearningPlatformSystem.Domain.Shared.Validators;

public static class DomainValidator
{
    public static string ValidateRequiredString(
    string? value,
    int maxLength,
    string requiredErrorMessage,
    string tooLongErrorMessage)
    {
        string normalizedValue = value?.Trim() ?? string.Empty;

        if (string.IsNullOrWhiteSpace(normalizedValue))
        {
            throw new DomainException(requiredErrorMessage);
        }

        if (normalizedValue.Length > maxLength)
        {
            throw new DomainException(tooLongErrorMessage);
        }

        return normalizedValue;
    }

    public static string? ValidateOptionalString(
    string? value,
    int maxLength,
    string tooLongErrorMessage)
    {
        if (value is null)
        {
            return null;
        }

        string normalizedValue = value.Trim();

        if (string.IsNullOrWhiteSpace(normalizedValue))
        {
            return null;
        }

        if (normalizedValue.Length > maxLength)
        {
            throw new DomainException(tooLongErrorMessage);
        }

        return normalizedValue;
    }


    // method-overloading - samma metodnamn, olika parameter
    public static string? ValidateOptionalString(
    string? value)
    {
        if (value is null)
        {
            return null;
        }

        string normalizedValue = value.Trim();

        if (string.IsNullOrWhiteSpace(normalizedValue))
        {
            return null;
        }

        return normalizedValue;
    }

    public static string ValidateRequiredStringWithLengthRange(
    string? value,
    int minLength,
    int maxLength,
    string requiredErrorMessage,
    string tooShortErrorMessage,
    string tooLongErrorMessage)
    {
        string normalizedValue = value?.Trim() ?? string.Empty;

        if (string.IsNullOrWhiteSpace(normalizedValue))
        {
            throw new DomainException(requiredErrorMessage);
        }

        if (normalizedValue.Length < minLength)
        {
            throw new DomainException(tooShortErrorMessage);
        }

        if (normalizedValue.Length > maxLength)
        {
            throw new DomainException(tooLongErrorMessage);
        }

        return normalizedValue;
    }

    // eftersom Guid är en struct (defaultvärde) och inte nullable, så kan den inte vara null
    public static void ValidateRequiredGuid(Guid value, string errorMessage)
    {
        if (value == Guid.Empty)
        {
            throw new DomainException(errorMessage);
        }
    }

    internal static void ValidateRequiredGuid(Guid coursePeriodId, object coursePeriodIdIsRequired)
    {
        throw new NotImplementedException();
    }
}
