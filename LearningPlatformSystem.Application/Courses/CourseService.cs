using LearningPlatformSystem.Application.Courses.Inputs;
using LearningPlatformSystem.Application.Courses.Outputs;
using LearningPlatformSystem.Application.Shared;
using LearningPlatformSystem.Domain.Categories;
using LearningPlatformSystem.Domain.Courses;

namespace LearningPlatformSystem.Application.Courses;
// RawSql: sök kurser på del av kursnamn, filtrera på subcategory, returnera lista. LIKE
public class CourseService(ICourseRepository _courseRepository, IUnitOfWork _iUnitOfWork, ICategoryRepository _categoryRepository) : ICourseService
{
    public async Task<ApplicationResult<Guid>> CreateCourseAsync(CreateCourseInput input, CancellationToken ct)
    {
        Course course = Course.Create(input.SubcategoryId, input.Title, input.Description, input.Credits);

        bool exists = await _courseRepository.ExistsByTitleAsync(course.Title, ct);
        if (exists)
        {
            return ApplicationResult<Guid>.Fail(CourseApplicationErrors.TitleAlreadyExists(course.Title));
        }


        bool subcategoryExists = await _categoryRepository.SubcategoryExistsAsync(course.SubcategoryId, ct);
        if (!subcategoryExists)
            return ApplicationResult<Guid>.Fail(CourseApplicationErrors.SubcategoryNotFound(course.SubcategoryId));


        await _courseRepository.AddAsync(course, ct);
        await _iUnitOfWork.SaveChangesAsync(ct);

        return ApplicationResult<Guid>.Success(course.Id);
    }


    public async Task<ApplicationResult> DeleteCourseAsync(Guid id, CancellationToken ct)
    {
        Course? course = await _courseRepository.GetByIdAsync(id, ct);

        if (course is null)
        {
            return ApplicationResult.Fail(CourseApplicationErrors.NotFound(id));
        }

        await _courseRepository.RemoveAsync(course.Id, ct);
        await _iUnitOfWork.SaveChangesAsync(ct);

        return ApplicationResult.Success();
    }


    public async Task<ApplicationResult<CourseOutput>> GetCourseById(Guid courseId, CancellationToken ct)
    {
        Course? course = await _courseRepository.GetByIdAsync(courseId, ct);

        if(course is null)
        {
            return ApplicationResult<CourseOutput>.Fail(CourseApplicationErrors.NotFound(courseId));
        }

        CourseOutput courseOutput = new CourseOutput
        (
            courseId,
            course.SubcategoryId,
            course.Title,
            course.Description,
            course.Credits
        );

        return ApplicationResult<CourseOutput>.Success(courseOutput);
    }

    public async Task<ApplicationResult<IReadOnlyList<CourseOutput>>> SearchCoursesAsync(SearchCoursesInput input, CancellationToken ct)
    {
        IReadOnlyList<Course> courses = await _courseRepository.SearchAsync(input.Title, input.SubcategoryId, ct);

        IReadOnlyList<CourseOutput> courseOutputs = courses.Select(course => new CourseOutput
        (
            course.Id,
            course.SubcategoryId,
            course.Title,
            course.Description,
            course.Credits
        )).ToList();

        return ApplicationResult<IReadOnlyList<CourseOutput>>.Success(courseOutputs);
    }
}
