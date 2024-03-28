using AirportFuelManagementWebAPI.Business;
using AirportFuelManagementWebAPI.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AirportFuelManagementWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TransactionController : ControllerBase
    {
        private readonly IBusiness business;

        public TransactionController( IBusiness business)
        {
            this.business = business;
        }
        [Authorize]
        [HttpGet]
        public async Task<ActionResult<List<FuelTransactionModel>>> GetAllTransactions()
        {

            List<FuelTransactionModel> transactions = await business.GetAllTransactions();
            if (transactions.Any())
            {
                return Ok(transactions);
            }
            return NotFound();
        }
        [Authorize]
        [HttpGet("GetTransactionById/{id}")]
        public async Task<ActionResult> GetTransactionById(int id)
        {
            FuelTransactionModel transaction = await business.GetTransactionById(id);
            if(transaction == null)
            {
                return NotFound();
            }
            return Ok(transaction);
        }
        [Authorize]
        [HttpGet("FuelConsumptionReport")]
        public async Task<ActionResult> FuelConsumptionReport()
        {

            List<AirportTransactionInfo> transactions = await business.FuelConsumptionReport();
            if (transactions.Any())
            {
                return Ok(transactions);
            }
            else
            {
                return NotFound();
            }
        }
        [Authorize]
        [HttpPost("InsertTransaction")]
        public async Task<ActionResult> InsertTransaction(FuelTransactionModel Transaction)
        {

            Tuple<string, bool> result = await business.InsertTransaction(Transaction);
            return Ok(result);
        }
        [Authorize]
        [HttpDelete("RemoveAllTransactions")]
        public async Task<ActionResult> RemoveAllTransactions()
        {
             bool flag = await business.RemoveAllTransactions();
            if (flag)
                return Ok(flag);
            else
                return BadRequest(new { Message = "Failed to remove transactions" });
        }
    }
}
