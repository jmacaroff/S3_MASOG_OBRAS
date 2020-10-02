using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using EFDataAccessLibrary.DataAccess;
using EFDataAccessLibrary.Models.Compras;
using EFDataAccessLibrary.Models.Proveedores;
using MASOG_OBRAS.Classes;

namespace MASOG_OBRAS.Pages.Compras.Facturas
{
    public class IndexModel : BaseIndexPage<FacturaCompra>
    {
        private readonly EFDataAccessLibrary.DataAccess.ProductContext _context;

        public IndexModel(EFDataAccessLibrary.DataAccess.ProductContext context)
        {
            _context = context;
        }

        // public IList<Cliente> Clientes { get;set; }

        public PaginatedList<FacturaCompra> FacturasCompra { get; set; }
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

            IQueryable<FacturaCompra> facturaIQ = from o in _context.FacturasCompra select o;

            //veo si el buscador esta vacio para poder realizar la búsqueda

            if (!String.IsNullOrEmpty(searchString))
            {
                facturaIQ = facturaIQ.Where(c => c.FechaFactura.ToString().Contains(searchString));
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
                    facturaIQ = facturaIQ.OrderBy(c => c.Id);
                    break;
            }

            int pageSize = 5;

            // Se agrega Include(c => c.Proveedor) para que recupere los datos del proveedor asociado a la orden

            FacturasCompra = await PaginatedList<FacturaCompra>.CreateAsync(facturaIQ.Include(p => p.Proveedor).AsNoTracking(), pageIndex ?? 1, pageSize);
        }
    }
}

