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
    public class SystemUsersController : Controller
    {
        private readonly CarServiceContext _context;

        public SystemUsersController(CarServiceContext context)
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
        // GET: SystemUsers
        public async Task<IActionResult> Index()
        {
            string? role = HttpContext.Session.GetString(SessionKeys.UserRole);
            long OrgId = long.Parse(HttpContext.Session.GetString(SessionKeys.OrganizationId));
            if (role == UserRoles.Admin)
            {
                return View(await _context.TblSystemUsers.ToListAsync());
            }
            else
            {
                return View(await _context.TblSystemUsers.Where(u=>u.FldOrgId == OrgId).ToListAsync());
            }
        }

        // GET: SystemUsers/Details/5
        public async Task<IActionResult> Details(long? id)
        {
            if (id == null || _context.TblSystemUsers == null)
            {
                return NotFound();
            }

            var tblSystemUser = await _context.TblSystemUsers
                .FirstOrDefaultAsync(m => m.FldUserId == id);
            if (tblSystemUser == null)
            {
                return NotFound();
            }

            return View(tblSystemUser);
        }

        // GET: SystemUsers/Create
        public IActionResult Create()
        {
            long OrgId = long.Parse(HttpContext.Session.GetString(SessionKeys.OrganizationId));

            SetOrganizationsInViewbag();
            return View(new TblSystemUser { FldOrgId = OrgId});
        }

        // POST: SystemUsers/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("FldUserId,FldOrgId,FldFullName,FldEmailId,FldMobileNumber,FldPassword,FldIsActive,FldRole")] TblSystemUser tblSystemUser)
        {
            if (ModelState.IsValid)
            {
                _context.Add(tblSystemUser);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            SetOrganizationsInViewbag();
            return View(tblSystemUser);
        }

        // GET: SystemUsers/Edit/5
        public async Task<IActionResult> Edit(long? id)
        {
            if (id == null || _context.TblSystemUsers == null)
            {
                return NotFound();
            }

            var tblSystemUser = await _context.TblSystemUsers.FindAsync(id);
            if (tblSystemUser == null)
            {
                return NotFound();
            }
            SetOrganizationsInViewbag();
            return View(tblSystemUser);
        }

        // POST: SystemUsers/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(long id, [Bind("FldUserId,FldOrgId,FldFullName,FldEmailId,FldMobileNumber,FldPassword,FldIsActive,FldRole")] TblSystemUser tblSystemUser)
        {
            if (id != tblSystemUser.FldUserId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(tblSystemUser);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TblSystemUserExists(tblSystemUser.FldUserId))
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
            return View(tblSystemUser);
        }

        // GET: SystemUsers/Delete/5
        public async Task<IActionResult> Delete(long? id)
        {
            if (id == null || _context.TblSystemUsers == null)
            {
                return NotFound();
            }

            var tblSystemUser = await _context.TblSystemUsers
                .FirstOrDefaultAsync(m => m.FldUserId == id);
            if (tblSystemUser == null)
            {
                return NotFound();
            }

            return View(tblSystemUser);
        }

        // POST: SystemUsers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(long id)
        {
            if (_context.TblSystemUsers == null)
            {
                return Problem("Entity set 'CarServiceContext.TblSystemUsers'  is null.");
            }
            var tblSystemUser = await _context.TblSystemUsers.FindAsync(id);
            if (tblSystemUser != null)
            {
                _context.TblSystemUsers.Remove(tblSystemUser);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TblSystemUserExists(long id)
        {
          return (_context.TblSystemUsers?.Any(e => e.FldUserId == id)).GetValueOrDefault();
        }


        private void SetOrganizationsInViewbag()
        {
           ViewBag.Organizations =  _context.TblOrganizationMasters.Select(x => new SelectListItem { Text = x.FldOrgName, Value = x.FldOrgId + "" }).ToList();
        }
    }
}
