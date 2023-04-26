using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CarService.Models.Entities;

public partial class TblJobRemark
{
    public long FldJobId { get; set; }

    [Display(Name ="Job Remark")]
    [Required]
    public string FldRemarkTitle { get; set; } = null!;
}
