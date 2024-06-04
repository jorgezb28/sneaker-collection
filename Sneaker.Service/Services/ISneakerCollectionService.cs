using Sneaker.Service.DTOs;
using Sneaker.Service.DTOs.Requests;
using Sneaker.Service.DTOs.Responses;

namespace Sneaker.Service.Services;

public interface ISneakerCollectionService
{
    Task<SneakerResponseDto> GetSneakerById(Guid id);
    Task<IEnumerable<SneakerResponseDto>> GetSneakerCollection();
    Dictionary<string, string> ValidateSneakerInformation(SneakerRequestDto newSneaker);
    Task<SneakerResponseDto> AddNewSneaker(SneakerRequestDto newSneaker);
    Task<SneakerResponseDto> UpdateSneaker(SneakerRequestDto sneakerToUpdateDto);
    Task DeleteSneaker(Guid id);
}