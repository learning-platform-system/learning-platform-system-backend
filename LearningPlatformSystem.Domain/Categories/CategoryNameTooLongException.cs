namespace LearningPlatformSystem.Domain.Categories;

public class CategoryNameTooLongException : Exception
{
    public CategoryNameTooLongException(int nameMaxLength) : base($"Kategorinamnet får vara max vara {nameMaxLength} tecken långt.")
    {     
    }
}
