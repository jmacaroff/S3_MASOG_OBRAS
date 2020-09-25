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

namespace MASOG_OBRAS.Pages.Inventarios.Depositos
{
    public class EditModel : BaseEditPage
    {
        private readonly EFDataAccessLibrary.DataAccess.ProductContext _context;

        public EditModel(EFDataAccessLibrary.DataAccess.ProductContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Deposito Deposito { get; set; }

        public async Task<IActionResult> OnGetAsync(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Deposito = await _context.Depositos.FirstOrDefaultAsync(m => m.Id == id);

            if (Deposito == null)
            {
                return NotFound();
            }
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
            if (ExistDescription(Deposito.Descripcion, Deposito.Id))
            {
                this.MessageError = "Descripción ya registrada.";
                return null;
            }

            _context.Attach(Deposito).State = EntityState.Modified;
            return await UpdateValue(_context);
        }

        private bool ExistDescription(string description, string ID)
        {
            Deposito deposito = _context.Depositos.Where(d => d.Descripcion == description && d.Id != ID).FirstOrDefault<Deposito>();
            return deposito != null;
        }
    }
}
