using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using EFDataAccessLibrary.DataAccess;
using EFDataAccessLibrary.Models.Ventas;

namespace MASOG_OBRAS.Pages.Ventas.FacturasVenta
{
    public class IndexModel : PageModel
    {
        private readonly EFDataAccessLibrary.DataAccess.ProductContext _context;

        public IndexModel(EFDataAccessLibrary.DataAccess.ProductContext context)
        {
            _context = context;
        }

        public IList<FacturaVenta> FacturaVenta { get;set; }

        public async Task OnGetAsync()
        {
            FacturaVenta = await _context.FacturasVenta
                .Include(f => f.Proyecto).ToListAsync();
        }
    }
}
