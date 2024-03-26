using AirportFuelManagementWebAPI.Business;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Text;

namespace AirportFuelManagementWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {

        private readonly IBusiness business;
        private readonly ILogger<LoginController> logger;
        private readonly IConfiguration config;


        public LoginController(ILogger<LoginController> logger, IBusiness business,IConfiguration configuration)
        {
            this.logger = logger;
            this.business = business;
            config = configuration;

        }
        [AllowAnonymous]
        [HttpPost("AuthenticateUser")]
        public async Task<IActionResult> AuthenticateUser([FromBody] UserCredentials credentials)
        {
            int userId = await business.CheckIfUserExists(credentials.UserEmail, credentials.UserPassword);
            IActionResult response = Unauthorized();
            if(userId != -1)
            {
                var token = GenerateToken(credentials);
                response = Ok(new {token = token});
            }
            return response;
        }
       
        private string GenerateToken(UserCredentials userCredentials)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config["Jwt:Key"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(config["Jwt:Issuer"], config["Jwt:Audience"],null,expires: DateTime.Now.AddMinutes(1),signingCredentials:credentials);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

       

    }
}
