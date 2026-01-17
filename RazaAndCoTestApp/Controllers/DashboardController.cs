using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace RazaAndCoTestApp.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class DashboardController : ControllerBase
    {
        [HttpGet]
        public IActionResult Get()
        {
            var role = User.FindFirst(ClaimTypes.Role)?.Value;
            return Ok(role == "Admin" ? "Admin Dashboard" : "User Dashboard");
        }
    }

}
