using LearningPlatformSystem.Domain.Classrooms;

namespace LearningPlatformSystem.API.Classrooms.Create;

public sealed record CreateClassroomRequest(
    string Name,
    int Capacity,
    ClassroomType Type
);
