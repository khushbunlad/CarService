using System;
using System.Collections.Generic;

namespace CarService.Models.Entities;

public partial class TblEstimateItem
{
    public long FldEstimateItemId { get; set; }

    public long FldEstimateId { get; set; }

    public string FldItemTitle { get; set; } = null!;

    public string FldItemType { get; set; } = null!;

    public decimal FldQuantity { get; set; }

    public string FldQuantityUnit { get; set; } = null!;

    public decimal FldUnitAmount { get; set; }

    public decimal FldDiscountAmount { get; set; }

    public decimal FldItemTotal { get; set; }

    public bool FldIsCancelled { get; set; }

    public string? FldCancelReason { get; set; }
}
