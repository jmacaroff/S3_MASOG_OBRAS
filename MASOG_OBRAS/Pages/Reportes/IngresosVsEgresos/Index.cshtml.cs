using System;
using System.Collections.Generic;
using System.IO;
using OfficeOpenXml;
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

        public string CurrentAF { get; set; }
        public string CurrentAT { get; set; }
        public string CurrentMF { get; set; }
        public string CurrentMT { get; set; }

        public async Task OnGetAsync(string sortOrder, string searchAF, string searchAT, string currentAF, string currentAT,
                              string searchMF, string searchMT, string currentMF, string currentMT, int? pageIndex)

        {
            SortAño = String.IsNullOrEmpty(sortOrder) ? "sortAño_desc" : "";
            SortMes = String.IsNullOrEmpty(sortOrder) ? "sortMes_desc" : "";

            //inicializo el orden actual
            CurrentSort = sortOrder;

            // controlamos los filtros de busqueda
            if (searchAF != null)
            {
                pageIndex = 1;
            }
            else
            {
                searchAF = currentAF;
            }
            if (searchAT != null)
            {
                pageIndex = 1;
            }
            else
            {
                searchAT = currentAT;
            }
            if (searchMF != null)
            {
                pageIndex = 1;
            }
            else
            {
                searchMF = currentMF;
            }
            if (searchMT != null)
            {
                pageIndex = 1;
            }
            else
            {
                searchMT = currentMT;
            }

            CurrentAF = searchAF;
            CurrentAT = searchAT;
            CurrentMF = searchMF;
            CurrentMT = searchMT;

            IQueryable<Comparativo> comparativosIQ = from c in _context.Comparativo where (c.Egresos != 0 || c.Ingresos != 0) select c; //where (c.Egresos != 0 || c.Ingresos != 0) group c by c.Año 

            //veo si el buscador esta vacio para poder realizar la búsqueda

            if (!String.IsNullOrEmpty(searchAF))
            {
                //comparativosIQ = comparativosIQ.Where(c => c.Año > Convert.ToDateTime(searchDateFrom));
                comparativosIQ = comparativosIQ.Where(c => c.Año >= int.Parse(searchAF));

            }

            if (!String.IsNullOrEmpty(searchAT))
            {
                comparativosIQ = comparativosIQ.Where(c => c.Año <= int.Parse(searchAT));
            }

            if (!String.IsNullOrEmpty(searchMF))
            {
                //comparativosIQ = comparativosIQ.Where(c => c.Año > Convert.ToDateTime(searchDateFrom));
                comparativosIQ = comparativosIQ.Where(c => c.Mes >= int.Parse(searchMF));

            }

            if (!String.IsNullOrEmpty(searchMT))
            {
                comparativosIQ = comparativosIQ.Where(c => c.Mes <= int.Parse(searchMT));
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

        public async Task<IActionResult> OnPostExportExcelAsync()
        {

            var myBUs = await _context.Comparativo.Where(c => c.Ingresos != 0 || c.Egresos != 0).ToListAsync();
            // above code loads the data using LINQ with EF (query of table), you can substitute this with any data source.
            var stream = new MemoryStream();
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
            using (var package = new ExcelPackage(stream))
            {
                var workSheet = package.Workbook.Worksheets.Add("Sheet1");
                workSheet.Cells.LoadFromCollection(myBUs, true);
                package.Save();
            }
            stream.Position = 0;

            string excelName = $"MasogObras-{DateTime.Now.ToString("dd MMMM yyyy")}.xlsx";
            // above I define the name of the file using the current datetime.

            return File(stream, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", excelName); // this will be the actual export.
        }
    }
}
