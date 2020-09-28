using ContosoUniversity.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace ContosoUniversity.Pages.Coaches
{
    public class DeleteModel : PageModel
    {
        private readonly ContosoUniversity.Data.SchoolContext _context;

        public DeleteModel(ContosoUniversity.Data.SchoolContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Coach Coach { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Coach = await _context.Coaches.FirstOrDefaultAsync(m => m.ID == id);

            if (Coach == null)
            {
                return NotFound();
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Coach coach = await _context.Coaches
                .Include(i => i.TeamAssignments)
                .SingleAsync(i => i.ID == id);

            if (coach == null)
            {
                return RedirectToPage("./Index");
            }

            var departments = await _context.Departments
                .Where(d => d.CoachID == id)
                .ToListAsync();
            departments.ForEach(d => d.CoachID = null);

            _context.Coaches.Remove(coach);

            await _context.SaveChangesAsync();
            return RedirectToPage("./Index");
        }
    }
}
