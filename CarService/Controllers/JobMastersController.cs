using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using CarService.Models.Entities;
using CarService.Models.Constants;
using Microsoft.AspNetCore.Mvc.Filters;
using CarService.Models;

namespace CarService.Controllers
{
    public class JobMastersController : Controller
    {
        private readonly CarServiceContext _context;

        public JobMastersController(CarServiceContext context)
        {
            _context = context;
        }

        public override void OnActionExecuting(ActionExecutingContext context)
        {
            if (HttpContext.Session.GetString(SessionKeys.UserId) == null && HttpContext.Session.GetString(SessionKeys.UserId) == null)
            {
                context.Result = Redirect("~/Home");
            }
            base.OnActionExecuting(context);
        }
        // GET: JobMasters
        public async Task<IActionResult> Index()
        {
            long OrgId = long.Parse(HttpContext.Session.GetString(SessionKeys.OrganizationId));
            string? UserRole = HttpContext.Session.GetString(SessionKeys.UserId);

            List<TblJobMaster> DisplayList = new List<TblJobMaster>();
            if (UserRole == UserRoles.Admin)
            {
                DisplayList = await _context.TblJobMasters.Where(j => j.FldIsCompleted == false).ToListAsync();
            }
            else
            {
                DisplayList = await _context.TblJobMasters.Where(j => j.FldOrgId == OrgId && j.FldIsCompleted == false).ToListAsync();
            }
            foreach (TblJobMaster job in DisplayList)
            {
                job.IsEstimateCreated = _context.TblEstimateMasters.Count(e => e.FldJobId == job.FldJobId) == 1 ? true : false;
                job.IsInvoiceCreated = _context.TblEstimateMasters.Count(e => e.FldJobId == job.FldJobId && e.FldIsInvoiceGenerated == true) == 1 ? true : false;
                if (job.IsEstimateCreated)
                {
                    job.EstimateInvoiceId = _context.TblEstimateMasters.Where(e => e.FldJobId == job.FldJobId).FirstOrDefault().FldEstimateId;
                    job.InvoicedAmount = (double)_context.TblEstimateItems.Where(e => e.FldEstimateId == job.EstimateInvoiceId && e.FldIsCancelled ==false).Sum(se => se.FldItemTotal);
                }
                else
                {
                    job.InvoicedAmount = 0;
                }
                job.PaidAmount = (double)_context.TblPayments.Where(p => p.FldJobId == job.FldJobId).Sum(p => p.FldPaidAmount);
            }

            return View(DisplayList);
        }

        public async Task<IActionResult> HistoryFilters()
        {
            return View();
        }
        public async Task<IActionResult> History(DateTime? StartDate,DateTime EndDate,string? SerchVehicle,string? SearchCustomer)
        {
            long OrgId = long.Parse(HttpContext.Session.GetString(SessionKeys.OrganizationId));
            string? UserRole = HttpContext.Session.GetString(SessionKeys.UserId);

            List<TblJobMaster> DisplayList = new List<TblJobMaster>();
            if (UserRole == UserRoles.Admin)
            {
                DisplayList = await _context.TblJobMasters.Where(j => j.FldIsCompleted == false).ToListAsync();
            }
            else
            {
                if(StartDate !=null && EndDate != null)
                {
                    DisplayList = await _context.TblJobMasters.Where(j => j.FldOrgId == OrgId 
                                && j.FldIsCompleted == true
                                && j.FldRegisteredOn >= StartDate
                                && j.FldHandedOverOn <=EndDate
                                ).ToListAsync();
                    if (!string.IsNullOrEmpty(SerchVehicle))
                    {
                        DisplayList = DisplayList.Where(j => j.FldVehicleRegisteredNumber.Contains(SerchVehicle)).ToList();
                    }
                    if (!string.IsNullOrEmpty(SearchCustomer))
                    {
                        DisplayList = DisplayList.Where(j => j.FldCustomerName.Contains(SearchCustomer) 
                                                || j.FldCustomerContact1.Contains(SearchCustomer)
                                                || j.FldCustomerContact2.Contains(SearchCustomer)
                                                ).ToList();
                    }
                }
                else if(!string.IsNullOrEmpty(SerchVehicle) && !string.IsNullOrEmpty(SearchCustomer))
                {
                    DisplayList = await _context.TblJobMasters.Where(j => j.FldOrgId == OrgId
                                && (
                                                j.FldCustomerName.Contains(SearchCustomer)
                                                || j.FldCustomerContact1.Contains(SearchCustomer)
                                                || j.FldCustomerContact2.Contains(SearchCustomer)
                                                || j.FldVehicleRegisteredNumber.Contains(SearchCustomer)
                                )
                                ).ToListAsync();
                }
                else if (!string.IsNullOrEmpty(SerchVehicle) )
                {
                    DisplayList = await _context.TblJobMasters.Where(j => j.FldOrgId == OrgId
                                && (
                                               
                                     j.FldVehicleRegisteredNumber.Contains(SerchVehicle)
                                )
                                ).ToListAsync();
                }
                else if (!string.IsNullOrEmpty(SearchCustomer))
                {
                    DisplayList = await _context.TblJobMasters.Where(j => j.FldOrgId == OrgId
                                && (
                                                j.FldCustomerName.Contains(SearchCustomer)
                                                || j.FldCustomerContact1.Contains(SearchCustomer)
                                                || j.FldCustomerContact2.Contains(SearchCustomer)
                                )
                                ).ToListAsync();
                }

            }
            foreach (TblJobMaster job in DisplayList)
            {
                job.IsEstimateCreated = _context.TblEstimateMasters.Count(e => e.FldJobId == job.FldJobId) == 1 ? true : false;
                job.IsInvoiceCreated = _context.TblEstimateMasters.Count(e => e.FldJobId == job.FldJobId && e.FldIsInvoiceGenerated == true) == 1 ? true : false;
                if (job.IsEstimateCreated)
                {
                    job.EstimateInvoiceId = _context.TblEstimateMasters.Where(e => e.FldJobId == job.FldJobId).FirstOrDefault().FldEstimateId;
                    job.InvoicedAmount = (double)_context.TblEstimateItems.Where(e => e.FldEstimateId == job.EstimateInvoiceId && e.FldIsCancelled == false).Sum(se => se.FldItemTotal);
                }
                else
                {
                    job.InvoicedAmount = 0;
                }
                job.PaidAmount = (double)_context.TblPayments.Where(p => p.FldJobId == job.FldJobId).Sum(p => p.FldPaidAmount);
            }

            return View(DisplayList);
        }

