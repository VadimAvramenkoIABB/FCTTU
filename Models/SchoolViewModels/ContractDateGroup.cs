using System;
using System.ComponentModel.DataAnnotations;

namespace ContosoUniversity.Models.SchoolViewModels
{
    public class ContractDateGroup
    {
        [DataType(DataType.Date)]
        public DateTime? ContractDate { get; set; }

        public int PlayerCount { get; set; }
    }
}