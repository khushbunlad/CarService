using CarService.Models.Entities;

namespace CarService.Models
{
    public class JobViewModel
    {
        public TblJobMaster Job { get; set; } = new TblJobMaster();
        public TblEstimateMaster Estimate { get; set; } = new TblEstimateMaster();
        public List<TblJobRemark> JobRemarks { get; set; } = new List<TblJobRemark>();
        public List<TblEstimateItem> EstimateItems { get; set; } = new List<TblEstimateItem>();
        public List<TblPayment> Payments { get; set; } = new List<TblPayment>();
    }
}
