using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using EFDataAccessLibrary.DataAccess;
using EFDataAccessLibrary.Models.Ventas;
using MASOG_OBRAS.Classes;
using EFDataAccessLibrary.Models.Clientes;
using EFDataAccessLibrary.Models.Inventarios;

namespace MASOG_OBRAS.Pages.Ventas.FacturasVenta
{
    public class CreateModel : BaseCreatePage
    {
        private readonly ProductContext _context;

        // Keys Necesarias (Supongo que son Estas) 
        private readonly string CLIENTE_KEY = "ClienteKey";
        private readonly string PROYECTO_KEY = "ProyectoKey";
        private readonly string FACTURAVENTA_KEY = "FacturaVentaKey";
        private readonly string LISTPRODUCTO_KEY = "ListaProductoKey";

        // Variables de Trabajo

        // Variables booleanas para control de flujo de datos
        [BindProperty]
        public bool HasCliente { get; set; }
        [BindProperty]
        public bool HasProyecto { get; set; }

        [BindProperty]
        public bool HasProduct { get; set; } = false;
        [BindProperty]
        public bool HasProductList { get; set; }
        [BindProperty]
        public bool HasFacturaItems { get; set; } = false;

        // Variables Multiproposito
        [BindProperty]
        public int ClienteId { get; set; }
        [BindProperty]
        public int ProyectoId { get; set; }
        [BindProperty]
        public string ProductoId { get; set; }
        [BindProperty]
        public double Total { get; set; }

        // Objectos para Trabajar
        [BindProperty]
        public List<Proyecto> ClienteProyecto { get; set; }
        [BindProperty]
        public Producto Producto { get; set; }
        [BindProperty]
        public FacturaVenta FacturaVenta { get; set; }
        [BindProperty]
        public FacturaVentaItem FacturaVentaItem { get; set; }
        public List<FacturaVentaItem> FacturaVentaItems { get; set; }


        public CreateModel(ProductContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
        ViewData["ClienteId"] = new SelectList(_context.Clientes, "Id", "Nombre");
        ViewData["ProyectoId"] = new SelectList(_context.Proyectos, "Id", "Direccion");
            return Page();
        }


        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.FacturasVenta.Add(FacturaVenta);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
