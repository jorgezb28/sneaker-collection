using Sneaker.Domain.Enums;

namespace Sneaker.Service.DTOs.Responses;

public class SneakerResponseDto : BaseResponseDto
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Brand { get; set; }
    public decimal Price { get; set; }
    public int Size { get; set; }
    public int Year { get; set; }
    public Rates Rate { get; set; }
}