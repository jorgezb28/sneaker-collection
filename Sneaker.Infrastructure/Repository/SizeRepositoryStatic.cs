using Sneaker.Domain.Entities;
using Sneaker.Domain.IRepository;

namespace Sneaker.Infrastructure.Repository;

public class SizeRepositoryStatic: ISizeRepository
{
    private List<Size> _sizes = new();
    
    public SizeRepositoryStatic()
    {
        _sizes = new List<Size>()
        { 
            new(){
                Id = Guid.NewGuid(),
                CountryCode = "US",
                Value = 7 
            }, 
            new(){
                Id = Guid.NewGuid(),
                CountryCode = "US",
                Value = 8 
            }
        };
    }
    
    public async Task<Size> GetSizeByValue(int sneakerSize)
    {
        return _sizes.FirstOrDefault(x => x.Value == sneakerSize);
    }
}