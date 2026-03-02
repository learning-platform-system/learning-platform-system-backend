using LearningPlatformSystem.API.Shared;
using LearningPlatformSystem.Application.CoursePeriods;
using LearningPlatformSystem.Application.CoursePeriods.Inputs;
using LearningPlatformSystem.Application.Shared;

namespace LearningPlatformSystem.API.CoursePeriods.AddCoursePeriodReview;

public static class AddCoursePeriodReviewEndpoint
{
        public static RouteGroupBuilder MapPostCoursePeriodReviewEndpoint(this RouteGroupBuilder group)
        {
            group.MapPost("/{coursePeriodId:guid}/reviews", HandleAsync);
            return group;
        }
    
        private static async Task<IResult> HandleAsync(Guid coursePeriodId, AddCoursePeriodReviewRequest request, ICoursePeriodService service, CancellationToken ct)
        {
            AddCoursePeriodReviewInput input = new(
                request.StudentId,
                coursePeriodId,
                request.Rating,
                request.Comment
            );
    
            ApplicationResult result = await service.AddReviewAsync(input, ct);
    
            if (result.IsFailure)
            {
                return result.ToHttpFailResult();
            }
            return Results.Created();
    }
}
