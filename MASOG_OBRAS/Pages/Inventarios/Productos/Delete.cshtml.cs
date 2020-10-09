using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using EFDataAccessLibrary.DataAccess;
using EFDataAccessLibrary.Models.Inventarios;
using MASOG_OBRAS.Classes;
using EFDataAccessLibrary.Models.Compras;

namespace MASOG_OBRAS.Pages.Inventarios.Productos
{
    public class DeleteModel : BaseDeletePage
    {
        private readonly EFDataAccessLibrary.DataAccess.ProductContext _context;

        public DeleteModel(EFDataAccessLibrary.DataAccess.ProductContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Producto Producto { get; set; }

        public async Task<IActionResult> OnGetAsync(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Producto = await _context.Productos.FirstOrDefaultAsync(m => m.Id == id);


            if (Producto == null)
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

            Producto = await _context.Productos.FindAsync(id);

            if (ExistFactura(Producto.Id))
            {
                this.MessageError = "Existe una factura asociada.";
                return null;
            }
            if (ExistOC(Producto.Id))
            {
                this.MessageError = "Existe una orden de compra asociada.";
                return null;
            }
            if (Producto != null)
            {
                _context.Productos.Remove(Producto);
                return await this.RemoveValue(_context);
            }

            return RedirectToPage("./Index");
        }

        private bool ExistFactura(string id)
        {
            FacturaCompraItem facturaCompraI = _context.FacturaCompraItems.Where(f => f.ProductoId == id).FirstOrDefault<FacturaCompraItem>();
            return facturaCompraI != null;
        }
        private bool ExistOC(string id)
        {
            OrdenItem orden = _context.OrdenItems.Where(o => o.ProductoId == id).FirstOrDefault<OrdenItem>();
            return orden != null;
        }
    }
}
