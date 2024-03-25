using Microsoft.AspNetCore.Mvc;

namespace Icon3DPack.API.Host.Controllers
{
    public class HealthController : ApiController
    {
        public IActionResult Health()
        {
            return Ok(new { messsage = "ok" });
        }
    }
}
