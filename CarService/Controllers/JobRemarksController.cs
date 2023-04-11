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
    public class JobRemarksController : Controller
    {
        private readonly CarServiceContext _context;

        public JobRemarksController(CarServiceContext context)
        {
            _context = context;
        }

        // GET: JobRemarks
        public async Task<IActionResult> Index()
        {
              return _context.TblJobRemarks != null ? 
                          View(await _context.TblJobRemarks.ToListAsync()) :
                          Problem("Entity set 'CarServiceContext.TblJobRemarks'  is null.");
        }

        // GET: JobRemarks/Details/5
        public async Task<IActionResult> Details(long? id)
        {
            if (id == null || _context.TblJobRemarks == null)
            {
                return NotFound();
            }

            var tblJobRemark = await _context.TblJobRemarks
                .FirstOrDefaultAsync(m => m.FldJobId == id);
            if (tblJobRemark == null)
            {
                return NotFound();
            }

            return View(tblJobRemark);
        }

        // GET: JobRemarks/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: JobRemarks/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("FldJobId,FldRemarkTitle")] TblJobRemark tblJobRemark)
        {
            if (ModelState.IsValid)
            {
                _context.Add(tblJobRemark);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(tblJobRemark);
        }

        // GET: JobRemarks/Edit/5
        public async Task<IActionResult> Edit(long? id)
        {
            if (id == null || _context.TblJobRemarks == null)
            {
                return NotFound();
            }

            var tblJobRemark = await _context.TblJobRemarks.FindAsync(id);
            if (tblJobRemark == null)
            {
                return NotFound();
            }
            return View(tblJobRemark);
        }

        // POST: JobRemarks/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(long id, [Bind("FldJobId,FldRemarkTitle")] TblJobRemark tblJobRemark)
        {
            if (id != tblJobRemark.FldJobId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(tblJobRemark);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TblJobRemarkExists(tblJobRemark.FldJobId))
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
            return View(tblJobRemark);
        }

        // GET: JobRemarks/Delete/5
        public async Task<IActionResult> Delete(long? id)
        {
            if (id == null || _context.TblJobRemarks == null)
            {
                return NotFound();
            }

            var tblJobRemark = await _context.TblJobRemarks
                .FirstOrDefaultAsync(m => m.FldJobId == id);
            if (tblJobRemark == null)
            {
                return NotFound();
            }

            return View(tblJobRemark);
        }

        // POST: JobRemarks/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(long id)
        {
            if (_context.TblJobRemarks == null)
            {
                return Problem("Entity set 'CarServiceContext.TblJobRemarks'  is null.");
            }
            var tblJobRemark = await _context.TblJobRemarks.FindAsync(id);
            if (tblJobRemark != null)
            {
                _context.TblJobRemarks.Remove(tblJobRemark);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TblJobRemarkExists(long id)
        {
          return (_context.TblJobRemarks?.Any(e => e.FldJobId == id)).GetValueOrDefault();
        }
    }
}
