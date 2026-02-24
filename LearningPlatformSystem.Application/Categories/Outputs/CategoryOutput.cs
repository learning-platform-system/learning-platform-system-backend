using LearningPlatformSystem.Domain.Subcategories;

namespace LearningPlatformSystem.Application.Categories.Outputs;

public sealed record CategoryOutput(Guid Id, string Name, IEnumerable<SubcategoryOutput> Subcategories);

