namespace Sneaker.Service.DTOs.Responses;

public class BaseResponseDto
{
    public int StatusCode { get; set; }
    public string ErrorMessage { get; set; } // This could be a list of errors if needed
}