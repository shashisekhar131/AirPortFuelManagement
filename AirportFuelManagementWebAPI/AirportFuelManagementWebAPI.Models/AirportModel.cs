using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AirportFuelManagementWebAPI.Models
{
    public class AirportModel
    {
        public int AirportId { get; set; }

        public string AirportName { get; set; } = null!;

        public decimal? FuelCapacity { get; set; }

        public decimal? FuelAvailable { get; set; }
    }
}
