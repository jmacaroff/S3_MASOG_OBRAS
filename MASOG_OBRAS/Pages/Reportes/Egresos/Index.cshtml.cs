using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EFDataAccessLibrary.Models.Compras;
using MASOG_OBRAS.Classes;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace MASOG_OBRAS.Pages.Reportes.Egresos
{
    public class IndexModel : BaseIndexPage<OrdenPago>
    {
        private readonly EFDataAccessLibrary.DataAccess.ProductContext _context;

        public IndexModel(EFDataAccessLibrary.DataAccess.ProductContext context)
        {
            _context = context;
        }

        public PaginatedList<OrdenesPagoDet> OrdenesPagoDet { get; set; }

        //Sort
        public string CustomerName { get; set; }
        public string ProductDescription { get; set; }
        public string ProductID { get; set; }
        public string DateSort { get; set; }
        //Search
        public string CurrentSort { get; set; }
        public string CurrentFilter { get; set; }
        public string CurrentFilter2 { get; set; }
        public string DateFilterFrom { get; set; }
        public string DateFilterTo { get; set; }

        public async Task OnGetAsync(string sortOrder, string searchDateFrom, string searchDateTo, string searchString, string searchString2,
                                     string dateFilterFrom, string dateFilterTo, string currentFilter, string currentFilter2, int? pageIndex)

        {
            CustomerName = String.IsNullOrEmpty(sortOrder) ? "customerName_desc" : "";
            ProductDescription = String.IsNullOrEmpty(sortOrder) ? "productDesciption_desc" : "";
            ProductID = String.IsNullOrEmpty(sortOrder) ? "productId_desc" : "";
            DateSort = sortOrder == "Date" ? "date_desc" : "Date";

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
            if (searchString2 != null)
            {
                pageIndex = 1;
            }
            else
            {
                searchString2 = currentFilter2;
            }
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

            CurrentFilter = searchString;
            CurrentFilter2 = searchString2;
            DateFilterFrom = searchDateFrom;
            DateFilterTo = searchDateTo;

            IQueryable<OrdenesPagoDet> recibosIQ = from o in _context.OrdenesPagoDet select o;

            //veo si el buscador esta vacio para poder realizar la búsqueda

            if (!String.IsNullOrEmpty(searchString))
            {
                recibosIQ = recibosIQ.Where(c => c.ProveedorNombre.Contains(searchString) || c.ProveedorId.ToString().Contains(searchString));
            }

            if (!String.IsNullOrEmpty(searchString2))
            {
                recibosIQ = recibosIQ.Where(c => c.ProductoId.Contains(searchString2) || c.ProductoDescripcion.Contains(searchString2));
            }

            if (!String.IsNullOrEmpty(searchDateFrom))
            {
                recibosIQ = recibosIQ.Where(c => c.FechaOrdenPago > Convert.ToDateTime(searchDateFrom));
            }

            if (!String.IsNullOrEmpty(searchDateTo))
            {
                recibosIQ = recibosIQ.Where(c => c.FechaOrdenPago < Convert.ToDateTime(searchDateTo));
            }

            ////analizo los casos para el ordenamiento
            switch (sortOrder)
            {
                case "customerName_desc":
                    recibosIQ = recibosIQ.OrderByDescending(p => p.ProveedorNombre);
                    break;
                case "productDesciption_desc":
                    recibosIQ = recibosIQ.OrderByDescending(p => p.ProductoDescripcion);
                    break;
                case "productId_desc":
                    recibosIQ = recibosIQ.OrderBy(p => p.ProductoId);
                    break;
                case "Date":
                    recibosIQ = recibosIQ.OrderBy(p => p.FechaOrdenPago);
                    break;
                case "date_desc":
                    recibosIQ = recibosIQ.OrderByDescending(p => p.FechaOrdenPago);
                    break;
                default:
                    recibosIQ = recibosIQ.OrderByDescending(c => c.ProveedorId);
                    break;
            }

            int pageSize = 5;

            // Se agrega Include(c => c.Proveedor) para que recupere los datos del proveedor asociado a la orden

            OrdenesPagoDet = await PaginatedList<OrdenesPagoDet>.CreateAsync(recibosIQ, pageIndex ?? 1, pageSize);

        }
    }
}
