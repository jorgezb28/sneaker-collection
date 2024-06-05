using AutoMapper;
using Sneaker.Domain.Entities;
using Sneaker.Service.DTOs.Auth;
using Sneaker.Service.DTOs.Requests;
using Sneaker.Service.DTOs.Responses;

namespace Sneaker.Service.Profiles;

public class SneakerCollectionProfile :Profile
{
    public SneakerCollectionProfile()
    {
        CreateMap<Domain.Entities.Sneaker, SneakerResponseDto>()
            .ForMember(dto => dto.Brand,
                dto => dto.MapFrom(e => e.Brand.Name))
            .ForMember(dto => dto.Size,
                dto => dto.MapFrom(e => e.Size.Value));

        CreateMap<SneakerRequestDto, Domain.Entities.Sneaker>()
            .ForMember(e => e.Brand, opt => opt.Ignore())
            .ForMember(e => e.Size, opt => opt.Ignore());
        
        CreateMap<UserDto, User>()
            .ForMember(e => e.Email,
                e => e.MapFrom(dto => dto.UserName))
            .ReverseMap();
    }
}