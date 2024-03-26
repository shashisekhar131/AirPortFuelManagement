using System;
using System.Collections.Generic;

namespace AirportFuelManagementWebAPI.DAL.Models;

public partial class FuelTransaction
{
    public int TransactionId { get; set; }

    public DateTime TransactionTime { get; set; }

    public int TransactionType { get; set; }

    public decimal Quantity { get; set; }

    public int? TransactionIdparent { get; set; }

    public int AirportId { get; set; }

    public int AircraftId { get; set; }

    public virtual Aircraft Aircraft { get; set; } = null!;

    public virtual Airport Airport { get; set; } = null!;
}
