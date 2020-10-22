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
        public bool HasCliente { get; set; } = false;
        [BindProperty]
        public bool HasProyecto { get; set; } = false;
        [BindProperty]
        public bool HasProyectoSelected { get; set; } = false;

        [BindProperty]
        public bool HasProduct { get; set; } = false;
        [BindProperty]
        public bool HasProductList { get; set; } = false;
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
        public decimal Total { get; set; }

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

        public IActionResult OnGet(string id, string action)
        {
            HttpContext.Session.Remove(FACTURAVENTA_KEY);
            HttpContext.Session.Remove(CLIENTE_KEY);
            ClienteProyecto = new List<Proyecto>();
            LoadCliente();
            return Page();
        }

        public void OnPostCliente()
        {
            FacturaVenta.ClienteId = ClienteId;
            HttpContext.Session.SetInt32(CLIENTE_KEY, ClienteId);
            LoadProyecto();
            if (HasProyecto)
            {
                HttpContext.Session.SetComplexData(FACTURAVENTA_KEY, FacturaVenta);
            }
            HasCliente = true;
            LoadCliente();
            LoadFacturaVenta();
        }
        public void OnPostProyecto()
        {
            HttpContext.Session.SetInt32(PROYECTO_KEY, ProyectoId);
            HasProyectoSelected = true;
            LoadFacturaVenta();
            LoadFacturaItems();
            LoadProyecto();
            LoadCliente();
            LoadProductos();
        }
        public void OnPostProduct()
        {
            Producto = _context.Productos.First(x => x.Id == ProductoId);
            HasProduct = true;
            FacturaVentaItem = new FacturaVentaItem();
            FacturaVentaItem.ProductoId = ProductoId;
            FacturaVentaItem.Precio = Producto.Precio;
            LoadFacturaItems();
            LoadFacturaVenta();
            LoadProyecto();
            LoadCliente();
            LoadProductos();
        }
        public void OnPostAddItem()
        {
            // ADD ORDEN
            LoadFacturaItems();
            FacturaVentaItems.Add(FacturaVentaItem);
            SaveFacturaItems();
            LoadFacturaVenta();
            LoadProyecto();
            LoadCliente();
            LoadProductos();
            LoadFacturaItems();
        }
        
        private void LoadProyecto()
        {
            int clienteId = !HttpContext.Session.Keys.Contains(CLIENTE_KEY) ? -1 : (int)HttpContext.Session.GetInt32(CLIENTE_KEY);
            if (clienteId != -1)
            {
                int proyectId = !HttpContext.Session.Keys.Contains(PROYECTO_KEY) ? -1 : (int)HttpContext.Session.GetInt32(PROYECTO_KEY);
                if (proyectId != -1)
                {
                    ViewData["ProyectoId"] = new SelectList(_context.Proyectos.Where(x => x.Id == proyectId).ToList(), "Id", "Nombre");
                    HasProyecto = true;
                    HasProyectoSelected = true;
                }
                else
                {
                    List<Proyecto> list = _context.Proyectos.Where(x => x.ClienteId == clienteId).ToList();
                    HasProyecto = list.Count != 0;
                    ViewData["ProyectoId"] = new SelectList(list, "Id", "Nombre");
                }
                
            }
        }
        private void LoadCliente()
        {
            int clienteId = !HttpContext.Session.Keys.Contains(CLIENTE_KEY) ? -1 : (int)HttpContext.Session.GetInt32(CLIENTE_KEY);
            if (clienteId != -1)
            {
                ViewData["ClienteId"] = new SelectList(_context.Clientes.Where(x => x.Id == clienteId).ToList(), "Id", "Nombre");
            }
            else
            {
                ViewData["ClienteId"] = new SelectList(_context.Clientes, "Id", "Nombre");              
            }
        }
        private void LoadProductos()
        {
            List<Producto> list = _context.Productos.ToList();
            FacturaVentaItems.ForEach(x =>
            {
                list = list.Where(y => y.Id != x.ProductoId).ToList();
            });
            HasProductList = list.Count > 0;
            ViewData["ProductoId"] = new SelectList(list, "Id", "Descripcion");
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
                HasFacturaItems = false;
            }
            else
            {
                Total = 0;
                FacturaVentaItems = list;
                HasFacturaItems = true;
                FacturaVentaItems.ForEach(x =>
                {
                    Total = Total + x.Cantidad * x.Precio;
                });
            }
        }
        private void SaveFacturaItems()
        {
            HttpContext.Session.SetComplexData(LISTPRODUCTO_KEY, FacturaVentaItems);
        }

        public void OnPostRemoveItem(string id)
        {
            LoadFacturaItems();
            FacturaVentaItems = FacturaVentaItems.Where(x => x.ProductoId != id).ToList();
            SaveFacturaItems();
            LoadFacturaItems();
            LoadFacturaVenta();
            LoadProyecto();
            LoadCliente();
            LoadProductos();
            LoadFacturaItems();
        }

        public async Task<IActionResult> OnPostSaveFacturaVenta()
        {
            LoadFacturaItems();
            LoadFacturaVenta();
            LoadFacturaItems();
            LoadProyecto();
            LoadCliente();
            if (FacturaVentaItems.Count == 0)
            {
                MessageError = "No hay items en la factura.";
                return Page();
            }
            else
            {
                FacturaVenta.FacturaVentaItems = FacturaVentaItems;
                FacturaVenta.Total = FacturaVenta.PendienteCobrar = (double)Total;
                FacturaVenta.ClienteId = (int)HttpContext.Session.GetInt32(CLIENTE_KEY);
                FacturaVenta.ProyectoId = (int)HttpContext.Session.GetInt32(PROYECTO_KEY);
                _context.FacturasVenta.Add(FacturaVenta);
                return await AddNewValue(_context);
            }
        }
    }
}
