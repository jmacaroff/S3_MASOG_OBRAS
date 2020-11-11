using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using EFDataAccessLibrary.DataAccess;
using EFDataAccessLibrary.Models.Compras;
using MASOG_OBRAS.Classes;

namespace MASOG_OBRAS.Pages.Compras.OrdenesPago
{
    public class IndexModel : BaseIndexPage<OrdenPago>
    {
        private readonly EFDataAccessLibrary.DataAccess.ProductContext _context;

        public IndexModel(EFDataAccessLibrary.DataAccess.ProductContext context)
        {
            _context = context;
        }

        public PaginatedList<OrdenPago> OrdenesPago { get;set; }

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

            IQueryable<OrdenPago> ordenesIQ = from o in _context.OrdenesPago select o;

            //veo si el buscador esta vacio para poder realizar la búsqueda

            if (!String.IsNullOrEmpty(searchString))
            {
                ordenesIQ = ordenesIQ.Where(c => c.FechaEmision.ToString().Contains(searchString));
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
                    ordenesIQ = ordenesIQ.OrderByDescending(c => c.FechaEmision);
                    break;
            }

            int pageSize = 8;

            // Se agrega Include(c => c.Proveedor) para que recupere los datos del proveedor asociado a la orden

            OrdenesPago = await PaginatedList<OrdenPago>.CreateAsync(ordenesIQ.Include(o => o.ConceptoPago)
                                                                              .Include(o => o.Proveedor)
                                                                              .AsNoTracking(), pageIndex ?? 1, pageSize);

            //Orden de Pago
            //OrdenPago = await _context.OrdenesPago
            //  .Include(o => o.ConceptoPago)
            //  .Include(o => o.Proveedor).ToListAsync();
        }
    }
}
