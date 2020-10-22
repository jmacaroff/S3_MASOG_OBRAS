using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using EFDataAccessLibrary.DataAccess;
using EFDataAccessLibrary.Models.Ventas;
using MASOG_OBRAS.Classes;

namespace MASOG_OBRAS.Pages.Ventas.FacturasVenta
{
    public class DeleteModel : BaseDeletePage
    {
        private readonly EFDataAccessLibrary.DataAccess.ProductContext _context;

        public DeleteModel(EFDataAccessLibrary.DataAccess.ProductContext context)
        {
            _context = context;
        }

        [BindProperty]
        public FacturaVenta FacturaVenta { get; set; }
        [BindProperty]
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

            if (FacturaVenta == null)
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

            FacturaVenta = await _context.FacturasVenta.FindAsync(id);

            if (ExistRecibo(FacturaVenta.Id))
            {
                this.MessageError = "Existe una Recibo asociado.";
                return null;
            }

            if (FacturaVenta != null)
            {
                _context.FacturasVenta.Remove(FacturaVenta);
                return await this.RemoveValue(_context);
                //await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }

        private bool ExistRecibo(int id)
        {
            ReciboItem reciboItem = _context.ReciboItems.Where(o => o.FacturaVentaId == id).FirstOrDefault<ReciboItem>();
            return reciboItem != null;
        }
    }
}
