using LearningPlatformSystem.Domain.Addresses;

namespace LearningPlatformSystem.Domain.Classrooms;

public class Classroom
{
    public const int NameMaxLength = 5;

    public Guid Id { get; private set; }
    public string Name { get; private set; } = null!;
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
        string normalizedName = name?.Trim() ?? string.Empty;

        ValidateName(normalizedName);
        ValidateCapacity(capacity);

        Guid id = Guid.NewGuid();
        Classroom classroom = new(id, normalizedName, capacity, type);
        return classroom;
    }

    public static void ValidateName(string name)
    {
        if (string.IsNullOrWhiteSpace(name))
        {
            throw new ClassroomNameIsRequired();
        }
        if (name.Length > NameMaxLength)
        {
            throw new ClassroomNameTooLong(NameMaxLength);
        }
    }

    public static void ValidateCapacity(int capacity) {
        if (capacity <= 0)
        {
            throw new ClassroomCapacityMustBePositive();
        }
    }
}
