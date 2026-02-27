using LearningPlatformSystem.Domain.Shared.Exceptions;
using LearningPlatformSystem.Domain.Shared.Validators;

namespace LearningPlatformSystem.Domain.CoursePeriodResources;
// CoursePeriodResource är child entity i CoursePeriod aggregate.
public sealed class CoursePeriodResource
{
    public const int TitleMaxLength = 200;
    public const int DescriptionMaxLength = 500;
    public const int UrlMaxLength = 500;

    public Guid Id { get; private set; }
    public Guid CoursePeriodId { get; private set; }
    public string Title { get; private set; }
    public string Url { get; private set; } 
    public string? Description { get; private set; }

    private CoursePeriodResource(Guid id, Guid coursePeriodId, string title, string url, string? description)
    {
        Id = id;
        CoursePeriodId = coursePeriodId;
        Title = title;
        Url = url;
        Description = description;
    }

    internal static CoursePeriodResource Create(Guid coursePeriodId, string title, string url, string? description)
    {
        DomainValidator.ValidateRequiredGuid(coursePeriodId, CoursePeriodResourceErrors.CoursePeriodIdIsRequired);

        string normalizedTitle = DomainValidator.ValidateRequiredString(title, TitleMaxLength, 
            CoursePeriodResourceErrors.CoursePeriodResourceTitleIsRequired, CoursePeriodResourceErrors.CoursePeriodResourceTitleIsTooLong(TitleMaxLength));

        string? normalizedDescription = DomainValidator.ValidateOptionalString(description, DescriptionMaxLength, 
            CoursePeriodResourceErrors.CoursePeriodResourceDescriptionIsTooLong(DescriptionMaxLength));

        string normalizedUrl = ValidateUrl(url);

        Guid id = Guid.NewGuid();
        return new CoursePeriodResource(id, coursePeriodId, normalizedTitle, normalizedUrl, normalizedDescription);
    }

    private static string ValidateUrl(string url)
    {
        string normalizedUrl = DomainValidator.ValidateRequiredString(url, UrlMaxLength,
            CoursePeriodResourceErrors.CoursePeriodResourceUrlIsRequired, CoursePeriodResourceErrors.CoursePeriodResourceUrlIsTooLong(UrlMaxLength));

        // Säkerställer att URL är en fullständig webbadress (http/https://example.com)
        if (!Uri.IsWellFormedUriString(normalizedUrl, UriKind.Absolute))
        {
            throw new DomainException(CoursePeriodResourceErrors.CoursePeriodResourceUrlIsInvalid);
        }
        return normalizedUrl;
    }

    internal static CoursePeriodResource Rehydrate(Guid id, Guid coursePeriodId, string title, string url, string? description)
    {
        CoursePeriodResource resource = new(id, coursePeriodId, title, url, description);
        return resource;
    }
}
