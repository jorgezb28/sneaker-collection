using Sneaker.Domain.IRepository;
using Sneaker.Domain.Excepetions;

namespace Sneaker.Infrastructure.Repository;

public class SneakerRepositoryStatic:ISneakerRepository
{
    private List<Domain.Entities.Sneaker> _sneakers = new();

    public async Task<Domain.Entities.Sneaker> CreateSneaker(Domain.Entities.Sneaker newSneaker)
    {
        newSneaker.Id = Guid.NewGuid();
        _sneakers.Add(newSneaker);
        return newSneaker;
    }

    public async Task<Domain.Entities.Sneaker> GetSneakerById(Guid sneakerId)
    {
        var foundSneaker =  _sneakers.FirstOrDefault(x => x.Id == sneakerId);
        return foundSneaker;
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

        _sneakers.Remove(existingSneaker);
        _sneakers.Add(sneaker);
        return sneaker;
    }

    public async Task DeleteSneaker(Guid sneakerId)
    {
        var existingSneaker =  await GetSneakerById(sneakerId);
        if (existingSneaker == null)
        {
            throw new DeleteSneakerNotFoundException($"Snekaer {sneakerId} doesn't exist.Delete failed.");
        }

        _sneakers.Remove(existingSneaker);
    }

    public async Task<IEnumerable<Domain.Entities.Sneaker>> GetSneakerCollection()
    {
        return _sneakers;
    }
}