namespace LearningPlatformSystem.Application.Shared.Exceptions;

public class PersistenceException : Exception
{
    public PersistenceException(string message, Exception ex) : base(message, ex)
    {

    }
    
}
