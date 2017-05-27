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
            //var univ= _context.Rectors.Include(c => c.University).OrderBy(i=>i.University.UniversityName);            
            return View(univ);
        }

        public IActionResult Create(University univ)
        {
            
            return View();
        }         

        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Error()
        {
            return View();
        }
    }
}
