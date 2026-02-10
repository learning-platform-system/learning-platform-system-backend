namespace LearningPlatformSystem.Domain.Classrooms;

public static class ClassroomErrors
{
    public const string CapacityMustBePositive = "Klassrummets kapacitet måste vara större än noll.";

    public const string ClassroomNameIsRequired = "Klassrummets namn måste anges.";
    public static string ClassroomNameIsTooLong(int classroomNameMaxLength) => 
        $"Klassrummets namn får inte vara längre än {classroomNameMaxLength} tecken.";
}
