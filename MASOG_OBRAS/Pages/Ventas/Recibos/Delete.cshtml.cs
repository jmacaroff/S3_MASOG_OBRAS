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
    public class DeleteModel : BaseDeletePage
    {
        private readonly EFDataAccessLibrary.DataAccess.ProductContext _context;

        public DeleteModel(EFDataAccessLibrary.DataAccess.ProductContext context)
        {
            _context = context;
        }

        [BindProperty]
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

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Recibo = await _context.Recibos.FindAsync(id);

            if (Recibo != null)
            {
                List<int> list = _context.ReciboItems.Where(z => z.ReciboId == id).Select(x => x.FacturaVentaId).ToList();
                list.ForEach(x =>
                {
                    _context.FacturasVenta.First(y => y.Id == x).PendienteCobrar = _context.FacturasVenta.First(y => y.Id == x).Total;
                });
                _context.Recibos.Remove(Recibo);
                return await RemoveValue(_context);
            }
            else
            {
                return Page();
            }
        }
    }
}
