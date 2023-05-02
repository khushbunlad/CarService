using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CarService.Models.Entities;

public partial class TblEstimateItem
{
    public long FldEstimateItemId { get; set; }

    public long FldEstimateId { get; set; }

    [Display(Name ="Item / Labor Name")]
    public string FldItemTitle { get; set; } = null!;

    [Display(Name = "Type")]
    public string FldItemType { get; set; } = null!;

    [Display(Name = "Quantity")]
    public decimal FldQuantity { get; set; }

    [Display(Name = "Unit")]
    public string FldQuantityUnit { get; set; } = null!;

    [Display(Name = "Amount")]
    public decimal FldUnitAmount { get; set; }

    [Display(Name = "Discount")]
    public decimal FldDiscountAmount { get; set; }

    [Display(Name = "Total")]
    public decimal FldItemTotal { get; set; }

    public bool FldIsCancelled { get; set; }

    public string? FldCancelReason { get; set; }
}
