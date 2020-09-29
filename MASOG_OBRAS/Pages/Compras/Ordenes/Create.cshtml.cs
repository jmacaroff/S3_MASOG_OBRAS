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

namespace MASOG_OBRAS.Pages.Compras.Ordenes
{
    public class CreateModel : BaseCreatePage
    {
        private readonly EFDataAccessLibrary.DataAccess.ProductContext _context;
        private readonly string LIST_KEY = "ListKey";
        private readonly string PROVEEDOR_KEY = "ProveedorKey";
        public CreateModel(EFDataAccessLibrary.DataAccess.ProductContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
            HttpContext.Session.Remove(LIST_KEY);
            HttpContext.Session.Remove(PROVEEDOR_KEY);
            loadViewData();
            OrdenItems = new List<OrdenItem>();
            return Page();
        }
        public bool HasProduct { get; set; } = true;
        public bool HasProveedor { get; set; } = false;
        [BindProperty]
        public int ProveedorId { get; set; }
        [BindProperty]
        public Orden Orden { get; set; }
        [BindProperty]
        public OrdenItem OrdenItem { get; set; }
        public List<OrdenItem> OrdenItems { get; set; }


        public void OnPostAddItem()
        {
            OrdenItem.OrdenId = Orden.Id;
            List<OrdenItem> list = HttpContext.Session.GetComplexData<List<OrdenItem>>(LIST_KEY);
            OrdenItems = list == null ? new List<OrdenItem>() : list;
            OrdenItems.Add(OrdenItem);
            HttpContext.Session.SetComplexData(LIST_KEY, OrdenItems);
            int proveedorId = !HttpContext.Session.Keys.Contains(PROVEEDOR_KEY) ? -1 : (int)HttpContext.Session.GetInt32(PROVEEDOR_KEY);
            if (proveedorId == -1)
            {
                HttpContext.Session.SetInt32(PROVEEDOR_KEY, ProveedorId);
            }
            loadViewData();
        }

        public async Task<IActionResult> OnPostSaveOrder()
        {
            Console.WriteLine(Orden);
            List<OrdenItem> list = HttpContext.Session.GetComplexData<List<OrdenItem>>(LIST_KEY);
            if (list == null)
            {
                return Page();
            }
            else
            {
                OrdenItems = list;
                Orden.OrdenItems = OrdenItems;
                Orden.ProveedorId = (int)HttpContext.Session.GetInt32(PROVEEDOR_KEY);
                _context.Ordenes.Add(Orden);
                return await AddNewValue(_context);
            }

        }

        private void loadViewData()
        {
            if (ProveedorId != 0)
            {
                HasProveedor = true;
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
                if(list.Count == 0)
                {
                    HasProduct = false;
                }else
                {
                    HasProduct = true;
                    ViewData["ProductoId"] = new SelectList(list, "Id", "Id");
                }
                
            }
            else
            {
                ViewData["ProductoId"] = new SelectList(_context.Productos, "Id", "Id");
            }
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
