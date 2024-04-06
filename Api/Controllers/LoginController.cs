using Api.Interfaces;
using Api.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly IConfiguration _config;
        private readonly IUserRepository _userRepository;

        public LoginController(IConfiguration config, IUserRepository userRepository)
        {
            _config = config;
            _userRepository = userRepository;
        }

        [HttpPost]
        [AllowAnonymous]
        public IActionResult Login(string username, string githubToken)
        {
            IActionResult response = Unauthorized();
            var user = AuthenticateUser(username, githubToken);

            if (user != null)
            {
                var tokenString = GenerateJSONWebToken(user);

                response = Ok(new
                {
                    userId = user.UserId,
                    username = user.Username,
                    token = tokenString,
                    secret = _config["Jwt:Key"]
                });
            }

            return response;
        }

        private string GenerateJSONWebToken(User user)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
            var claims = new[]
            {
                new Claim("username", user.Username)
            };

            var token = new JwtSecurityToken(_config["Jwt:Issuer"],
              _config["Jwt:Issuer"],
              claims,
              expires: DateTime.Now.AddMinutes(120),
              signingCredentials: credentials);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        private User? AuthenticateUser(string username, string githubToken)
        {
            // Validate user's github token to authenticate user
            User? user = _userRepository.AddOrGetUser(new() { Username = username }); ;
            return user;
        }
    }
}