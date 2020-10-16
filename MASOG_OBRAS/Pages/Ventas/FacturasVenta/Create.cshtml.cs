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
using Microsoft.AspNetCore.Http;
using EFDataAccessLibrary.Migrations;

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
        public int FacturaVentaId { get; set; }
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

        //public IActionResult OnGet()
        //{
        //    ViewData["ClienteId"] = new SelectList(_context.Clientes, "Id", "Nombre");
        //    ViewData["ProyectoId"] = new SelectList(_context.Proyectos, "Id", "Direccion");
        //    return Page();
        //}


        //// To protect from overposting attacks, enable the specific properties you want to bind to, for
        //// more details, see https://aka.ms/RazorPagesCRUD.
        //public async Task<IActionResult> OnPostAsync()
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        return Page();
        //    }

        //    _context.FacturasVenta.Add(FacturaVenta);
        //    await _context.SaveChangesAsync();

        //    return RedirectToPage("./Index");
        //}


        public IActionResult OnGet(string id, string action)
        {
            HttpContext.Session.Remove(FACTURAVENTA_KEY);
            HttpContext.Session.Remove(CLIENTE_KEY);
            ClienteProyecto = new List<Proyecto>();
            LoadCliente();
            LoadProyecto();
            LoadFacturaVenta();
            LoadFacturaItems();
            return Page();
            //ViewData["ClienteId"] = new SelectList(_context.Clientes, "Id", "Nombre");
            //ViewData["ProyectoId"] = new SelectList(_context.Proyectos, "Id", "Direccion");
            //    return Page();
        }

        public void OnPostCliente()
        {
            HttpContext.Session.SetInt32(CLIENTE_KEY, ClienteId);
            HasCliente = true;
            //HttpContext.Session.SetComplexData(FACTURAVENTA_KEY, FacturaVenta);
            //FacturaVentaItems = new List<FacturaVentaItem>();
            HttpContext.Session.SetComplexData(PROYECTO_KEY, ProyectoId);
            ClienteProyecto = new List<Proyecto>();
            LoadProyecto();
            LoadCliente();
        }
        public void OnPostProyecto()
        {
            HttpContext.Session.SetInt32(PROYECTO_KEY, ProyectoId);
            HttpContext.Session.SetInt32(FACTURAVENTA_KEY, FacturaVentaId);
            FacturaVentaItems = _context.FacturaVentaItems.Where(x => x.FacturaVentaId == FacturaVentaId).ToList();
            HasProyecto = true;
            HttpContext.Session.SetComplexData(LISTPRODUCTO_KEY, FacturaVentaItems);
            Total = (double)FacturaVentaItems.Sum(x => x.Precio * x.Cantidad);
            LoadFacturaVenta();
            LoadFacturaItems();
            LoadProyecto();
            LoadCliente();
        }

        // Declaración de Procedimientos
        private void LoadProyecto()
        {
            int clienteId = !HttpContext.Session.Keys.Contains(CLIENTE_KEY) ? -1 : (int)HttpContext.Session.GetInt32(CLIENTE_KEY);
            if (clienteId != -1)
            {
                List<Proyecto> list = _context.Proyectos.Where(x => x.ClienteId == clienteId).ToList();
                _context.FacturasVenta.ToList().ForEach(x =>
                {
                    list = list.Where(y => y.Id != x.ProyectoId).ToList();
                });
                HasProyecto = list.Count != 0;
                ViewData["ProyectoId"] = new SelectList(list, "Id", "Direccion");
            }
        }
        private void LoadCliente()
        {
            ViewData["ClienteId"] = new SelectList(_context.Clientes, "Id", "Nombre");
        }
        private void LoadFacturaVenta()
        {
            FacturaVenta = HttpContext.Session.GetComplexData<FacturaVenta>(FACTURAVENTA_KEY);
        }
        private void LoadFacturaItems()
        {
            List<FacturaVentaItem> list = HttpContext.Session.GetComplexData<List<FacturaVentaItem>>(LISTPRODUCTO_KEY);
            if (list == null)
            {
                FacturaVentaItems = new List<FacturaVentaItem>();
            }
            else
            {
                FacturaVentaItems = list;
                HasFacturaItems = true;
            }
        }

        // Controlar los productos

        public void OnPostProduct()
        {

            Producto = _context.Productos.First<Producto>(x => x.Id == ProductoId);
            HasProduct = true;
            FacturaVentaItem.Precio = Producto.Precio;
            FacturaVentaItem.ProductoId = Producto.Id;
            LoadFacturaVenta();
            LoadFacturaItems();
            LoadProyecto();
            LoadCliente();
        }
        private void SaveFacturaItems()
        {
            HttpContext.Session.SetComplexData(LISTPRODUCTO_KEY, FacturaVentaItems);
        }

        public void OnPostAddItem()
        {
            FacturaVentaItem.FacturaVentaId = FacturaVenta.Id;
            LoadFacturaItems();
            HasFacturaItems = true;
            FacturaVentaItems.Add(FacturaVentaItem);
            SaveFacturaItems();
            LoadFacturaVenta();
            LoadFacturaItems();
            LoadProyecto();
            LoadCliente();
        }
        public void OnPostRemoveItem(string id)
        {
            LoadFacturaItems();
            FacturaVentaItems = FacturaVentaItems.Where(x => x.ProductoId != id).ToList();
            HasFacturaItems = FacturaVentaItems.Count != 0;
            SaveFacturaItems();
            LoadFacturaVenta();
            LoadFacturaItems();
            LoadProyecto();
            LoadCliente();
        }

        public async Task<IActionResult> OnPostSaveFacturaVenta()
        {
            LoadFacturaItems();
            if (FacturaVentaItems.Count == 0)
            {
                MessageError = "No hay items en la factura.";
                LoadFacturaVenta();
                LoadFacturaItems();
                LoadProyecto();
                LoadCliente();
                return Page();
            }
            else
            {
                FacturaVenta.FacturaVentaItems = FacturaVentaItems;
                FacturaVenta.ClienteId = (int)HttpContext.Session.GetInt32(CLIENTE_KEY);
                FacturaVenta.ProyectoId = (int)HttpContext.Session.GetInt32(PROYECTO_KEY);
                _context.FacturasVenta.Add(FacturaVenta);
                return await AddNewValue(_context);
            }
        }
    }
}
