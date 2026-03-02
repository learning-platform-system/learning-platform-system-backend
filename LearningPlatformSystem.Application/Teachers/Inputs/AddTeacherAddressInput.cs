namespace LearningPlatformSystem.Application.Teachers.Inputs;

public sealed record AddTeacherAddressInput(Guid Id, string Street, string PostalCode, string City);
