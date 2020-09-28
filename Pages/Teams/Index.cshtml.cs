using ContosoUniversity.Models;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ContosoUniversity.Pages.Teams
{
    public class IndexModel : PageModel
    {
        private readonly ContosoUniversity.Data.SchoolContext _context;

        public IndexModel(ContosoUniversity.Data.SchoolContext context)
        {
            _context = context;
        }

        public IList<Team> Teams { get; set; }

        public async Task OnGetAsync()
        {
            Teams = await _context.Teams
                .Include(c => c.Department)
                .AsNoTracking()
                .ToListAsync();
        }
    }
}

