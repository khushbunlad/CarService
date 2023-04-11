using System;
using System.Collections.Generic;

namespace CarService.Models.Entities;

public partial class TblServiceItemMaster
{
    public long FldServiceItemId { get; set; }

    public string FldItemName { get; set; } = null!;

    public string FldItemType { get; set; } = null!;

    public string FldQuanitityUnit { get; set; } = null!;

    public decimal FldIdealAmount { get; set; }
}
