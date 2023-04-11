using System;
using System.Collections.Generic;

namespace CarService.Models.Entities;

public partial class TblPayment
{
    public long FldPaymentId { get; set; }

    public long FldJobId { get; set; }

    public decimal FldPaidAmount { get; set; }

    public string FldPaymentMode { get; set; } = null!;

    public DateTime FldPaymentDatetime { get; set; }
}
