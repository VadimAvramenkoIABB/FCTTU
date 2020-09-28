using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ContosoUniversity.Models.SchoolViewModels
{
    public class CoachIndexData
    {
        public IEnumerable<Coach> Coaches { get; set; }
        public IEnumerable<Team> Teams { get; set; }
        public IEnumerable<Contract> Contracts { get; set; }
    }
}