using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using GomelRectorCouncil.Data;
using GomelRectorCouncil.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using System.IO;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;

namespace GomelRectorCouncil.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize (Roles="admin")]
    public class UniversitiesController : Controller
    {
        private readonly CouncilDbContext _context;
        private IHostingEnvironment _environment;
        IConfiguration _iconfiguration;
        public UniversitiesController(CouncilDbContext context, IHostingEnvironment environment, IConfiguration iconfiguration)
        {
            _context = context;
            _environment = environment;
            _iconfiguration = iconfiguration;
        }

        // GET: Universities
        public async Task<IActionResult> Index()
        {
            return View(await _context.Universities.ToListAsync());
        }

        // GET: Universities/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var university = await _context.Universities
                .SingleOrDefaultAsync(m => m.UniversityId == id);
            if (university == null)
            {
                return NotFound();
            }

            return View(university);
        }

        // GET: Universities/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Universities/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("UniversityId,UniversityName,Address,Website,Logo")] University university, IFormFile upload)
        {
            if (ModelState.IsValid)
            {
                _context.Add(university);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(university);
        }

        // GET: Universities/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var university = await _context.Universities.SingleOrDefaultAsync(m => m.UniversityId == id);
            if (university == null)
            {
                return NotFound();
            }
            return View(university);
        }

        // POST: Universities/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(University university, IFormFile upload)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    if (upload!=null)
                    {
                        string uploadfileName = _iconfiguration.GetSection("Paths").GetSection("PathToLogos").Value+university.UniversityId.ToString()+ upload.FileName;
                        university.Logo = uploadfileName;
                        uploadfileName = _environment.WebRootPath + uploadfileName;
                        using (var fileStream = new FileStream(uploadfileName, FileMode.Create))
                        {
                            await upload.CopyToAsync(fileStream);
                        }

                    }

                    _context.Update(university);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!UniversityExists(university.UniversityId))
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
            return View(university);
        }

        // GET: Universities/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var university = await _context.Universities
                .SingleOrDefaultAsync(m => m.UniversityId == id);
            if (university == null)
            {
                return NotFound();
            }

            return View(university);
        }

        // POST: Universities/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var university = await _context.Universities.SingleOrDefaultAsync(m => m.UniversityId == id);
            _context.Universities.Remove(university);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        private bool UniversityExists(int id)
        {
            return _context.Universities.Any(e => e.UniversityId == id);
        }
    }
}
