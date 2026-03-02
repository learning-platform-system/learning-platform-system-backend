namespace LearningPlatformSystem.Application.Students.Inputs;

public sealed record AddStudentAddressInput(Guid Id, string Street, string PostalCode, string City);

