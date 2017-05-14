using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using GomelRectorCouncil.Data;
using GomelRectorCouncil.Models;

namespace GomelRectorCouncil.Controllers
{
    public class AchievementsController : Controller
    {
        private readonly CouncilDbContext _context;

        public AchievementsController(CouncilDbContext context)
        {
            _context = context;    
        }

        // GET: Achievements
        public async Task<IActionResult> Index()
        {
            var councilDbContext = _context.Achievements.Include(a => a.Indicator).Include(a => a.Univercity);
            return View(await councilDbContext.ToListAsync());
        }

        // GET: Achievements/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var achievement = await _context.Achievements
                .Include(a => a.Indicator)
                .Include(a => a.Univercity)
                .SingleOrDefaultAsync(m => m.AchievementId == id);
            if (achievement == null)
            {
                return NotFound();
            }

            return View(achievement);
        }

        // GET: Achievements/Create
        public IActionResult Create()
        {
            ViewData["IndicatorId"] = new SelectList(_context.Indicators, "IndicatorId", "IndicatorId");
            ViewData["UnivercityId"] = new SelectList(_context.Universities, "UniversityId", "UniversityId");
            return View();
        }

        // POST: Achievements/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("AchievementId,IndicatorId,UnivercityId,IndicatorValue")] Achievement achievement)
        {
            if (ModelState.IsValid)
            {
                _context.Add(achievement);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewData["IndicatorId"] = new SelectList(_context.Indicators, "IndicatorId", "IndicatorId", achievement.IndicatorId);
            ViewData["UnivercityId"] = new SelectList(_context.Universities, "UniversityId", "UniversityId", achievement.UnivercityId);
            return View(achievement);
        }

        // GET: Achievements/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var achievement = await _context.Achievements.SingleOrDefaultAsync(m => m.AchievementId == id);
            if (achievement == null)
            {
                return NotFound();
            }
            ViewData["IndicatorId"] = new SelectList(_context.Indicators, "IndicatorId", "IndicatorId", achievement.IndicatorId);
            ViewData["UnivercityId"] = new SelectList(_context.Universities, "UniversityId", "UniversityId", achievement.UnivercityId);
            return View(achievement);
        }

        // POST: Achievements/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit([Bind("AchievementId,IndicatorId,UnivercityId,IndicatorValue")] Achievement achievement)
        {
            

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(achievement);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AchievementExists(achievement.AchievementId))
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
            ViewData["IndicatorId"] = new SelectList(_context.Indicators, "IndicatorId", "IndicatorId", achievement.IndicatorId);
            ViewData["UnivercityId"] = new SelectList(_context.Universities, "UniversityId", "UniversityId", achievement.UnivercityId);
            return View(achievement);
        }

        // GET: Achievements/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var achievement = await _context.Achievements
                .Include(a => a.Indicator)
                .Include(a => a.Univercity)
                .SingleOrDefaultAsync(m => m.AchievementId == id);
            if (achievement == null)
            {
                return NotFound();
            }

            return View(achievement);
        }

        // POST: Achievements/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var achievement = await _context.Achievements.SingleOrDefaultAsync(m => m.AchievementId == id);
            _context.Achievements.Remove(achievement);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        private bool AchievementExists(int id)
        {
            return _context.Achievements.Any(e => e.AchievementId == id);
        }
    }
}
