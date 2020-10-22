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
using EFDataAccessLibrary.Models.Ventas;

namespace MASOG_OBRAS.Pages.Clientes.Proyectos
{
    public class DeleteModel : BaseDeletePage
    {
        private readonly EFDataAccessLibrary.DataAccess.ProductContext _context;

        public DeleteModel(EFDataAccessLibrary.DataAccess.ProductContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Proyecto Proyecto { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Proyecto = await _context.Proyectos.Include(p => p.Cliente).FirstOrDefaultAsync(m => m.Id == id);

            if (Proyecto == null)
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

            Proyecto = await _context.Proyectos.FindAsync(id);

            if (ExistFactura(Proyecto.Id))
            {
                this.MessageError = "Existe una factura de venta asociada al proyecto.";
                return null;
            }
            if (Proyecto != null)
            {
                _context.Proyectos.Remove(Proyecto);
                return await this.RemoveValue(_context);
            }
            else
            {
                return Page();
            }
        }

        private bool ExistFactura(int id)
        {
            FacturaVenta factura = _context.FacturasVenta.Where(o => o.ProyectoId == id).FirstOrDefault<FacturaVenta>();
            return factura != null;
        }
    }
}
