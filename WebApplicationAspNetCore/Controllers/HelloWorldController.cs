using Microsoft.AspNetCore.Mvc;

namespace WebApplicationAspNetCore.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HelloWorldController : ControllerBase
    {
        [HttpGet("{name}")]
        public IActionResult Get(string name)
        {
            return Ok($"Hello, {name}!");
        }
    }
}