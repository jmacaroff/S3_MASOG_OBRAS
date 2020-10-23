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
using EFDataAccessLibrary.Models.Clientes;

namespace MASOG_OBRAS.Pages.Inventarios.MovsStock
{
    public class CreateModel : BaseCreatePage
    {
        private readonly ProductContext _context;
        private readonly string LIST_KEY = "ListKey";
        private readonly string PROVEEDOR_KEY = "Proveedorey";
        private readonly string PROYECTO_KEY = "ProyectoKey";
        private readonly string DEPOSITO_KEY = "DepositoKey";
        private readonly string TIPOMOV_KEY = "TipoMovKey";
        private readonly string MOV_KEY = "MovKey";

        public bool HasHeader { get; set; } = false;
        public bool HasProduct { get; set; } = false;
        public bool HasProductList { get; set; }
        public bool HasMovStockItems { get; set; } = false;

        [BindProperty]
        public int TipoMovimientoId { get; set; }

        [BindProperty]
        public int ProyectoId { get; set; }

        [BindProperty]
        public string DepositoId { get; set; }

        [BindProperty]
        public int ProveedorId { get; set; }

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

        public IActionResult OnGet(string id, string action)
        {
            ViewData["TipoMovimientoId"] = new SelectList(_context.Set<TipoMovimiento>(), "Id", "Descripcion");
            ViewData["ProyectoId"] = new SelectList(_context.Set<Proyecto>(), "Id", "Nombre");
            ViewData["ProveedorId"] = new SelectList(_context.Set<Proveedor>(), "Id", "RazonSocial");
            ViewData["DepositoId"] = new SelectList(_context.Set<Deposito>(), "Id", "Descripcion");
            HttpContext.Session.Remove(LIST_KEY);
            HttpContext.Session.Remove(TIPOMOV_KEY);
            HttpContext.Session.Remove(PROVEEDOR_KEY);
            HttpContext.Session.Remove(PROYECTO_KEY);
            HttpContext.Session.Remove(DEPOSITO_KEY);
            LoadMovStockItems();
            LoadViewData();
            return Page();
        }
        public void OnPostHeader()
        {
            if ((TipoMovimientoId == 5 || TipoMovimientoId == 3) && ProyectoId == -1)
            {
                MessageError = "Debe ingresar un proyecto para envíos y devoluciones de obra.";
            }
            else if (TipoMovimientoId == 2 && ProveedorId == -1)
            {
                MessageError = "Debe ingresar un proveedor para los remitos de proveedor.";
            }
            else
            {
                if (DepositoId == null)
                {
                    MessageError = "Debe ingresar un depósito.";
                }
                else
                {
                    int tipomovId = !HttpContext.Session.Keys.Contains(TIPOMOV_KEY) ? -1 : (int)HttpContext.Session.GetInt32(TIPOMOV_KEY);
                    if (tipomovId == -1)
                    {
                        HttpContext.Session.SetInt32(TIPOMOV_KEY, TipoMovimientoId);
                    }
                    int proyectoId = !HttpContext.Session.Keys.Contains(PROYECTO_KEY) ? -1 : (int)HttpContext.Session.GetInt32(PROYECTO_KEY);
                    if (proyectoId == -1)
                    {
                        HttpContext.Session.SetInt32(PROYECTO_KEY, ProyectoId);
                    }
                    int proveedorId = !HttpContext.Session.Keys.Contains(PROVEEDOR_KEY) ? -1 : (int)HttpContext.Session.GetInt32(PROVEEDOR_KEY);
                    if (proveedorId == -1)
                    {
                        HttpContext.Session.SetInt32(PROVEEDOR_KEY, ProveedorId);
                    }
                    string depositoId = !HttpContext.Session.Keys.Contains(DEPOSITO_KEY) ? "-1" : (string)HttpContext.Session.GetString(DEPOSITO_KEY);
                    if (depositoId == "-1")
                    {
                        HttpContext.Session.SetString(DEPOSITO_KEY, DepositoId.Substring(0, 3));
                    }
                    HttpContext.Session.SetComplexData(MOV_KEY, MovStock);
                    HasHeader = true;
                }
            }
            LoadMovStockItems();
            LoadViewData();
        }

        public void OnPostProduct()
        {

            Producto = _context.Productos.First<Producto>(x => x.Id == ProductoId);
            HasProduct = true;
            MovStockItem.ProductoId = Producto.Id;
            LoadHeaderIds();
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
            LoadHeaderIds();
            LoadViewData();
        }

