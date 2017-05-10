using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using GomelRectorCouncil.Models;
using System.Threading.Tasks;
using GomelRectorCouncil.Areas.Admin.ViewModels;
using GomelRectorCouncil.Data;

namespace GomelRectorCouncil.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class UserController : Controller
    {
        UserManager<ApplicationUser> _userManager;
        private readonly CouncilDbContext db;


        public UserController(UserManager<ApplicationUser> userManager, CouncilDbContext context)
        {
            _userManager = userManager;
            db = context;
        }

        public IActionResult Index()
        {
            var Users = _userManager.Users;
            var Universities = db.Universities;

            var leftunion = from u in Users join t in Universities 
                            on u.UniversityId equals t.UniversityId
                            into a from b in a.DefaultIfEmpty()
                            select new
                                {
                                    u.Id,
                                    u.UserName,
                                    Name = b.UniversityName,
                                    u.Email,
                                    u.RegistrationDate
                                 };
            UserViewModel vm = new UserViewModel();
            

            return View(_userManager.Users.ToList());
        }

        public IActionResult Create() => View();

        [HttpPost]
        public async Task<IActionResult> Create(CreateUserViewModel model)
        {
            if (ModelState.IsValid)
            {
                ApplicationUser user = new ApplicationUser { Email = model.Email, UserName = model.Email, RegistrationDate = model.RegistrationDate };
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
            EditUserViewModel model = new EditUserViewModel { Id = user.Id, Email = user.Email, RegistrationDate = user.RegistrationDate };
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
                    user.UserName = model.Email;
                    user.RegistrationDate = model.RegistrationDate;

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
