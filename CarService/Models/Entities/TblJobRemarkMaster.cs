using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CarService.Models.Entities;

public partial class TblJobRemarkMaster
{
    public long FldRemarkId { get; set; }

    [Display(Name ="Remark Title")]
    [Required]
    public string FldRemarkTitle { get; set; } = null!;
}
