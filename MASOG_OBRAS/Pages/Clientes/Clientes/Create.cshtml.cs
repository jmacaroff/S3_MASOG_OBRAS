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

namespace MASOG_OBRAS.Pages.Clientes
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
        public Cliente Cliente { get; set; }

        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }
            if (ExistNombre(Cliente.Nombre))
            {
                this.MessageError = "Cliente ya registrado.";
                return null;
            }
            if (ExistDNI(Cliente.DNI))
            {
                this.MessageError = "DNI ya registrado.";
                return null;
            }
            _context.Clientes.Add(Cliente);
            return await AddNewValue(_context);
        }

        private bool ExistNombre(string nombre)
        {
            Cliente cliente = _context.Clientes.Where(p => p.Nombre == nombre).FirstOrDefault<Cliente>();
            return cliente != null;
        }

        private bool ExistDNI(double dni)
        {
            Cliente cliente = _context.Clientes.Where(p => p.DNI == dni).FirstOrDefault<Cliente>();
            return cliente != null;
        }
    }
}
