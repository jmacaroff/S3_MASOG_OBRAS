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

namespace MASOG_OBRAS.Pages.Compras.Ordenes
{
    public class DeleteModel : BaseDeletePage
    {
        private readonly EFDataAccessLibrary.DataAccess.ProductContext _context;

        public DeleteModel(EFDataAccessLibrary.DataAccess.ProductContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Orden Orden { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Orden = await _context.Ordenes
                .Include(o => o.Proveedor).FirstOrDefaultAsync(m => m.Id == id);

            if (Orden == null)
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

            Orden = await _context.Ordenes.FindAsync(id);

            if (ExistFactura(Orden.Id))
            {
                this.MessageError = "Existe una factura asociada.";
                return null;
            }
            if (Orden != null)
            {
                _context.Ordenes.Remove(Orden);
                return await this.RemoveValue(_context);
            }
            else
            {
                return Page();
            }
        }

        private bool ExistFactura(int id)
        {
            FacturaCompra facturaCompra = _context.FacturasCompra.Where(f => f.OrdenId == id).FirstOrDefault<FacturaCompra>();
            return facturaCompra != null;
        }
    }
}
