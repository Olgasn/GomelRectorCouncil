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
using GomelRectorCouncil.Areas.Admin.ViewModels;

namespace GomelRectorCouncil.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize (Roles="admin")]
    public class IndicatorsController : Controller
    {
        private readonly CouncilDbContext _context;

        public IndicatorsController(CouncilDbContext context)
        {
            _context = context;    
        }

        // GET: Indicators
        public IActionResult Index(int? currentYear, bool? disableForEdition)
        {
            bool enableForEdition = !(disableForEdition ?? true);
            int currYear = currentYear??DateTime.Now.Year;
            List<int> years=_context.Indicators
                .OrderByDescending(f=>f.Year)
                .Select(f=>f.Year)
                .ToList();
             years.Insert(0,currYear); years.Insert(0,currYear+1);
             var ListYears=new SelectList(years.Distinct(),currYear);
            IndicatorsViewModel indicators = new IndicatorsViewModel()
            {
                Indicators = _context.Indicators.Where(t => t.Year == currYear).ToList(),
                ListYears=new SelectList(years.Distinct(),currYear),
                EnableForEdition = enableForEdition

            };
            return View(indicators);
        }
        // POST: Indicators
        [HttpPost]
        public async Task<IActionResult> Index(int currentYear, bool? disableForEdition, string action)
        {
            bool enableForEdition = !(disableForEdition ?? true);

            List<int> years=_context.Indicators
                .OrderByDescending(f=>f.Year)
                .Select(f=>f.Year)
                .ToList();
            years.Insert(0,currentYear); years.Insert(0,currentYear+1);
            var ListYears=new SelectList(years.Distinct(),currentYear);
            var indicators = _context.Indicators.Where(t => t.Year == currentYear).ToList();

            switch (action)
            {
                case "FillDataFromLastYear":
                    if (indicators.Count() == 0)
                    {
                        var indicatorsLastYear = _context.Indicators
                            .Where(y => y.Year == (currentYear - 1));
                        foreach (var ind in indicatorsLastYear)
                        {
                            var indicator = new Indicator()
                            {
                                Year = currentYear,
                                IndicatorDescription= ind.IndicatorDescription,
                                IndicatorId1=ind.IndicatorId1,
                                IndicatorId2 = ind.IndicatorId2,
                                IndicatorId3 = ind.IndicatorId3,
                                IndicatorType=ind.IndicatorType,
                                IndicatorName=ind.IndicatorName,
                                IndicatorUnit=ind.IndicatorUnit
                            };
                            indicators.Add(indicator);
                        }
                        _context.AddRange(indicators);
                        await _context.SaveChangesAsync();
                    }
                    else
                    {
                        return View("Message", "¬ текущем году уже загружены данные!");
                    }
                    break;
                case "FillDataForUniversities":
                    //«агрузка набора показателей дл€ университетов на заданный год
                    if (await DeleteIndicatorsForUniversities(currentYear))
                    {
                            if (await PublishIndicatorsForUniversities(currentYear))
                            {
                                return Redirect("~/Admin/Achievements/Index");
                            };
                            return View("Message", "Ќевозможно вставить данные");
                    }
                    return View("Message", "Ќевозможно удалить данные");                   
                default:
                    break;
            }
            IndicatorsViewModel indicatorsViewModel = new IndicatorsViewModel()
            {
                Indicators = indicators,
                ListYears = new SelectList(years.Distinct(), currentYear),
                EnableForEdition = enableForEdition

            };
            return View(indicatorsViewModel);
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
        public async Task<IActionResult> DeleteConfirmed(int IndicatorId)
        {
            var indicator = await _context.Indicators.SingleOrDefaultAsync(m => m.IndicatorId == IndicatorId);
            _context.Indicators.Remove(indicator);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        private bool IndicatorExists(int id)
        {
            return _context.Indicators.Any(e => e.IndicatorId == id);
        }

        // «агрузка показателей дл€ университетов за заданный год
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
        // ”даление данных университетов за заданный год
        private async Task<bool> DeleteIndicatorsForUniversities(int currYear)
        {
            bool deleteResult = false;
            var achievements =  _context.Achievements.Where(m => m.Year == currYear);
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
