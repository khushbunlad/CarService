using System;
using System.Collections.Generic;

namespace CarService.Models.Entities;

public partial class TblJobRemark
{
    public long FldJobId { get; set; }

    public string FldRemarkTitle { get; set; } = null!;
}
