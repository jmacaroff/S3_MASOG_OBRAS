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

namespace MASOG_OBRAS.Pages.Clientes.Clientes
{
    public class IndexModel : BaseIndexPage<Cliente>
    {
        private readonly EFDataAccessLibrary.DataAccess.ProductContext _context;

        public IndexModel(EFDataAccessLibrary.DataAccess.ProductContext context)
        {
            _context = context;
        }

        // public IList<Cliente> Clientes { get;set; }

        public PaginatedList<Cliente> Clientes { get; set; }

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


            IQueryable<Cliente> clientesIQ = from c in _context.Clientes select c;

            //veo si el buscador esta vacio para poder realizar la busqueda
            if (!String.IsNullOrEmpty(searchString))
            {
                clientesIQ = clientesIQ.Where(c => c.Nombre.Contains(searchString));
            }


            ////analizo los casos para el ordenamiento
            switch (sortOrder)
            {
                default:
                    clientesIQ = clientesIQ.OrderBy(c => c.Id);
                    break;
            }

            int pageSize = 8;
            Clientes = await PaginatedList<Cliente>.CreateAsync(clientesIQ.AsNoTracking(), pageIndex ?? 1, pageSize);
        }
    }
}
