﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using EFDataAccessLibrary.DataAccess;
using EFDataAccessLibrary.Models.Inventarios;

namespace MASOG_OBRAS.Pages.Inventarios.Depositos
{
    public class DeleteModel : PageModel
    {
        private readonly EFDataAccessLibrary.DataAccess.ProductContext _context;

        public DeleteModel(EFDataAccessLibrary.DataAccess.ProductContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Deposito Deposito { get; set; }

        public async Task<IActionResult> OnGetAsync(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Deposito = await _context.Depositos.FirstOrDefaultAsync(m => m.Id == id);

            if (Deposito == null)
            {
                return NotFound();
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Deposito = await _context.Depositos.FindAsync(id);

            if (Deposito != null)
            {
                _context.Depositos.Remove(Deposito);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
