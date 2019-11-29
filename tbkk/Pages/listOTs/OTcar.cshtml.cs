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
    public class OTcarModel : PageModel
    {
        private readonly tbkk.Models.tbkkdbContext _context;

        public OTcarModel(tbkk.Models.tbkkdbContext context)
        {
            _context = context;
        }

        public Employee Employee { get; set; }
        public IList<OT> OT { get;set; }
        public IList<DetailOT> DetailOT { get; set; }
        public string TypeStatus { get; set; }

        public async Task OnGetAsync(int? id)
        {
            TypeStatus = "Pending";
            OT = await _context.OT.ToListAsync();
            DetailOT = await _context.DetailOT.ToListAsync();

            //OT = OT.Where(t => t.Status.Equals(TypeStatus)).ToList();
            //int a = DetailOT.Count();

           
            /*if(a == 0)
            {
                TypeStatus = "Confirm";
            }*/

            //OT = OT.Where(o => o.TypStatus.Equals(TypStatus)).ToList();


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
                 .Include(d => d.Part).ToListAsync();


            DetailOT = DetailOT.Where(e => e.Employee_EmpID == id).ToList();




        }
    }
}
