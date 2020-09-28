using ContosoUniversity.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace ContosoUniversity.Pages.Teams
{
    public class DetailsModel : PageModel
    {
        private readonly ContosoUniversity.Data.SchoolContext _context;

        public DetailsModel(ContosoUniversity.Data.SchoolContext context)
        {
            _context = context;
        }

        public Team Team { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Team = await _context.Teams
                 .AsNoTracking()
                 .Include(c => c.Department)
                 .FirstOrDefaultAsync(m => m.TeamID == id);

            if (Team == null)
            {
                return NotFound();
            }
            return Page();
        }
    }
}

