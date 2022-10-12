using Microsoft.AspNetCore.Mvc;
using NZWalks.API.Repositories.Interfaces;

namespace NZWalks.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AuthController : Controller
    {
        private readonly IUserRepository userRepository;
        private readonly ITokenHandler tokenHandler;

        public AuthController(IUserRepository userRepository, ITokenHandler tokenHandler)
        {
            this.userRepository = userRepository;
            this.tokenHandler = tokenHandler;
        }

        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> LoginAsync(Models.DTO.Requests.Login.LoginRequest loginRequest)
        {
            var user = await userRepository.AuthenticateAsync(loginRequest.Username, loginRequest.Password);
            if (user == null)
            {
                return BadRequest("Username or Password is incorrect");
            }

            var token = await tokenHandler.CreateTokenAsync(user);

            return Ok(token);
        }
    }
}
