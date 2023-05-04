using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Composition.Convention;

namespace CarService.Models.Entities;

public partial class TblJobMaster
{
    public long FldJobId { get; set; }

    [Display(Name ="Organization")]
    [Required]
    public long FldOrgId { get; set; }

    [Display(Name ="Job Number")]
    [Required]
    public string FldJobNo { get; set; } = null!;

    [Display(Name ="Vehicle No")]
    [Required]
    public string FldVehicleRegisteredNumber { get; set; } = null!;

    [Display(Name = "Chessis No")]
    public string? FldChessisNumber { get; set; }

    [Display(Name = "Engine No")]
    public string? FldEngineNumber { get; set; }

    [Display(Name = "Model Name & Number")]
    public string? FldModelNameNumber { get; set; }

    [Display(Name = "Service Type")]
    [Required]
    public string FldServiceType { get; set; } = null!;

    [Display(Name = "Registered")]
    [DataType(DataType.DateTime)]
    [DisplayFormat(ApplyFormatInEditMode = false, DataFormatString = "{0:dd-MM-yyyy HH:mm}")]
    [Required]
    public DateTime FldRegisteredOn { get; set; }

    [Display(Name = "Job Registered KM")]
    [Required]
    public decimal FldKmReadingOnRegistration { get; set; }

    [Display(Name = "Customer Name")]
    [Required]
    public string FldCustomerName { get; set; } = null!;

    [Display(Name = "Customer Contact Number")]
    [DataType(DataType.PhoneNumber)]
    [RegularExpression(@"^(\d{10})$", ErrorMessage = "Invalid mobile number")]
    [Required]
    public string FldCustomerContact1 { get; set; } = null!;

    [Display(Name = "Customer Contact Number")]
    [RegularExpression(@"^(\d{10})$", ErrorMessage = "Invalid mobile number")]
    public string? FldCustomerContact2 { get; set; } = null!;

    [Display(Name = "Delivered")]
    [DisplayFormat(ApplyFormatInEditMode = false, DataFormatString = "{0:dd-MM-yyyy HH:mm}")]
    [DataType(DataType.DateTime)]
    public DateTime? FldHandedOverOn { get; set; }

    public bool? FldIsCompleted { get; set; } = false;

    [NotMapped]
    public bool IsEstimateCreated { get; set; } = false;
    [NotMapped]
    public bool IsInvoiceCreated { get; set; } = false;
    [NotMapped]
    public long EstimateInvoiceId { get; set; } = -1;

    [NotMapped]
    public double InvoicedAmount { get; set; } = 0;
    [NotMapped]
    public double PaidAmount { get; set; } = 0;


}
