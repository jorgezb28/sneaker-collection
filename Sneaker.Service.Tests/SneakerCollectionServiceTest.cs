using AutoFixture;
using AutoMapper;
using FluentAssertions;
using Moq;
using Sneaker.Domain.DomainServices;
using Sneaker.Domain.IRepository;
using Sneaker.Service.Profiles;
using Sneaker.Service.Services.Implementations;

namespace Sneaker.Service.Tests;

public class SneakerCollectionServiceTest
{
    private readonly Mock<ISneakerRepository> _sneakerRepositoryMock;
    private readonly Mock<IBrandRepository> _brandRepositoryMock;
    private readonly Mock<ISizeRepository> _sizeRepositoryMock;
    private readonly Mock<ISneakerDomainService> _sneakerDomainServiceMock;
    private readonly IMapper _mapper;
    private readonly Fixture _fixture;
    private readonly SneakerCollectionService _sneakerCollectionService;

    public SneakerCollectionServiceTest()
    {
        _sneakerRepositoryMock = new Mock<ISneakerRepository>();
        _brandRepositoryMock = new Mock<IBrandRepository>();
        _sizeRepositoryMock = new Mock<ISizeRepository>();
        _sneakerDomainServiceMock = new Mock<ISneakerDomainService>();

        var config = new MapperConfiguration(cfg =>
        {                
            cfg.AddProfile(new SneakerCollectionProfile());
        });
        _mapper = config.CreateMapper();
        
        _sneakerCollectionService = new SneakerCollectionService(_sneakerRepositoryMock.Object, _mapper, _brandRepositoryMock.Object,
            _sizeRepositoryMock.Object, _sneakerDomainServiceMock.Object);
        _fixture = new Fixture();
    }

    [Fact]
    public async Task GetSneakerCollection_ShouldSuccess()
    {
        //Arrange 
        var allSneakers = _fixture.CreateMany<Domain.Entities.Sneaker>(3);
        _sneakerRepositoryMock.Setup(x => x.GetSneakerCollection()).ReturnsAsync(allSneakers);
        
        //Act
        var result = await _sneakerCollectionService.GetSneakerCollection();
        
        //Assert
        result.Should().NotBeEmpty();
        result.Should().BeEquivalentTo(allSneakers, config=> config
            .Excluding(ctx=>ctx.Brand)
            .Excluding(ctx=>ctx.Size));
    }
    
    
}