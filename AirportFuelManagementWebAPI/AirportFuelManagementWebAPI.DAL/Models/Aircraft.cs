using System;
using System.Collections.Generic;

namespace AirportFuelManagementWebAPI.DAL.Models;

public partial class Aircraft
{
    public int AircraftId { get; set; }

    public string AircraftNumber { get; set; } = null!;

    public string? AirLine { get; set; }

    public string? Source { get; set; }

    public string? Destination { get; set; }

    public virtual ICollection<FuelTransaction> FuelTransactions { get; set; } = new List<FuelTransaction>();
}
