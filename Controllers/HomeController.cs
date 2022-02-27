using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace JWTToken_App.Controllers
{
    [Route("api/general")]
    [Authorize]
    public class HomeController : ControllerBase
    {
        public HomeController()
        {

        }
        [HttpGet]
        public IActionResult GetData()
        {
            return Ok(new List<int> { 1,2,3,4});
        }
    }
}
