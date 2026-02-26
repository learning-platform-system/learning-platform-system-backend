using LearningPlatformSystem.Application.CoursePeriods.Inputs;
using LearningPlatformSystem.Application.CoursePeriods.Outputs;
using LearningPlatformSystem.Application.Shared;
using LearningPlatformSystem.Domain.Campuses;
using LearningPlatformSystem.Domain.CoursePeriods;
using LearningPlatformSystem.Domain.Courses;
using LearningPlatformSystem.Domain.Shared.Enums;
using LearningPlatformSystem.Domain.Teachers;
using Microsoft.Extensions.Caching.Memory;

namespace LearningPlatformSystem.Application.CoursePeriods;
// CACHING: GetByIdAsync, GetByCourseIdAsync. TTL-gräns 30-60sek min. Cache.Remove för Add/Update/Delete, IMemoryCache
// CoursePeriod GetAll rawsql
public class CoursePeriodService(ICoursePeriodRepository _coursePeriodRepository, ICourseRepository _courseRepository, ITeacherRepository _teacherRepository, ICampusRepository _campusRepository, IUnitOfWork _unitOfWork, IMemoryCache _cache) : ICoursePeriodService
{
    private static string GetByCourseIdCacheKey(Guid courseId)
    {
        return $"courseperiods:course:{courseId}";
    } 

    private static readonly MemoryCacheEntryOptions CacheOptions = new()
    {
        // Cachingen varar i 60sek oavsett användning. Efter 60sek tas den bort från cachen, även om den används under tiden. För att data eventuellt inte ska vara gammal för länge.
        AbsoluteExpirationRelativeToNow = TimeSpan.FromSeconds(60),
        // Cachen försvinner om den inte används igen inom 30sek. Men varje gång den används, förlängs cachningen med 30sek. För att snabbt kunna hämta data som används ofta.
        SlidingExpiration = TimeSpan.FromSeconds(30)
    };

    public async Task<ApplicationResult<IReadOnlyList<CoursePeriodOutput>>> GetCoursePeriodByCourseIdAsync(Guid courseId, CancellationToken ct)
    {
        // Skapar en unik nyckel (unik sträng) för kursen. Nyckeln visar platsen där värdet (CoursePeriods för en specifik Course) är cachad. 
        string cacheKey = GetByCourseIdCacheKey(courseId);

        // Kollar om cache redan innehåller värdet för nyckeln. Om ja -> skicka direkt. 
        if (_cache.TryGetValue(cacheKey, out IReadOnlyList<CoursePeriodOutput>? cachedValue))
        {
            return ApplicationResult<IReadOnlyList<CoursePeriodOutput>>.Success(cachedValue!);
        }

        // Annars hämta från databasen
        IReadOnlyList<CoursePeriod> coursePeriodsFromDatabase = await _coursePeriodRepository.GetByCourseIdAsync(courseId, ct);

        IReadOnlyList<CoursePeriodOutput> coursePeriodOutputs = coursePeriodsFromDatabase.Select(cp => new CoursePeriodOutput(
            Id: cp.Id,
            CourseId: cp.CourseId,
            TeacherId: cp.TeacherId,
            CampusId: cp.CampusId ?? Guid.Empty,
            StartDate: cp.StartDate,
            EndDate: cp.EndDate,
            Format: cp.Format
            )).ToList();


        // Lägg in hämtade värde i minnet under denna nyckel och använd dessa tidsregler. För att snabbt kunna hämta data
        _cache.Set(cacheKey, coursePeriodOutputs, CacheOptions);

        return ApplicationResult<IReadOnlyList<CoursePeriodOutput>>.Success(coursePeriodOutputs);
    }




    public async Task<ApplicationResult> AddAttendanceAsync(AddCourseSessionAttendanceInput input, CancellationToken ct)
    {
        // Hämtar enbart sessionerna för den aktuella coursePeriod. Inga attendances inkluderas, vilket betyder att alla courseSession attendance-listor är tomma.
        CoursePeriod? coursePeriod = await _coursePeriodRepository.GetByIdWithSessionsAsync(input.CoursePeriodId, ct);

        if (coursePeriod is null)
            return ApplicationResult.Fail(CoursePeriodApplicationErrors.NotFound(input.CoursePeriodId));

        // Lägger till exakt 1 attendance på exakt 1 session. Attendance-data finns enbart på den aktuella courseSession.
        coursePeriod.AddSessionAttendance(input.CourseSessionId, input.StudentId, input.Status);  
        
        await _coursePeriodRepository.AddSessionAttendanceAsync(coursePeriod, ct);

        await _unitOfWork.SaveChangesAsync(ct);
        _cache.Remove(GetByCourseIdCacheKey(coursePeriod.CourseId));

        return ApplicationResult.Success();
    }


