using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using EFDataAccessLibrary.DataAccess;
using EFDataAccessLibrary.Models.Compras;

namespace MASOG_OBRAS.Pages.Compras.OrdenesPago
{
    public class IndexModel : PageModel
    {
        private readonly EFDataAccessLibrary.DataAccess.ProductContext _context;

        public IndexModel(EFDataAccessLibrary.DataAccess.ProductContext context)
        {
            _context = context;
        }

        public IList<OrdenPago> OrdenPago { get;set; }

        public async Task OnGetAsync()
        {
            OrdenPago = await _context.OrdenesPago
                .Include(o => o.ConceptoPago)
                .Include(o => o.Proveedor).ToListAsync();
        }
    }
}
