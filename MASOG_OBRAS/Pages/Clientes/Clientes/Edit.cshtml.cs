﻿using System;
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

namespace MASOG_OBRAS.Pages.Clientes.Clientes
{
    public class EditModel : BaseEditPage
    {
        private readonly EFDataAccessLibrary.DataAccess.ProductContext _context;

        public EditModel(EFDataAccessLibrary.DataAccess.ProductContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Cliente Cliente { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Cliente = await _context.Clientes.FirstOrDefaultAsync(m => m.Id == id);

            if (Cliente == null)
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
            if (ExistNombre(Cliente.Nombre, Cliente.Id))
            {
                this.MessageError = "Cliente ya registrado.";
                return null;
            }
            if (ExistDNI(Cliente.DNI, Cliente.Id))
            {
                this.MessageError = "DNI ya registrado.";
                return null;
            }

            _context.Attach(Cliente).State = EntityState.Modified;
            return await UpdateValue(_context);
        }

        private bool ExistNombre(string nombre, int ID)
        {
            Cliente cliente = _context.Clientes.Where(p => p.Nombre == nombre && p.Id != ID).FirstOrDefault<Cliente>();
            return cliente != null;
        }

        private bool ExistDNI(double dni, int ID)
        {
            Cliente cliente = _context.Clientes.Where(p => p.DNI == dni && p.Id != ID).FirstOrDefault<Cliente>();
            return cliente != null;
        }
    }
}
