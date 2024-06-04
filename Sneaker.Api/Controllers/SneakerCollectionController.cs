using System.Net;
using System.Text;
using Microsoft.AspNetCore.Mvc;
using Sneaker.Domain.Excepetions;
using Sneaker.Service.DTOs.Requests;
using Sneaker.Service.DTOs.Responses;
using Sneaker.Service.Services;

namespace Sneaker.Api.Controllers;

[ApiController]
[Route("api/sneaker/")]
public class SneakerCollectionController : ControllerBase
{
    private readonly ISneakerCollectionService _sneakerCollectionService;

    public SneakerCollectionController(ISneakerCollectionService collectionService)
    {
        _sneakerCollectionService = collectionService;
    }
    
    [HttpGet("all")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<SneakerResponseDto>))]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<SneakerResponseDto>> GetSneakerCollection()
    {
        try
        {
            var sneakerCollection = await _sneakerCollectionService.GetSneakerCollection();
            return Ok(sneakerCollection);

        }
        catch (Exception exception)
        {
            return StatusCode(StatusCodes.Status500InternalServerError,exception.Message);
        }
    }

    [HttpGet("{id:guid}")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(SneakerResponseDto))]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<SneakerResponseDto>> GetSneakerById(Guid id)
    {
        var sneakerResponseDto = await _sneakerCollectionService.GetSneakerById(id);

        return sneakerResponseDto.StatusCode switch
        {
            (int)HttpStatusCode.NotFound => NotFound(sneakerResponseDto.ErrorMessage),
            (int)HttpStatusCode.InternalServerError => StatusCode(StatusCodes.Status500InternalServerError,sneakerResponseDto.ErrorMessage),
            _ => Ok(sneakerResponseDto)
        };
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status200OK, Type=typeof(SneakerResponseDto))]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<SneakerResponseDto>> AddNewSneaker([FromBody] SneakerRequestDto newSneaker)
    {
        try
        {
            var validationErrors = _sneakerCollectionService.ValidateSneakerInformation(newSneaker);
            if (!validationErrors.Any())
            {
                var sneakerAdded = await _sneakerCollectionService.AddNewSneaker(newSneaker);
                
                switch (sneakerAdded.StatusCode)
                {
                    case StatusCodes.Status500InternalServerError:
                        return StatusCode(StatusCodes.Status500InternalServerError,sneakerAdded.ErrorMessage);
                    case StatusCodes.Status200OK:
                        return Ok(sneakerAdded);
                }
            }

            return BadRequest(GetValidationErrorsMessage(validationErrors));
        }
        catch (Exception exception)
        {
            return StatusCode(StatusCodes.Status500InternalServerError,exception.Message);
        }
    }
    
    [HttpPut]
    [ProducesResponseType(StatusCodes.Status200OK, Type=typeof(SneakerResponseDto))]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<SneakerResponseDto>> UpdateSneaker([FromBody] SneakerRequestDto sneakerToUpdate)
    {
        try
        {
            var validationErrors = _sneakerCollectionService.ValidateSneakerInformation(sneakerToUpdate);
            if (!validationErrors.Any())
            {
                var sneakerUpdated = await _sneakerCollectionService.UpdateSneaker(sneakerToUpdate);
                
                switch (sneakerUpdated.StatusCode)
                {
                    case StatusCodes.Status500InternalServerError:
                        return StatusCode(StatusCodes.Status500InternalServerError,sneakerUpdated.ErrorMessage);
                    case StatusCodes.Status200OK:
                        return Ok(sneakerUpdated);
                }
            }

            return BadRequest(GetValidationErrorsMessage(validationErrors));
        }
        catch (Exception exception)
        {
            return StatusCode(StatusCodes.Status500InternalServerError,exception.Message);
        }
    }
    
    [HttpDelete("{id:guid}")]
    [ProducesResponseType(StatusCodes.Status200OK, Type=typeof(SneakerResponseDto))]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<SneakerResponseDto>> DeleteSneaker(Guid id)
    {
        try
        {
            if (id == Guid.Empty)
            {
                return BadRequest($"Invalid Id");
            }
           
            await _sneakerCollectionService.DeleteSneaker(id);
            return Ok();
        }
        catch (DeleteSneakerNotFoundException exception)
        {
            return StatusCode(StatusCodes.Status500InternalServerError,exception.Message);
        }
        catch (Exception exception)
        {
            return StatusCode(StatusCodes.Status500InternalServerError,exception.Message);
        }
    }

    private string GetValidationErrorsMessage(Dictionary<string,string> validationErrors)
    {
        var message = new StringBuilder("Error validations found in following fields:\n");
        
        foreach (var error in validationErrors)
        {
            message.Append($"{error.Key} - {error.Value}");
        }

        return message.ToString();
    }
}