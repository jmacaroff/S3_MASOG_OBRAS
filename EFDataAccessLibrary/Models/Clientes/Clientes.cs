using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Net.NetworkInformation;
using System.Text;

namespace EFDataAccessLibrary.Models.Clientes
{
    public class Cliente
    {
        [Key]
        public int Id { get; set; }
        public string Nombre { get; set; }

        public double MyP { get; set; }
    }
}
