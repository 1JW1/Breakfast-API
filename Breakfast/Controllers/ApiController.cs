using ErrorOr;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace Breakfast.Contracts;

// because its common for route to be same as class name without controller on the end
[ApiController]
[Route("[controller]")] 
public class ApiController : ControllerBase // own base controller implementation that other will controllers will inherit from
{
    protected IActionResult Problem(List<Error> errors)
    {   

        // if all we have is validation problems then we return that request with details on line 22
        // or take first erro (line 29) and send the appropiate response
        if (errors.All(e => e.Type == ErrorType.Validation))
        { 
            var modelStateDictionary = new ModelStateDictionary();

            foreach (var error in errors)
            {
              modelStateDictionary.AddModelError(error.Code, error.Description);
            }

            return ValidationProblem(modelStateDictionary);
        }

        // covers any error thats unexpected and not validation 
        if (errors.Any(e => e.Type == ErrorType.Unexpected))
        {
          return Problem();
        }

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