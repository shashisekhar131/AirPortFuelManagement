using AirportFuelManagementWebAPI.Business;
using AirportFuelManagementWebAPI.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AirportFuelManagementWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AircraftController : ControllerBase
    {
        private readonly IBusiness business;

        public AircraftController(IBusiness business)
        {
            this.business = business;
        }
        [Authorize]
        [HttpGet]
        public async Task<ActionResult<List<AircraftModel>>> GetAllAircrafts()
        {
           
            List<AircraftModel> aircrafts = await business.GetAllAircrafts();
            if(aircrafts.Any())
            {
                return Ok(aircrafts);
            }
            return NotFound();            
        }
        [Authorize]
        [HttpGet("{id}")]
        public async Task<ActionResult<AircraftModel>> GetAircraftById(int id)
        {
            
            AircraftModel aircraft = await business.GetAircraftById(id);
            if (aircraft == null)
            return NotFound();

            return Ok(aircraft);
            
        }
        [Authorize]
        [HttpPost("InsertAircraft")]
        public async Task<ActionResult> InsertAircraft(AircraftModel aircraft)
         {
            
            bool flag = await business.InsertAircraft(aircraft);
            if (flag)
                return Ok(aircraft);
            else
                return BadRequest(new { Message = "Failed to insert aircraft" });
           
        }
        [Authorize]
        [HttpPut("UpdateAircraft")]
        public async Task<ActionResult> UpdateAircraft(AircraftModel aircraft)
        {
            
            bool flag = await business.UpdateAircraft(aircraft);
            if (flag)
                return Ok(aircraft);
            else
                return BadRequest(new { Message = "Failed to update aircraft" });

        }
    }
}
