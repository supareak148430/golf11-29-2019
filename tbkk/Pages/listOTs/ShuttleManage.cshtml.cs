using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using tbkk.Models;

namespace tbkk.Pages.listOTs
{
    public class ShuttleManageModel : PageModel
    {
        private readonly tbkk.Models.tbkkdbContext _context;

        public ShuttleManageModel(tbkk.Models.tbkkdbContext context)
        {
            _context = context;
        }

        public DetailOT DetailOT { get; set; }
        public Employee Employee { get; set; }
        public IList<OT> OT { get; set; }
        public int OTs { get; set; }



        public async Task<IActionResult> OnGetAsync(int date, int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Employee = await _context.Employee
                .Include(e => e.Company)
                .Include(e => e.Department)
                .Include(e => e.EmployeeType)
                .Include(e => e.Location)
                .Include(e => e.Position).FirstOrDefaultAsync(m => m.EmployeeID == id);

            OT = await _context.OT.ToListAsync();
            OTs = date;




            if (DetailOT == null)
            {
                return NotFound();
            }
            return Page();
        }
    }
}
