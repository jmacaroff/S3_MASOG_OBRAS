using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using EFDataAccessLibrary.DataAccess;
using EFDataAccessLibrary.Models.Compras;

namespace MASOG_OBRAS.Pages.Compras.FacturaDeCompra
{
    public class DeleteModel : PageModel
    {
        private readonly EFDataAccessLibrary.DataAccess.ProductContext _context;

        public DeleteModel(EFDataAccessLibrary.DataAccess.ProductContext context)
        {
            _context = context;
        }

        [BindProperty]
        public FacturaCompra FacturaCompra { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            FacturaCompra = await _context.FacturasCompra
                .Include(f => f.Proveedor).FirstOrDefaultAsync(m => m.Id == id);

            if (FacturaCompra == null)
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

            FacturaCompra = await _context.FacturasCompra.FindAsync(id);

            if (FacturaCompra != null)
            {
                _context.FacturasCompra.Remove(FacturaCompra);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