        public void OnPostRemoveItem(string id)
        {
            LoadMovStockItems();
            MovStockItems = MovStockItems.Where(x => x.ProductoId != id).ToList();
            HasMovStockItems = MovStockItems.Count != 0;
            SaveMovStockItems();
            LoadHeaderIds();
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
                MovStock.DepositoId = HttpContext.Session.GetString(DEPOSITO_KEY).Substring(0,3);
                int clienteId = !HttpContext.Session.Keys.Contains(PROYECTO_KEY) ? -1 : (int)HttpContext.Session.GetInt32(PROYECTO_KEY);
                if (clienteId != -1)
                {
                    MovStock.ProyectoId = (int)HttpContext.Session.GetInt32(PROYECTO_KEY);
                }
                int proveedorId = !HttpContext.Session.Keys.Contains(PROVEEDOR_KEY) ? -1 : (int)HttpContext.Session.GetInt32(PROVEEDOR_KEY);
                if (proveedorId != -1)
                {
                    MovStock.ProveedorId = (int)HttpContext.Session.GetInt32(PROVEEDOR_KEY);
                }
                _context.MovsStock.Add(MovStock);
                return await AddNewValue(_context);
            }
        }
        private void LoadHeaderIds()
        {
            TipoMovimientoId = !HttpContext.Session.Keys.Contains(TIPOMOV_KEY) ? -1 : (int)HttpContext.Session.GetInt32(TIPOMOV_KEY);
            HasHeader = TipoMovimientoId != -1;
            ProyectoId = !HttpContext.Session.Keys.Contains(PROYECTO_KEY) ? -1 : (int)HttpContext.Session.GetInt32(PROYECTO_KEY);
            ProveedorId = !HttpContext.Session.Keys.Contains(PROVEEDOR_KEY) ? -1 : (int)HttpContext.Session.GetInt32(PROVEEDOR_KEY);
            DepositoId = !HttpContext.Session.Keys.Contains(DEPOSITO_KEY) ? "-1" : (string)HttpContext.Session.GetString(DEPOSITO_KEY);
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
            int tipomovId = !HttpContext.Session.Keys.Contains(TIPOMOV_KEY) ? -1 : (int)HttpContext.Session.GetInt32(TIPOMOV_KEY);
            if (tipomovId != -1)
            {
                ViewData["TipoMovimientoId"] = new SelectList(_context.Set<TipoMovimiento>().Where(x => x.Id == TipoMovimientoId), "Id", "Descripcion");
            }
            else
            {
                ViewData["TipoMovimientoId"] = new SelectList(_context.Set<TipoMovimiento>(), "Id", "Descripcion");
            }
            int proyectoId = !HttpContext.Session.Keys.Contains(PROYECTO_KEY) ? -1 : (int)HttpContext.Session.GetInt32(PROYECTO_KEY);
            if (proyectoId != -1)
            {
                ViewData["ProyectoId"] = new SelectList(_context.Set<Proyecto>().Where(x => x.Id == ProyectoId), "Id", "Nombre");
            }
            else
            {
                ViewData["ProyectoId"] = new SelectList(_context.Set<Proyecto>(), "Id", "Nombre");
            }
            int proveedorId = !HttpContext.Session.Keys.Contains(PROVEEDOR_KEY) ? -1 : (int)HttpContext.Session.GetInt32(PROVEEDOR_KEY);
            if (proveedorId != -1)
            {
                ViewData["ProveedorId"] = new SelectList(_context.Set<Proveedor>().Where(x => x.Id == ProveedorId), "Id", "RazonSocial");
            }
            else
            {
                ViewData["ProveedorId"] = new SelectList(_context.Set<Proveedor>(), "Id", "RazonSocial");
            }
            string depositoId = !HttpContext.Session.Keys.Contains(DEPOSITO_KEY) ? "-1" : (string)HttpContext.Session.GetString(DEPOSITO_KEY);
            if (depositoId != "-1")
            {
                ViewData["DepositoId"] = new SelectList(_context.Set<Deposito>().Where(x => x.Id == DepositoId), "Id", "Descripcion");
            }
            else
            {
                ViewData["DepositoId"] = new SelectList(_context.Set<Deposito>(), "Id", "Descripcion");
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
