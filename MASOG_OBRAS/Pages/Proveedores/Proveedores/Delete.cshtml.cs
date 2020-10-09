using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using EFDataAccessLibrary.DataAccess;
using EFDataAccessLibrary.Models.Proveedores;
using EFDataAccessLibrary.Models.Compras;
using MASOG_OBRAS.Classes;

namespace MASOG_OBRAS.Pages.Proveedores.Proveedores
{
    public class DeleteModel : BaseDeletePage
    {
        private readonly EFDataAccessLibrary.DataAccess.ProductContext _context;

        public DeleteModel(EFDataAccessLibrary.DataAccess.ProductContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Proveedor Proveedor { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Proveedor = await _context.Proveedores.FirstOrDefaultAsync(m => m.Id == id);

            if (Proveedor == null)
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

            Proveedor = await _context.Proveedores.FindAsync(id);

            if (ExistFactura(Proveedor.Id))
            {
                this.MessageError = "Existe una factura asociada.";
                return null;
            }
            if (ExistOC(Proveedor.Id))
            {
                this.MessageError = "Existe una orden de compra asociada.";
                return null;
            }
            if (Proveedor != null)
            {
                _context.Proveedores.Remove(Proveedor);
                return await this.RemoveValue(_context);
            }

            return RedirectToPage("./Index");
        }

        private bool ExistFactura(int id)
        {
            FacturaCompra facturaCompra = _context.FacturasCompra.Where(f => f.ProveedorId == id).FirstOrDefault<FacturaCompra>();
            return facturaCompra != null;
        }
        private bool ExistOC(int id)
        {
            Orden orden = _context.Ordenes.Where(o => o.ProveedorId == id).FirstOrDefault<Orden>();
            return orden != null;
        }
    }
}
