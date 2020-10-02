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
        private readonly string LIST_KEY = "ListKey";
        private readonly string PROVEEDOR_KEY = "ProveedorKey";

        public bool HasProveedor { get; set; } = false;
        public bool HasProduct { get; set; } = false;
        public bool HasFacturaCompraItems { get; set; } = false;
        [BindProperty]
        public int ProveedorId { get; set; }
        [BindProperty]
        public string ProductoId { get; set; }
        [BindProperty]
        public FacturaCompra FacturaCompra { get; set; }
        [BindProperty]
        public FacturaCompraItem FacturaCompraItem { get; set; }
        [BindProperty]
        public Producto Producto { get; set; }

        public List<FacturaCompraItem> FacturaCompraItems { get; set; }

        public CreateModel(ProductContext context)
        {
            _context = context;
        }

        public IActionResult OnGet(string id,string action)
        {
            HttpContext.Session.Remove(LIST_KEY);
            HttpContext.Session.Remove(PROVEEDOR_KEY);
            LoadFacturaCompraItems();
            LoadViewData();
            return Page();
        }
        public void OnPostProduct()
        {
            int proveedorId = !HttpContext.Session.Keys.Contains(PROVEEDOR_KEY) ? -1 : (int)HttpContext.Session.GetInt32(PROVEEDOR_KEY);
            if (proveedorId == -1)
            {
                HttpContext.Session.SetInt32(PROVEEDOR_KEY, ProveedorId);
            }
            Producto = _context.Productos.First<Producto>(x => x.Id == ProductoId);
            HasProduct = true;
            HasProveedor = true;
            FacturaCompraItem.Precio = Producto.Precio;
            FacturaCompraItem.ProductoId = Producto.Id;
            LoadFacturaCompraItems();
            LoadViewData();
        }

        public void OnPostAddItem()
        {
            FacturaCompraItem.FacturaCompraId = FacturaCompra.Id;
            LoadFacturaCompraItems();
            HasFacturaCompraItems = true;
            FacturaCompraItems.Add(FacturaCompraItem);
            SaveFacturaCompraItems();
            LoadProveedorId();
            LoadViewData();
        }
        public void OnPostRemoveItem(string id)
        {
            LoadFacturaCompraItems();
            FacturaCompraItems = FacturaCompraItems.Where(x => x.ProductoId != id).ToList();
            HasFacturaCompraItems = FacturaCompraItems.Count != 0;
            SaveFacturaCompraItems();
            LoadProveedorId();
            LoadViewData();
        }

        public async Task<IActionResult> OnPostSaveOrder()
        {
            LoadFacturaCompraItems();
            if (FacturaCompraItems == null)
            {
                return Page();
            }
            else
            {
                FacturaCompra.FacturaCompraItems = FacturaCompraItems;
                FacturaCompra.ProveedorId = (int)HttpContext.Session.GetInt32(PROVEEDOR_KEY);
                _context.FacturasCompra.Add(FacturaCompra);
                return await AddNewValue(_context);
            }

        }
        private void LoadProveedorId()
        {
            ProveedorId = !HttpContext.Session.Keys.Contains(PROVEEDOR_KEY) ? -1 : (int)HttpContext.Session.GetInt32(PROVEEDOR_KEY);
            HasProveedor = ProveedorId != -1;
        }
        private void LoadFacturaCompraItems()
        {
            List<FacturaCompraItem> list = HttpContext.Session.GetComplexData<List<FacturaCompraItem>>(LIST_KEY);
            if (list == null)
            {
                FacturaCompraItems = new List<FacturaCompraItem>();
            }
            else
            {
                FacturaCompraItems = list;
                HasFacturaCompraItems = true;
            }
        }
        private void LoadViewData()
        {
            if (ProveedorId != 0)
            {
                ViewData["ProveedorId"] = new SelectList(_context.Proveedores.Where(x => x.Id == ProveedorId), "Id", "RazonSocial");
            }
            else
            {
                ViewData["ProveedorId"] = new SelectList(_context.Proveedores, "Id", "RazonSocial");
            }
            if (FacturaCompraItems != null)
            {
                List<Producto> list = _context.Productos.ToList();
                foreach(FacturaCompraItem item in FacturaCompraItems)
                {
                    list = list.Where(x => x.Id != item.ProductoId).ToList();
                }
                ViewData["ProductoId"] = new SelectList(list, "Id", "Id");
            }
            else
            {
                ViewData["ProductoId"] = new SelectList(_context.Productos, "Id", "Id");
            }
        }
        private void SaveFacturaCompraItems()
        {
            HttpContext.Session.SetComplexData(LIST_KEY, FacturaCompraItems);
        }
    }

    public static class SessionExtensions
    {
        public static T GetComplexData<T>(this ISession session, string key)
        {
            var data = session.GetString(key);
            if (data == null)
            {
                return default(T);
            }
            return JsonConvert.DeserializeObject<T>(data);
        }

        public static void SetComplexData(this ISession session, string key, object value)
        {
            session.SetString(key, JsonConvert.SerializeObject(value));
        }
    }
}
