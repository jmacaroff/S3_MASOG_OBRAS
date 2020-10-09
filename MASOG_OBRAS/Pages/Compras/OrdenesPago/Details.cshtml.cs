using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using EFDataAccessLibrary.DataAccess;
using EFDataAccessLibrary.Models.Compras;

namespace MASOG_OBRAS.Pages.Compras.OrdenesPago
{
    public class DetailsModel : PageModel
    {
        private readonly EFDataAccessLibrary.DataAccess.ProductContext _context;

        public DetailsModel(EFDataAccessLibrary.DataAccess.ProductContext context)
        {
            _context = context;
        }

        public OrdenPago OrdenPago { get; set; }
        public List<OrdenPagoItem> OrdenPagoItems { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            OrdenPago = await _context.OrdenesPago
                .Include(o => o.ConceptoPago)
                .Include(o => o.Proveedor).FirstOrDefaultAsync(m => m.Id == id);

            OrdenPagoItems = await _context.OrdenPagoItems.Where(o => o.OrdenPagoId == OrdenPago.Id)
                .Include(o => o.OrdenPago)
                .Include(o => o.FacturaCompra).ToListAsync();

            if (OrdenPago == null)
            {
                return NotFound();
            }
            return Page();
        }
    }
}
