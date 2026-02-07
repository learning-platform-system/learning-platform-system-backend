namespace LearningPlatformSystem.Domain.Subcategories;

public class SubcategoryNameIsTooLong : Exception
{
    public SubcategoryNameIsTooLong(int nameMaxLength) : base($"Underkategorinamnet får vara max vara {nameMaxLength} tecken långt.")
    {  
    }
}
