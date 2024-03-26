using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AirportFuelManagementWebAPI.Models
{
    public class AirportTransactionInfo
    {
        public string AirportName { get; set; }
        public decimal? AirportFuelAvailable { get; set; }
        public List<TransactionItem> Transactions { get; set; }
    }  
    public class TransactionItem
    {
        public DateTime TransactionTime { get; set; }

        public int TransactionType { get; set; }

        public decimal Quantity { get; set; }
        public string AircraftName { get; set; }
    }

}
