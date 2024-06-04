using System.ComponentModel.DataAnnotations;
using Sneaker.Domain.Enums;

namespace Sneaker.Service.DTOs.Requests;

public class SneakerRequestDto
{
    public Guid Id { get; set; }
    [Required]
    public string Name { get; set; }
    [Required]
    public string Brand { get; set; }
    [Required]
    public decimal Price { get; set; }
    [Required]
    public int Size { get; set; }
    [Required]
    public int Year { get; set; }
    public Rates Rate { get; set; }
}