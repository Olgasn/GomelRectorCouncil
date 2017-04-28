using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GomelRectorCouncil.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GomelRectorCouncil.Controllers
{       
   [Authorize]
    public class UserController : Controller
    {
        private readonly ApplicationDbContext _context;
        public UserController(ApplicationDbContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            ViewBag.Users=_context.Users.ToList();
            return View();
        }

        

        public IActionResult Error()
        {
            return View();
        }
    }
}
