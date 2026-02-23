namespace LearningPlatformSystem.API.Classrooms.Update;

public sealed record UpdateClassroomRequest(Guid Id, string Name, int Capacity, string Type);

