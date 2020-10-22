using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using EFDataAccessLibrary.DataAccess;
using EFDataAccessLibrary.Models.Ventas;

namespace MASOG_OBRAS.Pages.Ventas.FacturasVenta
{
    public class DetailsModel : PageModel
    {
        private readonly EFDataAccessLibrary.DataAccess.ProductContext _context;

        public DetailsModel(EFDataAccessLibrary.DataAccess.ProductContext context)
        {
            _context = context;
        }

        public FacturaVenta FacturaVenta { get; set; }
        public List<FacturaVentaItem> FacturaVentaItems { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            FacturaVenta = await _context.FacturasVenta
                .Include(f => f.Cliente)
                .Include(f => f.Proyecto).FirstOrDefaultAsync(m => m.Id == id);

            FacturaVentaItems = await _context.FacturaVentaItems.Where(f => f.FacturaVentaId == FacturaVenta.Id)
                .Include(f => f.FacturaVenta)
                .Include(f => f.Producto).ToListAsync();

            if (FacturaVenta == null)
            {
                return NotFound();
            }
            return Page();
        }
    }
}
