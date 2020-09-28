using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using ContosoUniversity.Data;
using ContosoUniversity.Models;

namespace ContosoUniversity.Pages.Players
{
    public class DetailsModel : PageModel
    {
        private readonly ContosoUniversity.Data.SchoolContext _context;

        public DetailsModel(ContosoUniversity.Data.SchoolContext context)
        {
            _context = context;
        }

        public Player Player { get; set; }

        #region snippet_OnGetAsync
        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Player = await _context.Players
                .Include(s => s.Contracts)
                .ThenInclude(e => e.Team)
                .AsNoTracking()
                .FirstOrDefaultAsync(m => m.ID == id);

            if (Player == null)
            {
                return NotFound();
            }
            return Page();
        }
        #endregion
    }
}
