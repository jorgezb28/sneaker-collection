using Sneaker.Domain.Entities;

namespace Sneaker.Domain.IRepository;

public interface IUserRepository
{
    Task Register(User user);
    Task<User> FindUserByUserName(string userName);
}