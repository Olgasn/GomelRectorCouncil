using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using GomelRectorCouncil.Data;
using GomelRectorCouncil.Models;

namespace GomelRectorCouncil.Areas.Admin.Controllers

{
    [Area("Admin")]
    public class ChairpersonsController : Controller
    {
        private readonly CouncilDbContext _context;

        public ChairpersonsController(CouncilDbContext context)
        {
            _context = context;    
        }

        // GET: Chairpersons
        public async Task<IActionResult> Index()
        {
            var councilDbContext = _context.Chairpersons.Include(c => c.Rector);
            return View(await councilDbContext.ToListAsync());
        }

        // GET: Chairpersons/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var chairperson = await _context.Chairpersons
                .Include(c => c.Rector)
                .SingleOrDefaultAsync(m => m.ChairpersonId == id);
            if (chairperson == null)
            {
                return NotFound();
            }

            return View(chairperson);
        }

        // GET: Chairpersons/Create
        public IActionResult Create()
        {
            ViewData["RectorId"] = new SelectList(_context.Rectors, "RectorId", "FirstMidName");
            return View();
        }

        // POST: Chairpersons/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ChairpersonId,StartDate,StopDate,RectorId")] Chairperson chairperson)
        {
            if (ModelState.IsValid)
            {
                _context.Add(chairperson);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewData["RectorId"] = new SelectList(_context.Rectors, "RectorId", "FirstMidName", chairperson.RectorId);
            return View(chairperson);
        }

        // GET: Chairpersons/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var chairperson = await _context.Chairpersons.SingleOrDefaultAsync(m => m.ChairpersonId == id);
            if (chairperson == null)
            {
                return NotFound();
            }
            ViewData["RectorId"] = new SelectList(_context.Rectors, "RectorId", "FirstMidName", chairperson.RectorId);
            return View(chairperson);
        }

        // POST: Chairpersons/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit([Bind("ChairpersonId,StartDate,StopDate,RectorId")] Chairperson chairperson)
        {
            
            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(chairperson);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ChairpersonExists(chairperson.ChairpersonId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction("Index");
            }
            ViewData["RectorId"] = new SelectList(_context.Rectors, "RectorId", "FirstMidName", chairperson.RectorId);
            return View(chairperson);
        }

        // GET: Chairpersons/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var chairperson = await _context.Chairpersons
                .Include(c => c.Rector)
                .SingleOrDefaultAsync(m => m.ChairpersonId == id);
            if (chairperson == null)
            {
                return NotFound();
            }

            return View(chairperson);
        }

        // POST: Chairpersons/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var chairperson = await _context.Chairpersons.SingleOrDefaultAsync(m => m.ChairpersonId == id);
            _context.Chairpersons.Remove(chairperson);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        private bool ChairpersonExists(int id)
        {
            return _context.Chairpersons.Any(e => e.ChairpersonId == id);
        }
    }
}
