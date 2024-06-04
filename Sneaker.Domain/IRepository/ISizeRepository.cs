using Sneaker.Domain.Entities;

namespace Sneaker.Domain.IRepository;

public interface ISizeRepository
{
     Task<Size> GetSizeByValue(int sneakerSize);

}