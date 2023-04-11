using System;
using System.Collections.Generic;

namespace CarService.Models.Entities;

public partial class TblJobRemarkMaster
{
    public long FldRemarkId { get; set; }

    public string FldRemarkTitle { get; set; } = null!;
}
