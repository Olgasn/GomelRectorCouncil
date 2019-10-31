using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using GomelRectorCouncil.Data;
using GomelRectorCouncil.Models;
using System.Linq;

namespace GomelRectorCouncil.Controllers
{

    public class HomeController : Controller
    {
        private readonly CouncilDbContext _context;
        public HomeController(CouncilDbContext context)
        {
            _context=context;
        }
        
        public IActionResult Index()
        {
            var univ = _context.Universities.Include(c => c.Rector).OrderBy(i => i.UniversityName);

            ViewData["Title"] = "СРГО";
            if (_context.Chairpersons.Count()>0)
            {
            ViewData["ChairpersonName"] = _context.Chairpersons
                .Where(s => s.StopDate == null)
                .Select(s => s.Rector.FullName)
                .Last()
                .ToString();
            };

            return View("Index",univ);
        }

        public IActionResult Create(University univ)
        {
            
            return View();
        }         

        public IActionResult About()
        {
            ViewData["Message"] = "Сайт Совета ректоров Гомельской области";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Контакты";

            return View();
        }

        public IActionResult Error()
        {
            return View();
        }
    }
}
