using EFDataAccessLibrary.Models.Clientes;
using EFDataAccessLibrary.Models.Compras;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace EFDataAccessLibrary.Models.Ventas
{
    public class Recibo : BaseModel
    {
        [Key]
        public int Id { get; set; }

        //Proveedor al que hace referencia
        [DisplayName("Cliente")]
        [Required(ErrorMessage = "Se requiere un cliente")]
        public int ClienteId { get; set; }

        // Luego de seleccionar el proveedor se deberia poder visualizar cuales facturas están pendientes y seleccionar las que se van a pagar: PendientePago > 0 

        // Fecha en la que se emite
        [DataType(DataType.Date)]
        [Required(ErrorMessage = "Se requiere una fecha de emisión.")]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [Display(Name = "Fecha de Emision")]
        public DateTime FechaEmision { get; set; } = DateTime.Now;

        // Foma de pago (Efectivo, Débito, Crédito, Cheque). Se debe seleccionar con un drop down
        [Required(ErrorMessage = "Se requiere una forma de pago.")]
        public int ConceptoPagoId { get; set; }

        [DisplayName("Observación")]
        public string Observacion { get; set; }

        // Calculado que es la suma de los importes de todas las facturas seleccionadas
        [Required(ErrorMessage = "Se requiere un total.")]
        public double Total { get; set; }

        public Cliente Cliente { get; set; }
        public ConceptoPago ConceptoPago { get; set; }
        public ICollection<ReciboItem> ReciboItems { get; set; }

    }

    public class ReciboItem : BaseModel
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Se requiere un Recibo.")]
        public int ReciboId { get; set; }

        [Required(ErrorMessage = "Se requiere una Factura de Venta.")]
        public int FacturaVentaId { get; set; }

        // Lo recupera de PendientePago de la factura.
        [Required]
        public double Importe { get; set; }

        public Recibo Recibo { get; set; }
        public FacturaVenta FacturaVenta { get; set; }

    }
    public class RecibosDet
    {

        [Required(ErrorMessage = "Se requiere una fecha de emisión.")]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [Display(Name = "Fecha de Emision")]
        [DataType(DataType.Date)]
        public DateTime FechaRecibo { get; set; }
        public int ClienteId { get; set; }
        public string ClienteNombre { get; set; }
        public int FacturaVentaNumero { get; set; }
        public string ProductoId { get; set; }
        public string ProductoDescripcion { get; set; }

        [DisplayName("Precio")]
        [Required(ErrorMessage = "Se requiere un importe.")]
        [RegularExpression(@"^\d+\.{0,1}\d{0,2}$", ErrorMessage = "Se aceptan 2 decimales. Ingrese el valor decimal con ' . '.")]
        [Column(TypeName = "decimal(18,2)")]
        public decimal Total { get; set; }
    }
}
