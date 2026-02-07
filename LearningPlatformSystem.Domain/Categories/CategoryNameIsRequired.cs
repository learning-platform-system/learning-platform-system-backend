namespace LearningPlatformSystem.Domain.Categories;

public class CategoryNameIsRequired : Exception
{
    public CategoryNameIsRequired() : base("Kategorinamn måste anges")
    {
        
    }
}
