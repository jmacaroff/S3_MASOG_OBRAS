using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using EFDataAccessLibrary.DataAccess;
using EFDataAccessLibrary.Models.Clientes;
using MASOG_OBRAS.Classes;

namespace MASOG_OBRAS.Pages.Clientes.Proyectos
{
    public class EditModel : BaseEditPage
    {
        private readonly EFDataAccessLibrary.DataAccess.ProductContext _context;

        public EditModel(EFDataAccessLibrary.DataAccess.ProductContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Proyecto Proyecto { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Proyecto = await _context.Proyectos.FirstOrDefaultAsync(m => m.Id == id);

            if (Proyecto == null)
            {
                return NotFound();
            }

            ViewData["ClienteId"] = new SelectList(_context.Clientes, "Id", "Nombre");
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
            if (ExistNombre(Proyecto.Nombre, Proyecto.Id))
            {
                this.MessageError = "Proyecto ya registrado.";
                return null;
            }

            _context.Attach(Proyecto).State = EntityState.Modified;
            return await UpdateValue(_context);
        }

        private bool ExistNombre(string nombre, int ID)
        {
            Proyecto proyecto = _context.Proyectos.Where(p => p.Nombre == nombre && p.Id != ID).FirstOrDefault<Proyecto>();
            return proyecto != null;
        }

    }
}
