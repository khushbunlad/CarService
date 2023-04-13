using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CarService.Models.Entities;

public partial class TblOrganizationMaster
{

    public int FldOrgId { get; set; }

    [Display(Name ="Organization Name")]
    [Required]
    public string FldOrgName { get; set; } = null!;

    [Display(Name = "Organization Email")]
    [DataType(DataType.EmailAddress)]
    [Required]
    public string FldOrgEmail { get; set; } = null!;

    [Display(Name = "Organization Address")]
    [DataType(DataType.MultilineText)]
    [Required]
    public string FldAddress { get; set; } = null!;

    [Display(Name = "Contact person")]
    [Required]
    public string FldContactPerson1 { get; set; } = null!;

    [Display(Name = "Contact Number")]
    [DataType(DataType.PhoneNumber)]
    [Required]
    public string FldContactNumber1 { get; set; } = null!;

    [Display(Name = "Contact person secondary")]
    public string FldContactPerson2 { get; set; } = "";

    [Display(Name = "Contact Number secondary")]
    [DataType(DataType.PhoneNumber)]
    public string FldContactNumber2 { get; set; } = "";

    [Display(Name = "Logo")]
    public string FldLogo { get; set; } = null!;

    [NotMapped]
    public IFormFile? FldLogoFile { get; set; } = null;

    [Display(Name = "Full scubscription")]
    [Required]
    public bool FldIsFullSubscription { get; set; }

    [Display(Name = "Active until")]
    [DataType(DataType.Date)]
    public DateTime? FldActiveUntil { get; set; }

    [Display(Name = "Subscription License Number")]
    [Required]
    public string FldLicenseNumber { get; set; } = null!;
}
