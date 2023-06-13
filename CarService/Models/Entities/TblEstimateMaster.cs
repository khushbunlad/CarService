using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CarService.Models.Entities;

public partial class TblEstimateMaster
{
    public long FldEstimateId { get; set; }

    [Display(Name = "Created On")]
    [DataType(DataType.DateTime)]
    [DisplayFormat(ApplyFormatInEditMode = false, DataFormatString = "{0:dd-MMM-yyyy HH:mm}")]
    [Required]
    public DateTime FldCreatedOn { get; set; }= new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, DateTime.Now.Hour, 0, 0);

    [Display(Name = "Job Number")]
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
    [DisplayFormat(ApplyFormatInEditMode = false, DataFormatString = "{0:dd-MMM-yyyy HH:mm}")]
    public DateTime? FldInvoiceCreatedOn { get; set; }

    [Display(Name = "Invoice Type")]
    public string? FldInvoiceType { get; set; }

    [Display(Name = "Total Amount")]
    public decimal FldTotalAmount { get; set; }

    [Display(Name = "Invoice Remarks")]
    public string? FldInvoiceRemark { get; set; }

}
