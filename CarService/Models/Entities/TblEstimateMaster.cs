using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CarService.Models.Entities;

public partial class TblEstimateMaster
{
    public long FldEstimateId { get; set; }

    [Display(Name = "Created On")]
    [DataType(DataType.DateTime)]
    [Required]
    public DateTime FldCreatedOn { get; set; }= DateTime.Now;

    public long FldJobId { get; set; }

    [Display(Name = "Estimate Number")]
    [Required]
    public string FldEstimateNumber { get; set; } = null!;

    [Display(Name = "Invoice Generated")]
    public bool FldIsInvoiceGenerated { get; set; } = false;

    [Display(Name = "Invoice Number")]
    public string? FldInvoiceNumber { get; set; }

    [Display(Name = "Invoice Created On")]
    [DataType(DataType.DateTime)]
    public DateTime? FldInvoiceCreatedOn { get; set; }

    [Display(Name = "Invoice Type")]
    public string? FldInvoiceType { get; set; }

    [Display(Name = "Total Amount")]
    [DataType(DataType.Currency)]
    public decimal FldTotalAmount { get; set; }
}
