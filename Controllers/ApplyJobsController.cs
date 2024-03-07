using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using FPTJOB.Models;
using Microsoft.AspNetCore.Authorization;

namespace FPTJOB.Controllers
{
    public class ApplyJobsController : Controller
    {
        private readonly DBMyContext _context;

        public ApplyJobsController(DBMyContext context)
        {
            _context = context;
        }

        // GET: ApplyJobs
        public async Task<IActionResult> Index(int id)
        {
            var dBMyContext = _context.ApplyJobs.Include(p => p.ObjJob).Include(p => p.ObjProfile).Where(p => p.JobId == id);
            return View(await dBMyContext.ToListAsync());
        }

        // GET: ApplyJobs/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.ApplyJobs == null)
            {
                return NotFound();
            }

            var applyJob = await _context.ApplyJobs
                .Include(a => a.ObjJob)
                .Include(a => a.ObjProfile)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (applyJob == null)
            {
                return NotFound();
            }

            return View(applyJob);
        }

        // GET: ApplyJobs/Create
        public IActionResult Create(int id)
        {
            ApplyJob aj = new ApplyJob();
            aj.JobId = id;
            aj.RegDate = DateTime.Now;
            aj.ProfileId = _context.Profiles.Where(p => p.UserId == User.Identity.Name).FirstOrDefault().Id;
            _context.Add(aj);
            _context.SaveChanges();

            return RedirectToAction("ListJob","Jobs");
        }

        // POST: ApplyJobs/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,RegDate,ProfileId,JobId")] ApplyJob applyJob)
        {
            if (ModelState.IsValid)
            {
                _context.Add(applyJob);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["JobId"] = new SelectList(_context.Job, "Id", "Id", applyJob.JobId);
            ViewData["ProfileId"] = new SelectList(_context.Profiles, "Id", "Id", applyJob.ProfileId);
            return View(applyJob);
        }

        // GET: ApplyJobs/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.ApplyJobs == null)
            {
                return NotFound();
            }

            var applyJob = await _context.ApplyJobs.FindAsync(id);
            if (applyJob == null)
            {
                return NotFound();
            }
            ViewData["JobId"] = new SelectList(_context.Job, "Id", "Id", applyJob.JobId);
            ViewData["ProfileId"] = new SelectList(_context.Profiles, "Id", "Id", applyJob.ProfileId);
            return View(applyJob);
        }

        // POST: ApplyJobs/Edit/5
        [Authorize(Roles = "Admin")]

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,RegDate,ProfileId,JobId")] ApplyJob applyJob)
        {
            if (id != applyJob.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(applyJob);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ApplyJobExists(applyJob.Id))
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
            ViewData["JobId"] = new SelectList(_context.Job, "Id", "Id", applyJob.JobId);
            ViewData["ProfileId"] = new SelectList(_context.Profiles, "Id", "Id", applyJob.ProfileId);
            return View(applyJob);
        }

        // GET: ApplyJobs/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.ApplyJobs == null)
            {
                return NotFound();
            }

            var applyJob = await _context.ApplyJobs
                .Include(a => a.ObjJob)
                .Include(a => a.ObjProfile)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (applyJob == null)
            {
                return NotFound();
            }

            return View(applyJob);
        }

        // POST: ApplyJobs/Delete/5
        [Authorize(Roles = "Admin")]

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.ApplyJobs == null)
            {
                return Problem("Entity set 'DBMyContext.ApplyJobs'  is null.");
            }
            var applyJob = await _context.ApplyJobs.FindAsync(id);
            if (applyJob != null)
            {
                _context.ApplyJobs.Remove(applyJob);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ApplyJobExists(int id)
        {
          return (_context.ApplyJobs?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
