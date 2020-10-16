using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using EFDataAccessLibrary.DataAccess;
using EFDataAccessLibrary.Models.Ventas;
using MASOG_OBRAS.Classes;

namespace MASOG_OBRAS.Pages.Ventas.Recibos
{
    public class DetailsModel : BaseCreatePage
    {
        private readonly EFDataAccessLibrary.DataAccess.ProductContext _context;

        public DetailsModel(EFDataAccessLibrary.DataAccess.ProductContext context)
        {
            _context = context;
        }

        public Recibo Recibo { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Recibo = await _context.Recibos
                .Include(r => r.Cliente)
                .Include(r => r.ConceptoPago).FirstOrDefaultAsync(m => m.Id == id);

            if (Recibo == null)
            {
                return NotFound();
            }
            return Page();
        }
    }
}
