using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace EFDataAccessLibrary.Models.Inventarios
{
        public class Deposito : BaseModel
        {
            [Required(ErrorMessage = "Este campo es requerido")]
            [DisplayName("ID")]
            [Key, DatabaseGenerated(DatabaseGeneratedOption.None)]
            [Column(TypeName = "nchar(3)")]
            [RegularExpression(@"^[0-9]{3}$", ErrorMessage = "Sólo se aceptan 3 caracteres numéricos.")]
            public string Id { get; set; }


            [DisplayName("Descripción")]
            [Required(ErrorMessage = "Se requiere una descipción.")]
            public string Descripcion { get; set; }

            [DisplayName("Dirección")]
            public string Direccion { get; set; }
        }
}