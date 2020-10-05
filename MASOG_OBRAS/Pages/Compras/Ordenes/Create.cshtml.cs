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
using SQLitePCL;

namespace MASOG_OBRAS.Pages.Compras.Ordenes
{
    public class CreateModel : BaseCreatePage
    {
        private readonly ProductContext _context;
        private readonly string LIST_KEY = "ListKey";
        private readonly string PROVEEDOR_KEY = "ProveedorKey";


        public bool HasProveedor { get; set; } = false;
        public bool HasProduct { get; set; } = false;
        public bool HasOrdenItems { get; set; } = false;
        [BindProperty]
        public int ProveedorId { get; set; }

        [BindProperty]
        public string ProveedorRazonSocial { get; set; }

        [BindProperty]
        public string ProductoId { get; set; }
        [BindProperty]
        public Orden Orden { get; set; }
        [BindProperty]
        public OrdenItem OrdenItem { get; set; }
        [BindProperty]
        public Producto Producto { get; set; }

        public List<OrdenItem> OrdenItems { get; set; }

        public CreateModel(ProductContext context)
        {
            _context = context;
        }

        public IActionResult OnGet(string id,string action)
        {
            HttpContext.Session.Remove(LIST_KEY);
            HttpContext.Session.Remove(PROVEEDOR_KEY);
            LoadOrdenItems();
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
            OrdenItem.Precio = Producto.Precio;
            OrdenItem.ProductoId = Producto.Id;
            LoadOrdenItems();
            LoadViewData();
        }

        public void OnPostAddItem()
        {
            OrdenItem.OrdenId = Orden.Id;
            LoadOrdenItems();
            HasOrdenItems = true;
            OrdenItems.Add(OrdenItem);
            SaveOrdenItems();
            LoadProveedorId();
            LoadViewData();
        }
        public void OnPostRemoveItem(string id)
        {
            LoadOrdenItems();
            OrdenItems = OrdenItems.Where(x => x.ProductoId != id).ToList();
            HasOrdenItems = OrdenItems.Count != 0;
            SaveOrdenItems();
            LoadProveedorId();
            LoadViewData();
        }

        public async Task<IActionResult> OnPostSaveOrder()
        {
            LoadOrdenItems();
            if (OrdenItems.Count == 0)
            {
                MessageError = "No hay items en la orden.";
                LoadViewData();
                return Page();
            }
            else
            {
                Orden.OrdenItems = OrdenItems;
                Orden.ProveedorId = (int)HttpContext.Session.GetInt32(PROVEEDOR_KEY);
                _context.Ordenes.Add(Orden);
                return await AddNewValue(_context);
            }

        }
        private void LoadProveedorId()
        {
            ProveedorId = !HttpContext.Session.Keys.Contains(PROVEEDOR_KEY) ? -1 : (int)HttpContext.Session.GetInt32(PROVEEDOR_KEY);
            HasProveedor = ProveedorId != -1;
        }
        private void LoadOrdenItems()
        {
            List<OrdenItem> list = HttpContext.Session.GetComplexData<List<OrdenItem>>(LIST_KEY);
            if (list == null)
            {
                OrdenItems = new List<OrdenItem>();
            }else
            {
                OrdenItems = list;
                HasOrdenItems = true;
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
            if (OrdenItems != null)
            {
                List<Producto> list = _context.Productos.ToList();
                foreach(OrdenItem item in OrdenItems)
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
        private void SaveOrdenItems()
        {
            HttpContext.Session.SetComplexData(LIST_KEY, OrdenItems);
        }


        // Funcion para Buscar los Productos
        public IActionResult OnGetSearchProductos(string term)
        {
            var descripciones = _context.Productos.Where(p => p.Descripcion.ToLower().Contains(term.ToLower())
                                                            || p.Id.ToLower().Contains(term.ToLower()))
                                                    //.Select(p => new { p.Id, p.Descripcion }) 
                                                    .Select(p => p.Descripcion)
                                                    .ToList();

            return new JsonResult(descripciones);
        }

        // Funcion para Buscar los Proveedores
        public IActionResult OnGetSearchProveedores(string term)
        {
            var razonesSociales = _context.Proveedores.Where(p => p.RazonSocial.ToLower().Contains(term.ToLower()))
                                                    //.Select(p => new { p.Id, p.RazonSocial })
                                                    .Select(p => p.RazonSocial)
                                                    .ToList();

            return new JsonResult(razonesSociales);
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
