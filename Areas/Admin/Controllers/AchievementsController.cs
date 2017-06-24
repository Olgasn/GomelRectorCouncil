using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;
using GomelRectorCouncil.Data;
using GomelRectorCouncil.Areas.Admin.ViewModels;

namespace GomelRectorCouncil.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize (Roles="admin")]
    public class AchievementsController : Controller
    {
        private readonly CouncilDbContext _context;

        public AchievementsController(CouncilDbContext context)
        {
            _context = context;    
        }

        // GET: Achievements
        public IActionResult Index(int? currentYear, int page = 1)
        {
            int pageSize = 10;   // количество элементов на странице
            int currYear = currentYear ?? DateTime.Now.Year;
            var achievements = _context.Achievements
                    .Include(a => a.Indicator)
                    .Include(a => a.Univercity)
                    .Where(t => t.Year == currYear)
                    .OrderBy(c => c.Indicator.IndicatorCode);
            int count = achievements.Count();
            List<int> years = _context.Indicators
                .OrderByDescending(f => f.Year)
                .Select(f => f.Year)
                .ToList();
            years.Insert(0, currYear); years.Insert(0, currYear + 1);
            PageViewModel pageViewModel = new PageViewModel(count, page, pageSize);
            AchievementsViewModel achievementsViewModel = new AchievementsViewModel
            {
                PageViewModel = pageViewModel,
                Achievements = achievements.Skip((page - 1) * pageSize).Take(pageSize).ToList(),
                ListYears = new SelectList(years.Distinct(), currYear)
            };
            return View(achievementsViewModel);            
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


    }
}
