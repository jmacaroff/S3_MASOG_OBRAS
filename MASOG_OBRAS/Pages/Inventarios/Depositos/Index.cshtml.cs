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

namespace MASOG_OBRAS.Pages.Inventarios.Depositos
{
    public class IndexModel : BaseIndexPage<Deposito>
    {
        private readonly EFDataAccessLibrary.DataAccess.ProductContext _context;

        public IndexModel(EFDataAccessLibrary.DataAccess.ProductContext context)
        {
            _context = context;
        }

        //public IList<Deposito> Productos { get;set; }
        public PaginatedList<Deposito> Depositos { get; set; }

        public string CurrentFilter { get; set; }
        public string CurrentSort { get; set; }

        public async Task OnGetAsync(string sortOrder, string currentFilter, string searchString, int? pageIndex)
        {
            //inicializo el orden actual
            CurrentSort = sortOrder;

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

            IQueryable<Deposito> depositosIQ = from d in _context.Depositos select d;

            //veo si el buscador esta vacio para poder realizar la busqueda
            if (!String.IsNullOrEmpty(searchString))
            {
                depositosIQ = depositosIQ.Where(d => d.Id.Contains(searchString) || d.Descripcion.Contains(searchString));
            }

            ////analizo los casos para el ordenamiento
            switch (sortOrder)
            {
                default:
                    depositosIQ = depositosIQ.OrderBy(d => d.Id);
                    break;
            }

            int pageSize = 8;
            Depositos = await PaginatedList<Deposito>.CreateAsync(depositosIQ.AsNoTracking(), pageIndex ?? 1, pageSize);
        }
    }
}
