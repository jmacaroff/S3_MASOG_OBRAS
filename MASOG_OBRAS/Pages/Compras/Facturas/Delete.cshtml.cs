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

namespace MASOG_OBRAS.Pages.Compras.Facturas
{
    public class DeleteModel : BaseDeletePage
    {
        private readonly EFDataAccessLibrary.DataAccess.ProductContext _context;

        public DeleteModel(EFDataAccessLibrary.DataAccess.ProductContext context)
        {
            _context = context;
        }

        [BindProperty]
        public FacturaCompra FacturaCompra { get; set; }
        public List<FacturaCompraItem> FacturaCompraItems { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            FacturaCompra = await _context.FacturasCompra
                .Include(p => p.Proveedor).FirstOrDefaultAsync(m => m.Id == id);

            FacturaCompraItems = await _context.FacturaCompraItems.Where(o => o.FacturaCompraId == FacturaCompra.Id)
                .Include(o => o.FacturaCompra)
                .Include(p => p.Producto).ToListAsync();

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

            if (ExistOP(FacturaCompra.Id))
            {
                this.MessageError = "Existe una Orden de Pago asociada.";
                return null;
            }
            if (FacturaCompra != null)
            {
                _context.FacturasCompra.Remove(FacturaCompra);
                return await this.RemoveValue(_context);
            }else
            {
                return Page();
            }
        }

        private bool ExistOP(int id)
        {
            OrdenPagoItem ordenPagoI = _context.OrdenPagoItems.Where(o => o.FacturaCompraId == id).FirstOrDefault<OrdenPagoItem>();
            return ordenPagoI != null;
        }
    }
}
