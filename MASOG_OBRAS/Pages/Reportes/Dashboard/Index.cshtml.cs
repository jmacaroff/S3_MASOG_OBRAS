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

        //Search
        public string CurrentAF { get; set; }
        public string CurrentAT { get; set; }
        public string CurrentMF { get; set; }
        public string CurrentMT { get; set; }

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
        public string TotalIngresosS { get; set; }
        [BindProperty]
        public string TotalEgresosS { get; set; }
        [BindProperty]
        public string BalancePeriodoS { get; set; }
        [BindProperty]
        public int TotalProyecto { get; set; }
        [BindProperty]
        public RecibosDet RecibosDet { get; set; }

        public async Task<IActionResult> OnGetAsync(string searchAF, string searchAT, string currentAF, string currentAT,
                              string searchMF, string searchMT, string currentMF, string currentMT)
        {
            // TotalVentas = _context.RecibosDet.Select(r => r.FacturaVentaNumero).Distinct().Count();
            // TotalVentas = _context.RecibosDet.Select(r => r.Total).Sum(); => esto tira el total de toda la tabla
            // TotalCompra = _context.OrdenesPagoDet.Select(o => o.FacturaCompraNumero).Distinct().Count();


            // controlamos los filtros de busqueda
            if (searchAF == null)
            {
                searchAF = currentAF;
            }
            if (searchAT == null)
            {
                searchAT = currentAT;
            }
            if (searchMF == null)
            {
                searchMF = currentMF;
            }
            if (searchMT == null)
            {
                searchMT = currentMT;
            }

            CurrentAF = searchAF;
            CurrentAT = searchAT;
            CurrentMF = searchMF;
            CurrentMT = searchMT;

            IQueryable<Comparativo> comparativosIQ = from c in _context.Comparativo where (c.Egresos != 0 || c.Ingresos != 0) select c;

            if (!String.IsNullOrEmpty(searchAF))
            {
                comparativosIQ = comparativosIQ.Where(c => c.Año >= int.Parse(searchAF));

            }

            if (!String.IsNullOrEmpty(searchAT))
            {
                comparativosIQ = comparativosIQ.Where(c => c.Año <= int.Parse(searchAT));
            }

            if (!String.IsNullOrEmpty(searchMF))
            {
                comparativosIQ = comparativosIQ.Where(c => c.Mes >= int.Parse(searchMF));

            }

            if (!String.IsNullOrEmpty(searchMT))
            {
                comparativosIQ = comparativosIQ.Where(c => c.Mes <= int.Parse(searchMT));
            }

            Comparativos = await comparativosIQ.FirstOrDefaultAsync();
            TotalIngresos = await comparativosIQ.GroupBy(o => "1").Select(c => c.Sum(i => i.Ingresos)).FirstOrDefaultAsync();
            TotalEgresos = await comparativosIQ.GroupBy(o => "1").Select(c => c.Sum(i => i.Egresos)).FirstOrDefaultAsync();
            BalancePeriodo = await comparativosIQ.GroupBy(o => "1").Select(c => c.Sum(i => i.Ingresos - i.Egresos)).FirstOrDefaultAsync();

            if (TotalIngresos >= 1000000)
                TotalIngresosS = (TotalIngresos / 1000000D).ToString("0.##") + "M";
            else if (TotalIngresos >= 1000)
                TotalIngresosS = (TotalIngresos / 1000D).ToString("0.##") + "K";

            if (TotalEgresos >= 1000000)
                TotalEgresosS = (TotalEgresos / 1000000D).ToString("0.##") + "M";
            else if (TotalEgresos >= 1000)
                TotalEgresosS = (TotalEgresos / 1000D).ToString("0.##") + "K";

            if (BalancePeriodo >= 1000000)
                BalancePeriodoS = (BalancePeriodo / 1000000D).ToString("0.##") + "M";
            else if (BalancePeriodo >= 1000)
                BalancePeriodoS = (BalancePeriodo / 1000D).ToString("0.##") + "K";



            TotalProyecto = await _context.Proyectos.Distinct().CountAsync();
            return Page();
        }

        public async Task<JsonResult> OnGetInvoiceChartData(string searchAF, string searchAT, string searchMF, string searchMT)
        {

            IQueryable<Comparativo> comparativosIQ = from c in _context.Comparativo where (c.Egresos != 0 || c.Ingresos != 0) select c;

            if (!String.IsNullOrEmpty(searchAF))
            {
                comparativosIQ = comparativosIQ.Where(c => c.Año >= int.Parse(searchAF));

            }

            if (!String.IsNullOrEmpty(searchAT))
            {
                comparativosIQ = comparativosIQ.Where(c => c.Año <= int.Parse(searchAT));
            }

            if (!String.IsNullOrEmpty(searchMF))
            {
                comparativosIQ = comparativosIQ.Where(c => c.Mes >= int.Parse(searchMF));

            }

            if (!String.IsNullOrEmpty(searchMT))
            {
                comparativosIQ = comparativosIQ.Where(c => c.Mes <= int.Parse(searchMT));
            }

            ComparativosList = await comparativosIQ.ToListAsync();
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
