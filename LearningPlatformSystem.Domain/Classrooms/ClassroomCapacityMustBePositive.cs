namespace LearningPlatformSystem.Domain.Classrooms;

public class ClassroomCapacityMustBePositive : Exception
{
    public ClassroomCapacityMustBePositive() : base("Klassrummets kapacitet måste vara större än noll.")
    {
    }
}