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
using MASOG_OBRAS.Classes;

namespace MASOG_OBRAS.Pages.Inventarios.Productos
{
    public class EditModel : BaseEditPage
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
            if (!ModelState.IsValid)
            {
                return Page();
            }
            if (ExistDescription(Producto.Descripcion, Producto.Id))
            {
                MessageError = "Descripcion ya registrada";
                return Page();
            }
            _context.Attach(Producto).State = EntityState.Modified;
            return await this.UpdateValue(_context);
        }

        private bool ExistDescription(string description, string id)
        {
            Producto producto = _context.Productos.Where(p => p.Descripcion == description && p.Id != id).FirstOrDefault<Producto>();
            return producto != null;
        }
        private bool ProductoDescripcionExists(string descripcion)
        {
            this.MessageError = "Descripcion ya registrada";
            return _context.Productos.Any(e => e.Descripcion == descripcion);
        }
    }
}
