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
        public async Task<IActionResult> IndexJob(long id)
        {

            return _context.TblJobRemarks != null ?
                        View(await _context.TblJobRemarks.Where(r=>r.FldJobId == id).ToListAsync()) :
                        Problem("Entity set 'CarServiceContext.TblJobRemarks'  is null.");
        }

       
        // GET: JobRemarks/Create
        public IActionResult Create(long id)
        {
            SetJobRemarkSuggestionInViewbag();
            return View(new TblJobRemark { FldJobId=id,FldRemarkTitle=""});
        }

        // POST: JobRemarks/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        public async Task<IActionResult> Create([Bind("FldJobId,FldRemarkTitle")] TblJobRemark tblJobRemark)
        {
            SetJobRemarkSuggestionInViewbag();
            if (ModelState.IsValid)
            {
                _context.Add(tblJobRemark);
                _context.SaveChanges();
                return View(new TblJobRemark { FldJobId=tblJobRemark.FldJobId,FldRemarkTitle=""});

            }
            else
            {
                return View(tblJobRemark);
            }
        }

        // POST: JobRemarks/Delete/5
        [HttpPost]
        public async Task<IActionResult> DeleteConfirmed([Bind("FldJobId,FldRemarkTitle")] TblJobRemark tblJobRemark)
        {
           
            if (tblJobRemark != null)
            {
                _context.TblJobRemarks.Remove(tblJobRemark);
            }
            
            await _context.SaveChangesAsync();
            return Json("success");
        }

        private bool TblJobRemarkExists(long id)
        {
          return (_context.TblJobRemarks?.Any(e => e.FldJobId == id)).GetValueOrDefault();
        }

        private void SetJobRemarkSuggestionInViewbag()
        {
            ViewBag.Remarks = _context.TblJobRemarkMasters.Select(x => new SelectListItem { Text = x.FldRemarkTitle, Value = x.FldRemarkId + "" }).ToList();
        }
    }
}
