using LearningPlatformSystem.Application.Campuses.Inputs;
using LearningPlatformSystem.Application.Shared;
using LearningPlatformSystem.Domain.Campuses;

namespace LearningPlatformSystem.Application.Campuses;

public class CampusService(ICampusRepository _campusRepository, IUnitOfWork _unitOfWork) : ICampusService
{
    public async Task<ApplicationResult<Guid>> CreateCampusAsync(CreateCampusInput input, CancellationToken ct)
    {
        bool exists = await _campusRepository.ExistsByNameAsync(input.Name, ct);

        if (exists)
        {
            return ApplicationResult<Guid>.Fail(CampusApplicationErrors.NameAlreadyExists(input.Name));
        }

        Campus campus = Campus.Create(input.Name, input.Street, input.PostalCode, input.City);

        await _campusRepository.AddAsync(campus, ct);
        await _unitOfWork.SaveChangesAsync(ct);

        return ApplicationResult<Guid>.Success(campus.Id);
    }

    public async Task<ApplicationResult> DeleteCampusAsync(Guid id, CancellationToken ct)
    {
        bool isRemoved = await _campusRepository.RemoveAsync(id, ct);

        if (!isRemoved)
            return ApplicationResult.Fail(CampusApplicationErrors.NotFound(id));
        
        await _unitOfWork.SaveChangesAsync(ct);

        return ApplicationResult.Success();
    }
}
