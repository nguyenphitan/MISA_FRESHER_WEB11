using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MISA.Fresher.Web11.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmptyController : ControllerBase
    {
        [HttpGet]
        public IActionResult Get()
        {
            return Ok("Empty!");
        }
    }
}
