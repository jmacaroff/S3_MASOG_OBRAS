using System;
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


namespace MASOG_OBRAS.Pages.Compras.Facturas
{
    public class DetailsModel : PageModel
    {
        private readonly EFDataAccessLibrary.DataAccess.ProductContext _context;

        public DetailsModel(EFDataAccessLibrary.DataAccess.ProductContext context)
        {
            _context = context;
        }

        public FacturaCompra FacturaCompra { get; set; }
        public List<FacturaCompraItem> FacturaCompraItems { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            FacturaCompra = await _context.FacturasCompra
                .Include(p => p.Proveedor).FirstOrDefaultAsync(m => m.Id == id);

            FacturaCompraItems = await _context.FacturaCompraItems.Where(o => o.FacturaCompraId == FacturaCompra.Id)
                .Include(o => o.FacturaCompra)
                .Include(p => p.Producto).ToListAsync();

            if (FacturaCompra == null)
            {
                return NotFound();
            }
            return Page();
        }
    }
}
