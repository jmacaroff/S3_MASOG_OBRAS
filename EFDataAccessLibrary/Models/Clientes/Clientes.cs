using System;
using System.Collections.Generic;
using System.Net.NetworkInformation;
using System.Text;

namespace EFDataAccessLibrary.Models.Clientes
{
    public class Cliente
    {
        public int Id { get; set; }
        public string Nombre { get; set; }

        public double MyP { get; set; }
    }
}
