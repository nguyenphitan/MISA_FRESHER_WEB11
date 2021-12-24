using Microsoft.AspNetCore.Mvc;

namespace MISA.Fresher.Web11.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TanController : ControllerBase
    {
        [HttpGet]
        public IActionResult Get()
        {
            return Ok("Tan!");
        }
    }
}
