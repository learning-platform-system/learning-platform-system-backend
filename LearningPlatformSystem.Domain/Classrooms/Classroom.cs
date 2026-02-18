using LearningPlatformSystem.Domain.Shared.Exceptions;
using LearningPlatformSystem.Domain.Shared.Validators;

namespace LearningPlatformSystem.Domain.Classrooms;

public class Classroom
{
    public const int ClassroomNameMaxLength = 5;

    public Guid Id { get; private set; }
    public string Name { get; private set; } 
    public int Capacity { get; private set; }
    public ClassRoomType Type { get; private set; }

    private Classroom(Guid id, string name, int capacity, ClassRoomType type)
    {
        Id = id;
        Name = name;
        Capacity = capacity;
        Type = type;
    }

    public static Classroom Create(string name, int capacity, ClassRoomType type)
    {
        string normalizedName = DomainValidator.ValidateRequiredString(name, ClassroomNameMaxLength, 
            ClassroomErrors.ClassroomNameIsRequired, ClassroomErrors.ClassroomNameIsTooLong(ClassroomNameMaxLength));
        ValidateCapacity(capacity);

        Guid id = Guid.NewGuid();
        Classroom classroom = new(id, normalizedName, capacity, type);
        return classroom;
    }

    private static void ValidateCapacity(int capacity) {
        if (capacity <= 0)
        {
            throw new DomainException(ClassroomErrors.CapacityMustBePositive);
        }
    }
}
