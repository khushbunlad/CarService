using Microsoft.DotNet.Scaffolding.Shared.Messaging;
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

    [Display(Name = "Rate")]
    public decimal FldUnitAmount { get; set; }

    [Display(Name = "Discount (%)")]
    [Range(0,100, ErrorMessage = "range 0 to 100")]
    public decimal FldDiscountAmount { get; set; }

    [Display(Name = "Total")]
    public decimal FldItemTotal { get; set; }

    [Display(Name = "Cancel item")]
    public bool FldIsCancelled { get; set; }

    [Display(Name = "Cancellation reason")]
    public string? FldCancelReason { get; set; }

    [Display(Name = "HSN / SCN")]
    [Required]
    public string? FldHsnNumber { get; set; }
    public long? FldServiceItemId { get; set; }

    [Display(Name = "CGST (%)")]
    [Required]
    [Range(0,100,ErrorMessage ="range 0 to 100")]
    public decimal? FldCgstpercentage { get; set; } = 0;

    [Display(Name = "SGST (%)")]
    [Required]
    [Range(0,100, ErrorMessage = "range 0 to 100")]
    public decimal? FldSgstpercentage { get; set; } = 0;

    public decimal ItemTotal { get
        {
            return FldQuantity * FldUnitAmount;
        } }
    public decimal DiscountAmount
    {
        get
        {
            return Math.Round((ItemTotal*FldDiscountAmount)/100,2);
        }
    }
    public decimal ItemTotal_AfterDiscount
    {
        get
        {
            return Math.Round((ItemTotal - DiscountAmount), 2);
        }
    }
    public decimal CGST_Amount
    {
        get
        {
            return Math.Round((ItemTotal_AfterDiscount * FldCgstpercentage??0) / 100, 2);
        }
    }

    public decimal SGST_Amount
    {
        get
        {
            return Math.Round((ItemTotal_AfterDiscount * FldSgstpercentage ?? 0) / 100, 2);
        }
    }

}
