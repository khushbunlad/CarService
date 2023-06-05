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
    public class PaymentsController : Controller
    {
        private readonly CarServiceContext _context;
        private readonly IConfiguration _config;

        public PaymentsController(CarServiceContext context, IConfiguration config)
        {
            _context = context;
            _config = config;
        }
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            if (HttpContext.Session.GetString(SessionKeys.UserId) == null && HttpContext.Session.GetString(SessionKeys.UserId) == null)
            {
                context.Result = Redirect("~/Home");
            }
            base.OnActionExecuting(context);
        }

        // GET: Payments
        public async Task<IActionResult> Index(long id)
        {
            TblEstimateMaster est = _context.TblEstimateMasters.Where(e => e.FldJobId == id).FirstOrDefault();
            
            List<TblPayment> payments = await _context.TblPayments.Where(p => p.FldJobId == id).ToListAsync();

            if (est != null)
            {
                ViewBag.PendingAmount = Math.Round(est.FldTotalAmount - payments.Sum(p => p.FldPaidAmount),2);
            }
            return View(payments);
        }

        // GET: Payments/Details/5
        public async Task<IActionResult> Details(long? id)
        {

            if (id == null || _context.TblPayments == null)
            {
                return NotFound();
            }

            PaymentViewModel DisaplayData = new PaymentViewModel();
            DisaplayData.Payment = _context.TblPayments.Where(m => m.FldPaymentId == id).FirstOrDefault();
            DisaplayData.Job = _context.TblJobMasters.Where(m => m.FldJobId == DisaplayData.Payment.FldJobId).FirstOrDefault();
            DisaplayData.Org = _context.TblOrganizationMasters.Where(m => m.FldOrgId == DisaplayData.Job.FldOrgId).FirstOrDefault();
            if(DisaplayData.Job.EstimateInvoiceId >=0)
            {
                DisaplayData.Estimate = _context.TblEstimateMasters.Where(m => m.FldEstimateId == DisaplayData.Job.EstimateInvoiceId).FirstOrDefault();
            }
            else
            {
                DisaplayData.Estimate = null;
            }
            ViewBag.Watermark = DisaplayData.Org.Get_SavedWatermarkData(_config);

            return View(DisaplayData);
        }

        // GET: Payments/Create
        public IActionResult Create(long id)
        {

            
            return View( new TblPayment { FldJobId =id,FldPaymentDatetime= new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, DateTime.Now.Hour, 0, 0) });
        }

        // POST: Payments/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("FldPaymentId,FldJobId,FldPaidAmount,FldPaymentMode,FldPaymentDatetime")] TblPayment tblPayment)
        {
            if (ModelState.IsValid)
            {
                _context.Add(tblPayment);
                await _context.SaveChangesAsync();

                JobMastersController jc = new JobMastersController(_context);
                jc.CheckAndUpdateJobStatus(tblPayment.FldJobId);
            }
            return RedirectToAction("create");
        }

      
        // GET: Payments/Delete/5
        public async Task<IActionResult> Delete(long? id)
        {
            if (id == null || _context.TblPayments == null)
            {
                return NotFound();
            }

            var tblPayment = await _context.TblPayments
                .FirstOrDefaultAsync(m => m.FldPaymentId == id);
            if (tblPayment == null)
            {
                return NotFound();
            }

            return View(tblPayment);
        }

        // POST: Payments/Delete/5
        [HttpPost]
        public async Task<IActionResult> DeleteConfirmed(long id)
        {
            if (_context.TblPayments == null)
            {
                return Problem("Entity set 'CarServiceContext.TblPayments'  is null.");
            }
            var tblPayment = await _context.TblPayments.FindAsync(id);
            if (tblPayment != null)
            {
                _context.TblPayments.Remove(tblPayment);
            }
            
            await _context.SaveChangesAsync();

            JobMastersController jc = new JobMastersController(_context);
            jc.CheckAndUpdateJobStatus(tblPayment.FldJobId);

            return Json("success");
        }

        private bool TblPaymentExists(long id)
        {
          return (_context.TblPayments?.Any(e => e.FldPaymentId == id)).GetValueOrDefault();
        }
    }
}
