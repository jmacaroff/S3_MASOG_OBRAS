using EFDataAccessLibrary.Models.Clientes;
using EFDataAccessLibrary.Models.Inventarios;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace EFDataAccessLibrary.Models.Ventas
{
    public class FacturaVenta : BaseModel
    {
        [Key]
        public int Id { get; set; }

        [DisplayName("Cliente Asociado")]
        [Required(ErrorMessage = "Se requiere un cliente.")]
        public int ClienteId { get; set; }

        //Un Proyecto tiene ninguna o muchas facturas de Venta
        [DisplayName("Proyecto Asociado")]
        [Required(ErrorMessage = "Se requiere un proyecto.")]
        public int ProyectoId { get; set; }

        [Required(ErrorMessage = "Se requiere un punto de venta.")]
        [RegularExpression(@"^\d{1,5}$", ErrorMessage = "Formato incorrecto, no debe superar los 5 dígitos.")]
        [Range(1, int.MaxValue, ErrorMessage = "Sólo se aceptan números positivos.")]
        [Column(TypeName = "nchar(5)")]
        [Display(Name = "Punto de Venta")]
        public int PuntoVenta { get; set; }

        [Required(ErrorMessage = "Se requiere un tipo de factura.")]
        [RegularExpression(@"^[A-C]{1}$", ErrorMessage = "Sólo se acepta A, B o C.")]
        [Column(TypeName = "char(1)")]
        [Display(Name = "Tipo de Factura")]
        public string TipoFactura { get; set; }

        [DisplayName("Número")]
        [Required(ErrorMessage = "Se requiere un número.")]
        public int Numero { get; set; }

        [DataType(DataType.Date)]
        [Required(ErrorMessage = "Ingrese una fecha.")]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [Display(Name = "Fecha")]
        public DateTime Fecha { get; set; } = DateTime.Now;

        [DisplayName("Observación")]
        public string Observacion { get; set; }

        // Calculado que es la suma de cantidad * precio de todos los ítems
        [Required]
        public double Total { get; set; }

        [Required]
        public double PendienteCobrar { get; set; }

        // Relaciones
        public Cliente Cliente { get; set; }
        public Proyecto Proyecto { get; set; }
        public ICollection<FacturaVentaItem> FacturaVentaItems { get; set; }
    }

    public class FacturaVentaItem : BaseModel
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Se requiere una FacturaAsociada.")]
        public int FacturaVentaId { get; set; }

        [DisplayName("Producto")]
        [Required(ErrorMessage = "Se requiere un producto.")]
        [Column(TypeName = "varchar(6)")]
        [RegularExpression(@"^[a-zA-Z0-9]{1,6}$", ErrorMessage = "Sólo se aceptan 6 caracteres alfanuméricos.")]
        public string ProductoId { get; set; }

        [DisplayName("Precio")]
        [Required(ErrorMessage = "Se requiere un importe.")]
        [RegularExpression(@"^\d+\.{0,1}\d{0,2}$", ErrorMessage = "Se aceptan 2 decimales. Ingrese el valor decimal con ' . '.")]
        [Column(TypeName = "decimal(18,2)")]
        public decimal Precio { get; set; }

        [Required(ErrorMessage = "Se requiere una cantidad.")]
        [Range(1, int.MaxValue, ErrorMessage = "Sólo se aceptan números positivos.")]
        public int Cantidad { get; set; }

        [DisplayName("Observación")]
        public string Observacion { get; set; }

        public FacturaVenta FacturaVenta { get; set; }
        public Producto Producto { get; set; }
    }

    public class RankingProductos : BaseModel
    {

        [DisplayName("Producto")]
        [Required(ErrorMessage = "Se requiere un producto.")]
        [Column(TypeName = "varchar(6)")]
        [RegularExpression(@"^[a-zA-Z0-9]{1,6}$", ErrorMessage = "Sólo se aceptan 6 caracteres alfanuméricos.")]
        public string ProductoId { get; set; }

        [DisplayName("Descripción")]
        [Required(ErrorMessage = "Se requiere una descipción.")]
        public string Descripcion { get; set; }

        public int Ventas { get; set; }

        public Producto Producto { get; set; }
    }
}
