using AirportFuelManagementWebAPI.Business;
using AirportFuelManagementWebAPI.DAL.Models;
using AirportFuelManagementWebAPI.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;


namespace AirportFuelManagementWebAPI.Controllers
{
    public class UserCredentials
    {
        public string UserEmail { get; set; }
        public string UserPassword { get; set; }
    }
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IBusiness business;
        private readonly ILogger<UserController> logger;

        public UserController(ILogger<UserController> logger, IBusiness business)
        {
            this.logger = logger;
            this.business = business;
        }
        [HttpPost]  
        public async Task<ActionResult> InsertUser(UserModel user)
        {

            bool flag = await business.InsertUser(user);
            if (flag)
            {
                return Ok(flag);
            }
            else
            {
                return NotFound();
             }
        }

        [HttpPost("CheckEmail")]
        public async Task<ActionResult> CheckEmail(string email)
        {
            bool flag = await business.CheckIfEmailAlreadyExists(email);
            if (flag)
            {
                return Ok(flag);
            }
            else
            {
                return NotFound();
            }
        }
    }
}
