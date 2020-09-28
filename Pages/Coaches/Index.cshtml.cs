using ContosoUniversity.Models;
using ContosoUniversity.Models.SchoolViewModels;  // Add VM
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace ContosoUniversity.Pages.Coaches
{
    public class IndexModel : PageModel
    {
        private readonly ContosoUniversity.Data.SchoolContext _context;

        public IndexModel(ContosoUniversity.Data.SchoolContext context)
        {
            _context = context;
        }

        public CoachIndexData CoachData { get; set; }
        public int CoachID { get; set; }
        public int TeamID { get; set; }

        public async Task OnGetAsync(int? id, int? courseID)
        {
            CoachData = new CoachIndexData();
            CoachData.Coaches = await _context.Coaches
                .Include(i => i.OfficeAssignment)                 
                .Include(i => i.TeamAssignments)
                    .ThenInclude(i => i.Team)
                        .ThenInclude(i => i.Department)
                //.Include(i => i.CourseAssignments)
                //    .ThenInclude(i => i.Course)
                //        .ThenInclude(i => i.Enrollments)
                //            .ThenInclude(i => i.Student)
                //.AsNoTracking()
                .OrderBy(i => i.LastName)
                .ToListAsync();

            if (id != null)
            {
                CoachID = id.Value;
                Coach coach = CoachData.Coaches
                    .Where(i => i.ID == id.Value).Single();
                CoachData.Teams = coach.TeamAssignments.Select(s => s.Team);
            }

            if (courseID != null)
            {
                TeamID = courseID.Value;
                var selectedCourse = CoachData.Teams
                    .Where(x => x.TeamID == courseID).Single();
                await _context.Entry(selectedCourse).Collection(x => x.Contracts).LoadAsync();
                foreach (Contract enrollment in selectedCourse.Contracts)
                {
                    await _context.Entry(enrollment).Reference(x => x.Player).LoadAsync();
                }
                CoachData.Contracts = selectedCourse.Contracts;
            }
        }
    }
}
