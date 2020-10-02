using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using EFDataAccessLibrary.DataAccess;
using EFDataAccessLibrary.Models.Compras;

namespace MASOG_OBRAS.Pages.Compras.FacturaDeCompra
{
    public class IndexModel : PageModel
    {
        private readonly EFDataAccessLibrary.DataAccess.ProductContext _context;

        public IndexModel(EFDataAccessLibrary.DataAccess.ProductContext context)
        {
            _context = context;
        }

        public IList<FacturaCompra> FacturaCompra { get;set; }

        public async Task OnGetAsync()
        {
            FacturaCompra = await _context.FacturasCompra
                .Include(f => f.Proveedor).ToListAsync();
        }
    }
}
