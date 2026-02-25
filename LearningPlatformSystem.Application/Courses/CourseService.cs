using LearningPlatformSystem.Application.Courses.Inputs;
using LearningPlatformSystem.Application.Shared;
using LearningPlatformSystem.Domain.Categories;
using LearningPlatformSystem.Domain.Courses;

namespace LearningPlatformSystem.Application.Courses;
// RawSql: sök kurser på del av kursnamn, filtrera på subcategory, returnera lista. LIKE
public class CourseService(ICourseRepository _courseRepository, IUnitOfWork _iUnitOfWork, ICategoryRepository _categoryRepository) : ICourseService
{
    public async Task<ApplicationResult<Guid>> CreateAsync(CreateCourseInput input, CancellationToken ct)
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
}
