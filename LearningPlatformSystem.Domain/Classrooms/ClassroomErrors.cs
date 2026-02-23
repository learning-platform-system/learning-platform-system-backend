namespace LearningPlatformSystem.Domain.Classrooms;

public static class ClassroomErrors
{
    public const string CapacityMustBePositive = "Klassrummets kapacitet måste vara större än noll.";

    public const string NameIsRequired = "Klassrummets namn måste anges.";

    public const string IdIsRequired = "Klassrummets id måste anges.";

    public static string NameIsTooLong(int classroomNameMaxLength) => 
        $"Klassrummets namn får inte vara längre än {classroomNameMaxLength} tecken.";
}
