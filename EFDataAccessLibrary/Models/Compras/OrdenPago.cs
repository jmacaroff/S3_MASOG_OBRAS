using EFDataAccessLibrary.Models.Proveedores;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace EFDataAccessLibrary.Models.Compras
{
    public class OrdenPago : BaseModel
    {
        [Key]
        public int Id { get; set; }

        //Proveedor al que hace referencia
        [DisplayName("Proveedor")]
        [Required(ErrorMessage = "Se requiere un proveedor")]
        public int ProveedorId { get; set; }

        // Luego de seleccionar el proveedor se deberia poder visualizar cuales facturas están pendientes y seleccionar las que se van a pagar: PendientePago > 0 

        // Fecha en la que se emite
        [DataType(DataType.Date)]
        [Required(ErrorMessage = "Se requiere una fecha de emisión.")]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [Display(Name = "Fecha de Emision")]
        public DateTime FechaEmision { get; set; } = DateTime.Now;

        // Foma de pago (Efectivo, Débito, Crédito, Cheque). Se debe seleccionar con un drop down
        [Required(ErrorMessage = "Se requiere una forma de pago.")]
        [DisplayName("Forma de Pago.")]
        public int ConceptoPagoId { get; set; }

        [DisplayName("Observación")]
        public string Observacion { get; set; }

        // Calculado que es la suma de los importes de todas las facturas seleccionadas
        public double Total { get; set; }

        public Proveedor Proveedor { get; set; }
        public ConceptoPago ConceptoPago { get; set; }
        public ICollection<OrdenPagoItem> OrdenPagoItems { get; set; }

    }

    public class OrdenPagoItem : BaseModel
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Se requiere una Orden.")]
        public int OrdenPagoId { get; set; }

        [Required(ErrorMessage = "Se requiere una Factura.")]
        public int FacturaCompraId { get; set; }

        // Lo recupera de PendientePago de la factura.
        [Required]
        public double Importe { get; set; }

        public OrdenPago OrdenPago { get; set; }
        public FacturaCompra FacturaCompra { get; set; }

    }

    public class ConceptoPago : BaseModel
    {
        [Key]
        public int Id { get; set; }

        [DisplayName("Descripción")]
        [Required(ErrorMessage = "Se requiere una descipción.")]
        public string Descripcion { get; set; }
    }

    public class OrdenesPagoDet
    {
        [Required(ErrorMessage = "Se requiere una fecha de emisión.")]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [Display(Name = "Fecha de Emision")]
        [DataType(DataType.Date)]
        public DateTime FechaOrdenPago { get; set; }
        public int ProveedorId { get; set; }
        public string ProveedorNombre { get; set; }
        public int FacturaCompraNumero { get; set; }
        public string ProductoId { get; set; }
        public string ProductoDescripcion { get; set; }

        [DisplayName("Precio")]
        [Required(ErrorMessage = "Se requiere un importe.")]
        [RegularExpression(@"^\d+\.{0,1}\d{0,2}$", ErrorMessage = "Se aceptan 2 decimales. Ingrese el valor decimal con ' . '.")]
        [Column(TypeName = "decimal(18,2)")]
        public decimal Total { get; set; }
    }
}
