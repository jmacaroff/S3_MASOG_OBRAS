﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using EFDataAccessLibrary.DataAccess;
using EFDataAccessLibrary.Models.Clientes;
using MASOG_OBRAS.Classes;

namespace MASOG_OBRAS.Pages.Clientes.Clientes
{
    public class DeleteModel : BaseDeletePage
    {
        private readonly EFDataAccessLibrary.DataAccess.ProductContext _context;

        public DeleteModel(EFDataAccessLibrary.DataAccess.ProductContext context)
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

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Cliente = await _context.Clientes.FindAsync(id);

            if (ExistProyecto(Cliente.Id))
            {
                this.MessageError = "Existe un Proyecto asociado al cliente.";
                return null;
            }
            if (Cliente != null)
            {
                _context.Clientes.Remove(Cliente);
                return await this.RemoveValue(_context);
            }
            else
            {
                return Page();
            }
        }

        private bool ExistProyecto(int id)
        {
            Proyecto proyecto = _context.Proyectos.Where(o => o.ClienteId == id).FirstOrDefault<Proyecto>();
            return proyecto != null;
        }
    }
}
