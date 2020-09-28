using ContosoUniversity.Models;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace ContosoUniversity.Pages.Teams
{
    public class CreateModel : DepartmentNamePageModel
    {
        private readonly ContosoUniversity.Data.SchoolContext _context;

        public CreateModel(ContosoUniversity.Data.SchoolContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
            PopulateDepartmentsDropDownList(_context);
            return Page();
        }

        [BindProperty]
        public Team Team { get; set; }

        public async Task<IActionResult> OnPostAsync()
        {
            var emptyTeam = new Team();

            if (await TryUpdateModelAsync<Team>(
                 emptyTeam,
                 "course",   // Prefix for form value.
                 s => s.TeamID, s => s.DepartmentID, s => s.Title, s => s.Credits))
            {
                _context.Teams.Add(emptyTeam);
                await _context.SaveChangesAsync();
                return RedirectToPage("./Index");
            }

            // Select DepartmentID if TryUpdateModelAsync fails.
            PopulateDepartmentsDropDownList(_context, emptyTeam.DepartmentID);
            return Page();
        }
    }
}

