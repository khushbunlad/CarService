using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using CarService.Models.Entities;
using CarService.Models.Constants;

namespace CarService.Controllers
{
    public class EstimateItemsController : Controller
    {
        private readonly CarServiceContext _context;

        public EstimateItemsController(CarServiceContext context)
        {
            _context = context;
        }

        // GET: EstimateItems
        public async Task<IActionResult> Index()
        {
              return _context.TblEstimateItems != null ? 
                          View(await _context.TblEstimateItems.ToListAsync()) :
                          Problem("Entity set 'CarServiceContext.TblEstimateItems'  is null.");
        }

        public async Task<IActionResult> IndexEstimate(long id)
        {
            return _context.TblEstimateItems != null ?
                        View(await _context.TblEstimateItems.Where(ei=>ei.FldEstimateId == id).ToListAsync()) :
                        Problem("Entity set 'CarServiceContext.TblEstimateItems'  is null.");
        }

        // GET: EstimateItems/Details/5
        public async Task<IActionResult> Details(long? id)
        {
            if (id == null || _context.TblEstimateItems == null)
            {
                return NotFound();
            }

            var tblEstimateItem = await _context.TblEstimateItems
                .FirstOrDefaultAsync(m => m.FldEstimateItemId == id);
            if (tblEstimateItem == null)
            {
                return NotFound();
            }

            return View(tblEstimateItem);
        }

        // GET: EstimateItems/Create
        public IActionResult Create(long id)
        {
            SetServiceItemSuggestionInViewbag();
            return View(new TblEstimateItem {FldEstimateId=id,FldQuantityUnit = Units.unit,FldItemType=ServiceItemTypes.Part,FldIsCancelled=false });
        }

        // POST: EstimateItems/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        public async Task<IActionResult> Create([Bind("FldEstimateItemId,FldEstimateId,FldItemTitle,FldHsnNumber,FldItemType,FldQuantity,FldQuantityUnit,FldUnitAmount,FldDiscountAmount,FldItemTotal,FldIsCancelled,FldCancelReason")] TblEstimateItem tblEstimateItem)
        {
            SetServiceItemSuggestionInViewbag();
            if (ModelState.IsValid)
            {
                tblEstimateItem.FldIsCancelled = false;
                _context.Add(tblEstimateItem);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Create));
            }
           
            return View(tblEstimateItem);
        }

        // GET: EstimateItems/Edit/5
        public async Task<IActionResult> Edit(long? id)
        {
            SetServiceItemSuggestionInViewbag();
            if (id == null || _context.TblEstimateItems == null)
            {
                return NotFound();
            }

            var tblEstimateItem = await _context.TblEstimateItems.FindAsync(id);
            if (tblEstimateItem == null)
            {
                return NotFound();
            }
            return View(tblEstimateItem);
        }

        // POST: EstimateItems/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        public async Task<IActionResult> Edit(long id, [Bind("FldEstimateItemId,FldEstimateId,FldHsnNumber,FldItemTitle,FldItemType,FldQuantity,FldQuantityUnit,FldUnitAmount,FldDiscountAmount,FldItemTotal,FldIsCancelled,FldCancelReason")] TblEstimateItem tblEstimateItem)
        {
            SetServiceItemSuggestionInViewbag();
            if (id != tblEstimateItem.FldEstimateItemId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(tblEstimateItem);
                    await _context.SaveChangesAsync();
                    return Json("success");
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TblEstimateItemExists(tblEstimateItem.FldEstimateItemId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return View(tblEstimateItem);
            }
            return View(tblEstimateItem);
        }

        // GET: EstimateItems/Delete/5
        public async Task<IActionResult> Delete(long? id)
        {
            if (id == null || _context.TblEstimateItems == null)
            {
                return NotFound();
            }

            var tblEstimateItem = await _context.TblEstimateItems
                .FirstOrDefaultAsync(m => m.FldEstimateItemId == id);
            if (tblEstimateItem == null)
            {
                return NotFound();
            }

            return View(tblEstimateItem);
        }

        // POST: EstimateItems/Delete/5
        [HttpPost]    
        public async Task<IActionResult> DeleteConfirmed(long id)
        {
            if (_context.TblEstimateItems == null)
            {
                return Problem("Entity set 'CarServiceContext.TblEstimateItems'  is null.");
            }
            var tblEstimateItem = await _context.TblEstimateItems.FindAsync(id);
            if (tblEstimateItem != null)
            {
                _context.TblEstimateItems.Remove(tblEstimateItem);
            }
            
            await _context.SaveChangesAsync();
            return Json("success");
        }

        private bool TblEstimateItemExists(long id)
        {
          return (_context.TblEstimateItems?.Any(e => e.FldEstimateItemId == id)).GetValueOrDefault();
        }

        private void SetServiceItemSuggestionInViewbag()
        {
            ViewBag.ServiceItem = _context.TblServiceItemMasters.ToList();
        }

    }
}
