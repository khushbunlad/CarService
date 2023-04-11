using System;
using System.Collections.Generic;

namespace CarService.Models.Entities;

public partial class TblJobMaster
{
    public long FldJobId { get; set; }

    public long FldOrgId { get; set; }

    public string FldJobNo { get; set; } = null!;

    public string FldVehicleRegisteredNumber { get; set; } = null!;

    public string? FldChessisNumber { get; set; }

    public string? FldEngineNumber { get; set; }

    public string? FldModelNameNumber { get; set; }

    public string FldServiceType { get; set; } = null!;

    public DateTime FldRegisteredOn { get; set; }

    public decimal FldKmReadingOnRegistration { get; set; }

    public string FldCustomerName { get; set; } = null!;

    public string FldCustomerContact1 { get; set; } = null!;

    public string FldCustomerContact2 { get; set; } = null!;

    public DateTime? FldHandedOverOn { get; set; }
}
