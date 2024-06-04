namespace Sneaker.Domain.Excepetions;

public class BaseException : Exception
{
    public BaseException(string errorMessage)
    {
        Message = errorMessage;
    }
    
    public string Message { get; set; }
}