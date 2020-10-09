using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using EFDataAccessLibrary.DataAccess;
using EFDataAccessLibrary.Models.Compras;

namespace MASOG_OBRAS.Pages.Compras.OrdenesPago.OrdenPagoItems
{
    public class CreateModel : PageModel
    {
        private readonly EFDataAccessLibrary.DataAccess.ProductContext _context;

        public CreateModel(EFDataAccessLibrary.DataAccess.ProductContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
        ViewData["FacturaCompraId"] = new SelectList(_context.FacturasCompra, "Id", "TipoFactura");
        ViewData["OrdenPagoId"] = new SelectList(_context.OrdenesPago, "Id", "Id");
            return Page();
        }

        [BindProperty]
        public OrdenPagoItem OrdenPagoItem { get; set; }

        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.OrdenPagoItems.Add(OrdenPagoItem);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
