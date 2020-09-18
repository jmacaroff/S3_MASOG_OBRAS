using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using EFDataAccessLibrary.DataAccess;
using EFDataAccessLibrary.Models.Inventarios;

namespace MASOG_OBRAS.Pages.Inventarios.Productos
{
    public class EditModel : PageModel
    {
        private readonly EFDataAccessLibrary.DataAccess.ProductContext _context;

        public EditModel(EFDataAccessLibrary.DataAccess.ProductContext context)
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

        // No se valida al editar que los datos originales se conserven y se reemplace el precio por ejemplo
        public string MessageError { get; set; }

        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {

            _context.Attach(Producto).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ProductoExists(Producto.Id))
                {
                    ModelState.AddModelError(string.Empty, MessageError);
                    return Page();
                    //return NotFound();
                }
                if (!ProductoDescripcionExists(Producto.Descripcion))
                {
                    ModelState.AddModelError(string.Empty, MessageError);
                    return Page();
                    //return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToPage("./Index");
        }


        private bool ProductoExists(string id)
        {
            this.MessageError = "ID ya registrada";
            return _context.Productos.Any(e => e.Id == id);
        }
        private bool ProductoDescripcionExists(string descripcion)
        {
            this.MessageError = "Descripcion ya registrada";
            return _context.Productos.Any(e => e.Descripcion == descripcion);
        }
    }
}
