using LearningPlatformSystem.Domain.Classrooms;

namespace LearningPlatformSystem.Application.Classrooms.Inputs;

public sealed record UpdateClassroomInput(Guid Id, string Name, int Capacity, ClassroomType Type);