    public async Task<ApplicationResult> AddEnrollmentAsync(AddCoursePeriodEnrollmentInput input, CancellationToken ct)
    {
        CoursePeriod? coursePeriod = await _coursePeriodRepository.GetByIdWithEnrollmentsAsync(input.CoursePeriodId, ct);

        if (coursePeriod is null)
            return ApplicationResult.Fail(CoursePeriodApplicationErrors.NotFound(input.CoursePeriodId));

        coursePeriod.EnrollStudent(input.StudentId);

        await _coursePeriodRepository.AddEnrollmentAsync(coursePeriod, ct);

        await _unitOfWork.SaveChangesAsync(ct);
        _cache.Remove(GetByCourseIdCacheKey(coursePeriod.CourseId));

        return ApplicationResult.Success();
    }

    public async Task<ApplicationResult> AddResourceAsync(AddCoursePeriodResourceInput input, CancellationToken ct)
    {
        CoursePeriod? coursePeriod = await _coursePeriodRepository.GetByIdWithResourcesAsync(input.CoursePeriodId, ct);

        if (coursePeriod is null)
            return ApplicationResult.Fail(CoursePeriodApplicationErrors.NotFound(input.CoursePeriodId));

        coursePeriod.AddResource(input.Title, input.Url, input.Description);

        await _coursePeriodRepository.AddResourceAsync(coursePeriod, ct);

        await _unitOfWork.SaveChangesAsync(ct);
        _cache.Remove(GetByCourseIdCacheKey(coursePeriod.CourseId));

        return ApplicationResult.Success();
    }

    public async Task<ApplicationResult> AddReviewAsync(AddCoursePeriodReviewInput input, CancellationToken ct)
    {
        CoursePeriod? coursePeriod = await _coursePeriodRepository.GetByIdWithReviewsAsync(input.CoursePeriodId, ct);

        if (coursePeriod is null)
            return ApplicationResult.Fail(CoursePeriodApplicationErrors.NotFound(input.CoursePeriodId));

        coursePeriod.AddReview(input.StudentId, input.Rating.Value, input.Comment);

        await _coursePeriodRepository.AddReviewAsync(coursePeriod, ct);

        await _unitOfWork.SaveChangesAsync(ct);
        _cache.Remove(GetByCourseIdCacheKey(coursePeriod.CourseId));

        return ApplicationResult.Success();
    }

    public async Task<ApplicationResult> AddSessionAsync(AddCourseSessionInput input, CancellationToken ct)
    {
        CoursePeriod? coursePeriod = await _coursePeriodRepository.GetByIdWithSessionsAsync(input.CoursePeriodId, ct);

        if (coursePeriod is null)
            return ApplicationResult.Fail(CoursePeriodApplicationErrors.NotFound(input.CoursePeriodId));

        coursePeriod.AddSession(input.Format, input.ClassroomId, input.Date, input.StartTime, input.EndTime);

        await _coursePeriodRepository.AddSessionAsync(coursePeriod, ct);

        await _unitOfWork.SaveChangesAsync(ct);
        _cache.Remove(GetByCourseIdCacheKey(coursePeriod.CourseId));

        return ApplicationResult.Success();
    }

    

    public async Task<ApplicationResult<Guid>> CreateCoursePeriodAsync(CreateCoursePeriodInput input, CancellationToken ct)
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
        _cache.Remove(GetByCourseIdCacheKey(coursePeriod.CourseId));

        return ApplicationResult<Guid>.Success(coursePeriod.Id);
    }

    public async Task<ApplicationResult> SetCoursePeriodGradeAsync(SetGradeInput input, CancellationToken ct)
    {
        CoursePeriod? coursePeriod = await _coursePeriodRepository.GetByIdWithEnrollmentsAsync(input.CoursePeriodId, ct);

        if (coursePeriod is null)
            return ApplicationResult.Fail(CoursePeriodApplicationErrors.NotFound(input.CoursePeriodId));

        coursePeriod.SetStudentGrade(input.StudentId, input.Grade); 

        await _coursePeriodRepository.UpdateEnrollmentAsync(coursePeriod, ct);

        await _unitOfWork.SaveChangesAsync(ct);
        _cache.Remove(GetByCourseIdCacheKey(coursePeriod.CourseId));

        return ApplicationResult.Success();
    }

    public async Task<ApplicationResult> DeleteCoursePeriodAsync(Guid id, CancellationToken ct)
    {
        CoursePeriod? coursePeriod = await _coursePeriodRepository.GetByIdAsync(id, ct);

        if (coursePeriod is null)
            return ApplicationResult.Fail(CoursePeriodApplicationErrors.NotFound(id));

       await _coursePeriodRepository.RemoveAsync(id, ct);        

        await _unitOfWork.SaveChangesAsync(ct);
        _cache.Remove(GetByCourseIdCacheKey(coursePeriod.CourseId));

        return ApplicationResult.Success();
    }
}



