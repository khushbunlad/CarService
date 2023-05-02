using CarService.Models.Entities;

namespace CarService.Models
{
    public class EstimateInvoiceViewModel
    {
        public TblJobMaster Job { get;set; } = new TblJobMaster();
        public TblEstimateMaster Estimate { get;set; } = new TblEstimateMaster();
        public TblOrganizationMaster Org { get;set; } = new TblOrganizationMaster();
        public List<TblEstimateItem> EstimateItems { get;set;} = new List<TblEstimateItem>();
    }
}
