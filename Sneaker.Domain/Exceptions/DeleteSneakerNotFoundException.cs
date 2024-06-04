namespace Sneaker.Domain.Excepetions;

public class DeleteSneakerNotFoundException : BaseException
{
    public DeleteSneakerNotFoundException(string errorMessage) : base(errorMessage)
    {
    }
}