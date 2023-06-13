using CarService.Models.Constants;
using CarService.Models.Entities;

namespace CarService.Models
{
    public class EstimateInvoiceViewModel
    {
        public TblJobMaster Job { get;set; } = new TblJobMaster();
        public TblEstimateMaster Estimate { get;set; } = new TblEstimateMaster();
        public TblOrganizationMaster Org { get;set; } = new TblOrganizationMaster();
        public List<TblEstimateItem> EstimateItems { get;set;} = new List<TblEstimateItem>();

        public decimal Parts_Total
        {
            get
            {
                return Math.Round(EstimateItems.Sum(i=> i.FldItemType == ServiceItemTypes.Part && i.FldIsCancelled == false?i.ItemTotal:0),2);
            }
        }
        public decimal Parts_Payeble
        {
            get
            {
                return Math.Round(EstimateItems.Sum(i=> i.FldItemType == ServiceItemTypes.Part && i.FldIsCancelled == false?i.FldItemTotal:0),2);
            }
        }
        public decimal Parts_Taxable
        {
            get
            {
                return Math.Round(EstimateItems.Sum(i => i.FldItemType == ServiceItemTypes.Part && i.FldIsCancelled == false ? i.ItemTotal_AfterDiscount : 0), 2);
            }
        }

        public decimal Parts_CGST
        {
            get
            {
                return Math.Round(EstimateItems.Sum(i => i.FldItemType == ServiceItemTypes.Part && i.FldIsCancelled == false ? i.CGST_Amount : 0), 2);
            }
        }
        public decimal Parts_SGST
        {
            get
            {
                return Math.Round(EstimateItems.Sum(i => i.FldItemType == ServiceItemTypes.Part && i.FldIsCancelled == false ? i.SGST_Amount : 0), 2);
            }
        }
        public decimal Parts_DiscountAmount
        {
            get
            {
                return Math.Round(EstimateItems.Sum(i => i.FldItemType == ServiceItemTypes.Part && i.FldIsCancelled == false ? i.DiscountAmount : 0), 2);
            }
        }

        public decimal Labor_Total
        {
            get
            {
                return Math.Round(EstimateItems.Sum(i => i.FldItemType == ServiceItemTypes.Labor && i.FldIsCancelled == false ? i.ItemTotal : 0), 2);
            }
        }
        public decimal Labor_Taxable
        {
            get
            {
                return Math.Round(EstimateItems.Sum(i => i.FldItemType == ServiceItemTypes.Labor && i.FldIsCancelled == false ? i.ItemTotal_AfterDiscount : 0), 2);
            }
        }
        public decimal Labor_Payeble
        {
            get
            {
                return Math.Round(EstimateItems.Sum(i => i.FldItemType == ServiceItemTypes.Labor && i.FldIsCancelled == false ? i.FldItemTotal : 0), 2);
            }
        }
        public decimal Labor_CGST
        {
            get
            {
                return Math.Round(EstimateItems.Sum(i => i.FldItemType == ServiceItemTypes.Labor && i.FldIsCancelled == false ? i.CGST_Amount : 0), 2);
            }
        }
        public decimal Labor_SGST
        {
            get
            {
                return Math.Round(EstimateItems.Sum(i => i.FldItemType == ServiceItemTypes.Labor && i.FldIsCancelled == false ? i.SGST_Amount : 0), 2);
            }
        }
        public decimal Labor_DiscountAmount
        {
            get
            {
                return Math.Round(EstimateItems.Sum(i => i.FldItemType == ServiceItemTypes.Labor && i.FldIsCancelled == false ? i.DiscountAmount : 0), 2);
            }
        }


        public decimal Total_MRP
        {
            get
            {
                return Math.Round((Parts_Total + Labor_Total), 2);
            }
        }
        public decimal Total_Taxable
        {
            get
            {
                return Math.Round((Parts_Taxable + Labor_Taxable), 2);
            }
        }


        public decimal Total_Discount
        {
            get
            {
                return Math.Round((Parts_DiscountAmount + Labor_DiscountAmount), 2);
            }
        }
        public decimal Total_CGST
        {
            get
            {
                return Math.Round((Parts_CGST + Labor_CGST), 2);
            }
        }
        public decimal Total_SGST
        {
            get
            {
                return Math.Round((Parts_SGST + Labor_SGST), 2);
            }
        }
        public decimal GrandTotal { get
            {
                return Math.Round((Parts_Payeble + Labor_Payeble), 2);
            } }


    }

    public class CsvViewModel
    {
        public TblJobMaster Job { get; set; } = new TblJobMaster();
        public TblEstimateMaster Estimate { get; set; } = new TblEstimateMaster();
    }


}
