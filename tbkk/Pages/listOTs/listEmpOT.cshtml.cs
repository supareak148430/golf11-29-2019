﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using tbkk.Models;

namespace tbkk.Pages.listOTs
{
    public class listEmpOTModel : PageModel
    {
        private readonly tbkk.Models.tbkkdbContext _context;

        public listEmpOTModel(tbkk.Models.tbkkdbContext context)
        {
            _context = context;
        }

        public IList<DetailOT> DetailOT { get;set; }
        public Employee Employee { get; set; }

        public async Task OnGetAsync(int? id)
        {
            DetailOT = await _context.DetailOT
                .Include(d => d.CarType)
                .Include(d => d.Employee)
                .Include(d => d.FoodSet)
                .Include(d => d.OT)
                .Include(d => d.Part).ToListAsync();
            Employee = await _context.Employee
              .Include(e => e.Company)
              .Include(e => e.Department)
              .Include(e => e.EmployeeType)
              .Include(e => e.Location)
              .Include(e => e.Position).FirstOrDefaultAsync(m => m.EmployeeID == id);

           
        }
    }
}
