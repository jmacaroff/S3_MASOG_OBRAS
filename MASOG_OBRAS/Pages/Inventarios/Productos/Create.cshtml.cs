using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using EFDataAccessLibrary.DataAccess;
using EFDataAccessLibrary.Models.Inventarios;
using Microsoft.Data.SqlClient;
using System.Reflection;
using Microsoft.EntityFrameworkCore;
using EFDataAccessLibrary.Models;
using MASOG_OBRAS.Classes;

namespace MASOG_OBRAS.Pages.Inventarios.Productos
{
    public class CreateModel : BaseCreatePage
    {
      
        private readonly ProductContext _context;

        public CreateModel(ProductContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {            
            return Page();
        }

        [BindProperty]
        public Producto Producto { get; set; }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }
            if (ExistProduct(Producto.Id))
            {
                this.MessageError = "Producto ya registrado";
                return null;
            }
            if (ExistDescription(Producto.Descripcion))
            {
                this.MessageError = "Descripcion ya registrada";
                return null;
            }
            _context.Productos.Add(Producto);
            return await AddNewValue(_context);
        }

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
    }
}