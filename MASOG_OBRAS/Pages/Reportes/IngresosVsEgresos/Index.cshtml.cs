using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EFDataAccessLibrary.Models.Ventas;
using MASOG_OBRAS.Classes;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace MASOG_OBRAS.Pages.Reportes.IngresosVsEgresos
{
    public class IndexModel : BaseIndexPage<Comparativo>
    {
        private readonly EFDataAccessLibrary.DataAccess.ProductContext _context;

        public IndexModel(EFDataAccessLibrary.DataAccess.ProductContext context)
        {
            _context = context;
        }

        public PaginatedList<Comparativo> Comparativo { get; set; }
        //Sort
        public string SortAño { get; set; }
        public string SortMes { get; set; }
        public string CurrentSort { get; set; }

        //Search

        public string CurrentFilter { get; set; }
        public string DateFilterAño { get; set; }
        public string DateFilterMes { get; set; }

        public async Task OnGetAsync(string sortOrder, string searchDateFrom, string searchDateTo,
                              string dateFilterFrom, string dateFilterTo, int? pageIndex)

        {
            SortAño = String.IsNullOrEmpty(sortOrder) ? "sortAño_desc" : "";
            SortMes = String.IsNullOrEmpty(sortOrder) ? "sortMes_desc" : "";

            //inicializo el orden actual
            CurrentSort = sortOrder;

            // controlamos los filtros de busqueda
            if (searchDateFrom != null)
            {
                pageIndex = 1;
            }
            else
            {
                searchDateFrom = dateFilterFrom;
            }
            if (searchDateTo != null)
            {
                pageIndex = 1;
            }
            else
            {
                searchDateTo = dateFilterTo;
            }

            DateFilterAño = searchDateFrom;
            DateFilterMes = searchDateTo;

            IQueryable<Comparativo> comparativosIQ = from c in _context.Comparativo where (c.Egresos != 0 || c.Ingresos != 0) select c; //where (c.Egresos != 0 || c.Ingresos != 0) group c by c.Año 

            //veo si el buscador esta vacio para poder realizar la búsqueda

            if (!String.IsNullOrEmpty(searchDateFrom))
            {
                //comparativosIQ = comparativosIQ.Where(c => c.Año > Convert.ToDateTime(searchDateFrom));
                comparativosIQ = comparativosIQ.Where(c => c.Año >= int.Parse(searchDateFrom));

            }

            if (!String.IsNullOrEmpty(searchDateTo))
            {
                comparativosIQ = comparativosIQ.Where(c => c.Mes <= int.Parse(searchDateTo));
            }

            ////analizo los casos para el ordenamiento
            switch (sortOrder)
            {
                case "sortAño_desc":
                    comparativosIQ = comparativosIQ.OrderBy(c => c.Año);
                    break;
                case "sortMes_desc":
                    comparativosIQ = comparativosIQ.OrderByDescending(c => c.Mes);
                    break;
               
                default:
                    comparativosIQ = comparativosIQ.OrderByDescending(c => c.Año);
                    break;
            }

            int pageSize = 8;

            // Se agrega Include(c => c.Proveedor) para que recupere los datos del proveedor asociado a la orden

            Comparativo = await PaginatedList<Comparativo>.CreateAsync(comparativosIQ.AsNoTracking(), pageIndex ?? 1, pageSize);
        }
    }
}
