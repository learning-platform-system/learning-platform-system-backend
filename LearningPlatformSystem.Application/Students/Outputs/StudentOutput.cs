namespace LearningPlatformSystem.Application.Students.Outputs;

public sealed record StudentOutput(Guid Id, string FirstName, string LastName, string Email, string PhoneNumber);

