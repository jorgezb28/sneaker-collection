using Sneaker.Domain.Enums;

namespace Sneaker.Domain.Entities;

public class Sneaker
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public Brand Brand { get; set; }
    public decimal Price { get; set; }
    public Size Size { get; set; }
    public int Year { get; set; }
    public Rates Rate { get; set; }
}