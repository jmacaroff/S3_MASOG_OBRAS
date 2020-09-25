using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using EFDataAccessLibrary.DataAccess;
using EFDataAccessLibrary.Models.Proveedores;
using MASOG_OBRAS.Classes;

namespace MASOG_OBRAS.Pages.Proveedores.Proveedores
{
    public class EditModel : BaseEditPage
    {
        private readonly EFDataAccessLibrary.DataAccess.ProductContext _context;

        public EditModel(EFDataAccessLibrary.DataAccess.ProductContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Proveedor Proveedor { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Proveedor = await _context.Proveedores.FirstOrDefaultAsync(m => m.Id == id);

            if (Proveedor == null)
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
            if (ExistRazonSocial(Proveedor.RazonSocial, Proveedor.Id))
            {
                this.MessageError = "Cliente ya registrado.";
                return null;
            }
            if (ExistCuit(Proveedor.CUIT, Proveedor.Id))
            {
                this.MessageError = "CUIT ya registrado";
                return null;
            }

            _context.Attach(Proveedor).State = EntityState.Modified;
            return await UpdateValue(_context);
        }
        private bool ExistRazonSocial(string nombre, int ID)
        {
            Proveedor proveedor = _context.Proveedores.Where(p => p.RazonSocial == nombre && p.Id != ID).FirstOrDefault<Proveedor>();
            return proveedor != null;
        }
        private bool ExistCuit(double cuit, int ID)
        {
            Proveedor proveedor = _context.Proveedores.Where(p => p.CUIT == cuit && p.Id != ID).FirstOrDefault<Proveedor>();
            return proveedor != null;
        }
    }
}
