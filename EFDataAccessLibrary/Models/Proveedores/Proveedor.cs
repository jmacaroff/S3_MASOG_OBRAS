using EFDataAccessLibrary.Models.Compras;
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
        [Key]
        public int Id { get; set; }


        [DisplayName("Razón Social")]
        [Required(ErrorMessage = "Este campo es requerido.")]
        [RegularExpression(@"^([^0-9]*)$", ErrorMessage ="No se aceptan números")]
        public string RazonSocial { get; set; }


        [DisplayName("CUIT")]
        [Required(ErrorMessage = "Este campo es requerido.")]
        [RegularExpression(@"\d{11}$", ErrorMessage = "Número incorrecto.")]
        public double CUIT { get; set; }

        [DisplayName("Dirección")]
        public string Direccion { get; set; }

        [DisplayName("Teléfono")]
        [RegularExpression(@"^\d{0,15}$", ErrorMessage = "Teléfono incorrecto.")]
        //agregandole double? hago que no sea requerido
        public double? Telefono { get; set; }

        [EmailAddress(ErrorMessage = "El formato es incorrecto.")]
        public string Correo { get; set; }

        [DisplayName("Observación")]
        public string Observacion { get; set; }
        public ICollection<Orden> Ordenes { get; set; }
        public ICollection<FacturaCompra> FacturasCompra { get; set; }
    }
}
