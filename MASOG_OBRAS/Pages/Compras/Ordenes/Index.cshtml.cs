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

namespace MASOG_OBRAS.Pages.Compras.Ordenes
{
    public class IndexModel : BaseIndexPage<Orden>
    {
        private readonly EFDataAccessLibrary.DataAccess.ProductContext _context;

        public IndexModel(EFDataAccessLibrary.DataAccess.ProductContext context)
        {
            _context = context;
        }

        // public IList<Cliente> Clientes { get;set; }

        public PaginatedList<Orden> Ordenes { get; set; }
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

            IQueryable<Orden> ordenesIQ = from o in _context.Ordenes select o;

            //veo si el buscador esta vacio para poder realizar la búsqueda

            if (!String.IsNullOrEmpty(searchString))
            {
                ordenesIQ = ordenesIQ.Where(c => c.Fecha.ToString().Contains(searchString));
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
                    ordenesIQ = ordenesIQ.OrderByDescending(c => c.Fecha);
                    break;
            }

            int pageSize = 8;

            // Se agrega Include(c => c.Proveedor) para que recupere los datos del proveedor asociado a la orden

            Ordenes = await PaginatedList<Orden>.CreateAsync(ordenesIQ.Include(c => c.Proveedor).AsNoTracking(), pageIndex ?? 1, pageSize);
        }
    }
}

