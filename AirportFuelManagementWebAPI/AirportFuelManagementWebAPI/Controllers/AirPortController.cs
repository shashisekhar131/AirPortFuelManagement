using AirportFuelManagementWebAPI.Business;
using AirportFuelManagementWebAPI.DAL.Models;
using AirportFuelManagementWebAPI.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AirportFuelManagementWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AirPortController : ControllerBase
    {

        private IBusiness business;

        public AirPortController( IBusiness business)
        {
            this.business = business;
        }
        [Authorize]
        [HttpGet]
        public async Task<ActionResult> GetAllAirports()
        {
       
            List<AirportModel> airports = await business.GetAllAirports();  
            if(airports.Count == 0)
            {
                return NotFound();
            }
            return Ok(airports);
        }
        [Authorize]
        [HttpGet("GetAiportSummary")]
        public async Task<ActionResult> GetAirportSummary()
        {

            List<AirportModel> airports = await business.GetAllAirports();
            if (airports.Count == 0)
            {
                return NotFound();
            }
            return Ok(airports);
        }
        [Authorize]
        [HttpGet("{id}")]
        public async Task<ActionResult> GetAirportById(int id)
        {

            AirportModel airport = await business.GetAirportById(id);
            if (airport == null)
            {
                return NotFound();
            }
            return Ok(airport);
        }
        [Authorize]
        [HttpPost("InsertAirport")]
        public async Task<ActionResult> InsertAirport(AirportModel airport)
        {

            bool flag = await business.InsertAirport(airport);
            if(flag)
            {
                return Ok(flag);
            }
            else
            {
                return BadRequest(new { Message = "Failed to insert airport" });
            }
        }
        [Authorize]
        [HttpPut("UpdateAirport")]
        public async Task<ActionResult> UpdateAirport(AirportModel airport)
        {

            bool flag = await business.UpdateAirport(airport);
            if (flag)
            {
                return Ok(flag);
            }
            else
            {
                return BadRequest(new { Message = "Failed to update airport" });
            }
        }

    }

   
}
