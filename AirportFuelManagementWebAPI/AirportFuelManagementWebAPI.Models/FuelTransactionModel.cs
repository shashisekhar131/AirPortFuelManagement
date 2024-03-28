using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AirportFuelManagementWebAPI.Models
{
    public class FuelTransactionModel
    {
        public int TransactionId { get; set; }

        public DateTime TransactionTime { get; set; }

        public int TransactionType { get; set; }

        public decimal Quantity { get; set; }

        public int? TransactionIdparent { get; set; }

        public int AirportId { get; set; }

        public int? AircraftId { get; set; }
        public string? AirportName { get; set; }

        public string? AircraftName { get; set; }

    }
}
