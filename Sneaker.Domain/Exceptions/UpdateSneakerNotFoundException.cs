namespace Sneaker.Domain.Excepetions;

public class UpdateSneakerNotFoundException :BaseException
{
    public UpdateSneakerNotFoundException(string errorMessage) : base(errorMessage)
    {
    }
}