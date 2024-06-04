using Sneaker.Domain.Entities;

namespace Sneaker.Domain.IRepository;

public interface IBrandRepository
{
    Task<Brand> FindBrandByName(string brandName);
}