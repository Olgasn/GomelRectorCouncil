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
using System.IO;

namespace GomelRectorCouncil.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "admin")]
    public class DocumentsController : Controller
    {
        private readonly CouncilDbContext _context;
        private readonly IWebHostEnvironment _environment;
        private readonly IConfiguration _iconfiguration;
        private readonly DocumentExternalFile _externalFile;

        public DocumentsController(CouncilDbContext context, IWebHostEnvironment environment, IConfiguration iconfiguration)
        {
            _context = context;
            _environment = environment;
            _iconfiguration = iconfiguration;
            _externalFile = new DocumentExternalFile(_environment, _iconfiguration);
    
        }

        // GET: Documents
        public async Task<IActionResult> Index()
        {
            var councilDbContext = _context.Documents.Include(d => d.Chairperson).Include(r=>r.Chairperson.Rector);
            return View(await councilDbContext.ToListAsync());
        }

        // GET: Documents/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var document = await _context.Documents
                .Include(d => d.Chairperson)
                .SingleOrDefaultAsync(m => m.DocumentId == id);
            if (document == null)
            {
                return NotFound();
            }

            return View(document);
        }

        // GET: Documents/Create
        public IActionResult Create()
        {
            ViewData["ChairpersonId"] = new SelectList(_context.Chairpersons.Include(r=>r.Rector), "ChairpersonId", "Rector.FullName");
            return View();
        }

        // POST: Documents/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("DocumentId,RegistrationNumber,DocumentName,DocumentDescription,RegistrationDate,DocumentURL,ChairpersonId")] Document document, IFormFile upload)
        {
            if (ModelState.IsValid)
            {
                var documentWithFile = await _externalFile.UploadDocument(document, upload);
                _context.Add(documentWithFile);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewData["ChairpersonId"] = new SelectList(_context.Chairpersons.Include(r=>r.Rector), "ChairpersonId", "Rector.FullName", document.ChairpersonId);
            return View(document);
        }

        // GET: Documents/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var document = await _context.Documents.SingleOrDefaultAsync(m => m.DocumentId == id);
            if (document == null)
            {
                return NotFound();
            }
            ViewData["ChairpersonId"] = new SelectList(_context.Chairpersons.Include(r=>r.Rector), "ChairpersonId", "Rector.FullName", document.ChairpersonId);
            return View(document);
        }

        // POST: Documents/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Document document)
        {
            
            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(document);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DocumentExists(document.DocumentId))
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
            ViewData["ChairpersonId"] = new SelectList(_context.Chairpersons.Include(r=>r.Rector), "ChairpersonId", "Rector.FullName", document.ChairpersonId);
            return View(document);
        }

        // GET: Documents/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var document = await _context.Documents
                .Include(d => d.Chairperson)
                .SingleOrDefaultAsync(m => m.DocumentId == id);
            if (document == null)
            {
                return NotFound();
            }

            return View(document);
        }

        // POST: Documents/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int DocumentId)
        {

            var document = await _context.Documents.SingleOrDefaultAsync(m => m.DocumentId == DocumentId);
            string fullFileName = _environment.WebRootPath + document.DocumentURL; 
            _context.Documents.Remove(document);
            await _context.SaveChangesAsync();
            //Удаление фотографии
            try
            {
                System.IO.File.Delete(fullFileName);
                return RedirectToAction("Index");
            }
            catch (IOException deleteError)
            {
                return View("Message", deleteError.Message);
            }
        }

        private bool DocumentExists(int id)
        {
            return _context.Documents.Any(e => e.DocumentId == id);
        }
    }
}
