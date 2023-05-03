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

namespace CarService.Controllers
{
    public class PaymentsController : Controller
    {
        private readonly CarServiceContext _context;

        public PaymentsController(CarServiceContext context)
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

        // GET: Payments
        public async Task<IActionResult> Index(long id)
        {
              return _context.TblPayments != null ? 
                          View(await _context.TblPayments.Where(p=>p.FldJobId == id).ToListAsync()) :
                          Problem("Entity set 'CarServiceContext.TblPayments'  is null.");
        }

        // GET: Payments/Details/5
        public async Task<IActionResult> Details(long? id)
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

        // GET: Payments/Create
        public IActionResult Create(long id)
        {
            return View( new TblPayment { FldJobId =id,FldPaymentDatetime=DateTime.Now});
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
            }
            return View(tblPayment);
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
            return Json("success");
        }

        private bool TblPaymentExists(long id)
        {
          return (_context.TblPayments?.Any(e => e.FldPaymentId == id)).GetValueOrDefault();
        }
    }
}
