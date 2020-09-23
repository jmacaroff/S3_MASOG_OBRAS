using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using EFDataAccessLibrary.DataAccess;
using EFDataAccessLibrary.Models.Proveedores;
using MASOG_OBRAS.Classes;

namespace MASOG_OBRAS.Pages.Proveedores.Proveedores
{
    public class IndexModel : BaseIndexPage<Proveedor>
    {
        private readonly EFDataAccessLibrary.DataAccess.ProductContext _context;

        public IndexModel(EFDataAccessLibrary.DataAccess.ProductContext context)
        {
            _context = context;
        }

        public IList<Proveedor> Proveedores { get;set; }

        public async Task OnGetAsync(int pageNumber, int pageSize)
        {
            pageNumber = pageNumber == 0 ? 1 : pageNumber;
            pageSize = pageSize == 0 ? 10 : pageSize;
            this.CurrentPage = pageNumber;
            Init(await _context.Proveedores.ToListAsync(), pageNumber, pageSize);
            Proveedores = LoadPage();
        }

    }
}
