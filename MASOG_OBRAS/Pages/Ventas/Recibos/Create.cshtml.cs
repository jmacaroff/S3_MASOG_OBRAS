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
using EFDataAccessLibrary.Models.Compras;
using Microsoft.AspNetCore.Http;

namespace MASOG_OBRAS.Pages.Ventas.Recibos
{
    public class CreateModel : BaseCreatePage
    {
        private readonly EFDataAccessLibrary.DataAccess.ProductContext _context;
        private readonly string RECIBO_KEY = "ReciboKey";
        private readonly string CLIENTE_KEY = "ClienteKey";
        [BindProperty]
        public bool HasCliente { get; set; } = false;
        [BindProperty]
        public bool HasFacturas { get; set; } = false;
        [BindProperty]
        public bool HasCancelButton { get; set; } = false;
        [BindProperty]
        public List<FacturaSelectedItem> FacturaItems { get; set; }

        public CreateModel(EFDataAccessLibrary.DataAccess.ProductContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
            FacturaItems = new List<FacturaSelectedItem>();
            ViewData["ClienteId"] = new SelectList(_context.Clientes, "Id", "Nombre");
            ViewData["ConceptoPagoId"] = new SelectList(_context.Set<ConceptoPago>(), "Id", "Descripcion");
            return Page();
        }

        [BindProperty]
        public Recibo Recibo { get; set; }

        public void OnPostCliente()
        {
            List<FacturaVenta> list = _context.FacturasVenta.Where(x => x.ClienteId == Recibo.ClienteId && x.PendienteCobrar != 0).ToList();
            HasFacturas = list.Count != 0;
            if (!HasFacturas)
            {
                ViewData["ConceptoPagoId"] = new SelectList(_context.Set<ConceptoPago>(), "Id", "Descripcion");
                ViewData["ClienteId"] = new SelectList(_context.Clientes, "Id", "Nombre");
                HasCancelButton = true;
            }
            else
            {
                HttpContext.Session.SetInt32(CLIENTE_KEY, Recibo.ClienteId);
                HttpContext.Session.SetComplexData(RECIBO_KEY, Recibo);
                FacturaItems = new List<FacturaSelectedItem>();
                list.ForEach(x =>
                {
                    FacturaItems.Add(new FacturaSelectedItem()
                    {
                        IsSelected = false,
                        Id = x.Id,
                        Total = x.Total,
                        FechaFactura = x.Fecha
                    });
                });
                ViewData["ConceptoPagoId"] = new SelectList(_context.Set<ConceptoPago>().Where(x => x.Id == Recibo.ConceptoPagoId), "Id", "Descripcion");
                ViewData["ClienteId"] = new SelectList(_context.Clientes.Where(x => x.Id == Recibo.ClienteId), "Id", "Nombre");
                HasCliente = true;
            }
        }

        public async Task<IActionResult> OnPostSaveRecibo()
        {
            int clienteId = (int)HttpContext.Session.GetInt32(CLIENTE_KEY);
            Recibo = HttpContext.Session.GetComplexData<Recibo>(RECIBO_KEY);
            List<FacturaVenta> list = _context.FacturasVenta.Where(x => x.ClienteId == clienteId && x.PendienteCobrar != 0).ToList();
            List<FacturaSelectedItem> itemList = new List<FacturaSelectedItem>();
            List<ReciboItem> reciboList = new List<ReciboItem>();
            double total = 0;
            list.ForEach(x =>
            {
                itemList.Add(new FacturaSelectedItem()
                {
                    IsSelected = false,
                    Id = x.Id,
                    Total = x.Total,
                    FechaFactura = x.Fecha
                });
            });
            for (int i = 0; i < itemList.Count; i++)
            {
                if (FacturaItems[i].IsSelected)
                {
                    reciboList.Add(new ReciboItem()
                    {
                        ReciboId = Recibo.Id,
                        FacturaVentaId = itemList[i].Id,
                        Importe = itemList[i].Total
                    });
                    total += itemList[i].Total;
                    list.First(x => x.Id == itemList[i].Id).PendienteCobrar = 0;
                }
            }
            if (reciboList.Count == 0)
            {
                MessageError = "No hay items seleccionados.";
                LoadViewData();
                return Page();
            }
            Recibo.ReciboItems = reciboList;
            Recibo.Total = total;
            _context.Recibos.Add(Recibo);
            return await AddNewValue(_context);
        }

        private void LoadViewData()
        {
            if (Recibo != null)
            {
                ViewData["ConceptoPagoId"] = new SelectList(_context.Set<ConceptoPago>().Where(x => x.Id == Recibo.ConceptoPagoId), "Id", "Descripcion");
                ViewData["ClienteId"] = new SelectList(_context.Clientes.Where(x => x.Id == Recibo.ClienteId), "Id", "Nombre");
            }
            else
            {
                ViewData["ConceptoPagoId"] = new SelectList(_context.Set<ConceptoPago>(), "Id", "Descripcion");
                ViewData["ClienteId"] = new SelectList(_context.Clientes, "Id", "Nombre");
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