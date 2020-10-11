using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using EFDataAccessLibrary.DataAccess;
using EFDataAccessLibrary.Models.Clientes;
using MASOG_OBRAS.Classes;

namespace MASOG_OBRAS.Pages.Clientes.Proyectos
{
    public class CreateModel : BaseCreatePage
    {
        private readonly EFDataAccessLibrary.DataAccess.ProductContext _context;

        public CreateModel(EFDataAccessLibrary.DataAccess.ProductContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
            ViewData["ClienteId"] = new SelectList(_context.Clientes, "Id", "Nombre");
            return Page();
        }

        [BindProperty]
        public Proyecto Proyecto { get; set; }

        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }
            if (ExistNombre(Proyecto.Nombre))
            {
                this.MessageError = "Proyecto ya registrado.";
                return null;
            }
            _context.Proyectos.Add(Proyecto);
            return await AddNewValue(_context);
        }

        private bool ExistNombre(string nombre)
        {
            Proyecto proyecto = _context.Proyectos.Where(p => p.Nombre == nombre).FirstOrDefault<Proyecto>();
            return proyecto != null;
        }

    }
}
