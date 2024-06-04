
using Sneaker.Domain.Entities;

namespace Sneaker.Domain.IRepository;

public interface ISneakerRepository
{
    Task<Entities.Sneaker> CreateSneaker(Entities.Sneaker sneaker);
    Task<Entities.Sneaker?> GetSneakerById(Guid sneakerId);
    Task<IEnumerable<Entities.Sneaker>> GetSneakerFiltered(Guid sneakerId); // TODO: add filter model
    Task<Entities.Sneaker> UpdateSneaker(Entities.Sneaker sneaker);
    Task DeleteSneaker(Guid sneakerId);

    Task<IEnumerable<Entities.Sneaker>> GetSneakerCollection();
}