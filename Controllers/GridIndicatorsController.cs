using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using GomelRectorCouncil.Data;
using GomelRectorCouncil.Models;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;

namespace GomelRectorCouncil.Controllers
{
    public class GridIndicatorsController : Controller
    {
        private readonly CouncilDbContext _context;

        public GridIndicatorsController(CouncilDbContext context)
        {
            _context = context;    
        }

        // GET: Indicators
        public IActionResult Index(int? currentYear)
        {
            int currYear = currentYear ?? DateTime.Now.Year;
            List<int> years = _context.Indicators
                .OrderByDescending(f => f.Year)
                .Select(f => f.Year)
                .ToList();
            years.Insert(0, currYear); years.Insert(0, currYear + 1);
            SelectList ListYears = new SelectList(years.Distinct(), currYear);

            return View(ListYears);
        }

        public string GetIndicators(string sidx, string sord, int page, int rows, bool _search, string searchField, string searchOper, string searchString)
        {

            string currentYear = Request.Form["currentYear"].ToString();
            sord = (sord == null) ? "" : sord;
            int pageIndex = Convert.ToInt32(page) - 1;
            int pageSize = rows;
            var indicators = _context.Indicators.Select(
                    t => new
                    {
                        t.IndicatorId,
                        t.IndicatorCode,
                        t.IndicatorId1,
                        t.IndicatorId2,
                        t.IndicatorId3,
                        t.IndicatorName,
                        t.IndicatorType,
                        t.IndicatorUnit,
                        t.IndicatorDescription,
                        t.Year
                    });

            if (_search)
            {
                switch (searchField)
                {
                    case "Year":
                        indicators = indicators.Where(t => t.Year.ToString().Contains(searchString));
                        break;

                }
            }

            int totalRecords = indicators.Count();
            var totalPages = (int)Math.Ceiling((float)totalRecords / (float)rows);
            if (sord.ToUpper() == "DESC")
            {
                indicators = indicators.OrderByDescending(t => t.IndicatorCode);
                indicators = indicators.Skip(pageIndex * pageSize).Take(pageSize);
            }
            else
            {
                indicators = indicators.OrderBy(t => t.IndicatorCode);
                indicators = indicators.Skip(pageIndex * pageSize).Take(pageSize);
            }
            //var jsonData = new
            //{
            //    total = totalPages,
            //    page,
            //    records = totalRecords,
            //    rows = indicators.ToList()
            //};
            var jsonData = new
            {
                total = totalPages,
                page,
                records = totalRecords,
                rows = indicators
            };


             
            return JsonConvert.SerializeObject(jsonData);
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
        public IActionResult Create(int id)
        {
            ViewData["Year"] = id;

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
