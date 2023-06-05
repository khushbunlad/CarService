using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using CarService.Models.Entities;
using CarService.Models.Constants;
using CarService.Models;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Data;

namespace CarService.Controllers
{
    public class EstimateMastersController : Controller
    {
        private readonly CarServiceContext _context;
        private readonly IConfiguration _config;

        public EstimateMastersController(CarServiceContext context,IConfiguration configuration)
        {
            _context = context;
            _config = configuration;
        }

        public override void OnActionExecuting(ActionExecutingContext context)
        {
            if (HttpContext.Session.GetString(SessionKeys.UserId) == null && HttpContext.Session.GetString(SessionKeys.UserId) == null)
            {
                context.Result = Redirect("~/Home");
            }
            base.OnActionExecuting(context);
        }


        // GET: EstimateMasters
        public async Task<IActionResult> Index()
        {
            return _context.TblEstimateMasters != null ?
                        View(await _context.TblEstimateMasters.ToListAsync()) :
                        Problem("Entity set 'CarServiceContext.TblEstimateMasters'  is null.");
        }

        // GET: EstimateMasters/Details/5
        public async Task<IActionResult> Details(long? id)
        {
            if (id == null || _context.TblEstimateMasters == null)
            {
                return NotFound();
            }

            var tblEstimateMaster = await _context.TblEstimateMasters
                .FirstOrDefaultAsync(m => m.FldEstimateId == id);

            EstimateInvoiceViewModel DisaplayData = new EstimateInvoiceViewModel();
            DisaplayData.Estimate = _context.TblEstimateMasters.Where(m => m.FldEstimateId == id).FirstOrDefault();
            DisaplayData.Job = _context.TblJobMasters.Where(m => m.FldJobId == DisaplayData.Estimate.FldJobId).FirstOrDefault();
            DisaplayData.EstimateItems = _context.TblEstimateItems.Where(m => m.FldEstimateId == id).ToList();
            DisaplayData.Org = _context.TblOrganizationMasters.Where(m => m.FldOrgId == DisaplayData.Job.FldOrgId).FirstOrDefault();

            ViewBag.Watermark = DisaplayData.Org.Get_SavedWatermarkData(_config);

            return View(DisaplayData);
        }

        // GET: EstimateMasters/Create
        public IActionResult Create(long id)
        {
            string jobNo = _context.TblJobMasters.Where(j => j.FldJobId == id).FirstOrDefault().FldJobNo;
            SetJobListInViewBag();
            return View(new TblEstimateMaster
            {
                FldCreatedOn = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, DateTime.Now.Hour, 0, 0),
                FldInvoiceType = InvoiceTypes.Estimate,
                FldJobId = id,
                FldIsInvoiceGenerated=false,
                FldEstimateNumber =  (!string.IsNullOrEmpty(jobNo)?"EST_"+jobNo:""),
                FldInvoiceNumber =  (!string.IsNullOrEmpty(jobNo)?"INV_"+jobNo:"")
            });
        }

        public void SetJobListInViewBag()
        {
            long[] GeneratedEstimates = _context.TblEstimateMasters.Select(e => e.FldJobId).ToArray();
            ViewBag.Jobs = _context.TblJobMasters.Where(j => !GeneratedEstimates.Contains(j.FldJobId)).ToList().Select(x => new SelectListItem { Text = x.FldJobNo, Value = x.FldJobId + "" }).ToList();
        }

        // POST: EstimateMasters/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("FldEstimateId,FldCreatedOn,FldJobId,FldEstimateNumber,FldIsInvoiceGenerated,FldInvoiceNumber,FldInvoiceCreatedOn,FldInvoiceType,FldTotalAmount")] TblEstimateMaster tblEstimateMaster)
        {
            SetJobListInViewBag();
            if (ModelState.IsValid)
            {
                _context.Add(tblEstimateMaster);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Edit), new { id = tblEstimateMaster.FldEstimateId });
            }

            return View(tblEstimateMaster);
        }

        // GET: EstimateMasters/Edit/5
        public async Task<IActionResult> Edit(long? id)
        {
            if (id == null || _context.TblEstimateMasters == null)
            {
                return NotFound();
            }

            var tblEstimateMaster = await _context.TblEstimateMasters.FindAsync(id);
            if (tblEstimateMaster == null)
            {
                return NotFound();
            }
            ViewBag.JobNumber = _context.TblJobMasters.Where(j => j.FldJobId == tblEstimateMaster.FldJobId).FirstOrDefault().FldJobNo;
            return View(tblEstimateMaster);
        }

        // POST: EstimateMasters/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(long id, [Bind("FldEstimateId,FldCreatedOn,FldJobId,FldEstimateNumber,FldIsInvoiceGenerated,FldInvoiceNumber,FldInvoiceCreatedOn,FldInvoiceType,FldTotalAmount")] TblEstimateMaster tblEstimateMaster)
        {
            if (id != tblEstimateMaster.FldEstimateId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    if(tblEstimateMaster.FldInvoiceType != InvoiceTypes.Estimate && !string.IsNullOrEmpty(tblEstimateMaster.FldInvoiceNumber) && tblEstimateMaster.FldInvoiceCreatedOn !=null)
                    {
                        tblEstimateMaster.FldIsInvoiceGenerated = true;
                    }
                    else
                    {
                        tblEstimateMaster.FldIsInvoiceGenerated = false;
                    }
                    _context.Update(tblEstimateMaster);
                    await _context.SaveChangesAsync();

                    JobMastersController jc = new JobMastersController(_context);
                    jc.CheckAndUpdateJobStatus(tblEstimateMaster.FldJobId);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TblEstimateMasterExists(tblEstimateMaster.FldEstimateId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
            }
            ViewBag.JobNumber = _context.TblJobMasters.Where(j => j.FldJobId == tblEstimateMaster.FldJobId).FirstOrDefault().FldJobNo;

            return View(tblEstimateMaster);
        }

        // GET: EstimateMasters/Delete/5
        //public async Task<IActionResult> Delete(long? id)
        //{
        //    if (id == null || _context.TblEstimateMasters == null)
        //    {
        //        return NotFound();
        //    }

        //    var tblEstimateMaster = await _context.TblEstimateMasters
        //        .FirstOrDefaultAsync(m => m.FldEstimateId == id);
        //    if (tblEstimateMaster == null)
        //    {
        //        return NotFound();
        //    }

        //    return View(tblEstimateMaster);
        //}

        //// POST: EstimateMasters/Delete/5
        //[HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> DeleteConfirmed(long id)
        //{
        //    if (_context.TblEstimateMasters == null)
        //    {
        //        return Problem("Entity set 'CarServiceContext.TblEstimateMasters'  is null.");
        //    }
        //    var tblEstimateMaster = await _context.TblEstimateMasters.FindAsync(id);
        //    if (tblEstimateMaster != null)
        //    {
        //        _context.TblEstimateMasters.Remove(tblEstimateMaster);
        //    }

        //    await _context.SaveChangesAsync();
        //    return RedirectToAction(nameof(Index));
        //}

        private bool TblEstimateMasterExists(long id)
        {
            return (_context.TblEstimateMasters?.Any(e => e.FldEstimateId == id)).GetValueOrDefault();
        }
    }
}
