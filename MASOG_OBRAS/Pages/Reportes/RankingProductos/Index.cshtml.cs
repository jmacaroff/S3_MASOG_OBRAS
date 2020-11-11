using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using EFDataAccessLibrary.DataAccess;
using EFDataAccessLibrary.Models.Ventas;
using EFDataAccessLibrary.Models.Inventarios;
using MASOG_OBRAS.Classes;

namespace MASOG_OBRAS.Pages.Reportes.RankingProductos
{
    public class IndexModel : BaseIndexPage<Producto>
    {
        private readonly EFDataAccessLibrary.DataAccess.ProductContext _context;

        public IndexModel(EFDataAccessLibrary.DataAccess.ProductContext context)
        {
            _context = context;
        }

        public PaginatedList<EFDataAccessLibrary.Models.Ventas.RankingProductos> RankingProductos { get; set; }

        //Search
        public string CurrentSort { get; set; }
        public string CurrentFilter { get; set; }

        public async Task OnGetAsync(string sortOrder, string searchString, string currentFilter, int? pageIndex)

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

            CurrentFilter = searchString;

            IQueryable<EFDataAccessLibrary.Models.Ventas.RankingProductos> rankingIQ = from r in _context.RankingProductos select r;

            //veo si el buscador esta vacio para poder realizar la búsqueda

            if (!String.IsNullOrEmpty(searchString))
            {
                rankingIQ = rankingIQ.Where(c => c.ProductoId.Contains(searchString) || c.Descripcion.Contains(searchString));                  
            }

            ////analizo los casos para el ordenamiento
            switch (sortOrder)
            {
                default:
                    rankingIQ = rankingIQ.OrderByDescending(c => c.Ventas);
                    break;
            }

            int pageSize = 8;

            // Se agrega Include(c => c.Proveedor) para que recupere los datos del proveedor asociado a la orden

            RankingProductos = await PaginatedList<EFDataAccessLibrary.Models.Ventas.RankingProductos>.CreateAsync(rankingIQ, pageIndex ?? 1, pageSize);
        }
    }
}
