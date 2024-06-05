using Sneaker.Domain.Entities;
using Sneaker.Domain.IRepository;

namespace Sneaker.Infrastructure.Repository;

public class UserRepositoryStatic: IUserRepository
{
    private List<User> _users = new();

    public async Task Register(User user)
    {
        user.UserId = Guid.NewGuid();
        _users.Add(user);
    }

    public async Task<User> FindUserByUserName(string userName)
    {
        return _users.FirstOrDefault(x => x.Email == userName);
    }
}