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
    public async Task<ApplicationResult> AddEnrollmentAsync(AddCoursePeriodEnrollmentInput input, CancellationToken ct)
    {
        CoursePeriod? coursePeriod = await _coursePeriodRepository.GetForAddEnrollmentAsync(input.CoursePeriodId, ct);

        if (coursePeriod is null)
            return ApplicationResult.Fail(CoursePeriodApplicationErrors.NotFound(input.CoursePeriodId));

        coursePeriod.EnrollStudent(input.StudentId);

        await _coursePeriodRepository.AddEnrollmentAsync(coursePeriod, ct);
        await _unitOfWork.SaveChangesAsync(ct);

        return ApplicationResult.Success();
    }

    public async Task<ApplicationResult> AddResourceAsync(AddCoursePeriodResourceInput input, CancellationToken ct)
    {
        CoursePeriod? coursePeriod = await _coursePeriodRepository.GetForAddResourceAsync(input.CoursePeriodId, ct);

        if (coursePeriod is null)
            return ApplicationResult.Fail(CoursePeriodApplicationErrors.NotFound(input.CoursePeriodId));

        coursePeriod.AddResource(input.Title, input.Url, input.Description);

        await _coursePeriodRepository.AddResourceAsync(coursePeriod, ct);
        await _unitOfWork.SaveChangesAsync(ct);

        return ApplicationResult.Success();
    }

    public async Task<ApplicationResult> AddReviewAsync(AddCoursePeriodReviewInput input, CancellationToken ct)
    {
        CoursePeriod? coursePeriod = await _coursePeriodRepository.GetForAddReviewAsync(input.CoursePeriodId, ct);

        if (coursePeriod is null)
            return ApplicationResult.Fail(CoursePeriodApplicationErrors.NotFound(input.CoursePeriodId));

        coursePeriod.AddReview(input.StudentId, input.Rating.Value, input.Comment);

        await _coursePeriodRepository.AddReviewAsync(coursePeriod, ct);
        await _unitOfWork.SaveChangesAsync(ct);

        return ApplicationResult.Success();
    }

    public async Task<ApplicationResult> AddSessionAsync(AddCourseSessionInput input, CancellationToken ct)
    {
        CoursePeriod? coursePeriod = await _coursePeriodRepository.GetForAddSessionAsync(input.CoursePeriodId, ct);

        if (coursePeriod is null)
            return ApplicationResult.Fail(CoursePeriodApplicationErrors.NotFound(input.CoursePeriodId));

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
            return ApplicationResult<Guid>.Fail(CoursePeriodApplicationErrors.CourseNotFound(coursePeriod.CourseId));

        // TEACHER EXISTS
        bool teacherExists = await _teacherRepository.ExistsAsync(input.TeacherId, ct);

        if (!teacherExists)
            return ApplicationResult<Guid>.Fail(CoursePeriodApplicationErrors.TeacherNotFound(coursePeriod.TeacherId));

        // CAMPUS EXISTS/ADD
        if (input.Format is CourseFormat.Onsite && input.CampusId.HasValue)
        {
            bool campusExists = await _campusRepository.ExistsAsync(input.CampusId!.Value, ct);

            if (!campusExists)
                return ApplicationResult<Guid>.Fail(CoursePeriodApplicationErrors.CampusNotFound(coursePeriod.CampusId));

            coursePeriod.ConnectToCampus(input.CampusId!.Value);
        }

        await _coursePeriodRepository.AddAsync(coursePeriod, ct);
        await _unitOfWork.SaveChangesAsync(ct);

        return ApplicationResult<Guid>.Success(coursePeriod.Id);
    }

    public async Task<ApplicationResult> SetGradeAsync(SetGradeInput input, CancellationToken ct)
    {
        CoursePeriod? coursePeriod = await _coursePeriodRepository.GetByIdWithEnrollmentsAsync(input.CoursePeriodId, ct);

        if (coursePeriod is null)
            return ApplicationResult.Fail(CoursePeriodApplicationErrors.NotFound(input.CoursePeriodId));

        coursePeriod.SetStudentGrade(input.StudentId, input.Grade); 

        await _coursePeriodRepository.UpdateEnrollmentAsync(coursePeriod, ct);
        await _unitOfWork.SaveChangesAsync(ct);

        return ApplicationResult.Success();
    }
}



