#region snippet_All
using ContosoUniversity.Data;
using ContosoUniversity.Models;
using ContosoUniversity.Models.SchoolViewModels;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Collections.Generic;
using System.Linq;

namespace ContosoUniversity.Pages.Coaches
{
    public class CoachTeamsPageModel : PageModel
    {

        public List<AssignedTeamData> AssignedTeamDataList;

        public void PopulateAssignedTeamData(SchoolContext context, 
                                               Coach coach)
        {
            var allTeams = context.Teams;
            var coachTeams = new HashSet<int>(
                coach.TeamAssignments.Select(c => c.TeamID));
            AssignedTeamDataList = new List<AssignedTeamData>();
            foreach (var team in allTeams)
            {
                AssignedTeamDataList.Add(new AssignedTeamData
                {
                    TeamID = team.TeamID,
                    Title = team.Title,
                    Assigned = coachTeams.Contains(team.TeamID)
                });
            }
        }

        public void UpdateCoachTeams(SchoolContext context, 
            string[] selectedTeams, Coach coachToUpdate)
        {
            #region snippet_IfNull
            if (selectedTeams == null)
            {
                coachToUpdate.TeamAssignments = new List<TeamAssignment>();
                return;
            }
            #endregion

            var selectedTeamsHS = new HashSet<string>(selectedTeams);
            var coachTeams= new HashSet<int>
                (coachToUpdate.TeamAssignments.Select(c => c.Team.TeamID));
            foreach (var team in context.Teams)
            {
                #region snippet_UpdateCourses
                if (selectedTeamsHS.Contains(team.TeamID.ToString()))
                {
                    if (!coachTeams.Contains(team.TeamID))
                    {
                        coachToUpdate.TeamAssignments.Add(
                            new TeamAssignment
                            {
                                CoachID = coachToUpdate.ID,
                                TeamID = team.TeamID
                            });
                    }
                }
                #endregion
                #region snippet_UpdateCoursesElse
                else
                {
                    if (coachTeams.Contains(team.TeamID))
                    {
                        TeamAssignment courseToRemove
                            = coachToUpdate
                                .TeamAssignments
                                .SingleOrDefault(i => i.TeamID == team.TeamID);
                        context.Remove(courseToRemove);
                    }
                }
                #endregion
            }
        }
    }
}
#endregion
