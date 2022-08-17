using ErrorOr;
using Microsoft.AspNetCore.Mvc;

namespace Breakfast.Contracts;

// because its common for route to be same as class name without controller on the end
[ApiController]
[Route("[controller]")] 
public class ApiController : ControllerBase // own base controller implementation that other will controllers will inherit from
{
    protected IActionResult Problem(List<Error> errors)
    {
        var firstError = errors[0];

        var statusCode = firstError.Type switch // Type computes correct status code
        {
            ErrorType.NotFound => StatusCodes.Status404NotFound,
            ErrorType.Validation => StatusCodes.Status400BadRequest,
            ErrorType.Conflict => StatusCodes.Status409Conflict,
            _ => StatusCodes.Status500InternalServerError
        };

        // using problem method from controller base to return status code and descr.. of first error
        return Problem(statusCode: statusCode, title: firstError.Description);
    }
}