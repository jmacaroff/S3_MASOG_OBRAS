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
        [DisplayName("Id : ")]
        [Key, DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Column("producto_id",TypeName = "varchar(6)")]
        public string Id { get; set; }


        [DisplayName("Descripcion : ")]
        [Required(ErrorMessage = "Este campo es requerido")]
        [RegularExpression("^[a-zA-Z ]+$", ErrorMessage = "Solo se permiten Letras")]
        public string Descripcion { get; set; }


        [DisplayName("Precio : ")]
        [Required(ErrorMessage = "Se requiere un precio")]
        [RegularExpression(@"^\d+\.\d{2}$", ErrorMessage = "Se aceptan 2 decimales. Ingrese el valor decimal con ' . '")]
        [Column(TypeName = "decimal(18,2)")]
        public decimal Precio { get; set; }



        public string Observacion { get; set; }


        [DisplayName("Activo : ")]
        [Required]
        public bool Activo { get; set; }
    }
}

