using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using EFDataAccessLibrary.DataAccess;
using EFDataAccessLibrary.Models.Compras;

namespace MASOG_OBRAS.Pages.Compras.OrdenesPago.OrdenPagoItems
{
    public class EditModel : PageModel
    {
        private readonly EFDataAccessLibrary.DataAccess.ProductContext _context;

        public EditModel(EFDataAccessLibrary.DataAccess.ProductContext context)
        {
            _context = context;
        }

        [BindProperty]
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
           ViewData["FacturaCompraId"] = new SelectList(_context.FacturasCompra, "Id", "TipoFactura");
           ViewData["OrdenPagoId"] = new SelectList(_context.OrdenesPago, "Id", "Id");
            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Attach(OrdenPagoItem).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!OrdenPagoItemExists(OrdenPagoItem.Id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToPage("./Index");
        }

        private bool OrdenPagoItemExists(int id)
        {
            return _context.OrdenPagoItems.Any(e => e.Id == id);
        }
    }
}
