using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using ContosoUniversity.Data;
using ContosoUniversity.Models;

namespace ContosoUniversity.Pages.Players
{
    public class CreateModel : PageModel
    {
        private readonly ContosoUniversity.Data.SchoolContext _context;

        public CreateModel(ContosoUniversity.Data.SchoolContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        [BindProperty]
        public Player Player { get; set; }

        #region snippet_OnPostAsync
        public async Task<IActionResult> OnPostAsync()
        {
            #region snippet_TryUpdateModelAsync
            var emptyPlayer = new Player();

            if (await TryUpdateModelAsync<Player>(
                emptyPlayer,
                "student",   // Prefix for form value.
                s => s.FirstMidName, s => s.LastName, s => s.ContractDate))
            {
                _context.Players.Add(emptyPlayer);
                await _context.SaveChangesAsync();
                return RedirectToPage("./Index");
            }
            #endregion

            return Page();
        }
        #endregion
    }
}