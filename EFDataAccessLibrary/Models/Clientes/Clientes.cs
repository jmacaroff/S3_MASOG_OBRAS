using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Net.NetworkInformation;
using System.Text;

namespace EFDataAccessLibrary.Models.Clientes
{
    public class Cliente
    {
        [Key]
        public int Id { get; set; }

        [DisplayName("Nombre")]
        [Required(ErrorMessage = "Este campo es requerido.")]
        public string Nombre { get; set; }

        [DisplayName("DNI")]
        [Required(ErrorMessage = "Este campo es requerido.")]
        [RegularExpression(@"\d{8}$", ErrorMessage = "Número incorrecto.")]
        public double DNI { get; set; }

        [DisplayName("Dirección")]
        public string Direccion { get; set; }

        [DisplayName("Teléfono")]
        [RegularExpression(@"^\d{0,15}$", ErrorMessage = "Teléfono incorrecto.")]
        //agregandole double? hago que no sea requerido
        public double? Telefono { get; set; }

        [DisplayName("Correo Electronico")]
        [EmailAddress(ErrorMessage = "El formato es incorrecto.")]
        public string Correo { get; set; }
        public ICollection<Proyecto> Proyectos { get; set; }
    }
}
