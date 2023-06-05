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
    public class ServiceItemMastersController : Controller
    {
        private readonly CarServiceContext _context;

        public ServiceItemMastersController(CarServiceContext context)
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

        // GET: ServiceItemMasters
        public async Task<IActionResult> Index()
        {
              return _context.TblServiceItemMasters != null ? 
                          View(await _context.TblServiceItemMasters.ToListAsync()) :
                          Problem("Entity set 'CarServiceContext.TblServiceItemMasters'  is null.");
        }

        // GET: ServiceItemMasters/Details/5
        public async Task<IActionResult> Details(long? id)
        {
            if (id == null || _context.TblServiceItemMasters == null)
            {
                return NotFound();
            }

            var tblServiceItemMaster = await _context.TblServiceItemMasters
                .FirstOrDefaultAsync(m => m.FldServiceItemId == id);
            if (tblServiceItemMaster == null)
            {
                return NotFound();
            }

            return View(tblServiceItemMaster);
        }

        // GET: ServiceItemMasters/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: ServiceItemMasters/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("FldServiceItemId,FldItemName,FldItemType,FldQuanitityUnit,FldIdealAmount,FldHsnNumber,FldCgstpercentage,FldSgstpercentage")] TblServiceItemMaster tblServiceItemMaster)
        {
            //if (_context.TblServiceItemMasters.Count(s => s.FldHsnNumber == tblServiceItemMaster.FldHsnNumber) > 0)
            //{
            //    ModelState.AddModelError("FldHsnNumber", "HSN/SAC number already used");
            //}
            //else
            //{
                if (ModelState.IsValid)
                {
                    _context.Add(tblServiceItemMaster);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
           // }
            return View(tblServiceItemMaster);
        }

        // GET: ServiceItemMasters/Edit/5
        public async Task<IActionResult> Edit(long? id)
        {
            if (id == null || _context.TblServiceItemMasters == null)
            {
                return NotFound();
            }

            var tblServiceItemMaster = await _context.TblServiceItemMasters.FindAsync(id);
            if (tblServiceItemMaster == null)
            {
                return NotFound();
            }
            return View(tblServiceItemMaster);
        }

        // POST: ServiceItemMasters/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(long id, [Bind("FldServiceItemId,FldItemName,FldItemType,FldQuanitityUnit,FldIdealAmount,FldHsnNumber,FldCgstpercentage,FldSgstpercentage")] TblServiceItemMaster tblServiceItemMaster)
        {
            if (id != tblServiceItemMaster.FldServiceItemId)
            {
                return NotFound();
            }
            //if (_context.TblServiceItemMasters.Count(s => s.FldHsnNumber == tblServiceItemMaster.FldHsnNumber && s.FldServiceItemId != tblServiceItemMaster.FldServiceItemId) > 0)
            //{
            //    ModelState.AddModelError("FldHsnNumber", "HSN/SAC number already used");
            //}
            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(tblServiceItemMaster);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TblServiceItemMasterExists(tblServiceItemMaster.FldServiceItemId))
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
            return View(tblServiceItemMaster);
        }

        // GET: ServiceItemMasters/Delete/5
        public async Task<IActionResult> Delete(long? id)
        {
            if (id == null || _context.TblServiceItemMasters == null)
            {
                return NotFound();
            }

            var tblServiceItemMaster = await _context.TblServiceItemMasters
                .FirstOrDefaultAsync(m => m.FldServiceItemId == id);
            if (tblServiceItemMaster == null)
            {
                return NotFound();
            }

            return View(tblServiceItemMaster);
        }

        // POST: ServiceItemMasters/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(long id)
        {
            if (_context.TblServiceItemMasters == null)
            {
                return Problem("Entity set 'CarServiceContext.TblServiceItemMasters'  is null.");
            }
            var tblServiceItemMaster = await _context.TblServiceItemMasters.FindAsync(id);
            if (tblServiceItemMaster != null)
            {
                _context.TblServiceItemMasters.Remove(tblServiceItemMaster);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TblServiceItemMasterExists(long id)
        {
          return (_context.TblServiceItemMasters?.Any(e => e.FldServiceItemId == id)).GetValueOrDefault();
        }


        public IActionResult ItemUse()
        {
            ViewBag.Items =  _context.TblServiceItemMasters.ToList();
            return View();
        }

        [HttpPost]
        public IActionResult ItemUseResult(DateTime? StartDate,DateTime? EndDate, long ItemId)
        {
            decimal Count = 0;
            if (StartDate.HasValue && EndDate.HasValue)
            {
                long[] MatchedInvoices = _context.TblEstimateMasters.Where(e => e.FldInvoiceCreatedOn >= StartDate && e.FldInvoiceCreatedOn <= EndDate).Select(e => e.FldEstimateId).ToArray();
                 Count = _context.TblEstimateItems.Where(i => MatchedInvoices.Contains(i.FldEstimateId) && i.FldServiceItemId == ItemId).Sum(i => i.FldQuantity);
            }
            else
            {
                Count = _context.TblEstimateItems.Where(i=>i.FldServiceItemId == ItemId).Sum(i => i.FldQuantity);
            }

            return Json(Math.Round(Count,2));
        }
    }
}
