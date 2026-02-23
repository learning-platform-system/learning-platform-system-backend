using LearningPlatformSystem.Domain.Classrooms;

namespace LearningPlatformSystem.Application.Classrooms.Outputs;

public sealed record ClassroomOutput(Guid Id, string Name, int Capacity, ClassroomType Type);
