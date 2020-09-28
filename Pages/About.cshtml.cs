using ContosoUniversity.Models.SchoolViewModels;
using ContosoUniversity.Data;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ContosoUniversity.Models;

namespace ContosoUniversity.Pages
{
    public class AboutModel : PageModel
    {
        private readonly SchoolContext _context;

        public AboutModel(SchoolContext context)
        {
            _context = context;
        }

        public IList<ContractDateGroup> Players { get; set; }

        public async Task OnGetAsync()
        {
            IQueryable<ContractDateGroup> data =
                from player in _context.Players
                group player by player.ContractDate into dateGroup
                select new ContractDateGroup()
                {
                    ContractDate = dateGroup.Key,
                    PlayerCount = dateGroup.Count()
                };

            Players = await data.AsNoTracking().ToListAsync();
        }
    }
}
