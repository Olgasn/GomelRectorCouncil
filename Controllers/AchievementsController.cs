using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;
using GomelRectorCouncil.Data;
using GomelRectorCouncil.ViewModels;

namespace GomelRectorCouncil.Controllers
{
    [Authorize(Roles = "user, admin")]
    public class AchievementsController : Controller
    {
        private readonly CouncilDbContext _context;
        private readonly ApplicationDbContext _contextUser;


        public AchievementsController(CouncilDbContext context, ApplicationDbContext contextUser)
        {
            _context = context;
            _contextUser = contextUser;


        }

        // GET: Achievements
        public IActionResult Index(int? currentYear, int page = 1, SortState sortOrder = SortState.IndicatorCodeAsc)
        {
            int pageSize = 10;   // количество элементов на странице
            int currYear = currentYear ?? DateTime.Now.Year;
            int univercityId = GetUniversiryId();
            if (univercityId == 0)
            {
                string message = "Текущий пользователь не прив¤зан к университету";
                return View("Message", message);
            }
            var achievements = _context.Achievements
                    .Include(a => a.Indicator)
                    .Include(a => a.Univercity)
                    .Where(t => (t.Year == currYear) & (t.UnivercityId == univercityId))
                    .OrderBy(s => s.Indicator.IndicatorCode);


            // сортировка
            switch (sortOrder)
            {
                case SortState.IndicatorCodeDesc:
                    achievements = achievements.OrderByDescending(s => s.Indicator.IndicatorCode);
                    break;
                default:
                    achievements = achievements.OrderBy(s => s.Indicator.IndicatorCode);
                    break;
            }

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
                SortViewModel = new SortViewModel(sortOrder),
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
        private int GetUniversiryId()
        {

            int[] universiryId = _contextUser.Users.Where(t => t.UserName == User.Identity.Name).Select(t => t.UniversityId).ToArray<int>();
            return universiryId[0];
        }


    }
}
