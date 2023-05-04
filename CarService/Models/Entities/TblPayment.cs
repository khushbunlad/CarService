using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CarService.Models.Entities;

public partial class TblPayment
{
    public long FldPaymentId { get; set; }

    public long FldJobId { get; set; }

    [Display(Name ="Paid Amount")]
    [Required]
    public decimal FldPaidAmount { get; set; }

    [Display(Name = "Payment Method")]
    [Required]
    public string FldPaymentMode { get; set; } = null!;

    [Display(Name = "Date & time")]
    [Required]
    [DisplayFormat(ApplyFormatInEditMode = false, DataFormatString = "{0:dd-MM-yyyy HH:mm}")]
    public DateTime FldPaymentDatetime { get; set; }
}
