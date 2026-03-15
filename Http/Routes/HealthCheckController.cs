using Microsoft.AspNetCore.Mvc;

namespace scheduling_management.Http.Routes;

[ApiController]
[Route("api/[controller]")]
public class HealthCheckController : ControllerBase
{
    [HttpGet]
    public string Get()
    {
        return "ok";
    }
}