using AutoFixture;
using FluentAssertions;
using Sneaker.Domain.DomainServices.Implementations;

namespace Sneaker.Domain.Tests;

public class SneakerDomainServiceTest
{
    private readonly SneakerDomainService _sneakerDomainService = new();
    private readonly Fixture _fixture = new();

    [Fact]
    public void ValidateSneakerSize_ShouldReturnTrue_WithValidData()
    {
        //Arrange
        var sneaker = _fixture.Create<Entities.Sneaker>();
        sneaker.Size.Value = 8;
        //Act
        var result = _sneakerDomainService.ValidateSneakerSize(sneaker);

        //Assert
        result.Should().BeTrue();
    }
    
    [Fact]
    public void ValidateSneakerSize_ShouldReturnFalse_WithInValidData()
    {
        //Arrange
        var sneaker = _fixture.Create<Entities.Sneaker>();
        sneaker.Size.Value = 50;
        //Act
        var result = _sneakerDomainService.ValidateSneakerSize(sneaker);

        //Assert
        result.Should().BeFalse();
    }
}