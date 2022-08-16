using Microsoft.AspNetCore.Mvc;

namespace Breakfast.Controllers;

public class ErrorsController : ControllerBase
{
    [Route("/error")]
    public IActionResult Error()
    {
        // any error handling methods go here
        // problem method is from controller base, and returns 500
        return Problem();
    }
}