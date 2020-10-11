using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Net.NetworkInformation;
using System.Text;

namespace EFDataAccessLibrary.Models.Clientes
{
    public class Proyecto
    {
        [Key]
        [DisplayName("ID")]
        public int Id { get; set; }

        [DisplayName("Cliente")]
        [Required(ErrorMessage = "Este campo es requerido.")]
        public int ClienteId { get; set; }

        [DisplayName("Nombre")]
        [Required(ErrorMessage = "Este campo es requerido.")]
        public string Nombre { get; set; }

        [DisplayName("Dirección")]
        [Required(ErrorMessage = "Este campo es requerido.")]
        public string Direccion { get; set; }

        [DisplayName("Observación")]
        public string Observacion { get; set; }
        public Cliente Cliente { get; set; }

    }
}
