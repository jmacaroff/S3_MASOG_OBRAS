using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using EFDataAccessLibrary.DataAccess;
using EFDataAccessLibrary.Models.Inventarios;

namespace MASOG_OBRAS.Pages.Inventarios.Productos
{
    public class IndexModel : PageModel
    {
        private readonly EFDataAccessLibrary.DataAccess.ProductContext _context;

        public IndexModel(EFDataAccessLibrary.DataAccess.ProductContext context)
        {
            _context = context;
        }

        public IList<Producto> Producto { get;set; }

        public async Task OnGetAsync()
        {
            Producto = await _context.Productos.ToListAsync();
        }
    }
}
