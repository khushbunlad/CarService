using System;
using System.Collections.Generic;

namespace CarService.Models.Entities;

public partial class TblOrganizationMaster
{
    public int FldOrgId { get; set; }

    public string FldOrgName { get; set; } = null!;

    public string FldOrgEmail { get; set; } = null!;

    public string FldAddress { get; set; } = null!;

    public string FldContactPerson1 { get; set; } = null!;

    public string FldContactNumber1 { get; set; } = null!;

    public string FldContactPerson2 { get; set; } = null!;

    public string FldContactNumber2 { get; set; } = null!;

    public string FldLogo { get; set; } = null!;

    public bool FldIsFullSubscription { get; set; }

    public DateTime? FldActiveUntil { get; set; }

    public string FldLicenseNumber { get; set; } = null!;
}
