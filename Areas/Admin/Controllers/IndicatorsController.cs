using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using GomelRectorCouncil.Data;
using GomelRectorCouncil.Models;
using GomelRectorCouncil.Areas.Admin.ViewModels;

namespace GomelRectorCouncil.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class IndicatorsController : Controller
    {
        private readonly CouncilDbContext _context;

        public IndicatorsController(CouncilDbContext context)
        {
            _context = context;    
        }

        // GET: Indicators
        public IActionResult Index(int? currentYear)
        {
            int currYear = currentYear??DateTime.Now.Year;
            List<int> years=_context.Indicators
                .OrderByDescending(f=>f.Year)
                .Select(f=>f.Year)
                .ToList();
             years.Insert(0,currYear); years.Insert(0,currYear+1);

             IndicatorsViewModel indicators = new IndicatorsViewModel
            {
                Indicators = _context.Indicators.Where(t => t.Year == currYear).ToList(),
                ListYears=new SelectList(years.Distinct(),currYear)
            };
            return View(indicators);
        }



        // GET: Indicators/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var indicator = await _context.Indicators
                .SingleOrDefaultAsync(m => m.IndicatorId == id);
            if (indicator == null)
            {
                return NotFound();
            }

            return View(indicator);
        }

        // GET: Indicators/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Indicators/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IndicatorId,IndicatorId1,IndicatorId2,IndicatorId3,IndicatorName,IndicatorUnit,IndicatorType,IndicatorDescription,Year")] Indicator indicator)
        {
            if (ModelState.IsValid)
            {
                _context.Add(indicator);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(indicator);
        }

        // GET: Indicators/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var indicator = await _context.Indicators.SingleOrDefaultAsync(m => m.IndicatorId == id);
            if (indicator == null)
            {
                return NotFound();
            }
            return View(indicator);
        }

        // POST: Indicators/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit([Bind("IndicatorId,IndicatorId1,IndicatorId2,IndicatorId3,IndicatorName,IndicatorUnit,IndicatorType,IndicatorDescription,Year")] Indicator indicator)
        {
            
            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(indicator);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!IndicatorExists(indicator.IndicatorId))
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
            return View(indicator);
        }

        // GET: Indicators/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var indicator = await _context.Indicators
                .SingleOrDefaultAsync(m => m.IndicatorId == id);
            if (indicator == null)
            {
                return NotFound();
            }

            return View(indicator);
        }

        // POST: Indicators/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var indicator = await _context.Indicators.SingleOrDefaultAsync(m => m.IndicatorId == id);
            _context.Indicators.Remove(indicator);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        private bool IndicatorExists(int id)
        {
            return _context.Indicators.Any(e => e.IndicatorId == id);
        }
    }
}
