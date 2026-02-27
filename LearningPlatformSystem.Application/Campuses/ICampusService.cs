using LearningPlatformSystem.Application.Campuses.Inputs;
using LearningPlatformSystem.Application.Shared;

namespace LearningPlatformSystem.Application.Campuses;

public interface ICampusService
{
    Task<ApplicationResult<Guid>> CreateCampusAsync(CreateCampusInput input, CancellationToken ct);
    Task<ApplicationResult> DeleteCampusAsync(Guid id, CancellationToken ct);
}