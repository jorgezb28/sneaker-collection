namespace Sneaker.Domain.DomainServices;

public interface ISneakerDomainService
{
    bool ValidateSneakerSize(Entities.Sneaker sneaker);
}