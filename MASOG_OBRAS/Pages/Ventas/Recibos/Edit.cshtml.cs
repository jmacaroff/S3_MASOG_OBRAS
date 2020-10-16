using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using EFDataAccessLibrary.DataAccess;
using EFDataAccessLibrary.Models.Ventas;
using EFDataAccessLibrary.Models.Compras;

namespace MASOG_OBRAS.Pages.Ventas.Recibos
{
    public class EditModel : PageModel
    {
        private readonly EFDataAccessLibrary.DataAccess.ProductContext _context;

        public EditModel(EFDataAccessLibrary.DataAccess.ProductContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Recibo Recibo { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Recibo = await _context.Recibos
                .Include(r => r.Cliente)
                .Include(r => r.ConceptoPago).FirstOrDefaultAsync(m => m.Id == id);

            if (Recibo == null)
            {
                return NotFound();
            }
           ViewData["ClienteId"] = new SelectList(_context.Clientes, "Id", "Nombre");
           ViewData["ConceptoPagoId"] = new SelectList(_context.Set<ConceptoPago>(), "Id", "Descripcion");
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

            _context.Attach(Recibo).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ReciboExists(Recibo.Id))
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

        private bool ReciboExists(int id)
        {
            return _context.Recibos.Any(e => e.Id == id);
        }
    }
}
