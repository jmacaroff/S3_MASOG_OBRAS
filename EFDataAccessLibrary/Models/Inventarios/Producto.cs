using EFDataAccessLibrary.Models.Compras;
using EFDataAccessLibrary.Models.Ventas;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace EFDataAccessLibrary.Models.Inventarios
{
    public class Producto : BaseModel
    {
        [Required(ErrorMessage = "Este campo es requerido")]
        [DisplayName("ID")]
        [Key, DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Column(TypeName = "varchar(6)")]
        [RegularExpression(@"^[a-zA-Z0-9]{1,6}$", ErrorMessage = "Sólo se aceptan 6 caracteres alfanuméricos.")]
        public string Id { get; set; }


        [DisplayName("Descripción")]
        [Required(ErrorMessage = "Se requiere una descipción.")]
        public string Descripcion { get; set; }


        [DisplayName("Precio")]
        [Required(ErrorMessage = "Se requiere un precio.")]
        [RegularExpression(@"^\d+\.{0,1}\d{0,2}$", ErrorMessage = "Se aceptan 2 decimales. Ingrese el valor decimal con ' . '.")]
        [Column(TypeName = "decimal(18,2)")]
        public decimal Precio { get; set; }


        [DisplayName("Observación")]
        public string Observacion { get; set; }


        [DisplayName("Activo")]
        [Required]
        public bool Activo { get; set; }
        public ICollection<OrdenItem> OrdenItems { get; set; }
        public ICollection<FacturaCompraItem> FacturaCompraItems { get; set; }
        public ICollection<FacturaVentaItem> FacturaVentaItems { get; set; }
    }
}

