using System;
using System.Collections.Generic;

namespace CarService.Models.Entities;

public partial class TblSystemUser
{
    public long FldUserId { get; set; }

    public long FldOrgId { get; set; }

    public string FldFullName { get; set; } = null!;

    public string FldEmailId { get; set; } = null!;

    public string FldMobileNumber { get; set; } = null!;

    public string FldPassword { get; set; } = null!;

    public bool FldIsActive { get; set; }

    public string FldRole { get; set; } = null!;
}
