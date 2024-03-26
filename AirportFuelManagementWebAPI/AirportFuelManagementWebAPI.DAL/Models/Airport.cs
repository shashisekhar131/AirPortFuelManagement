using System;
using System.Collections.Generic;

namespace AirportFuelManagementWebAPI.DAL.Models;

public partial class Airport
{
    public int AirportId { get; set; }

    public string AirportName { get; set; } = null!;

    public decimal? FuelCapacity { get; set; }

    public decimal? FuelAvailable { get; set; }

    public virtual ICollection<FuelTransaction> FuelTransactions { get; set; } = new List<FuelTransaction>();
}
