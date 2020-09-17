using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace EFDataAccessLibrary.Models.Proveedores
{
    public class Proveedor
    {
        [Required(ErrorMessage = "Este campo es requerido.")]
        [DisplayName("ID")]
        [Key, DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Id { get; set; }


        [DisplayName("Razón Social")]
        [Required(ErrorMessage = "Este campo es requerido.")]
        public string RazonSocial { get; set; }


            [DisplayName("CUIT")]
            [Required(ErrorMessage = "Este campo es requerido.")]
            [RegularExpression(@"\d{11}$", ErrorMessage = "Número incorrecto.")]
        public double CUIT { get; set; }

        [DisplayName("Dirección")]
        [Required(ErrorMessage = "Este campo es requerido.")]
        public string Direccion { get; set; }

        [DisplayName("Teléfono")]
        [RegularExpression(@"\d{0,15}$", ErrorMessage = "Teléfono incorrecto.")]
        public double Telefono { get; set; }

        [EmailAddress(ErrorMessage = "El formato es incorrecto.")]
        public string Correo { get; set; }

        [DisplayName("Observación")]
        public string Observacion { get; set; }
    }
}
