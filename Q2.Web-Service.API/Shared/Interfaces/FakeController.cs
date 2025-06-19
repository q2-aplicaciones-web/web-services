using System.Net.Mime;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace Q2.Web_Service.API.Shared.Interfaces;

[ApiController]
[Route("fake")]
[Produces(MediaTypeNames.Application.Json)]
[SwaggerTag("Available Project Endpoints")]
public class FakeController
{
    
    [HttpGet]
    public Task<IActionResult> GetFakeData()
    {
        // This is a placeholder for a fake endpoint.
        return Task.FromResult<IActionResult>(new OkObjectResult("This is a fake endpoint for testing purposes."));
    }
    
}