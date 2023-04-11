using System;
using System.Collections.Generic;

namespace CarService.Models.Entities;

public partial class TblEstimateMaster
{
    public long FldEstimateId { get; set; }

    public DateTime FldCreatedOn { get; set; }

    public long FldJobId { get; set; }

    public string FldEstimateNumber { get; set; } = null!;

    public bool FldIsInvoiceGenerated { get; set; }

    public string? FldInvoiceNumber { get; set; }

    public DateTime? FldInvoiceCreatedOn { get; set; }

    public string? FldInvoiceType { get; set; }

    public decimal FldTotalAmount { get; set; }
}
