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
    public class ShuttleManageViewModel : PageModel
    {
        private readonly tbkk.Models.tbkkdbContext _context;

        public ShuttleManageViewModel(tbkk.Models.tbkkdbContext context)
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

            Employee = await _context.Employee
                .Include(e => e.Company)
                .Include(e => e.Department)
                .Include(e => e.EmployeeType)
                .Include(e => e.Location)
                .Include(e => e.Position).FirstOrDefaultAsync(m => m.EmployeeID == id);

            DetailOT = await _context.DetailOT
                .Include(d => d.CarType)
                .Include(d => d.Employee)
                .Include(d => d.FoodSet)
                .Include(d => d.OT)
                .Include(d => d.Part).FirstOrDefaultAsync(m => m.DetailOTID == id);


            if (DetailOT == null)
            {
                return NotFound();
            }
            return Page();
        }
    }
}
