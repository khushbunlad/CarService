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
    public class JobMastersController : Controller
    {
        private readonly CarServiceContext _context;

        public JobMastersController(CarServiceContext context)
        {
            _context = context;
        }

        // GET: JobMasters
        public async Task<IActionResult> Index()
        {
              return _context.TblJobMasters != null ? 
                          View(await _context.TblJobMasters.ToListAsync()) :
                          Problem("Entity set 'CarServiceContext.TblJobMasters'  is null.");
        }

        // GET: JobMasters/Details/5
        public async Task<IActionResult> Details(long? id)
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

        // GET: JobMasters/Create
        public IActionResult Create()
        {
            SetOrganizationsInViewbag();
            return View(new TblJobMaster { FldRegisteredOn = DateTime.Now});
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
            var tblJobMaster = await _context.TblJobMasters.FindAsync(id);
            if (tblJobMaster != null)
            {
                _context.TblJobMasters.Remove(tblJobMaster);
            }
            
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
