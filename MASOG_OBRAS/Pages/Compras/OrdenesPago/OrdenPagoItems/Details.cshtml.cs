using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using EFDataAccessLibrary.DataAccess;
using EFDataAccessLibrary.Models.Compras;

namespace MASOG_OBRAS.Pages.Compras.OrdenesPago.OrdenPagoItems
{
    public class DetailsModel : PageModel
    {
        private readonly EFDataAccessLibrary.DataAccess.ProductContext _context;

        public DetailsModel(EFDataAccessLibrary.DataAccess.ProductContext context)
        {
            _context = context;
        }

        public OrdenPagoItem OrdenPagoItem { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            OrdenPagoItem = await _context.OrdenPagoItems
                .Include(o => o.FacturaCompra)
                .Include(o => o.OrdenPago).FirstOrDefaultAsync(m => m.Id == id);

            if (OrdenPagoItem == null)
            {
                return NotFound();
            }
            return Page();
        }
    }
}
