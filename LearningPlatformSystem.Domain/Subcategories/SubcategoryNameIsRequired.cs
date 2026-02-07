namespace LearningPlatformSystem.Domain.Subcategories;

public class SubcategoryNameIsRequired : Exception
{
    public SubcategoryNameIsRequired() : base("Underkategorinamn måste anges")
    {
        
    }
}
