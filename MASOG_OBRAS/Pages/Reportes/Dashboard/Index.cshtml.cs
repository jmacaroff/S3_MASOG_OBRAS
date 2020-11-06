using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EFDataAccessLibrary.Models.Ventas;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace MASOG_OBRAS.Pages.Reportes.Dashboard
{
    public class IndexModel : PageModel
    {
        private readonly EFDataAccessLibrary.DataAccess.ProductContext _context;

        public IndexModel(EFDataAccessLibrary.DataAccess.ProductContext context)
        {
            _context = context;
        }

        //Sort
        public int TotalVentas { get; set; }
        public int TotalCompra { get; set; }
        public int TotalProyecto { get; set; }
        public RecibosDet RecibosDet { get; set; }

        public IActionResult OnGet()
        {
            TotalVentas = _context.RecibosDet.Select(r => r.FacturaVentaNumero).Distinct().Count();
            TotalCompra = _context.OrdenesPagoDet.Select(o => o.FacturaCompraNumero).Distinct().Count();
            TotalProyecto = _context.Proyectos.Distinct().Count();
            return Page();
        }
    }
}
