using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AirportFuelManagementWebAPI.Models
{
    public class AircraftModel
    {
        public int AircraftId { get; set; }

        public string AircraftNumber { get; set; } = null!;

        public string? AirLine { get; set; }

        public string? Source { get; set; }

        public string? Destination { get; set; }
        public string? SourceName { get; set; }
        public string? DestinationName { get; set; }

    }
}
