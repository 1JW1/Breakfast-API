using Breakfast.Contracts;
using Breakfast.Contracts.Breakfast;
using Breakfast.Models;
using Breakfast.ServiceErrors;
using Breakfast.Services.Breakfasts;
using ErrorOr;
using Microsoft.AspNetCore.Mvc;

namespace Breakfast.Controllers;

public class BreakfastsController : ApiController
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

        ErrorOr<Created> createBreakfastResult = _breakfastService.CreateBreakfast(breakfast);

        return createBreakfastResult.Match(
            created => CreatedAtGetBreakfast(breakfast),
            errors => Problem(errors));
    }

    [HttpGet("{id:guid}")]
    public IActionResult GetBreakfast(Guid id)
    {
        // calling service 
        ErrorOr<ABreakfast> getBreakfastResult = _breakfastService.GetBreakfast(id);

        return getBreakfastResult.Match(
            breakfast => Ok(MapBreakfastResponse(breakfast)),
            errors => Problem(errors)
        );

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
        ErrorOr<UpsertedBreakfast> upsertBreakfastResult = _breakfastService.UpsertBreakfast(breakfast);

        return upsertBreakfastResult.Match(
            upserted => upserted.isNewlyCreated ? CreatedAtGetBreakfast(breakfast) : NoContent(),
            errors => Problem(errors)
        );
    }

    [HttpDelete("{id:guid}")]
    public IActionResult DeleteBreakfast(Guid id)
    {
        ErrorOr<Deleted> deleteBreakfastResult = _breakfastService.DeleteBreakfast(id);
        
        return deleteBreakfastResult.Match(
            deleted => NoContent(),
            errors => Problem(errors)
        );
    }


    private static BreakfastResponse MapBreakfastResponse(ABreakfast aBreakfast)
    {
        return new BreakfastResponse(
            aBreakfast.Id,
            aBreakfast.Name,
            aBreakfast.Description,
            aBreakfast.StartDateTime,
            aBreakfast.EndDateTime,
            aBreakfast.LastModifiedDateTime,
            aBreakfast.Savoury,
            aBreakfast.Sweet
        );
    }
     private CreatedAtActionResult CreatedAtGetBreakfast(ABreakfast breakfast)
    {
        return CreatedAtAction(
            actionName: nameof(GetBreakfast),
            routeValues: new { id = breakfast.Id },
            value: MapBreakfastResponse(breakfast));
    }
}