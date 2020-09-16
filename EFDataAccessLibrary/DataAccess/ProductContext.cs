using EFDataAccessLibrary.Models.Inventarios;
using EFDataAccessLibrary.Models.Proveedores;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace EFDataAccessLibrary.DataAccess
{
    public class ProductContext : DbContext
    {
        public ProductContext(DbContextOptions option) : base(option) {  }
        public DbSet<Producto> Productos { get; set; }
        public DbSet<Proveedor> Proveedores { get; set; }
    }

}

// me base en Entity Framework Best Practices - Should EFCore Be Your Data Access of Choice? --> https://youtu.be/qkJ9keBmQWo
// Obviamente no me vi todo solo los primeros minutos y me parecio interesante estructurarlo asi
