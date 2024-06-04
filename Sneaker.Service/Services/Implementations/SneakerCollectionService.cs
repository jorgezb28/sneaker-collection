using System.Net;
using AutoMapper;
using Sneaker.Domain.IRepository;
using Sneaker.Service.DTOs.Requests;
using Sneaker.Service.DTOs.Responses;

namespace Sneaker.Service.Services.Implementations;

public class SneakerCollectionService : ISneakerCollectionService
{
    private readonly ISneakerRepository _sneakerRepository;
    private readonly IBrandRepository _brandRepository;
    private readonly ISizeRepository _sizeRepository;
    private readonly IMapper _mapper;

    public SneakerCollectionService(ISneakerRepository sneakerRepository, IMapper mapper, 
        IBrandRepository brandRepository, ISizeRepository sizeRepository)
    {
        _sneakerRepository = sneakerRepository;
        _mapper = mapper;
        _brandRepository = brandRepository;
        _sizeRepository = sizeRepository;
    }
    public async Task<SneakerResponseDto> GetSneakerById(Guid id)
    {
        try
        {
            var sneaker = await _sneakerRepository.GetSneakerById(id);
            if (sneaker == null)
            {
                return new SneakerResponseDto()
                {
                    StatusCode = (int)HttpStatusCode.NotFound,
                    ErrorMessage = $"Sneaker with Id {id} not found."
                };
            }

            var sneakerDto = _mapper.Map<SneakerResponseDto>(sneaker);
            sneakerDto.StatusCode = (int)HttpStatusCode.OK;

            return sneakerDto;

        }
        catch (Exception exception)
        {
            return new SneakerResponseDto()
            {
                StatusCode = (int)HttpStatusCode.InternalServerError,
                ErrorMessage = exception.Message
            };
        }
    }

    public async Task<IEnumerable<SneakerResponseDto>> GetSneakerCollection()
    {
        var allSneakers = await _sneakerRepository.GetSneakerCollection();
        return _mapper.Map<IEnumerable<SneakerResponseDto>>(allSneakers);
    }

    public Dictionary<string,string> ValidateSneakerInformation(SneakerRequestDto newSneaker)
    {
        var validationErrors = new Dictionary<string, string>();
        
        var existingBrand = _brandRepository.FindBrandByName(newSneaker.Brand).Result;
        if (existingBrand == null)
        {
            validationErrors.Add(nameof(newSneaker.Brand),"Invalid Brand");
        }

        if (newSneaker.Price <= 0)
        {
            validationErrors.Add(nameof(newSneaker.Price),"Price must be greater than zero.");
        }

        var existingSize = _sizeRepository.GetSizeByValue(newSneaker.Size).Result;
        if (existingSize == null)
        {
            validationErrors.Add(nameof(newSneaker.Size),"Invalid Size");
        }
        
        if (newSneaker.Year <= 1980)
        {
            validationErrors.Add(nameof(newSneaker.Year),"Year must be greater than 1980.");
        }
        
        return validationErrors;
    }

    public async Task<SneakerResponseDto> AddNewSneaker(SneakerRequestDto newSneaker)
    {
        try
        {
            var sneakerToAdd = _mapper.Map<Domain.Entities.Sneaker>(newSneaker);
        
            sneakerToAdd.Brand =  await _brandRepository.FindBrandByName(newSneaker.Brand);
            sneakerToAdd.Size = await _sizeRepository.GetSizeByValue(newSneaker.Size);

            var sneakerAdded = await _sneakerRepository.CreateSneaker(sneakerToAdd);
            var sneakerAddedDto =_mapper.Map<SneakerResponseDto>(sneakerAdded);
            sneakerAddedDto.StatusCode = (int)HttpStatusCode.OK;
            
            return sneakerAddedDto;
        }
        catch (Exception exception)
        {
            return new SneakerResponseDto()
            {
                StatusCode = (int)HttpStatusCode.InternalServerError,
                ErrorMessage = exception.Message
            };
        }
    }

    public async Task<SneakerResponseDto> UpdateSneaker(SneakerRequestDto sneakerToUpdateDto)
    {
        try
        {
            var sneakerToUpdate = _mapper.Map<Domain.Entities.Sneaker>(sneakerToUpdateDto);
        
            sneakerToUpdate.Brand =  await _brandRepository.FindBrandByName(sneakerToUpdateDto.Brand);
            sneakerToUpdate.Size = await _sizeRepository.GetSizeByValue(sneakerToUpdateDto.Size);

            var sneakerUpdated = await _sneakerRepository.UpdateSneaker(sneakerToUpdate);
            var sneakerUpdatedDto =_mapper.Map<SneakerResponseDto>(sneakerUpdated);
            sneakerUpdatedDto.StatusCode = (int)HttpStatusCode.OK;
            
            return sneakerUpdatedDto;
        }
        catch (Exception exception)
        {
            return new SneakerResponseDto()
            {
                StatusCode = (int)HttpStatusCode.InternalServerError,
                ErrorMessage = exception.Message
            };
        }
    }

    public Task DeleteSneaker(Guid id)
    {
        return _sneakerRepository.DeleteSneaker(id);
    }
}