        // GET: JobMasters/Details/5
        public async Task<IActionResult> Details(long? id)
        {
            if (id == null || _context.TblJobMasters == null)
            {
                return NotFound();
            }
            JobViewModel JobDetail = new JobViewModel();
            JobDetail.Job = _context.TblJobMasters.Where(m => m.FldJobId == id).FirstOrDefault();
            JobDetail.JobRemarks = _context.TblJobRemarks.Where(r => r.FldJobId == id).ToList();
            if(_context.TblEstimateMasters.Count(e=>e.FldJobId == id)>0)
            {
                JobDetail.Estimate = _context.TblEstimateMasters.Where(e => e.FldJobId == id).FirstOrDefault();
                JobDetail.EstimateItems = _context.TblEstimateItems.Where(e => e.FldEstimateId == JobDetail.Estimate.FldEstimateId).ToList();
            }
            else
            {
                JobDetail.Estimate = null;
            }
            JobDetail.Payments = _context.TblPayments.Where(p => p.FldJobId == id).ToList();
            if (JobDetail.Job == null)
            {
                return NotFound();
            }

            return View(JobDetail);
        }

        // GET: JobMasters/Create
        public IActionResult Create()
        {
            SetOrganizationsInViewbag();
            SetCustomerSuggestionInViewbag();

            long OrgId = long.Parse(HttpContext.Session.GetString(SessionKeys.OrganizationId));

            return View(new TblJobMaster {FldIsCompleted = false, FldRegisteredOn = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, DateTime.Now.Hour, 0, 0), FldOrgId=OrgId });
        }

