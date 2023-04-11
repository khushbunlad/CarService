using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using CarService.Models.Entities;

namespace CarService.Controllers
{
    public class PaymentsController : Controller
    {
        private readonly CarServiceContext _context;

        public PaymentsController(CarServiceContext context)
        {
            _context = context;
        }

        // GET: Payments
        public async Task<IActionResult> Index()
        {
              return _context.TblPayments != null ? 
                          View(await _context.TblPayments.ToListAsync()) :
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
        public IActionResult Create()
        {
            return View();
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
                return RedirectToAction(nameof(Index));
            }
            return View(tblPayment);
        }

        // GET: Payments/Edit/5
        public async Task<IActionResult> Edit(long? id)
        {
            if (id == null || _context.TblPayments == null)
            {
                return NotFound();
            }

            var tblPayment = await _context.TblPayments.FindAsync(id);
            if (tblPayment == null)
            {
                return NotFound();
            }
            return View(tblPayment);
        }

        // POST: Payments/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(long id, [Bind("FldPaymentId,FldJobId,FldPaidAmount,FldPaymentMode,FldPaymentDatetime")] TblPayment tblPayment)
        {
            if (id != tblPayment.FldPaymentId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(tblPayment);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TblPaymentExists(tblPayment.FldPaymentId))
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
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
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
            return RedirectToAction(nameof(Index));
        }

        private bool TblPaymentExists(long id)
        {
          return (_context.TblPayments?.Any(e => e.FldPaymentId == id)).GetValueOrDefault();
        }
    }
}
