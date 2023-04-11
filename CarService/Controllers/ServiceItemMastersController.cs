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
    public class ServiceItemMastersController : Controller
    {
        private readonly CarServiceContext _context;

        public ServiceItemMastersController(CarServiceContext context)
        {
            _context = context;
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
        public async Task<IActionResult> Create([Bind("FldServiceItemId,FldItemName,FldItemType,FldQuanitityUnit,FldIdealAmount")] TblServiceItemMaster tblServiceItemMaster)
        {
            if (ModelState.IsValid)
            {
                _context.Add(tblServiceItemMaster);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
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
        public async Task<IActionResult> Edit(long id, [Bind("FldServiceItemId,FldItemName,FldItemType,FldQuanitityUnit,FldIdealAmount")] TblServiceItemMaster tblServiceItemMaster)
        {
            if (id != tblServiceItemMaster.FldServiceItemId)
            {
                return NotFound();
            }

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
    }
}
