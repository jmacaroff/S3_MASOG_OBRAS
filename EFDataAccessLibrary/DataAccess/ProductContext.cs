using EFDataAccessLibrary.Models.Clientes;
using EFDataAccessLibrary.Models.Inventarios;
using EFDataAccessLibrary.Models.Proveedores;
using EFDataAccessLibrary.Models.Compras;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace EFDataAccessLibrary.DataAccess
{
    public class ProductContext : DbContext
    {
        public ProductContext(DbContextOptions option) : base(option) {  }
        public DbSet<Producto> Productos { get; set; }
        public DbSet<Deposito> Depositos { get; set; }
        public DbSet<Proveedor> Proveedores { get; set; }
        public DbSet<Cliente> Clientes { get; set; }
        public DbSet<Orden> Ordenes { get; set; }
        public DbSet<OrdenItem> OrdenItems { get; set; }
        public DbSet<FacturaCompra> FacturasCompra { get; set; }
        public DbSet<FacturaCompraItem> FacturaCompraItems { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            foreach (var relationship in modelBuilder.Model.GetEntityTypes().SelectMany(e => e.GetForeignKeys()))
            {
                relationship.DeleteBehavior = DeleteBehavior.Restrict;
            }

            base.OnModelCreating(modelBuilder);
        }
    }

}

// me base en Entity Framework Best Practices - Should EFCore Be Your Data Access of Choice? --> https://youtu.be/qkJ9keBmQWo
// Obviamente no me vi todo solo los primeros minutos y me parecio interesante estructurarlo asi
