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

        // Luego de seleccionar el proveedor se deberia poder visualizar cuales factura estan pendientes y seleccionar las que se van a pagar: saldo > 0 

        // Fecha en la que se emite
        [DataType(DataType.Date)]
        [Required(ErrorMessage = "Se requiere una fecha")]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [Display(Name = "Fecha de Emision")]
        public DateTime FechaEmision { get; set; } = DateTime.Now;

        [DisplayName("Observación")]
        public string Observacion { get; set; }

        [DisplayName("Detalle")]
        public string Detalle { get; set; }


        // Muestra el total de la Orden de Pago segun las facturas que se pagaron

        [Range(1, 1000000000)]
        [DataType(DataType.Currency)]
        [Column(TypeName = "decimal(18, 2)")]
        public decimal ImporteTotal { get; set; }


        public Proveedor Proveedor { get; set; }

        public ICollection<OrdenPagoItem> OrdenPagoItems { get; set; }

    }

    public class OrdenPagoItem : BaseModel
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Se requiere una ordenPago")]
        public int OrdenPagoId { get; set; }

        [Required(ErrorMessage = "Se requiere una Factura")]
        public int FacturaId { get; set; }

        // Foma de pago   (Efectico, Debito, Credito, Cheque). Se debe seleccionar con un drop down
        [Required]
        [DisplayName("Forma de Pago")]
        public string Concepto { get; set; }

        [Range(1, 1000000000)]
        [DataType(DataType.Currency)]
        [Column(TypeName = "decimal(18, 2)")]
        public decimal ImportePago { get; set; }

        public OrdenPago OrdenPago { get; set; }
        public FacturaCompra FacturaCompra { get; set; }

    }

}
