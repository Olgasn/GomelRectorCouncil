using GomelRectorCouncil.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace GomelRectorCouncil.Controllers
{
    [Authorize(Roles = "user")]
    public class DocumentsController : Controller
    {
        private readonly CouncilDbContext _context;

        public DocumentsController(CouncilDbContext context)
        {
            _context = context;
        }

        // GET: Documents
        public async Task<IActionResult> Index()
        {
            var councilDbContext = _context.Documents.Include(d => d.Chairperson).Include(r => r.Chairperson.Rector);
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


    }
}
