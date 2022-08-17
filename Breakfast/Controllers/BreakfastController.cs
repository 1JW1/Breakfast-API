using Breakfast.Contracts.Breakfast;
using Breakfast.Models;
using Breakfast.ServiceErrors;
using Breakfast.Services.Breakfasts;
using ErrorOr;
using Microsoft.AspNetCore.Mvc;

namespace Breakfast.Controllers;

[ApiController]
[Route("[controller]")] // because its common for route to be same as class name without controller on the end
public class BreakfastsController : ControllerBase
{
    private readonly IBreakfastService _breakfastService;
    
    public BreakfastsController(IBreakfastService breakfastService)
    {
        _breakfastService = breakfastService;
    }

    [HttpPost]
    public IActionResult CreateBreakfast(CreateBreakfastRequest request)
    {   
        // mapping data we get from request to the language our app speaks
        var breakfast = new ABreakfast(
            Guid.NewGuid(),
            request.Name,
            request.Description,
            request.StartDateTime,
            request.LastDateTime,
            DateTime.UtcNow,
            request.Savoury,
            request.Sweet
        );

        // TODO: save breakfast to db
        _breakfastService.CreateBreakfast(breakfast);

        // taking data from our system and converting it back to api definition 
        var response = new BreakfastResponse(
            breakfast.Id,
            breakfast.Name,
            breakfast.Description,
            breakfast.StartDateTime,
            breakfast.EndDateTime,
            breakfast.LastModifiedDateTime,
            breakfast.Savoury,
            breakfast.Sweet
        );

        return CreatedAtAction(
            actionName: nameof(GetBreakfast),
            routeValues: new { id = breakfast.Id },
            value: response);
    }

    [HttpGet("{id:guid}")]
    public IActionResult GetBreakfast(Guid id)
    {   
        // getting the breakfast
        ErrorOr<ABreakfast> getBreakfastResult = _breakfastService.GetBreakfast(id);

        if (getBreakfastResult.IsError && getBreakfastResult.FirstError == Errors.Breakfast.NotFound)
        {
            return NotFound();
        }

        var aBreakfast = getBreakfastResult.Value;

        // mapping the data to an object that can pass through the API 
        var response = new BreakfastResponse(
            aBreakfast.Id,
            aBreakfast.Name,
            aBreakfast.Description,
            aBreakfast.StartDateTime,
            aBreakfast.EndDateTime,
            aBreakfast.LastModifiedDateTime,
            aBreakfast.Savoury,
            aBreakfast.Sweet
        );

        return Ok(response);
    }

    [HttpPut("{id:guid}")]
    public IActionResult UpsertBreakfast(Guid id, UpsertBreakfastRequest request)
    {   
        var breakfast = new ABreakfast(
            id,
            request.Name,
            request.Description,
            request.StartDateTime,
            request.LastDateTime,
            DateTime.UtcNow,
            request.Savoury,
            request.Sweet
        );
        
        // todo: return 201 if breakfast was created
        _breakfastService.UpsertBreakfast(breakfast);

        return NoContent();
    }

    [HttpDelete("{id:guid}")]
    public IActionResult DeleteBreakfast(Guid id)
    {
        _breakfastService.DeleteBreakfast(id);
        return NoContent();
    }
}