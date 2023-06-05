using CarService.Models.Entities;

namespace CarService.Models
{
    public class PaymentViewModel
    {
        public TblPayment Payment { get; set; } = new TblPayment();
        public TblJobMaster Job { get; set; } = new TblJobMaster();
        public TblEstimateMaster Estimate { get; set; } = new TblEstimateMaster();
        public TblOrganizationMaster Org { get; set; } = new TblOrganizationMaster();
    }
}
