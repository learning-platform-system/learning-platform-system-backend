using LearningPlatformSystem.Application.CoursePeriods.Inputs;
using LearningPlatformSystem.Application.Shared;
using LearningPlatformSystem.Domain.Campuses;
using LearningPlatformSystem.Domain.CoursePeriods;
using LearningPlatformSystem.Domain.Courses;
using LearningPlatformSystem.Domain.Shared.Enums;
using LearningPlatformSystem.Domain.Teachers;

namespace LearningPlatformSystem.Application.CoursePeriods;
// CACHING: GetByIdAsync, GetByCourseIdAsync. TTL-gräns 30-60sek min. Cache.Remove för Add/Update/Delete, IMemoryCache
public class CoursePeriodService(ICoursePeriodRepository _coursePeriodRepository, ICourseRepository _courseRepository, ITeacherRepository _teacherRepository, ICampusRepository _campusRepository, IUnitOfWork _unitOfWork) : ICoursePeriodService
{
    public async Task<ApplicationResult> AddSessionAsync(AddCourseSessionInput input, CancellationToken ct)
    {
        CoursePeriod? coursePeriod = await _coursePeriodRepository.GetByIdAsync(input.CoursePeriodId, ct);
        if (coursePeriod is null)
        {
            ApplicationResultError error = CoursePeriodApplicationErrors.NotFound(input.CoursePeriodId);
            return ApplicationResult.Fail(error);

        }
        //CourseFormat format, Guid classroomId, DateOnly date, TimeOnly startTime, TimeOnly endTime
        coursePeriod.AddSession(input.Format, input.ClassroomId, input.Date, input.StartTime, input.EndTime);

        await _coursePeriodRepository.AddSessionAsync(coursePeriod, ct);
        await _unitOfWork.SaveChangesAsync(ct);

        return ApplicationResult.Success();
    }

    public async Task<ApplicationResult<Guid>> CreateAsync(CreateCoursePeriodInput input, CancellationToken ct)
    {
        CoursePeriod coursePeriod = CoursePeriod.Create(Guid.NewGuid(), input.CourseId, input.TeacherId, input.StartDate, input.EndDate, input.Format);

        // COURSE EXISTS
        bool courseExists = await _courseRepository.ExistsAsync(input.CourseId, ct);

        if (!courseExists) 
        {
            ApplicationResultError error = CoursePeriodApplicationErrors.CourseNotFound(coursePeriod.CourseId);
            return ApplicationResult<Guid>.Fail(error);
        }

        // TEACHER EXISTS
        bool teacherExists = await _teacherRepository.ExistsAsync(input.TeacherId, ct);

        if (!teacherExists)
        {
            ApplicationResultError error = CoursePeriodApplicationErrors.TeacherNotFound(coursePeriod.TeacherId);
            return ApplicationResult<Guid>.Fail(error);
        }

        // CAMPUS EXISTS/ADD
        if (input.Format is CourseFormat.Onsite && input.CampusId.HasValue)
        {
            bool campusExists = await _campusRepository.ExistsAsync(input.CampusId!.Value, ct);

            if (!campusExists)
            {
                ApplicationResultError error = CoursePeriodApplicationErrors.CampusNotFound(coursePeriod.CampusId);
                return ApplicationResult<Guid>.Fail(error);
            }

            coursePeriod.ConnectToCampus(input.CampusId!.Value);
        }

        await _coursePeriodRepository.AddAsync(coursePeriod, ct);
        await _unitOfWork.SaveChangesAsync(ct);

        return ApplicationResult<Guid>.Success(coursePeriod.Id);
    }
}



