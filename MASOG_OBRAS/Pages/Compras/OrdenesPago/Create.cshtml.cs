using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using EFDataAccessLibrary.DataAccess;
using EFDataAccessLibrary.Models.Compras;
using MASOG_OBRAS.Classes;
using Microsoft.AspNetCore.Http;

namespace MASOG_OBRAS.Pages.Compras.OrdenesPago
{
    public class CreateModel : BaseCreatePage
    {
        private readonly ProductContext _context;
        private readonly string ORDEN_KEY = "OrdenKey";
        private readonly string PROVEEDOR_KEY = "ProveedorKey";
        [BindProperty]
        public bool HasProveedor { get; set; }
        [BindProperty]
        public bool HasFacturas { get; set; }
        [BindProperty]
        public List<FacturaSelectedItem> FacturaItems { get; set; }
        public CreateModel(EFDataAccessLibrary.DataAccess.ProductContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
            FacturaItems = new List<FacturaSelectedItem>();
            ViewData["ConceptoPagoId"] = new SelectList(_context.Set<ConceptoPago>(), "Id", "Descripcion");
            ViewData["ProveedorId"] = new SelectList(_context.Proveedores, "Id", "RazonSocial");
            return Page();
        }

        [BindProperty]
        public OrdenPago OrdenPago { get; set; }

        public void OnPostProveedor()
        {
            List<FacturaCompra> list = _context.FacturasCompra.Where(x => x.ProveedorId == OrdenPago.ProveedorId && x.PendientePago != 0).ToList();
            HasFacturas = list.Count != 0;
            HasProveedor = true;
            if (!HasFacturas)
            {
                ViewData["ConceptoPagoId"] = new SelectList(_context.Set<ConceptoPago>(), "Id", "Descripcion");
                ViewData["ProveedorId"] = new SelectList(_context.Proveedores, "Id", "RazonSocial");
            }
            else
            {
                HttpContext.Session.SetInt32(PROVEEDOR_KEY, OrdenPago.ProveedorId);
                HttpContext.Session.SetComplexData(ORDEN_KEY, OrdenPago);
                FacturaItems = new List<FacturaSelectedItem>();
                list.ForEach(x =>
                {
                    FacturaItems.Add(new FacturaSelectedItem()
                    {
                        IsSelected = false,
                        Id = x.Id,
                        Total = x.Total,
                        FechaFactura = x.FechaFactura
                    });
                });
                ViewData["ConceptoPagoId"] = new SelectList(_context.Set<ConceptoPago>().Where(x => x.Id == OrdenPago.ConceptoPagoId), "Id", "Descripcion");
                ViewData["ProveedorId"] = new SelectList(_context.Proveedores.Where(x => x.Id == OrdenPago.ProveedorId), "Id", "RazonSocial");
            }
        }
        public async Task<IActionResult> OnPostSaveOrden()
        {
            int proveedorId = (int)HttpContext.Session.GetInt32(PROVEEDOR_KEY);
            OrdenPago = HttpContext.Session.GetComplexData<OrdenPago>(ORDEN_KEY);
            List<FacturaCompra> list = _context.FacturasCompra.Where(x => x.ProveedorId == proveedorId && x.PendientePago != 0).ToList();
            List<FacturaSelectedItem> itemList = new List<FacturaSelectedItem>();
            List<OrdenPagoItem> ordenList = new List<OrdenPagoItem>();
            double total = 0;
            list.ForEach(x =>
            {
                itemList.Add(new FacturaSelectedItem()
                {
                    IsSelected = false,
                    Id = x.Id,
                    Total = x.Total,
                    FechaFactura = x.FechaFactura
                });
            });
            for (int i = 0; i < itemList.Count; i++)
            {
                if (FacturaItems[i].IsSelected)
                {
                    ordenList.Add(new OrdenPagoItem()
                    {
                        OrdenPagoId = OrdenPago.Id,
                        FacturaCompraId = itemList[i].Id,
                        Importe = itemList[i].Total
                    });
                    total += itemList[i].Total;
                    list.First(x => x.Id == itemList[i].Id).PendientePago = 0;
                }
            }
            OrdenPago.OrdenPagoItems = ordenList;
            OrdenPago.Total = total;
            _context.OrdenesPago.Add(OrdenPago);
            return await AddNewValue(_context);
        }
        private void LoadViewData()
        {
            if (OrdenPago != null)
            {
                ViewData["ConceptoPagoId"] = new SelectList(_context.Set<ConceptoPago>().Where(x => x.Id == OrdenPago.ConceptoPagoId), "Id", "Descripcion");
                ViewData["ProveedorId"] = new SelectList(_context.Proveedores.Where(x => x.Id == OrdenPago.ProveedorId), "Id", "RazonSocial");
            }
            else
            {
                ViewData["ConceptoPagoId"] = new SelectList(_context.Set<ConceptoPago>(), "Id", "Descripcion");
                ViewData["ProveedorId"] = new SelectList(_context.Proveedores, "Id", "RazonSocial");
            }
        }
    }
    public class FacturaSelectedItem
    {
        public int Id { get; set; }
        public bool IsSelected { get; set; }
        public DateTime FechaFactura { get; set; }
        public double Total { get; set; }
    }
}
