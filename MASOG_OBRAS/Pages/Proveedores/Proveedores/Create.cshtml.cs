using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using EFDataAccessLibrary.DataAccess;
using EFDataAccessLibrary.Models.Proveedores;
using MASOG_OBRAS.Classes;

namespace MASOG_OBRAS.Pages.Proveedores.Proveedores
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
            return Page();
        }

        [BindProperty]
        public Proveedor Proveedor { get; set; }


        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }
            if (ExistRazonSocial(Proveedor.RazonSocial))
            {
                this.MessageError = "Cliente ya registrado.";
                return null;
            }
            if (ExistCuit(Proveedor.CUIT))
            {
                this.MessageError = "CUIT ya registrado";
                return null;
            }
            _context.Proveedores.Add(Proveedor);
            return await AddNewValue(_context); ;
        }

        private bool ExistRazonSocial(string nombre)
        {
            Proveedor proveedor = _context.Proveedores.Where(p => p.RazonSocial == nombre).FirstOrDefault<Proveedor>();
            return proveedor != null;
        }
        private bool ExistCuit(double cuit)
        {
            Proveedor proveedor = _context.Proveedores.Where(p => p.CUIT == cuit).FirstOrDefault<Proveedor>();
            return proveedor != null;
        }

    }
}
