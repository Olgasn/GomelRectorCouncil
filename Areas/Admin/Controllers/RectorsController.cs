using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using GomelRectorCouncil.Data;
using GomelRectorCouncil.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Http;

namespace GomelRectorCouncil.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize (Roles="admin")]
    public class RectorsController : Controller
    {
        private readonly CouncilDbContext _context;
        private IHostingEnvironment _environment;
        private IConfiguration _iconfiguration;
        private RectorExternalFile _externalFile;

        public RectorsController(CouncilDbContext context, IHostingEnvironment environment, IConfiguration iconfiguration)
        {
            _context = context;    
        }

        // GET: Rectors
        public async Task<IActionResult> Index()
        {
            var councilDbContext = _context.Rectors.Include(r => r.University);
            return View(await councilDbContext.ToListAsync());
        }

        // GET: Rectors/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var rector = await _context.Rectors
                .Include(r => r.University)
                .SingleOrDefaultAsync(m => m.RectorId == id);
            if (rector == null)
            {
                return NotFound();
            }

            return View(rector);
        }

        // GET: Rectors/Create
        public IActionResult Create()
        {
            ViewData["UniversityId"] = new SelectList(_context.Universities, "UniversityId", "UniversityName");
            return View();
        }

        // POST: Rectors/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("RectorId,LastName,FirstMidName,MiddleName,Email,Photo,UniversityId")] Rector rector,  IFormFile upload)
        {
            if (ModelState.IsValid)
            {
                var rectorWithPhoto = await _externalFile.UploadRectorPhoto(rector, upload);
                _context.Add(rectorWithPhoto);              
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewData["UniversityId"] = new SelectList(_context.Universities, "UniversityId", "UniversityName", rector.UniversityId);
            return View(rector);
        }

        // GET: Rectors/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var rector = await _context.Rectors.SingleOrDefaultAsync(m => m.RectorId == id);
            if (rector == null)
            {
                return NotFound();
            }
            ViewData["UniversityId"] = new SelectList(_context.Universities, "UniversityId", "UniversityName", rector.UniversityId);
            return View(rector);
        }

        // POST: Rectors/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Rector rector, IFormFile upload)
        {
            
            if (ModelState.IsValid)
            {
                try
                {
                    rector = await _externalFile.UploadRectorPhoto(rector, upload);                    
                    _context.Update(rector);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!RectorExists(rector.RectorId))
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
            ViewData["UniversityId"] = new SelectList(_context.Universities, "UniversityId", "UniversityName", rector.UniversityId);
            return View(rector);
        }

        // GET: Rectors/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var rector = await _context.Rectors
                .Include(r => r.University)
                .SingleOrDefaultAsync(m => m.RectorId == id);
            if (rector == null)
            {
                return NotFound();
            }

            return View(rector);
        }

        // POST: Rectors/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var rector = await _context.Rectors.SingleOrDefaultAsync(m => m.RectorId == id);
            _context.Rectors.Remove(rector);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        private bool RectorExists(int id)
        {
            return _context.Rectors.Any(e => e.RectorId == id);
        }
    }
}
