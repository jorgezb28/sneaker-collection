using Microsoft.EntityFrameworkCore;
using Sneaker.Domain.Entities;
using Sneaker.Domain.IRepository;
using Sneaker.Infrastructure.Context;

namespace Sneaker.Infrastructure.Repository;

public class UserRepository: RepositoryBase<User>, IUserRepository
{
    public UserRepository(SneakerContext context) : base(context)
    {
    }

    public async Task Register(User user)
    {
        Context.Users.Add(user);
        await Context.SaveChangesAsync();
    }

    public async Task<User> FindUserByUserName(string userName)
    {
        return await Context.Users.FirstOrDefaultAsync(x => x.Email == userName);
    }
}