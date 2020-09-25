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

namespace MASOG_OBRAS.Pages.Inventarios.Depositos
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
        public Deposito Deposito { get; set; }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }
            if (ExistDeposito(Deposito.Id))
            {
                this.MessageError = "Depósito ya registrado.";
                return null;
            }
            if (ExistDescription(Deposito.Descripcion))
            {
                this.MessageError = "Descripción ya registrada.";
                return null;
            }

            _context.Depositos.Add(Deposito);
            return await AddNewValue(_context);
        }

        private bool ExistDeposito(string id)
        {
            Deposito deposito = _context.Depositos.Find(id);
            return deposito != null;
        }

        private bool ExistDescription(string description)
        {
            Deposito deposito = _context.Depositos.Where(d => d.Descripcion == description).FirstOrDefault<Deposito>();
            return deposito != null;
        }
    }
}
