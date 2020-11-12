using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EFDataAccessLibrary.Models.Ventas;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace MASOG_OBRAS.Pages.Reportes.Dashboard
{
    public class IndexModel : PageModel
    {
        private readonly EFDataAccessLibrary.DataAccess.ProductContext _context;

        public IndexModel(EFDataAccessLibrary.DataAccess.ProductContext context)
        {
            _context = context;
        }
        [BindProperty]
        Comparativo Comparativos { get; set; }
        //Variables
        [BindProperty]
        List<Comparativo> ComparativosList { get; set; }
        [BindProperty]
        public double TotalIngresos { get; set; }
        [BindProperty]
        public double TotalEgresos { get; set; }
        [BindProperty]
        public double BalancePeriodo { get; set; }
        [BindProperty]
        public int TotalProyecto { get; set; }
        [BindProperty]
        public RecibosDet RecibosDet { get; set; }

        public async Task<IActionResult> OnGetAsync()
        {
            // TotalVentas = _context.RecibosDet.Select(r => r.FacturaVentaNumero).Distinct().Count();
            // TotalVentas = _context.RecibosDet.Select(r => r.Total).Sum(); => esto tira el total de toda la tabla
            // TotalCompra = _context.OrdenesPagoDet.Select(o => o.FacturaCompraNumero).Distinct().Count();

            // Usando Comparativos y Obteniendo los ultimos datos del mes
            Comparativos = await _context.Comparativo.OrderByDescending(c => c.Año).ThenBy(c => c.Mes).Where(c => c.Ingresos != 0 || c.Egresos != 0).FirstOrDefaultAsync();
            TotalIngresos = await _context.Comparativo.GroupBy(o => "1").Select(c => c.Sum(i => i.Ingresos)).FirstOrDefaultAsync();
            TotalEgresos = await _context.Comparativo.GroupBy(o => "1").Select(c => c.Sum(i => i.Egresos)).FirstOrDefaultAsync();
            BalancePeriodo = await _context.Comparativo.GroupBy(o => "1").Select(c => c.Sum(i => i.Ingresos - i.Egresos)).FirstOrDefaultAsync();

            TotalProyecto = await _context.Proyectos.Distinct().CountAsync();
            return Page();
        }

        public JsonResult OnGetInvoiceChartData()
        {
            ComparativosList = _context.Comparativo.Where(c => c.Ingresos != 0 || c.Egresos != 0).ToList();
            var resumenChart = new CategoryChartModel();
            resumenChart.AmountEntryList = new List<double>();
            resumenChart.AmountEgressList = new List<double>();
            resumenChart.MonthList = new List<string>();

            //para el nombre del mes

            foreach (var inv in ComparativosList)
            {
                resumenChart.AmountEntryList.Add(inv.Ingresos);
                resumenChart.AmountEgressList.Add(inv.Egresos);
                resumenChart.MonthList.Add(MonthName(inv.Mes));                 
            }

            return new JsonResult(resumenChart);
        }

        public string MonthName(int m)
        {
            string mes = "";
            switch (m)
            {
                case 1:
                    mes = "Enero";
                    break;

                case 2:
                    mes = "Febrero";
                    break;

                case 3:
                    mes = "Marzo";
                    break;

                case 4:
                    mes = "Abril";
                    break;

                case 5:
                    mes = "Mayo";
                    break;

                case 6:

                    mes = "Junio";
                    break;

                case 7:
                    mes = "Julio";
                    break;

                case 8:
                    mes = "Agosto";
                    break;

                case 9:
                    mes = "Septiembre";
                    break;

                case 10:
                    mes = "Octubre";
                    break;

                case 11:
                    mes = "Noviembre";
                    break;

                case 12:
                    mes = "Diciembre";
                    break;

            };
            return mes;

        }

    }
}
