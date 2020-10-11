using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using EFDataAccessLibrary.DataAccess;
using EFDataAccessLibrary.Models.Clientes;
using MASOG_OBRAS.Classes;

namespace MASOG_OBRAS.Pages.Clientes.Proyectos
{
    public class IndexModel : BaseIndexPage<Proyecto>
    {
        private readonly EFDataAccessLibrary.DataAccess.ProductContext _context;

        public IndexModel(EFDataAccessLibrary.DataAccess.ProductContext context)
        {
            _context = context;
        }

        // public IList<Proyecto> Proyecto { get;set; }

        public PaginatedList<Proyecto> Proyectos { get; set; }

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


            IQueryable<Proyecto> proyectosIQ = from p in _context.Proyectos select p;

            //veo si el buscador esta vacio para poder realizar la busqueda
            if (!String.IsNullOrEmpty(searchString))
            {
                proyectosIQ = proyectosIQ.Where(p => p.Nombre.Contains(searchString)); 
            }


            ////analizo los casos para el ordenamiento
            switch (sortOrder)
            {
                default:
                    proyectosIQ = proyectosIQ.OrderBy(p => p.Id);
                    break;
            }

            int pageSize = 5;
            Proyectos = await PaginatedList<Proyecto>.CreateAsync(proyectosIQ.Include(p => p.Cliente).AsNoTracking(), pageIndex ?? 1, pageSize);
        }
    }
}
