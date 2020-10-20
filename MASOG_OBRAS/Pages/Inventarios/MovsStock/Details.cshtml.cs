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


namespace MASOG_OBRAS.Pages.Inventarios.MovsStock
{
    public class DetailsModel : PageModel
    {
        private readonly EFDataAccessLibrary.DataAccess.ProductContext _context;

        public DetailsModel(EFDataAccessLibrary.DataAccess.ProductContext context)
        {
            _context = context;
        }

        public MovStock MovStock { get; set; }
        public List<MovStockItem> MovStockItems { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            MovStock = await _context.MovsStock
                .Include(m => m.TipoMovimiento).FirstOrDefaultAsync(m => m.Id == id);

            MovStockItems = await _context.MovStockItems.Where(o => o.MovStockId == MovStock.Id)
                .Include(o => o.MovStock)
                .Include(o => o.Producto).ToListAsync();

            if (MovStock == null)
            {
                return NotFound();
            }
            return Page();
        }
    }
}
