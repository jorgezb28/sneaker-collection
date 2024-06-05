using Microsoft.EntityFrameworkCore;
using Sneaker.Domain.Excepetions;
using Sneaker.Domain.IRepository;
using Sneaker.Infrastructure.Context;

namespace Sneaker.Infrastructure.Repository;

public class SneakerRepository : RepositoryBase<Domain.Entities.Sneaker>, ISneakerRepository
{
    public SneakerRepository(SneakerContext context) : base(context)
    {
    }

    public async Task<Domain.Entities.Sneaker> CreateSneaker(Domain.Entities.Sneaker sneaker)
    {
        Context.Sneakers.Add(sneaker);
        await Context.SaveChangesAsync();
        return sneaker;
    }

    public Task<Domain.Entities.Sneaker?> GetSneakerById(Guid sneakerId)
    {
        return Context.Sneakers.FirstOrDefaultAsync(x => x.Id == sneakerId);
    }

    public Task<IEnumerable<Domain.Entities.Sneaker>> GetSneakerFiltered(Guid sneakerId)
    {
        throw new NotImplementedException();
    }

    public async Task<Domain.Entities.Sneaker> UpdateSneaker(Domain.Entities.Sneaker sneaker)
    {
        var existingSneaker =  await GetSneakerById(sneaker.Id);
        if (existingSneaker == null)
        {
            throw new UpdateSneakerNotFoundException($"Snekaer {sneaker.Id} doesn't exist.Update failed.");
        }

        Context.Sneakers.Remove(existingSneaker);
        Context.Sneakers.Add(sneaker);
        await Context.SaveChangesAsync();
        return sneaker;
    }

    public async Task DeleteSneaker(Guid sneakerId)
    {
        var existingSneaker =  await GetSneakerById(sneakerId);
        if (existingSneaker == null)
        {
            throw new DeleteSneakerNotFoundException($"Snekaer {sneakerId} doesn't exist.Delete failed.");
        }

        Context.Sneakers.Remove(existingSneaker);
        await Context.SaveChangesAsync();
    }

    public async Task<IEnumerable<Domain.Entities.Sneaker>> GetSneakerCollection()
    {
        return await Context.Sneakers.ToListAsync();
    }
}