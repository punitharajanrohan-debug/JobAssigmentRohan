using AssignmentRohanback.Dto;
using AssignmentRohanback.Service.AuthService;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AssignmentRohanback.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost("login")]
        public async Task<LoginResponse> Login(LoginRequest request)
        {
            Console.WriteLine(System.Text.Json.JsonSerializer.Serialize(request));
            LoginResponse response = await _authService.loginasync(request);
            return response;
        }
    }
}

