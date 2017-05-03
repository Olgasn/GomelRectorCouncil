using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GomelRectorCouncil.Data;
using GomelRectorCouncil.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;

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
            var univ= db.Universities.Include(c => c.Rector);
            return View(univ);
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
