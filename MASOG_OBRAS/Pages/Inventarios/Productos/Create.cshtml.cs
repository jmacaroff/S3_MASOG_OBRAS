using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using EFDataAccessLibrary.DataAccess;
using EFDataAccessLibrary.Models.Inventarios;
using Microsoft.EntityFrameworkCore;

namespace MASOG_OBRAS.Pages.Inventarios.Productos
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
            return Page();
        }

        [BindProperty]
        public Producto Producto { get; set; }

        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            Producto existeProductoId = await _context.Productos.SingleOrDefaultAsync(
                m => m.Id == Producto.Id);
            Producto existeProductoDescripcion = await _context.Productos.SingleOrDefaultAsync(
                m => m.Descripcion == Producto.Descripcion);
            if (existeProductoId != null)
            {
                // El producto ya existe.
                // Entonces.
                ModelState.AddModelError(string.Empty, "El producto ya existe.");
            }
            else
            {
                if (existeProductoDescripcion != null)
                {
                    ModelState.AddModelError(string.Empty, "La descripcion ya existe.");
                }
            }

            if (!ModelState.IsValid)
            {
                return Page();
            }
            _context.Productos.Add(Producto);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
