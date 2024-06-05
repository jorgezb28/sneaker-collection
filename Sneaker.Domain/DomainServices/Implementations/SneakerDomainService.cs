namespace Sneaker.Domain.DomainServices.Implementations;

public class SneakerDomainService : ISneakerDomainService
{

    public bool ValidateSneakerSize(Entities.Sneaker sneaker)
    {
        if (sneaker.Size == null)
        {
            return false;
        }

        if (sneaker.Size.Value<=0 || sneaker.Size.Value>30)
        {
            return false;
        }

        if (string.IsNullOrWhiteSpace(sneaker.Size.CountryCode))
        {
            return false;
        }

        return true;
    }
}