using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using EFDataAccessLibrary.DataAccess;
using EFDataAccessLibrary.Models.Inventarios;
using MASOG_OBRAS.Classes;

namespace MASOG_OBRAS.Pages.Inventarios.Productos
{

    public class IndexModel : BaseIndexPage<Producto>
    {
        private readonly EFDataAccessLibrary.DataAccess.ProductContext _context;

        public IndexModel(EFDataAccessLibrary.DataAccess.ProductContext context)
        {
            _context = context;
        }

        //public IList<Producto> Productos { get;set; }
        public PaginatedList<Producto> Productos { get; set; }


        public string PrecioSort { get; set; }
        public string DescriptionSort { get; set; }
        public string CurrentFilter { get; set; }
        public string CurrentSort { get; set; }

        public async Task OnGetAsync(string sortOrder, string currentFilter ,string searchString, int?pageIndex)
        {
            //inicializo el orden actual
            CurrentSort = sortOrder;

            //Ordenamiento y Filtro de Busqueda
            //PrecioSort = String.IsNullOrEmpty(sortOrder) ? "precio" : "";
            //DescriptionSort = String.IsNullOrEmpty(sortOrder) ? "name_dec" : "";

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

            IQueryable<Producto> productosIQ = from p in _context.Productos select p;

            //veo si el buscador esta vacio para poder realizar la busqueda
            if (!String.IsNullOrEmpty(searchString))
            {
                productosIQ = productosIQ.Where(p => p.Id.Contains(searchString) || p.Descripcion.Contains(searchString));
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
                      productosIQ = productosIQ.OrderBy(p => p.Id);
                      break;
            }

            int pageSize = 2;
            Productos = await PaginatedList<Producto>.CreateAsync(productosIQ.AsNoTracking(), pageIndex ?? 1, pageSize);
        }

    }
}