        // POST: JobMasters/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("FldJobId,FldIsCompleted,FldOrgId,FldJobNo,FldVehicleRegisteredNumber,FldChessisNumber,FldEngineNumber,FldModelNameNumber,FldServiceType,FldRegisteredOn,FldKmReadingOnRegistration,FldCustomerName,FldCustomerContact1,FldCustomerContact2,FldHandedOverOn")] TblJobMaster tblJobMaster)
        {
            if(_context.TblJobMasters.Count(j=>j.FldJobNo == tblJobMaster.FldJobNo)>0)
            {
                ModelState.AddModelError("FldJobNo", "Job number already used");
            }

            if (ModelState.IsValid)
            {
                if (tblJobMaster.FldCustomerContact2 == null)
                {
                    tblJobMaster.FldCustomerContact2 = "";
                }
                _context.Add(tblJobMaster);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Edit),new {id=tblJobMaster.FldJobId});
            }
            SetOrganizationsInViewbag();
            SetCustomerSuggestionInViewbag();
            return View(tblJobMaster);
        }

        // GET: JobMasters/Edit/5
        public async Task<IActionResult> Edit(long? id)
        {
            if (id == null || _context.TblJobMasters == null)
            {
                return NotFound();
            }

            var tblJobMaster = await _context.TblJobMasters.FindAsync(id);
            if (tblJobMaster == null)
            {
                return NotFound();
            }
            SetOrganizationsInViewbag();
            return View(tblJobMaster);
        }

        // POST: JobMasters/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(long id, [Bind("FldJobId,FldOrgId,FldJobNo,FldVehicleRegisteredNumber,FldChessisNumber,FldEngineNumber,FldModelNameNumber,FldServiceType,FldRegisteredOn,FldKmReadingOnRegistration,FldCustomerName,FldCustomerContact1,FldCustomerContact2,FldHandedOverOn")] TblJobMaster tblJobMaster)
        {
            if (id != tblJobMaster.FldJobId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                if(tblJobMaster.FldCustomerContact2 == null)
                {
                    tblJobMaster.FldCustomerContact2 = "";
                }
                try
                {
                    _context.Update(tblJobMaster);
                    await _context.SaveChangesAsync();
                    CheckAndUpdateJobStatus(tblJobMaster.FldJobId);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TblJobMasterExists(tblJobMaster.FldJobId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            SetOrganizationsInViewbag();
            return View(tblJobMaster);
        }

        // GET: JobMasters/Delete/5
        public async Task<IActionResult> Delete(long? id)
        {
            if (id == null || _context.TblJobMasters == null)
            {
                return NotFound();
            }

            var tblJobMaster = await _context.TblJobMasters
                .FirstOrDefaultAsync(m => m.FldJobId == id);
            if (tblJobMaster == null)
            {
                return NotFound();
            }

            return View(tblJobMaster);
        }

        // POST: JobMasters/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(long id)
        {
            if (_context.TblJobMasters == null)
            {
                return Problem("Entity set 'CarServiceContext.TblJobMasters'  is null.");
            }
            var j = await _context.TblJobMasters.FindAsync(id);

            //----------Delete job items----------
            List<TblJobRemark> jobremarks = _context.TblJobRemarks.Where(u => u.FldJobId == j.FldJobId).ToList();
            foreach (TblJobRemark jr in jobremarks)
            {
                _context.TblJobRemarks.Remove(jr);
                _context.SaveChanges();
            }

            //----------Delete job payments----------
            List<TblPayment> jobpayments = _context.TblPayments.Where(u => u.FldJobId == j.FldJobId).ToList();
            foreach (TblPayment p in jobpayments)
            {
                _context.TblPayments.Remove(p);
                _context.SaveChanges();
            }

            //----------Delete Estimate----------
            List<TblEstimateMaster> estimate = _context.TblEstimateMasters.Where(u => u.FldJobId == j.FldJobId).ToList();
            foreach (TblEstimateMaster e in estimate)
            {
                //----------Delete Estimate items----------
                List<TblEstimateItem> estimateitems = _context.TblEstimateItems.Where(u => u.FldEstimateId == e.FldEstimateId).ToList();
                foreach (TblEstimateItem ei in estimateitems)
                {
                    _context.TblEstimateItems.Remove(ei);
                    _context.SaveChanges();
                }


                _context.TblEstimateMasters.Remove(e);
                _context.SaveChanges();
            }



            //----------Delete job master----------
            _context.TblJobMasters.Remove(j);
            _context.SaveChanges();

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TblJobMasterExists(long id)
        {
            return (_context.TblJobMasters?.Any(e => e.FldJobId == id)).GetValueOrDefault();
        }

        private void SetOrganizationsInViewbag()
        {

            ViewBag.Organizations = _context.TblOrganizationMasters.Select(x => new SelectListItem { Text = x.FldOrgName, Value = x.FldOrgId + "" }).ToList();
        }

        private void SetCustomerSuggestionInViewbag()
        {

            ViewBag.Customers = _context.TblJobMasters.Select(x => new CustomerSuggestionListViewModel { 
            Contact1 = x.FldCustomerContact1,
            Contact2 = x.FldCustomerContact2+"",
            CustomerName = x.FldCustomerName,
            VehicleNumber = x.FldVehicleRegisteredNumber
            }).ToList();
        }


        public void CheckAndUpdateJobStatus(long jobId)
        {
            TblJobMaster Job=  _context.TblJobMasters.Where(j=>j.FldJobId ==jobId).FirstOrDefault();
            if(Job != null)
            {
                if (Job.FldHandedOverOn.HasValue == false)
                {
                    return;
                }
                TblEstimateMaster Estimate = _context.TblEstimateMasters.Where(e=>e.FldJobId ==jobId).FirstOrDefault();
                if (Estimate != null)
                {
                    if(Estimate.FldIsInvoiceGenerated == false)
                    {
                        return;
                    }
                    if(_context.TblEstimateItems.Count(i=>i.FldEstimateId == Estimate.FldEstimateId) == 0)
                    {
                        return;
                    }

                    decimal PaiedAmount = _context.TblPayments.Where(p => p.FldJobId == jobId).Sum(p => p.FldPaidAmount);
                    decimal TotalAmount = _context.TblEstimateItems.Where(i => i.FldEstimateId == Estimate.FldEstimateId && i.FldIsCancelled == false).Sum(a => a.FldItemTotal);
                    
                    if(PaiedAmount >=TotalAmount)
                    {
                        Job.FldIsCompleted = true;
                       
                    }
                    else
                    {
                        Job.FldIsCompleted = false;
                    }
                    _context.TblJobMasters.Update(Job);
                    _context.SaveChanges();

                }
            }
        }

    }
}
