using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;
using GomelRectorCouncil.Data;
using GomelRectorCouncil.Models;
using Newtonsoft.Json;

namespace GomelRectorCouncil.Controllers
{
    [Authorize(Roles = "user, admin")]
    public class GridAchievementsController : Controller
    {

        private readonly CouncilDbContext _context;
        private readonly ApplicationDbContext _contextUser;


        public GridAchievementsController(CouncilDbContext context, ApplicationDbContext contextUser)
        {
            _context = context;
            _contextUser = contextUser;


        }

        // GET: Achievements
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
        public string GetAchievements(int? currentYear, string sidx, string sord, int page, int rows, bool _search, string searchField, string searchOper, string searchString)
        {
            int currYear = currentYear ?? DateTime.Now.Year;
            int univercityId = GetUniversiryId();
            if (univercityId == 0)
            {
                string message = "Текущий пользователь не привязан к университету";
                return message;
            }
            sord = (sord == null) ? "" : sord;
            int pageIndex = Convert.ToInt32(page) - 1;
            int pageSize = rows;
            var achievements = _context.Achievements.Where(t => (t.Year == currYear)&(t.UnivercityId== univercityId)).Select(
                    t => new
                    {
                        t.AchievementId,
                        t.Indicator.IndicatorCode,
                        t.Indicator.IndicatorName,
                        t.IndicatorValue
                    });

            int totalRecords = achievements.Count();
            var totalPages = (int)Math.Ceiling((float)totalRecords / (float)rows);
            if (sord.ToUpper() == "DESC")
            {
                achievements = achievements.OrderByDescending(t => t.IndicatorCode);
                achievements = achievements.Skip(pageIndex * pageSize).Take(pageSize);
            }
            else
            {
                achievements = achievements.OrderBy(t => t.IndicatorCode);
                achievements = achievements.Skip(pageIndex * pageSize).Take(pageSize);
            }
            var jsonData = new
            {
                total = totalPages,
                page,
                records = totalRecords,
                rows = achievements
            };

            return JsonConvert.SerializeObject(jsonData);
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
            ViewData["IndicatorId"] = new SelectList(_context.Indicators, "IndicatorId", "IndicatorName", achievement.IndicatorId);
            ViewData["UnivercityId"] = new SelectList(_context.Universities, "UniversityId", "UniversityName", achievement.UnivercityId);
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
            ViewData["IndicatorId"] = new SelectList(_context.Indicators, "IndicatorId", "IndicatorName", achievement.IndicatorId);
            ViewData["UnivercityId"] = new SelectList(_context.Universities, "UniversityId", "UniversityName", achievement.UnivercityId);
            return View(achievement);
        }


        private bool AchievementExists(int id)
        {
            return _context.Achievements.Any(e => e.AchievementId == id);
        }
        private int GetUniversiryId()
        {

            int[] universiryId = _contextUser.Users.Where(t => t.UserName == User.Identity.Name).Select(t => t.UniversityId).ToArray<int>();
            return universiryId[0];
        }
    }
}
