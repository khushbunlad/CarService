﻿using CarService.Models.Constants;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CarService.Models.Entities;

public partial class TblOrganizationMaster
{

    public int FldOrgId { get; set; }

    [Display(Name = "Organization Name")]
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
    [RegularExpression(@"^(\d{10})$", ErrorMessage = "Invalid mobile number")]
    [Required]
    public string FldContactNumber1 { get; set; } = null!;

    [Display(Name = "Contact person secondary")]
    public string FldContactPerson2 { get; set; } = "";

    [Display(Name = "Contact Number secondary")]
    [DataType(DataType.PhoneNumber)]
    [RegularExpression(@"^(\d{10})$", ErrorMessage = "Invalid mobile number")]
    public string FldContactNumber2 { get; set; } = "";

    [Display(Name = "Logo")]
    public string FldLogo { get; set; } = null!;

    [NotMapped]
    public IFormFile? FldLogoFile { get; set; } = null;
    [NotMapped]
    public IFormFile? fldWatermarkFile { get; set; } = null;

    public string Get_SavedLogoData(IConfiguration configuration)
    {
        string LogoPath = configuration.GetValue<string>(ConfigKeys.StorageBasePath) + this.FldLogo;
        if (System.IO.File.Exists(LogoPath))
        {
            byte[] imageArray = System.IO.File.ReadAllBytes(LogoPath);
            return "data:image/png;base64," + Convert.ToBase64String(imageArray);
        }
        return "";
    }
    public string Get_SavedWatermarkData(IConfiguration configuration)
    {
        string LogoPath = configuration.GetValue<string>(ConfigKeys.StorageBasePath) + this.FldWatermark;
        if (System.IO.File.Exists(LogoPath))
        {
            byte[] imageArray = System.IO.File.ReadAllBytes(LogoPath);
            return "data:image/png;base64," + Convert.ToBase64String(imageArray);
        }
        return "";
    }
    
    [Display(Name = "Full scubscription")]
    [Required]
    public bool FldIsFullSubscription { get; set; }

    [Display(Name = "Active until")]
    [DataType(DataType.Date)]
    [DisplayFormat(ApplyFormatInEditMode = false, DataFormatString = "{0:dd-MMM-yyyy HH:mm}")]
    public DateTime? FldActiveUntil { get; set; }

    [Display(Name = "Subscription License Number")]
    [Required]
    public string FldLicenseNumber { get; set; } = null!;

    [Display(Name = "Watermark Image")]
    public string? FldWatermark { get; set; } = "";

    [Display(Name = "GST Number")]
    public string? FldGstnumber { get; set; } = "";

}
