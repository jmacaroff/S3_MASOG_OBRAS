﻿// <auto-generated />
using System;
using EFDataAccessLibrary.DataAccess;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace EFDataAccessLibrary.Migrations
{
    [DbContext(typeof(ProductContext))]
    [Migration("20201016011518_FacturaVenta")]
    partial class FacturaVenta
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.9")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("EFDataAccessLibrary.Models.Clientes.Cliente", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Correo")
                        .HasColumnType("nvarchar(max)");

                    b.Property<double>("DNI")
                        .HasColumnType("float");

                    b.Property<string>("Direccion")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Nombre")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<double?>("Telefono")
                        .HasColumnType("float");

                    b.HasKey("Id");

                    b.ToTable("Clientes");
                });

            modelBuilder.Entity("EFDataAccessLibrary.Models.Clientes.Proyecto", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("ClienteId")
                        .HasColumnType("int");

                    b.Property<string>("Direccion")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Nombre")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Observacion")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("ClienteId");

                    b.ToTable("Proyectos");
                });

            modelBuilder.Entity("EFDataAccessLibrary.Models.Compras.ConceptoPago", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Descripcion")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("ConceptoPago");
                });

            modelBuilder.Entity("EFDataAccessLibrary.Models.Compras.FacturaCompra", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("FechaAlta")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("FechaFactura")
                        .HasColumnType("datetime2");

                    b.Property<int>("Numero")
                        .HasColumnType("int");

                    b.Property<string>("Observacion")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("OrdenId")
                        .HasColumnType("int");

                    b.Property<double>("PendientePago")
                        .HasColumnType("float");

                    b.Property<int>("ProveedorId")
                        .HasColumnType("int");

                    b.Property<string>("PuntoVenta")
                        .IsRequired()
                        .HasColumnType("nchar(5)");

                    b.Property<string>("TipoFactura")
                        .IsRequired()
                        .HasColumnType("char(1)");

                    b.Property<double>("Total")
                        .HasColumnType("float");

                    b.HasKey("Id");

                    b.HasIndex("OrdenId");

                    b.HasIndex("ProveedorId");

                    b.ToTable("FacturasCompra");
                });

            modelBuilder.Entity("EFDataAccessLibrary.Models.Compras.FacturaCompraItem", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("Cantidad")
                        .HasColumnType("int");

                    b.Property<int>("FacturaCompraId")
                        .HasColumnType("int");

                    b.Property<string>("Observacion")
                        .HasColumnType("nvarchar(max)");

                    b.Property<decimal>("Precio")
                        .HasColumnType("decimal(18,2)");

                    b.Property<string>("ProductoId")
                        .IsRequired()
                        .HasColumnType("varchar(6)");

                    b.HasKey("Id");

                    b.HasIndex("FacturaCompraId");

                    b.HasIndex("ProductoId");

                    b.ToTable("FacturaCompraItems");
                });

            modelBuilder.Entity("EFDataAccessLibrary.Models.Compras.Orden", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("Fecha")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("FechaEntrega")
                        .HasColumnType("datetime2");

                    b.Property<string>("Observacion")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("ProveedorId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("ProveedorId");

                    b.ToTable("Ordenes");
                });

            modelBuilder.Entity("EFDataAccessLibrary.Models.Compras.OrdenItem", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("Cantidad")
                        .HasColumnType("int");

                    b.Property<string>("Observacion")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("OrdenId")
                        .HasColumnType("int");

                    b.Property<decimal>("Precio")
                        .HasColumnType("decimal(18,2)");

                    b.Property<string>("ProductoId")
                        .IsRequired()
                        .HasColumnType("varchar(6)");

                    b.HasKey("Id");

                    b.HasIndex("OrdenId");

                    b.HasIndex("ProductoId");

                    b.ToTable("OrdenItems");
                });

            modelBuilder.Entity("EFDataAccessLibrary.Models.Compras.OrdenPago", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("ConceptoPagoId")
                        .HasColumnType("int");

                    b.Property<DateTime>("FechaEmision")
                        .HasColumnType("datetime2");

                    b.Property<string>("Observacion")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("ProveedorId")
                        .HasColumnType("int");

                    b.Property<double>("Total")
                        .HasColumnType("float");

                    b.HasKey("Id");

                    b.HasIndex("ConceptoPagoId");

                    b.HasIndex("ProveedorId");

                    b.ToTable("OrdenesPago");
                });

            modelBuilder.Entity("EFDataAccessLibrary.Models.Compras.OrdenPagoItem", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("FacturaCompraId")
                        .HasColumnType("int");

                    b.Property<double>("Importe")
                        .HasColumnType("float");

                    b.Property<int>("OrdenPagoId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("FacturaCompraId");

                    b.HasIndex("OrdenPagoId");

                    b.ToTable("OrdenPagoItems");
                });

            modelBuilder.Entity("EFDataAccessLibrary.Models.Inventarios.Deposito", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nchar(3)");

                    b.Property<string>("Descripcion")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Direccion")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Depositos");
                });

            modelBuilder.Entity("EFDataAccessLibrary.Models.Inventarios.Producto", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("varchar(6)");

                    b.Property<bool>("Activo")
                        .HasColumnType("bit");

                    b.Property<string>("Descripcion")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Observacion")
                        .HasColumnType("nvarchar(max)");

                    b.Property<decimal>("Precio")
                        .HasColumnType("decimal(18,2)");

                    b.HasKey("Id");

                    b.ToTable("Productos");
                });

            modelBuilder.Entity("EFDataAccessLibrary.Models.Proveedores.Proveedor", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<double>("CUIT")
                        .HasColumnType("float");

                    b.Property<string>("Correo")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Direccion")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Observacion")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("RazonSocial")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<double?>("Telefono")
                        .HasColumnType("float");

                    b.HasKey("Id");

                    b.ToTable("Proveedores");
                });

            modelBuilder.Entity("EFDataAccessLibrary.Models.Ventas.FacturaVenta", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("Fecha")
                        .HasColumnType("datetime2");

                    b.Property<string>("Observacion")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("ProyectoID")
                        .HasColumnType("int");

                    b.Property<string>("PuntoVenta")
                        .IsRequired()
                        .HasColumnType("nchar(5)");

                    b.Property<string>("TipoFactura")
                        .IsRequired()
                        .HasColumnType("char(1)");

                    b.Property<double>("Total")
                        .HasColumnType("float");

                    b.HasKey("Id");

                    b.HasIndex("ProyectoID");

                    b.ToTable("FacturasVenta");
                });

            modelBuilder.Entity("EFDataAccessLibrary.Models.Ventas.FacturaVentaItem", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("Cantidad")
                        .HasColumnType("int");

                    b.Property<int>("FacturaVentaId")
                        .HasColumnType("int");

                    b.Property<string>("Observacion")
                        .HasColumnType("nvarchar(max)");

                    b.Property<decimal>("Precio")
                        .HasColumnType("decimal(18,2)");

                    b.Property<string>("ProductoId")
                        .IsRequired()
                        .HasColumnType("varchar(6)");

                    b.Property<double>("Subtotal")
                        .HasColumnType("float");

                    b.HasKey("Id");

                    b.HasIndex("FacturaVentaId");

                    b.HasIndex("ProductoId");

                    b.ToTable("FacturasVentaItems");
                });

            modelBuilder.Entity("EFDataAccessLibrary.Models.Clientes.Proyecto", b =>
                {
                    b.HasOne("EFDataAccessLibrary.Models.Clientes.Cliente", "Cliente")
                        .WithMany("Proyectos")
                        .HasForeignKey("ClienteId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();
                });

            modelBuilder.Entity("EFDataAccessLibrary.Models.Compras.FacturaCompra", b =>
                {
                    b.HasOne("EFDataAccessLibrary.Models.Compras.Orden", "Orden")
                        .WithMany("FacturaCompra")
                        .HasForeignKey("OrdenId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("EFDataAccessLibrary.Models.Proveedores.Proveedor", "Proveedor")
                        .WithMany("FacturasCompra")
                        .HasForeignKey("ProveedorId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();
                });

            modelBuilder.Entity("EFDataAccessLibrary.Models.Compras.FacturaCompraItem", b =>
                {
                    b.HasOne("EFDataAccessLibrary.Models.Compras.FacturaCompra", "FacturaCompra")
                        .WithMany("FacturaCompraItems")
                        .HasForeignKey("FacturaCompraId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("EFDataAccessLibrary.Models.Inventarios.Producto", "Producto")
                        .WithMany("FacturaCompraItems")
                        .HasForeignKey("ProductoId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();
                });

            modelBuilder.Entity("EFDataAccessLibrary.Models.Compras.Orden", b =>
                {
                    b.HasOne("EFDataAccessLibrary.Models.Proveedores.Proveedor", "Proveedor")
                        .WithMany("Ordenes")
                        .HasForeignKey("ProveedorId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();
                });

            modelBuilder.Entity("EFDataAccessLibrary.Models.Compras.OrdenItem", b =>
                {
                    b.HasOne("EFDataAccessLibrary.Models.Compras.Orden", "Orden")
                        .WithMany("OrdenItems")
                        .HasForeignKey("OrdenId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("EFDataAccessLibrary.Models.Inventarios.Producto", "Producto")
                        .WithMany("OrdenItems")
                        .HasForeignKey("ProductoId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();
                });

            modelBuilder.Entity("EFDataAccessLibrary.Models.Compras.OrdenPago", b =>
                {
                    b.HasOne("EFDataAccessLibrary.Models.Compras.ConceptoPago", "ConceptoPago")
                        .WithMany()
                        .HasForeignKey("ConceptoPagoId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("EFDataAccessLibrary.Models.Proveedores.Proveedor", "Proveedor")
                        .WithMany()
                        .HasForeignKey("ProveedorId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();
                });

            modelBuilder.Entity("EFDataAccessLibrary.Models.Compras.OrdenPagoItem", b =>
                {
                    b.HasOne("EFDataAccessLibrary.Models.Compras.FacturaCompra", "FacturaCompra")
                        .WithMany()
                        .HasForeignKey("FacturaCompraId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("EFDataAccessLibrary.Models.Compras.OrdenPago", "OrdenPago")
                        .WithMany("OrdenPagoItems")
                        .HasForeignKey("OrdenPagoId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();
                });

            modelBuilder.Entity("EFDataAccessLibrary.Models.Ventas.FacturaVenta", b =>
                {
                    b.HasOne("EFDataAccessLibrary.Models.Clientes.Proyecto", "Proyecto")
                        .WithMany("FacturaVentas")
                        .HasForeignKey("ProyectoID")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();
                });

            modelBuilder.Entity("EFDataAccessLibrary.Models.Ventas.FacturaVentaItem", b =>
                {
                    b.HasOne("EFDataAccessLibrary.Models.Ventas.FacturaVenta", "FacturaCompra")
                        .WithMany("FacturaVentaItems")
                        .HasForeignKey("FacturaVentaId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("EFDataAccessLibrary.Models.Inventarios.Producto", "Producto")
                        .WithMany("FacturaVentaItems")
                        .HasForeignKey("ProductoId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
