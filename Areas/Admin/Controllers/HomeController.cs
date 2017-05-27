using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using GomelRectorCouncil.Models;
using System.Threading.Tasks;
using GomelRectorCouncil.Areas.Admin.ViewModels;
using GomelRectorCouncil.Data;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;

namespace GomelRectorCouncil.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class HomeController : Controller
    {
        UserManager<ApplicationUser> _userManager;
        private readonly CouncilDbContext _context;


        public HomeController(UserManager<ApplicationUser> userManager, CouncilDbContext context)
        {
            _userManager = userManager;
            _context = context;
        }

        public IActionResult Index()
        {
            var users = _userManager.Users.OrderBy(user => user.Id);
            var universities = _context.Universities;

            List<UserViewModel> userViewModel = new List<UserViewModel>();


            string uname = "";
            foreach (var user in users)
            {
                var universityName = (from un in universities
                                      where (un.UniversityId == user.UniversityId)
                                      select un.UniversityName);
                if (universityName.Count()>0)
                {
                    uname = universityName.FirstOrDefault().ToString() ?? "";
                }
                userViewModel.Add(
                    new UserViewModel
                    {
                        Id = user.Id,
                        UserName = user.UserName,
                        Email = user.Email,
                        RegistrationDate = user.RegistrationDate,
                        UniversityName = uname
                    });            
                


            }

            

            
            return View(userViewModel);
        }

        public IActionResult Create()
        {
            ViewData["UniversityId"] = new SelectList(_context.Universities, "UniversityId", "UniversityName");
            return View();

        }
        

        [HttpPost]
        public async Task<IActionResult> Create(CreateUserViewModel model)
        {
            if (ModelState.IsValid)
            {
                ApplicationUser user = new ApplicationUser
                {
                    Email = model.Email,
                    UserName = model.UserName,
                    RegistrationDate = model.RegistrationDate,
                    UniversityId=model.UniversityId
                };
                var result = await _userManager.CreateAsync(user, model.Password);
                if (result.Succeeded)
                {
                    return RedirectToAction("Index");
                }
                else
                {
                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError(string.Empty, error.Description);
                    }
                }
            }
            return View(model);
        }

        public async Task<IActionResult> Edit(string id)
        {
            ApplicationUser user = await _userManager.FindByIdAsync(id);
            if (user == null)
            {
                return NotFound();
            }
            EditUserViewModel model = new EditUserViewModel
            {
                Id=user.Id,
                Email = user.Email,
                UserName = user.UserName,
                RegistrationDate = user.RegistrationDate,
                UniversityId = user.UniversityId
            };
            ViewData["UniversityId"] = new SelectList(_context.Universities, "UniversityId", "UniversityName", model.UniversityId);
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(EditUserViewModel model)
        {
            if (ModelState.IsValid)
            {
                ApplicationUser user = await _userManager.FindByIdAsync(model.Id);
                if (user != null)
                {
                    user.Email = model.Email;
                    user.UserName = model.UserName;
                    user.RegistrationDate = model.RegistrationDate;
                    user.UniversityId = model.UniversityId;


                    var result = await _userManager.UpdateAsync(user);
                    if (result.Succeeded)
                    {
                        return RedirectToAction("Index");
                    }
                    else
                    {
                        foreach (var error in result.Errors)
                        {
                            ModelState.AddModelError(string.Empty, error.Description);
                        }
                    }
                }
            }
            return View(model);
        }

        [HttpPost]
        public async Task<ActionResult> Delete(string id)
        {
            ApplicationUser user = await _userManager.FindByIdAsync(id);
            if (user != null)
            {
                IdentityResult result = await _userManager.DeleteAsync(user);
            }
            return RedirectToAction("Index");
        }
    }
}
