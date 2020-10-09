using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using EFDataAccessLibrary.DataAccess;
using EFDataAccessLibrary.Models.Compras;
using MASOG_OBRAS.Classes;

namespace MASOG_OBRAS.Pages.Compras.OrdenesPago
{
    public class DeleteModel : BaseDeletePage
    {
        private readonly EFDataAccessLibrary.DataAccess.ProductContext _context;

        public DeleteModel(EFDataAccessLibrary.DataAccess.ProductContext context)
        {
            _context = context;
        }

        [BindProperty]
        public OrdenPago OrdenPago { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            OrdenPago = await _context.OrdenesPago
                .Include(o => o.ConceptoPago)
                .Include(o => o.Proveedor).FirstOrDefaultAsync(m => m.Id == id);

            if (OrdenPago == null)
            {
                return NotFound();
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            OrdenPago = await _context.OrdenesPago.FindAsync(id);

            if (OrdenPago != null)
            {
                _context.OrdenesPago.Remove(OrdenPago);
                return await this.RemoveValue(_context);
            }
            else
            {
                return Page();
            }
        }
    }
}
