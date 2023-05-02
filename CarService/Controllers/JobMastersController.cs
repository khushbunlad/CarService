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
                DisplayList = await _context.TblJobMasters.ToListAsync();
            }
            else
            {
                DisplayList = await _context.TblJobMasters.Where(j => j.FldOrgId == OrgId).ToListAsync();
            }
            foreach (TblJobMaster job in DisplayList)
            {
                job.IsEstimateCreated = _context.TblEstimateMasters.Count(e => e.FldJobId == job.FldJobId) == 1 ? true : false;
                job.IsInvoiceCreated = _context.TblEstimateMasters.Count(e => e.FldJobId == job.FldJobId && e.FldIsInvoiceGenerated == true) == 1 ? true : false;
                if (job.IsEstimateCreated)
                {
                    job.EstimateInvoiceId = _context.TblEstimateMasters.Where(e => e.FldJobId == job.FldJobId).FirstOrDefault().FldEstimateId;
                }
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
            long OrgId = long.Parse(HttpContext.Session.GetString(SessionKeys.OrganizationId));

            return View(new TblJobMaster { FldRegisteredOn = DateTime.Now,FldOrgId=OrgId });
        }

        // POST: JobMasters/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("FldJobId,FldOrgId,FldJobNo,FldVehicleRegisteredNumber,FldChessisNumber,FldEngineNumber,FldModelNameNumber,FldServiceType,FldRegisteredOn,FldKmReadingOnRegistration,FldCustomerName,FldCustomerContact1,FldCustomerContact2,FldHandedOverOn")] TblJobMaster tblJobMaster)
        {
            if (ModelState.IsValid)
            {
                if (tblJobMaster.FldCustomerContact2 == null)
                {
                    tblJobMaster.FldCustomerContact2 = "";
                }
                _context.Add(tblJobMaster);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            SetOrganizationsInViewbag();
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



    }
}
