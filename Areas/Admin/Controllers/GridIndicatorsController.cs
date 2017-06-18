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
using Microsoft.AspNetCore.Authorization;

namespace GomelRectorCouncil.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize (Roles="admin")]
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
        // POST: Indicators
        [HttpPost]
        public async Task<IActionResult> Index(int currentYear)
        {
            List<int> years = _context.Indicators
                .OrderByDescending(f => f.Year)
                .Select(f => f.Year)
                .ToList();
            years.Insert(0, currentYear); years.Insert(0, currentYear + 1);
            SelectList ListYears = new SelectList(years.Distinct(), currentYear);
            if (await DeleteIndicatorsForUniversities(currentYear))
            {
                        if (await PublishIndicatorsForUniversities(currentYear))
                        {

                        };
             
            }

            return View(ListYears);
        }

        public string GetIndicators(int? currentYear, string sidx, string sord, int page, int rows, bool _search, string searchField, string searchOper, string searchString)
        {
            int currYear = currentYear ?? DateTime.Now.Year;

            sord = (sord == null) ? "" : sord;
            int pageIndex = Convert.ToInt32(page) - 1;
            int pageSize = rows;
            var indicators = _context.Indicators.Where(t => t.Year == currYear).Select(
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
                        t.IndicatorDescription
                    });
 
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


        // POST: Indicators/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        public string Create([Bind("IndicatorId1,IndicatorId2,IndicatorId3,IndicatorName,IndicatorUnit,IndicatorType,IndicatorDescription,Year")] Indicator indicator)
        {

            string msg ="Модель не прошла валидацию";
            if (ModelState.IsValid)
            {
                _context.Add(indicator);
                _context.SaveChanges();
                msg = "Сохранено";
                return msg;
            }
            return msg;
        }

        
        // POST: Indicators/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.

        public string Edit(Indicator indicator)
        {
            string msg ="Модель не прошла валидацию";

            if (ModelState.IsValid)
            {
                try
                {
                    msg = "Сохранено";
                    _context.Update(indicator);
                    _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!IndicatorExists(indicator.IndicatorId))
                    {
                        msg = "Не найдено";
                        return msg;
                    }
                    else
                    {
                        throw;
                    }
                }
                return msg;
            }
            return msg;
        }



        // POST: Indicators/Delete/5
        public string Delete(int id)
        {
            var indicator = _context.Indicators.SingleOrDefault(m => m.IndicatorId == id);
            _context.Indicators.Remove(indicator);
            _context.SaveChangesAsync();
            return "Запись удалена";
        }

        private bool IndicatorExists(int id)
        {
            return _context.Indicators.Any(e => e.IndicatorId == id);
        }
        private async Task<bool> PublishIndicatorsForUniversities (int currYear)
        {
            bool publishResult = false;
            List<int> indicators = _context.Indicators.Where(y => y.Year == currYear).Select(id=>id.IndicatorId).ToList();
            List<int> universities=_context.Universities.Select(u=>u.UniversityId).ToList();

            foreach (int university in universities)
            {
                foreach (int indicator in indicators)
                {
                    Achievement achievement = new Achievement
                    {
                    Year = currYear,
                    IndicatorId = indicator,
                    UnivercityId = university
                    };
                    _context.Add(achievement);

                }
            }
            await _context.SaveChangesAsync();
            publishResult = true;

            return publishResult;
        }
        private async Task<bool> DeleteIndicatorsForUniversities(int Year)
        {
            bool deleteResult = false;
            var achievements =  _context.Achievements.Where(m => m.Year == Year);
            if (achievements.Count()>0)
            {
                _context.Achievements.RemoveRange(achievements);
                await _context.SaveChangesAsync();
            }
            deleteResult = true;

            return deleteResult;
        }
    }
}
