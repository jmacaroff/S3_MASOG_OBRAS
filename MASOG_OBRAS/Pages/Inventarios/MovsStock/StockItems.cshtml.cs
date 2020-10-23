using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EFDataAccessLibrary.Models.Inventarios;
using MASOG_OBRAS.Classes;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace MASOG_OBRAS.Pages.Inventarios.MovsStock
{
    public class StockItemsModel : BaseIndexPage<Stock>
    {
        private readonly EFDataAccessLibrary.DataAccess.ProductContext _context;

        public StockItemsModel(EFDataAccessLibrary.DataAccess.ProductContext context)
        {
            _context = context;
        }

        public PaginatedList<Stock> Stocks { get; set; }
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

            IQueryable<Stock> stockIQ = from o in _context.Stock select o;

            //veo si el buscador esta vacio para poder realizar la búsqueda

            if (!String.IsNullOrEmpty(searchString))
            {
                stockIQ = stockIQ.Where(c => c.ProductoId.ToLower().Contains(searchString) || c.Producto.Descripcion.Contains(searchString));
            }

            ////analizo los casos para el ordenamiento
            switch (sortOrder)
            {
                default:
                    stockIQ = stockIQ.OrderBy(c => c.Id);
                    break;
            }

            int pageSize = 5;

            // Se agrega Include(c => c.Proveedor) para que recupere los datos del proveedor asociado a la orden

            Stocks = await PaginatedList<Stock>.CreateAsync(stockIQ
                .Include(c => c.Deposito)
                .Include(c => c.Producto)
                .AsNoTracking(), pageIndex ?? 1, pageSize);
        }
    }
}
