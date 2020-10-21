using EFDataAccessLibrary.Models.Clientes;
using EFDataAccessLibrary.Models.Compras;
using EFDataAccessLibrary.Models.Proveedores;
using EFDataAccessLibrary.Models.Ventas;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace EFDataAccessLibrary.Models.Inventarios
{
    public class MovStock : BaseModel
    {
        [DisplayName("ID")]
        [Key]
        public int Id { get; set; }

        // Tipo de movimiento. Se debe seleccionar con un drop down
        [Required(ErrorMessage = "Se requiere un tipo de movimiento.")]
        [DisplayName("Tipo de Movimiento.")]
        public int TipoMovimientoId { get; set; }

        [DisplayName("Cliente Asociado")]
        public int? ClienteId { get; set; }

        [DisplayName("Proveedor Asociado")]
        public int? ProveedorId { get; set; }

        [DataType(DataType.Date)]
        [Required(ErrorMessage = "Se requiere una fecha.")]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [Display(Name = "Fecha")]
        public DateTime Fecha { get; set; } = DateTime.Now;

        [DisplayName("Observación")]
        public string Observacion { get; set; }

        [Required(ErrorMessage = "Se requiere un tipo.")]
        public TipoMovimiento TipoMovimiento { get; set; }
        public Proveedor Proveedores { get; set; }
        public Cliente Clientes { get; set; }
        public ICollection<MovStockItem> MovStockItems { get; set; }

    }

    public class MovStockItem : BaseModel
    {
        [DisplayName("ID")]
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Se requiere un movimiento de stock.")]
        public int MovStockId { get; set; }

        [DisplayName("Producto")]
        [Required(ErrorMessage = "Se requiere un producto.")]
        [Column(TypeName = "varchar(6)")]
        [RegularExpression(@"^[a-zA-Z0-9]{1,6}$", ErrorMessage = "Sólo se aceptan 6 caracteres alfanuméricos.")]
        public string ProductoId { get; set; }

        [Required(ErrorMessage = "Se requiere una cantidad.")]
        [Range(1, int.MaxValue, ErrorMessage = "Sólo se aceptan números positivos.")]
        public int Cantidad { get; set; }

        [DisplayName("Observación")]
        public string Observacion { get; set; }

        public MovStock MovStock { get; set; }
        public Producto Producto { get; set; }

    }
    public class TipoMovimiento : BaseModel
    {
        [Key]
        public int Id { get; set; }

        [DisplayName("Es de Egreso")]
        public bool EsEgreso { get; set; }

        [DisplayName("Descripción")]
        [Required(ErrorMessage = "Se requiere una descipción.")]
        public string Descripcion { get; set; }
    }
}

