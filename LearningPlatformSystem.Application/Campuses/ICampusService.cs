using LearningPlatformSystem.Application.Campuses.Inputs;
using LearningPlatformSystem.Application.Campuses.Outputs;
using LearningPlatformSystem.Application.Shared;

namespace LearningPlatformSystem.Application.Campuses;

public interface ICampusService
{
    Task<ApplicationResult> AddCampusContactInformationAsync(AddCampusContactInformationInput input, CancellationToken ct);
    Task<ApplicationResult<Guid>> CreateCampusAsync(CreateCampusInput input, CancellationToken ct);
    Task<ApplicationResult> DeleteCampusAsync(Guid id, CancellationToken ct);
    Task<ApplicationResult<IReadOnlyList<CampusOutput>>> GetAllCampusesAsync(CancellationToken ct);
}