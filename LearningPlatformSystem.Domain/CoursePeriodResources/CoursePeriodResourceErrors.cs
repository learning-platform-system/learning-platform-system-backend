

namespace LearningPlatformSystem.Domain.CoursePeriodResources;

public static class CoursePeriodResourceErrors
{
    public const string CoursePeriodIdIsRequired = "Kursperiodens id måste anges.";

    public const string CoursePeriodResourceTitleIsRequired = "Kursperiodresursens titel måste anges.";
    public const string CoursePeriodResourceUrlIsRequired = "Kursperiodresursens URL måste anges.";
    internal static string CoursePeriodResourceUrlIsInvalid = "Kursperiodresursens URL är inte en giltig URL.";

    public const string CoursePeriodResourceNotFound = "Kursperiodresursen kunde inte hittas.";

    public static string CoursePeriodResourceTitleIsTooLong(int titleMaxLength) => $"Kursperiodresursens titel får inte vara längre än {titleMaxLength} tecken.";

    public static string CoursePeriodResourceUrlIsTooLong(int urlMaxLength) => $"Kursperiodresursens URL får inte vara längre än {urlMaxLength} tecken.";

    public static string CoursePeriodResourceDescriptionIsTooLong(int descriptionMaxLength) => $"Kursperiodresursens beskrivning får inte vara längre än {descriptionMaxLength} tecken.";

}
