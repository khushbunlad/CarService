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
    public class JobRemarkMastersController : Controller
    {
        private readonly CarServiceContext _context;

        public JobRemarkMastersController(CarServiceContext context)
        {
            _context = context;
        }

        // GET: JobRemarkMasters
        public async Task<IActionResult> Index()
        {
              return _context.TblJobRemarkMasters != null ? 
                          View(await _context.TblJobRemarkMasters.ToListAsync()) :
                          Problem("Entity set 'CarServiceContext.TblJobRemarkMasters'  is null.");
        }

       
        // GET: JobRemarkMasters/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: JobRemarkMasters/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("FldRemarkId,FldRemarkTitle")] TblJobRemarkMaster tblJobRemarkMaster)
        {
            if (ModelState.IsValid)
            {
                _context.Add(tblJobRemarkMaster);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(tblJobRemarkMaster);
        }

       
        // GET: JobRemarkMasters/Delete/5
        public async Task<IActionResult> Delete(long? id)
        {
            if (id == null || _context.TblJobRemarkMasters == null)
            {
                return NotFound();
            }

            var tblJobRemarkMaster = await _context.TblJobRemarkMasters
                .FirstOrDefaultAsync(m => m.FldRemarkId == id);
            if (tblJobRemarkMaster == null)
            {
                return NotFound();
            }

            return View(tblJobRemarkMaster);
        }

        // POST: JobRemarkMasters/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(long id)
        {
            if (_context.TblJobRemarkMasters == null)
            {
                return Problem("Entity set 'CarServiceContext.TblJobRemarkMasters'  is null.");
            }
            var tblJobRemarkMaster = await _context.TblJobRemarkMasters.FindAsync(id);
            if (tblJobRemarkMaster != null)
            {
                _context.TblJobRemarkMasters.Remove(tblJobRemarkMaster);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TblJobRemarkMasterExists(long id)
        {
          return (_context.TblJobRemarkMasters?.Any(e => e.FldRemarkId == id)).GetValueOrDefault();
        }
    }
}
