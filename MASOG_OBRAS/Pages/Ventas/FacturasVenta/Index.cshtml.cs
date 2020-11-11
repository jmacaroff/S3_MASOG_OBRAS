using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using EFDataAccessLibrary.DataAccess;
using EFDataAccessLibrary.Models.Ventas;
using MASOG_OBRAS.Classes;

namespace MASOG_OBRAS.Pages.Ventas.FacturasVenta
{
    public class IndexModel : BaseIndexPage<FacturaVenta>
    {
        private readonly EFDataAccessLibrary.DataAccess.ProductContext _context;

        public IndexModel(EFDataAccessLibrary.DataAccess.ProductContext context)
        {
            _context = context;
        }

        public PaginatedList<FacturaVenta> FacturaVenta { get;set; }

        public string CurrentFilter { get; set; }
        public string CurrentSort { get; set; }

        public async Task OnGetAsync(string sortOrder, string currentFilter, string searchString, int? pageIndex)
        {
            //inicializo el orden actual
            CurrentSort = sortOrder;

            // controlamos los filtros de busqueda
            if (searchString != null)
            {
                pageIndex = 1;
            }
            else
            {
                searchString = currentFilter;
            }

            //Agrego filtro de busqueda
            CurrentFilter = searchString;

            IQueryable<FacturaVenta> facturasIQ = from o in _context.FacturasVenta select o;

            //veo si el buscador esta vacio para poder realizar la búsqueda

            if (!String.IsNullOrEmpty(searchString))
            {
                facturasIQ = facturasIQ.Where(c => c.Fecha.ToString().Contains(searchString));
            }
            ////analizo los casos para el ordenamiento
            switch (sortOrder)
            {
                //    case "precio_desc":
                //        productosIQ = productosIQ.OrderBy(p => p.Precio);
                //        break;
                //    case "name_dec":
                //        productosIQ = productosIQ.OrderByDescending(p => p.Descripcion);
                //        break;
                default:
                    facturasIQ = facturasIQ.OrderByDescending(c => c.Fecha);
                    break;
            }

            int pageSize = 8;

            FacturaVenta = await PaginatedList<FacturaVenta>.CreateAsync(
                facturasIQ.Include(c => c.Cliente)
                          .Include(p => p.Proyecto)
                .AsNoTracking(),
                pageIndex ?? 1, pageSize);
        }
    }
}
