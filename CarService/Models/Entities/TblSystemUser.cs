using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CarService.Models.Entities;

public partial class TblSystemUser
{
    public long FldUserId { get; set; }

    [Display(Name ="Organization Name")]
    [Required]
    [Range(0,int.MaxValue)]
    public long FldOrgId { get; set; }

    [Display(Name = "Full Name")]
    [Required]
    public string FldFullName { get; set; } = null!;


    [Display(Name = "Email Id")]
    [DataType(DataType.EmailAddress)]
    [Required]
    public string FldEmailId { get; set; } = null!;
    [Display(Name = "Mobile Number")]
    [DataType(DataType.PhoneNumber)]
    [Required]
    public string FldMobileNumber { get; set; } = null!;
  
    [Display(Name = "Password")]
    [Required]
    public string FldPassword { get; set; } = null!;

    [Display(Name = "Active")]
    public bool FldIsActive { get; set; }

    [Display(Name = "Role")]
    public string FldRole { get; set; } = null!;
}
