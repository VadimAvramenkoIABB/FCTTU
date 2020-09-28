namespace ContosoUniversity.Models
{
    public class TeamAssignment
    {
        public int CoachID { get; set; }
        public int TeamID { get; set; }
        public Coach Coach { get; set; }
        public Team Team { get; set; }
    }
}