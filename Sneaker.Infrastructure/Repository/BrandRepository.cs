using Microsoft.EntityFrameworkCore;
using Sneaker.Domain.Entities;
using Sneaker.Domain.IRepository;
using Sneaker.Infrastructure.Context;

namespace Sneaker.Infrastructure.Repository;

public class BrandRepository : RepositoryBase<Brand>, IBrandRepository
{
    public BrandRepository(SneakerContext context) : base(context)
    {
    }

    public async Task<Brand> FindBrandByName(string brandName)
    {
        return await Context.Brands.FirstOrDefaultAsync(x => x.Name == brandName);
    }
}