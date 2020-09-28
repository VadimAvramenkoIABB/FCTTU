using ContosoUniversity.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ContosoUniversity.Pages.Coaches
{
    public class CreateModel : CoachTeamsPageModel
    {
        private readonly ContosoUniversity.Data.SchoolContext _context;

        public CreateModel(ContosoUniversity.Data.SchoolContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
            var coach = new Coach();
            coach.TeamAssignments = new List<TeamAssignment>();

            // Provides an empty collection for the foreach loop
            // foreach (var course in Model.AssignedCourseDataList)
            // in the Create Razor page.
            PopulateAssignedTeamData(_context, coach);
            return Page();
        }

        [BindProperty]
        public Coach Coach { get; set; }

        public async Task<IActionResult> OnPostAsync(string[] selectedTeams)
        {
            var newCoach = new Coach();
            if (selectedTeams != null)
            {
                newCoach.TeamAssignments = new List<TeamAssignment>();
                foreach (var team in selectedTeams)
                {
                    var teamToAdd = new TeamAssignment
                    {
                        TeamID = int.Parse(team)
                    };
                    newCoach.TeamAssignments.Add(teamToAdd);
                }
            }

            if (await TryUpdateModelAsync<Coach>(
                newCoach,
                "Coach",
                i => i.FirstMidName, i => i.LastName,
                i => i.HireDate, i => i.OfficeAssignment))
            {
                _context.Coaches.Add(newCoach);                
                await _context.SaveChangesAsync();
                return RedirectToPage("./Index");
            }
            PopulateAssignedTeamData(_context, newCoach);
            return Page();
        }
    }
}