using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using GomelRectorCouncil.Data;
using GomelRectorCouncil.Models;


namespace GomelRectorCouncil.Controllers
{

    public class HomeController : Controller
    {
        private readonly CouncilDbContext db;
        public HomeController(CouncilDbContext context)
        {
            db=context;
        }
        
        public IActionResult Index()
        {
            //var univ= db.Universities.Include(c => c.Rector);
            var univ= db.Rectors.Include(c => c.University);
            
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
