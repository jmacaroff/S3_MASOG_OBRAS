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
using EFDataAccessLibrary.Models.Inventarios;
using Microsoft.AspNetCore.Http;
using EFDataAccessLibrary.Models.Clientes;

namespace MASOG_OBRAS.Pages.Ventas.FacturasVenta
{
    public class CreateModel : BaseCreatePage
    {
        //Declaracion de Variables
        private readonly ProductContext _context;
        private readonly string LIST_KEY = "ListKey";
        private readonly string PROYECTO_KEY = "ProyectoKey";
        private readonly string FACTURA_KEY = "FacturaKey";

        [BindProperty]
        public IEnumerable<Proyecto> proyectoNombre { get; set; }

        [BindProperty]
        public bool HasProyecto { get; set; }

        [BindProperty]
        public bool HasFactura { get; set; }

        [BindProperty]
        public bool HasProduct { get; set; } = false;

        [BindProperty]
        public bool HasProductList { get; set; }

        [BindProperty]
        public bool HasFacturaItems { get; set; } = false;

        [BindProperty]
        public int ProyectoId { get; set; }

        [BindProperty]
        public string ProyectoNombre { get; set; }

        [BindProperty]
        public FacturaVenta FacturaVenta { get; set; }

        [BindProperty]
        public double Total { get; set; }

        [BindProperty]
        public double SubTotal { get; set; }

        [BindProperty]
        public FacturaVentaItem FacturaVentaItem { get; set; }

        public List<FacturaVentaItem> L_FacturaVentaItems { get; set; }

        [BindProperty]
        public Producto Producto { get; set; }

        [BindProperty]
        public string ProductoId { get; set; }

        public CreateModel(EFDataAccessLibrary.DataAccess.ProductContext context)
        {
            _context = context;
        }

        public IActionResult OnGet(string id, string action)
        {
            HttpContext.Session.Remove(FACTURA_KEY);
            HttpContext.Session.Remove(PROYECTO_KEY);
            LoadFacturaVentaItems();
            LoadViewData();
            return Page();
        }

        public void OnPostHeader()
        {
            //Total = (double)L_FacturaVentaItems.Sum(x => x.Precio * x.Cantidad);
            LoadFacturaVentaItems();
            LoadViewData();
        }

            public void OnPostProduct()
        {

            Producto = _context.Productos.First<Producto>(x => x.Id == ProductoId);
            HasProduct = true;
            FacturaVentaItem.Precio = Producto.Precio;
            FacturaVentaItem.ProductoId = Producto.Id;
            FacturaVentaItem.Subtotal = (double)(Producto.Precio * FacturaVentaItem.Cantidad);
            LoadProyectoId();
            LoadFacturaVentaItems();
            LoadViewData();
        }

        public void OnPostAddItem()
        {
            FacturaVentaItem.FacturaVentaId = FacturaVenta.Id;
            LoadFacturaVentaItems();
            HasFacturaItems = true;
            L_FacturaVentaItems.Add(FacturaVentaItem);
            SaveFacturaVentaItems();
            LoadProyectoId();
            LoadViewData();
        }


        public async Task<IActionResult> OnPostSaveFactura()
        {
            LoadFacturaVentaItems();
            if (L_FacturaVentaItems.Count == 0)
            {
                MessageError = "No hay items en la orden.";
                LoadViewData();
                return Page();
            }
            else
            {
                FacturaVenta.FacturaVentaItems = L_FacturaVentaItems;
                FacturaVenta.ProyectoID = (int)HttpContext.Session.GetInt32(PROYECTO_KEY);
                _context.FacturasVenta.Add(FacturaVenta);
                return await AddNewValue(_context);
            }
        }


        //Carga ID de Proyectos 
        private void LoadProyectoId()
        {
            ProyectoId = !HttpContext.Session.Keys.Contains(PROYECTO_KEY) ? -1 : (int)HttpContext.Session.GetInt32(PROYECTO_KEY);
            HasProyecto = ProyectoId != -1;
        }

        private void LoadFacturaVentaItems()
        {
            List<FacturaVentaItem> list = HttpContext.Session.GetComplexData<List<FacturaVentaItem>>(LIST_KEY);
            if (list == null)
            {
                L_FacturaVentaItems = new List<FacturaVentaItem>();
            }
            else
            {
                L_FacturaVentaItems = list;
                HasFacturaItems = true;
            }
        }
        private void LoadViewData()
        {
            if (ProyectoId != 0)
            {
                ViewData["ProyectoId"] = new SelectList(_context.Proyectos.Where(x => x.Id == ProyectoId), "Id", "Nombre");
            }
            else
            {
                ViewData["ProyectoId"] = new SelectList(_context.Proyectos, "Id", "Nombre");
            }
            if (L_FacturaVentaItems.Count > 0)
            {
                List<Producto> list = _context.Productos.ToList();
                foreach (FacturaVentaItem item in L_FacturaVentaItems)
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
        private void SaveFacturaVentaItems()
        {
            HttpContext.Session.SetComplexData(LIST_KEY, L_FacturaVentaItems);

        }
    }
}
