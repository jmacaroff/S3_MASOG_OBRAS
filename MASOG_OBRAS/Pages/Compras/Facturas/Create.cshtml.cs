using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using EFDataAccessLibrary.DataAccess;
using EFDataAccessLibrary.Models.Compras;
using Microsoft.Data.SqlClient;
using System.Reflection;
using Microsoft.EntityFrameworkCore;
using EFDataAccessLibrary.Models;
using MASOG_OBRAS.Classes;
using EFDataAccessLibrary.Models.Inventarios;
using EFDataAccessLibrary.Models.Proveedores;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;

namespace MASOG_OBRAS.Pages.Compras.Facturas
{
    public class CreateModel : BaseCreatePage
    {
        private readonly ProductContext _context;
        private readonly string FACTURA_KEY = "FacturaKey";
        private readonly string ORDEN_KEY = "OrdenKey";
        private readonly string ORDEN_ITEMS_KEY = "OrdenItemsKey";
        private readonly string PROVEEDOR_KEY = "ProveedorKey";

        [BindProperty]
        public bool HasProveedor { get; set; }
        [BindProperty]
        public bool HasOrden { get; set; }
        [BindProperty]
        public bool HasOrdenItem { get; set; }
        [BindProperty]
        public int ProveedorId { get; set; }
        [BindProperty]
        public int OrdenId { get; set; }
        [BindProperty]
        public double Total { get; set; }
        [BindProperty]
        public List<OrdenItem> OrdenItems { get; set; }
        [BindProperty]
        public FacturaCompra FacturaCompra { get; set; }
        [BindProperty]
        public FacturaCompraItem FacturaCompraItem { get; set; }
        public List<FacturaCompraItem> FacturaCompraItems { get; set; }

        public CreateModel(ProductContext context)
        {
            _context = context;
        }

        public IActionResult OnGet(string id, string action)
        {
            HttpContext.Session.Remove(FACTURA_KEY);
            HttpContext.Session.Remove(PROVEEDOR_KEY);
            OrdenItems = new List<OrdenItem>();
            LoadProveedor();
            return Page();
        }
        public void OnPostProveedor()
        {
            if (FacturaCompra.FechaFactura > FacturaCompra.FechaAlta)
            {
                MessageError = "Fechas incorrectas.";
            }
            else
            {
                HttpContext.Session.SetInt32(PROVEEDOR_KEY, ProveedorId);
                HasProveedor = true;
                HttpContext.Session.SetComplexData(FACTURA_KEY, FacturaCompra);
            }
            OrdenItems = new List<OrdenItem>();
            LoadOrden();
            LoadProveedor();
        }

        public void OnPostOrden()
        {
            HttpContext.Session.SetInt32(ORDEN_KEY, OrdenId);
            OrdenItems = _context.OrdenItems.Where(x => x.OrdenId == OrdenId).ToList();
            HasOrdenItem = true;
            HttpContext.Session.SetComplexData(ORDEN_ITEMS_KEY, OrdenItems);
            Total = (double) OrdenItems.Sum(x => x.Precio * x.Cantidad);
            LoadFactura();
            LoadOrden();
            LoadProveedor();
        }

        public async Task<IActionResult> OnPostSaveFactura()
        {
            List<OrdenItem> list = HttpContext.Session.GetComplexData<List<OrdenItem>>(ORDEN_ITEMS_KEY);
            int ordenId = (int)HttpContext.Session.GetInt32(ORDEN_KEY);
            int proveedorId = (int)HttpContext.Session.GetInt32(PROVEEDOR_KEY);
            FacturaCompra factura = HttpContext.Session.GetComplexData<FacturaCompra>(FACTURA_KEY);
            factura.OrdenId = ordenId;
            factura.ProveedorId = proveedorId;
            List<FacturaCompraItem> facturaCompraItems = new List<FacturaCompraItem>();
            decimal total = 0;
            list.ForEach(x =>
            {
                facturaCompraItems.Add(new FacturaCompraItem()
                {
                    FacturaCompraId = factura.Id,
                    ProductoId = x.ProductoId,
                    Precio = x.Precio,
                    Cantidad = x.Cantidad,
                    Observacion = x.Observacion
                });
                total += x.Precio * x.Cantidad;
            });
            factura.FacturaCompraItems = facturaCompraItems;
            factura.Total = (double)total;
            factura.PendientePago = (double)total;
            _context.FacturasCompra.Add(factura);
            return await AddNewValue(_context);

        }
        private void LoadOrden()
        {
            int proveedorId = !HttpContext.Session.Keys.Contains(PROVEEDOR_KEY) ? -1 : (int)HttpContext.Session.GetInt32(PROVEEDOR_KEY);
            if (proveedorId != -1)
            {
                List<Orden> list = _context.Ordenes.Where(x => x.ProveedorId == proveedorId).ToList();
                _context.FacturasCompra.ToList().ForEach(x =>
                {
                    list = list.Where(y => y.Id != x.OrdenId).ToList();
                });
                HasOrden = list.Count != 0;
                ViewData["OrdenId"] = new SelectList(list, "Id", "Id");
            }
        }
        private void LoadProveedor()
        {
            ViewData["ProveedorId"] = new SelectList(_context.Proveedores, "Id", "RazonSocial");
        }
        private void LoadFactura()
        {
            FacturaCompra = HttpContext.Session.GetComplexData<FacturaCompra>(FACTURA_KEY);
        }
    }
}
