#region snippet_All
using ContosoUniversity.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;

namespace ContosoUniversity.Pages.Coaches
{
    public class EditModel : CoachTeamsPageModel
    {
        private readonly ContosoUniversity.Data.SchoolContext _context;

        public EditModel(ContosoUniversity.Data.SchoolContext context)
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

            Coach = await _context.Coaches
                .Include(i => i.OfficeAssignment)
                .Include(i => i.TeamAssignments).ThenInclude(i => i.Team)
                .AsNoTracking()
                .FirstOrDefaultAsync(m => m.ID == id);

            if (Coach == null)
            {
                return NotFound();
            }
            PopulateAssignedTeamData(_context, Coach);
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id, string[] selectedCourses)
        {
            if (id == null)
            {
                return NotFound();
            }

            var coachToUpdate = await _context.Coaches
                .Include(i => i.OfficeAssignment)
                .Include(i => i.TeamAssignments)
                    .ThenInclude(i => i.Team)
                .FirstOrDefaultAsync(s => s.ID == id);

            if (coachToUpdate == null)
            {
                return NotFound();
            }

            if (await TryUpdateModelAsync<Coach>(
                coachToUpdate,
                "Coach",
                i => i.FirstMidName, i => i.LastName,
                i => i.HireDate, i => i.OfficeAssignment))
            {
                if (String.IsNullOrWhiteSpace(
                    coachToUpdate.OfficeAssignment?.Location))
                {
                    coachToUpdate.OfficeAssignment = null;
                }
                UpdateCoachTeams(_context, selectedCourses, coachToUpdate);
                await _context.SaveChangesAsync();
                return RedirectToPage("./Index");
            }
            UpdateCoachTeams(_context, selectedCourses, coachToUpdate);
            PopulateAssignedTeamData(_context, coachToUpdate);
            return Page();
        }
    }
}
#endregion

