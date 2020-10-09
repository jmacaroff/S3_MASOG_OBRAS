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

namespace MASOG_OBRAS.Pages.Compras.OrdenesPago
{
    public class EditModel : PageModel
    {
        private readonly EFDataAccessLibrary.DataAccess.ProductContext _context;

        public EditModel(EFDataAccessLibrary.DataAccess.ProductContext context)
        {
            _context = context;
        }

        [BindProperty]
        public OrdenPago OrdenPago { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            OrdenPago = await _context.OrdenesPago
                .Include(o => o.ConceptoPago)
                .Include(o => o.Proveedor).FirstOrDefaultAsync(m => m.Id == id);

            if (OrdenPago == null)
            {
                return NotFound();
            }
           ViewData["ConceptoPagoId"] = new SelectList(_context.Set<ConceptoPago>(), "Id", "Descripcion");
           ViewData["ProveedorId"] = new SelectList(_context.Proveedores, "Id", "RazonSocial");
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

            _context.Attach(OrdenPago).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!OrdenPagoExists(OrdenPago.Id))
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

        private bool OrdenPagoExists(int id)
        {
            return _context.OrdenesPago.Any(e => e.Id == id);
        }
    }
}
