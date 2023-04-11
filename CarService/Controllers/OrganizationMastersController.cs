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
    public class OrganizationMastersController : Controller
    {
        private readonly CarServiceContext _context;

        public OrganizationMastersController(CarServiceContext context)
        {
            _context = context;
        }

        // GET: OrganizationMasters
        public async Task<IActionResult> Index()
        {
              return _context.TblOrganizationMasters != null ? 
                          View(await _context.TblOrganizationMasters.ToListAsync()) :
                          Problem("Entity set 'CarServiceContext.TblOrganizationMasters'  is null.");
        }

        // GET: OrganizationMasters/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.TblOrganizationMasters == null)
            {
                return NotFound();
            }

            var tblOrganizationMaster = await _context.TblOrganizationMasters
                .FirstOrDefaultAsync(m => m.FldOrgId == id);
            if (tblOrganizationMaster == null)
            {
                return NotFound();
            }

            return View(tblOrganizationMaster);
        }

        // GET: OrganizationMasters/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: OrganizationMasters/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("FldOrgId,FldOrgName,FldOrgEmail,FldAddress,FldContactPerson1,FldContactNumber1,FldContactPerson2,FldContactNumber2,FldLogo,FldIsFullSubscription,FldActiveUntil,FldLicenseNumber")] TblOrganizationMaster tblOrganizationMaster)
        {
            if (ModelState.IsValid)
            {
                _context.Add(tblOrganizationMaster);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(tblOrganizationMaster);
        }

        // GET: OrganizationMasters/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.TblOrganizationMasters == null)
            {
                return NotFound();
            }

            var tblOrganizationMaster = await _context.TblOrganizationMasters.FindAsync(id);
            if (tblOrganizationMaster == null)
            {
                return NotFound();
            }
            return View(tblOrganizationMaster);
        }

        // POST: OrganizationMasters/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("FldOrgId,FldOrgName,FldOrgEmail,FldAddress,FldContactPerson1,FldContactNumber1,FldContactPerson2,FldContactNumber2,FldLogo,FldIsFullSubscription,FldActiveUntil,FldLicenseNumber")] TblOrganizationMaster tblOrganizationMaster)
        {
            if (id != tblOrganizationMaster.FldOrgId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(tblOrganizationMaster);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TblOrganizationMasterExists(tblOrganizationMaster.FldOrgId))
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
            return View(tblOrganizationMaster);
        }

        // GET: OrganizationMasters/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.TblOrganizationMasters == null)
            {
                return NotFound();
            }

            var tblOrganizationMaster = await _context.TblOrganizationMasters
                .FirstOrDefaultAsync(m => m.FldOrgId == id);
            if (tblOrganizationMaster == null)
            {
                return NotFound();
            }

            return View(tblOrganizationMaster);
        }

        // POST: OrganizationMasters/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.TblOrganizationMasters == null)
            {
                return Problem("Entity set 'CarServiceContext.TblOrganizationMasters'  is null.");
            }
            var tblOrganizationMaster = await _context.TblOrganizationMasters.FindAsync(id);
            if (tblOrganizationMaster != null)
            {
                _context.TblOrganizationMasters.Remove(tblOrganizationMaster);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TblOrganizationMasterExists(int id)
        {
          return (_context.TblOrganizationMasters?.Any(e => e.FldOrgId == id)).GetValueOrDefault();
        }
    }
}
