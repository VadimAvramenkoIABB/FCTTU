using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ContosoUniversity.Data;
using ContosoUniversity.Models;

namespace ContosoUniversity.Pages.Players
{
    public class EditModel : PageModel
    {
        private readonly ContosoUniversity.Data.SchoolContext _context;

        public EditModel(ContosoUniversity.Data.SchoolContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Player Player { get; set; }

        #region snippet_OnGetPost
        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Player = await _context.Players.FindAsync(id);

            if (Player == null)
            {
                return NotFound();
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int id)
        {
            var playerToUpdate = await _context.Players.FindAsync(id);

            if (playerToUpdate == null)
            {
                return NotFound();
            }

            if (await TryUpdateModelAsync<Player>(
                playerToUpdate,
                "player",
                s => s.FirstMidName, s => s.LastName, s => s.ContractDate))
            {
                await _context.SaveChangesAsync();
                return RedirectToPage("./Index");
            }

            return Page();
        }
        #endregion

        private bool PlayerExists(int id)
        {
            return _context.Players.Any(e => e.ID == id);
        }
    }
}
