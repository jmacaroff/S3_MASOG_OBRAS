﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using EFDataAccessLibrary.DataAccess;
using EFDataAccessLibrary.Models.Compras;
using Microsoft.Data.SqlClient;
using System.Reflection;
using EFDataAccessLibrary.Models;
using MASOG_OBRAS.Classes;
using EFDataAccessLibrary.Models.Inventarios;
using EFDataAccessLibrary.Models.Proveedores;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;


namespace MASOG_OBRAS.Pages.Compras.Ordenes
{
    public class DetailsModel : PageModel
    {
        private readonly EFDataAccessLibrary.DataAccess.ProductContext _context;

        public DetailsModel(EFDataAccessLibrary.DataAccess.ProductContext context)
        {
            _context = context;
        }

        public Orden Orden { get; set; }
        public List<OrdenItem> OrdenItems { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Orden = await _context.Ordenes
                .Include(o => o.Proveedor).FirstOrDefaultAsync(m => m.Id == id);

            OrdenItems = await _context.OrdenItems.Where(o => o.OrdenId == Orden.Id)
                .Include(o => o.Orden)
                .Include(o => o.Producto).ToListAsync();

            if (Orden == null)
            {
                return NotFound();
            }
            return Page();
        }
    }
}
