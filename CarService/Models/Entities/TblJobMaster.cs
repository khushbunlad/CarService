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
    [Required]
    public string FldCustomerContact1 { get; set; } = null!;

    [Display(Name = "Customer Contact Number")]
    public string? FldCustomerContact2 { get; set; } = null!;

    [Display(Name = "Delivered")]
    [DataType(DataType.DateTime)]
    public DateTime? FldHandedOverOn { get; set; }


    [NotMapped]
    public bool IsEstimateCreated { get; set; } = false;
    [NotMapped]
    public bool IsInvoiceCreated { get; set; } = false;
    [NotMapped]
    public long EstimateInvoiceId { get; set; } = -1;
}
