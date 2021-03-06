﻿using EFDataAccessLibrary.Models.Clientes;
using EFDataAccessLibrary.Models.Inventarios;
using EFDataAccessLibrary.Models.Proveedores;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace EFDataAccessLibrary.Models.Compras
{
    public class FacturaCompra : BaseModel
    {
        [DisplayName("ID")]
        [Key]
        public int Id { get; set; }

        [DisplayName("Proveedor")]
        [Required(ErrorMessage = "Se requiere un proveedor.")]
        public int ProveedorId { get; set; }

        [DisplayName("Orden Asociada")]
        public int OrdenId { get; set; }

        [Required(ErrorMessage = "Se requiere un punto de venta.")]
        [RegularExpression(@"^\d{1,5}$", ErrorMessage = "Formato incorrecto, no debe superar los 5 dígitos.")]
        [Range(1, int.MaxValue, ErrorMessage = "Sólo se aceptan números positivos.")]
        [Column(TypeName = "nchar(5)")]
        [Display(Name = "Punto de Venta")]
        public int PuntoVenta { get; set; }

        [DisplayName("Número")]
        [Required(ErrorMessage = "Se requiere un número.")]
        public int Numero { get; set; }

        [Required(ErrorMessage = "Se requiere un tipo de factura.")]
        [RegularExpression(@"^[A-C]{1}$", ErrorMessage = "Sólo se acepta A, B o C.")]
        [Column(TypeName = "char(1)")]
        [Display(Name = "Tipo de Factura")]
        public string TipoFactura { get; set; }

        [DataType(DataType.Date)]
        [Required(ErrorMessage = "Se requiere una fecha.")]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [Display(Name = "Fecha Factura")]
        public DateTime FechaFactura { get; set; } = DateTime.Now;

        [DataType(DataType.Date)]
        [Required(ErrorMessage = "Se requiere una fecha de entrega estimada.")]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [Display(Name = "Fecha de Alta")]
        public DateTime FechaAlta { get; set; } = DateTime.Now.AddDays(1);

        [DisplayName("Observación")]
        public string Observacion { get; set; }

        // Calculado que es la suma de cantidad * precio de todos los ítems
        [Required]
        public double Total { get; set; }

        // Igual al total, se pone a 0 cuando la factura se paga en una orden de pago
        [Required]
        public double PendientePago { get; set; }

        public Proveedor Proveedor { get; set; }
        public Orden Orden { get; set; }
        public ICollection<FacturaCompraItem> FacturaCompraItems { get; set; }

    }

    public class FacturaCompraItem : BaseModel
    {
        [DisplayName("ID")]
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Se requiere una factura.")]
        public int FacturaCompraId { get; set; }

        [DisplayName("Producto")]
        [Required(ErrorMessage = "Se requiere un producto.")]
        [Column(TypeName = "varchar(6)")]
        [RegularExpression(@"^[a-zA-Z0-9]{1,6}$", ErrorMessage = "Sólo se aceptan 6 caracteres alfanuméricos.")]
        public string ProductoId { get; set; }

        [Required(ErrorMessage = "Se requiere una cantidad.")]
        [Range(1, int.MaxValue, ErrorMessage = "Sólo se aceptan números positivos.")]
        public int Cantidad { get; set; }

        [DisplayName("Precio")]
        [Required(ErrorMessage = "Se requiere un importe.")]
        [RegularExpression(@"^\d+\.{0,1}\d{0,2}$", ErrorMessage = "Se aceptan 2 decimales. Ingrese el valor decimal con ' . '.")]
        [Column(TypeName = "decimal(18,2)")]
        public decimal Precio { get; set; }

        [DisplayName("Observación")]
        public string Observacion { get; set; }

        public FacturaCompra FacturaCompra { get; set; }
        public Producto Producto { get; set; }

    }
}
