using Shared.Models;
using Api.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Net.Http.Headers;
using System.Text.Json.Nodes;
using System.Text.Json;

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
        public async Task<IActionResult> Login(string username, string githubToken)
        {
            IActionResult response = Unauthorized("Missing or invalid github token");
            var user = await AuthenticateUser(username, githubToken);

            if (user != null)
            {
                var tokenString = GenerateJSONWebToken(user);

                response = Ok(new
                {
                    userId = user.UserId,
                    username = user.Username,
                    token = tokenString
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
                new Claim("username", user.Username),
                new Claim(ClaimTypes.NameIdentifier, user.UserId.ToString())
            };

            var token = new JwtSecurityToken(_config["Jwt:Issuer"],
              _config["Jwt:Issuer"],
              claims,
              expires: DateTime.Now.AddMinutes(120),
              signingCredentials: credentials);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        private async Task<User> AuthenticateUser(string username, string githubToken)
        {
            string githubUserLink = "https://api.github.com/user";
            var client = new HttpClient();
            var request = new HttpRequestMessage(HttpMethod.Get, githubUserLink);
            request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", githubToken);
            request.Headers.UserAgent.ParseAdd("Bookmarker API");

            HttpResponseMessage response = await client.SendAsync(request);


            if (!response.IsSuccessStatusCode)
            {
                return null;
            }

            var body = await response.Content.ReadAsStringAsync();

            JsonObject userInfo = JsonSerializer.Deserialize<JsonObject>(body);
            string githubUsername = userInfo["login"].ToString();

            if (username != githubUsername)
                return null;

            User? user = _userRepository.AddOrGetUser(username);
            return user;
        }
    }
}