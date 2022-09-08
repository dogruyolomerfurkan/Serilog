using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class SerilogController : ControllerBase
{
    [HttpPost]
    public IActionResult Post([FromBody]string name, string surname, string age)
    {
        return Ok();
    }
}
