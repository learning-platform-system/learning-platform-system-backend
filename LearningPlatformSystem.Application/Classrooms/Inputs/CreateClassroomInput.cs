using LearningPlatformSystem.Domain.Classrooms;

namespace LearningPlatformSystem.Application.Classrooms.Inputs;

public sealed record CreateClassroomInput( 
    string Name, 
    int Capacity, 
    ClassroomType Type
);

