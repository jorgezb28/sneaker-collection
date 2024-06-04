using Sneaker.Domain.Entities;
using Sneaker.Domain.IRepository;

namespace Sneaker.Infrastructure.Repository;

public class BrandRepositoryStatic : IBrandRepository
{
    private List<Brand> _brands = new();

    public BrandRepositoryStatic()
    {
        _brands.Add(new Brand()
        {
            Id = Guid.NewGuid(),
            Name = "Nike"
        });
    }

    public async Task<Brand> FindBrandByName(string brandName)
    {
        return _brands.FirstOrDefault(x => x.Name == brandName);
    }
}