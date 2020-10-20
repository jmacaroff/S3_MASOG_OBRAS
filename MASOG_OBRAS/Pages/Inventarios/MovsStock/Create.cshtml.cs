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
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.CodeAnalysis.Options;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace MASOG_OBRAS.Pages.Inventarios.MovsStock
{
    public class CreateModel : BaseCreatePage
    {
        private readonly ProductContext _context;
        private readonly string LIST_KEY = "ListKey";
        private readonly string TIPOMOV_KEY = "TipoMovKey";
        private readonly string MOV_KEY = "MovKey";

        public bool HasHeader { get; set; } = false;
        public bool HasProduct { get; set; } = false;
        public bool HasProductList { get; set; }
        public bool HasMovStockItems { get; set; } = false;

        [BindProperty]
        public int TipoMovimientoId { get; set; }

        [BindProperty]
        public string ProductoId { get; set; }

        [BindProperty]
        public MovStock MovStock { get; set; }

        [BindProperty]
        public MovStockItem MovStockItem { get; set; }

        [BindProperty]
        public Producto Producto { get; set; }

        public List<MovStockItem> MovStockItems { get; set; }

        public CreateModel(ProductContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
            ViewData["TipoMovimientoId"] = new SelectList(_context.Set<TipoMovimiento>(), "Id", "Descripcion");
            HttpContext.Session.Remove(LIST_KEY);
            HttpContext.Session.Remove(TIPOMOV_KEY);
            LoadMovStockItems();
            LoadViewData();
            return Page();
        }
        public void OnPostHeader()
        {
            if (MovStock.Fecha == null)      // Arreglar validación de fecha no nula!!!!
            {
                MessageError = "Fecha incorrecta";
            }
            else
            {
                MessageError = null;
                int tipomovId = !HttpContext.Session.Keys.Contains(TIPOMOV_KEY) ? -1 : (int)HttpContext.Session.GetInt32(TIPOMOV_KEY);
                if (tipomovId == -1)
                {
                    HttpContext.Session.SetInt32(TIPOMOV_KEY, TipoMovimientoId);
                }
                HttpContext.Session.SetComplexData(MOV_KEY, MovStock);
                HasHeader = true;
            }
            LoadMovStockItems();
            LoadViewData();
        }

        public void OnPostProduct()
        {

            Producto = _context.Productos.First<Producto>(x => x.Id == ProductoId);
            HasProduct = true;
            MovStockItem.ProductoId = Producto.Id;
            LoadTipoMovimientoId();
            LoadMovStockItems();
            LoadViewData();
        }

        public void OnPostAddItem()
        {
            MovStockItem.MovStockId = MovStock.Id;
            LoadMovStockItems();
            HasMovStockItems = true;
            MovStockItems.Add(MovStockItem);
            SaveMovStockItems();
            LoadTipoMovimientoId();
            LoadViewData();
        }
        public void OnPostRemoveItem(string id)
        {
            LoadMovStockItems();
            MovStockItems = MovStockItems.Where(x => x.ProductoId != id).ToList();
            HasMovStockItems = MovStockItems.Count != 0;
            SaveMovStockItems();
            LoadTipoMovimientoId();
            LoadViewData();
        }

        public async Task<IActionResult> OnPostSaveOrder()
        {
            LoadMovStockItems();
            if (MovStockItems.Count == 0)
            {
                MessageError = "No hay items en el movimiento.";
                LoadViewData();
                return Page();
            }
            else
            {
                MovStock.MovStockItems = MovStockItems;
                MovStock.TipoMovimientoId = (int)HttpContext.Session.GetInt32(TIPOMOV_KEY);
                _context.MovsStock.Add(MovStock);
                return await AddNewValue(_context);
            }
        }
        private void LoadTipoMovimientoId()
        {
            TipoMovimientoId = !HttpContext.Session.Keys.Contains(TIPOMOV_KEY) ? -1 : (int)HttpContext.Session.GetInt32(TIPOMOV_KEY);
            HasHeader = TipoMovimientoId != -1;
        }
        private void LoadMovStockItems()
        {
            List<MovStockItem> list = HttpContext.Session.GetComplexData<List<MovStockItem>>(LIST_KEY);
            if (list == null)
            {
                MovStockItems = new List<MovStockItem>();
            }
            else
            {
                MovStockItems = list;
                HasMovStockItems = true;
            }
        }
        private void LoadViewData()
        {
            if (TipoMovimientoId != 0)
            {
                ViewData["TipoMovimientoId"] = new SelectList(_context.Set<TipoMovimiento>().Where(x => x.Id == TipoMovimientoId), "Id", "Descripcion");
            }
            else
            {
                ViewData["TipoMovimientoId"] = new SelectList(_context.Set<TipoMovimiento>(), "Id", "Descripcion");
            }
            if (MovStockItems.Count > 0)
            {
                List<Producto> list = _context.Productos.ToList();
                foreach (MovStockItem item in MovStockItems)
                {
                    list = list.Where(x => x.Id != item.ProductoId).ToList();
                }
                if (list.Count == 0)
                {
                    HasProductList = false;
                }
                else
                {
                    ViewData["ProductoId"] = new SelectList(list, "Id", "Descripcion");
                    HasProductList = true;
                }
            }
            else
            {
                if (_context.Productos.ToList().Count == 0)
                {
                    HasProductList = false;
                }
                else
                {
                    ViewData["ProductoId"] = new SelectList(_context.Productos, "Id", "Descripcion");
                    HasProductList = true;
                }
            }
        }
        private void SaveMovStockItems()
        {
            HttpContext.Session.SetComplexData(LIST_KEY, MovStockItems);
        }

    }
}
