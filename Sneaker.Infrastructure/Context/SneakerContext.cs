using Microsoft.EntityFrameworkCore;
using Sneaker.Domain.Entities;

namespace Sneaker.Infrastructure.Context;

public class SneakerContext : DbContext
{
    public SneakerContext(DbContextOptions<SneakerContext> options) :base(options)
    { }

    public DbSet<Domain.Entities.Sneaker> Sneakers { get; set; }
    public DbSet<Size> Sizes { get; set; }
    public DbSet<Brand> Brands { get; set; }
    public DbSet<User> Users { get; set; }
}