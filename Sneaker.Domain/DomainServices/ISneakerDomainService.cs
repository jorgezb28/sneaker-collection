namespace Sneaker.Domain.DomainServices;

public interface ISneakerDomainService
{
    bool ValidateSneakerSize(Entities.Sneaker sneaker);
    bool ValidateSneakerGeneralInfo(Entities.Sneaker sneaker);
}