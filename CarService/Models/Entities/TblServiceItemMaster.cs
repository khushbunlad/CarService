using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CarService.Models.Entities;

public partial class TblServiceItemMaster
{
    public long FldServiceItemId { get; set; }

    [Display(Name ="Part/Labour Title")]
    [Required]
    public string FldItemName { get; set; } = null!;

    [Display(Name = "Type")]
    [Required]
    public string FldItemType { get; set; } = null!;


    [Display(Name = "Measurement Unit")]
    [Required]
    public string FldQuanitityUnit { get; set; } = null!;

    [Display(Name = "Approximate Amount")]
    [DataType(DataType.Currency)]
    [Required]
    public decimal FldIdealAmount { get; set; } = 0;
}
