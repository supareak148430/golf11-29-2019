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
    public class ConfirmShuttleModel : PageModel
    {
        private readonly tbkk.Models.tbkkdbContext _context;

        public ConfirmShuttleModel(tbkk.Models.tbkkdbContext context)
        {
            _context = context;
        }

        public DetailOT DetailOT { get; set; }
        public Employee Employee { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            DetailOT = await _context.DetailOT
                .Include(d => d.CarType)
                .Include(d => d.Employee)
                .Include(d => d.FoodSet)
                .Include(d => d.OT)
                .Include(d => d.Part).FirstOrDefaultAsync(m => m.DetailOTID == id);

            Employee = await _context.Employee
                .Include(d => d.Company)
                .Include(d => d.Department)
                .Include(d => d.Location)
                .Include(d => d.EmployeeType)
                .Include(d => d.Position).FirstOrDefaultAsync(m => m.EmployeeID == id);

            if (DetailOT == null)
            {
                return NotFound();
            }
            return Page();
        }
    }
}
