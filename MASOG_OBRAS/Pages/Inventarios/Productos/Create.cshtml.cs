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

        [BindProperty]
        public string MessageError { get; set; }

        private bool ExistProduct(string id)
        {
            Producto producto = _context.Productos.Find(id);
            return producto != null;
        }

        private bool ExistDescription(string description)
        {
            Producto producto = _context.Productos.Where(p => p.Descripcion == description).FirstOrDefault<Producto>();
            return producto != null;
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {                
                return Page();
            }
            if (ExistProduct(Producto.Id))
            {
                this.MessageError = "ID ya registrada";
                ModelState.AddModelError(string.Empty, MessageError);
                return Page();
            }
            if (ExistDescription(Producto.Descripcion))
            {
                this.MessageError = "Descripcion ya registrada";
                ModelState.AddModelError(string.Empty, MessageError);
                return Page();
            }
            _context.Productos.Add(Producto);
            await _context.SaveChangesAsync();
            return Redirect("./Index");
        }
    }
}
