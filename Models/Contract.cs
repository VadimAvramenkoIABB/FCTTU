using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ContosoUniversity.Models
{
    public enum Grade
    {
        A, B, C, D, F
    }

    public class Contract
    {
        public int ContractID { get; set; }
        public int TeamID { get; set; }
        public int PlayerID { get; set; }
        [DisplayFormat(NullDisplayText = "No contract")]
        public Grade? Grade { get; set; }

        public Team Team { get; set; }
        public Player Player { get; set; }
    }
}
