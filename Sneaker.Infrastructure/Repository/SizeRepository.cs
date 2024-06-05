using Microsoft.EntityFrameworkCore;
using Sneaker.Domain.Entities;
using Sneaker.Domain.IRepository;
using Sneaker.Infrastructure.Context;

namespace Sneaker.Infrastructure.Repository;

public class SizeRepository: RepositoryBase<Size>, ISizeRepository
{
    public SizeRepository(SneakerContext context) : base(context)
    {
    }

    public async Task<Size> GetSizeByValue(int sneakerSize)
    {
        return await Context.Sizes.FirstOrDefaultAsync(x => x.Value == sneakerSize);
    }
}