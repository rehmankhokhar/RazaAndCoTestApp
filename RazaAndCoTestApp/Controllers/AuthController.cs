using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RazaAndCoTestApp.Data;
using RazaAndCoTestApp.Services;
using RazaCoTestApp.DTOs;

namespace RazaAndCoTestApp.Controllers
{
    [ApiController]
    [Route("api/auth")]
    public class AuthController : ControllerBase
    {
        private readonly IJwtService _jwtService;
        private readonly AppDbContext _context;
        public AuthController(IJwtService jwtService, AppDbContext context)
        {
            _jwtService = jwtService;
            _context = context;
        }
        [HttpPost("login")]
        public IActionResult Login(LoginRequestDto request)
        {
            var user = _context.Users
         .FirstOrDefault(x => x.Username == request.Username);

            if (user == null ||
                !BCrypt.Net.BCrypt.Verify(request.Password, user.PasswordHash))
            {
                return Unauthorized("Invalid username or password");
            }

            var token = _jwtService.GenerateToken(user.Username, user.Role);

            return Ok(new
            {
                token,
                username = user.Username,
                role = user.Role
            });
        }
    }

}
