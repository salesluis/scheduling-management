using Microsoft.AspNetCore.Mvc;
using scheduling_management.Aplication.Services.Token;

namespace scheduling_management.Controllers;

[ApiController]
[Route("v1/[controller]")]
public class AcountController(TokenService tokenService)
    : ControllerBase
{
    [HttpPost("login")]
    public ActionResult Login()
    {
        try
        {
            var token = tokenService.Generate(null);
            return Ok(token);
        }
        catch (Exception e)
        {
            Console.WriteLine(e.StackTrace);
            return BadRequest(e.Message);
            throw;
        }
    }
}