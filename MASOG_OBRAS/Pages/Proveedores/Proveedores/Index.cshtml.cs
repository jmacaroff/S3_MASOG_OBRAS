using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using EFDataAccessLibrary.DataAccess;
using EFDataAccessLibrary.Models.Proveedores;
using MASOG_OBRAS.Classes;

namespace MASOG_OBRAS.Pages.Proveedores.Proveedores
{
    public class IndexModel : BaseIndexPage<Proveedor>
    {
        private readonly EFDataAccessLibrary.DataAccess.ProductContext _context;

        public IndexModel(EFDataAccessLibrary.DataAccess.ProductContext context)
        {
            _context = context;
        }

        // public IList<Proveedor> Proveedores { get;set; }

        public PaginatedList<Proveedor> Proveedores { get; set; }

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


            IQueryable<Proveedor> proveedoresIQ = from p in _context.Proveedores select p;

            //veo si el buscador esta vacio para poder realizar la busqueda
            if (!String.IsNullOrEmpty(searchString))
            {
                proveedoresIQ = proveedoresIQ.Where(p => p.RazonSocial.Contains(searchString));
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
                    proveedoresIQ = proveedoresIQ.OrderBy(p => p.Id);
                    break;
            }

            int pageSize = 2;
            Proveedores = await PaginatedList<Proveedor>.CreateAsync(proveedoresIQ.AsNoTracking(), pageIndex ?? 1, pageSize);

        }

    }
}
