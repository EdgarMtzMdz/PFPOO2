﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using ProyectoFinal;

#nullable disable

namespace ProyectoFinal.Migrations
{
    [DbContext(typeof(ApplicationDBContext))]
    [Migration("20240625192047_InitialMigrations")]
    partial class InitialMigrations
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.6")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("ProyectoFinal.Clientes", b =>
                {
                    b.Property<Guid>("idClientes")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("Puntos")
                        .HasColumnType("int");

                    b.Property<DateTime>("fechaRegistrio")
                        .HasColumnType("datetime2");

                    b.Property<string>("nombreClientes")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("idClientes");

                    b.ToTable("Clientes");
                });

            modelBuilder.Entity("ProyectoFinal.Empleados", b =>
                {
                    b.Property<Guid>("idEmpleados")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Puesto")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<decimal>("Salario")
                        .HasColumnType("decimal(18,2)");

                    b.Property<string>("Telefono")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("nombreEmpleados")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("idEmpleados");

                    b.ToTable("Empleados");
                });

            modelBuilder.Entity("ProyectoFinal.Inventario", b =>
                {
                    b.Property<int>("codBarras")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("codBarras"));

                    b.Property<Guid?>("ProveedoresidProveedores")
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("cantProducto")
                        .HasColumnType("int");

                    b.Property<int>("codProveedor")
                        .HasColumnType("int");

                    b.Property<float>("costoProducto")
                        .HasColumnType("real");

                    b.Property<Guid?>("idProveedores")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("nombreProducto")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("codBarras");

                    b.HasIndex("ProveedoresidProveedores");

                    b.ToTable("Inventario");
                });

            modelBuilder.Entity("ProyectoFinal.Proveedores", b =>
                {
                    b.Property<Guid>("idProveedores")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("codProveedores")
                        .HasColumnType("int");

                    b.Property<string>("nombreProveedores")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("idProveedores");

                    b.ToTable("Proveedores");
                });

            modelBuilder.Entity("ProyectoFinal.Inventario", b =>
                {
                    b.HasOne("ProyectoFinal.Proveedores", "Proveedores")
                        .WithMany("Inventario")
                        .HasForeignKey("ProveedoresidProveedores");

                    b.Navigation("Proveedores");
                });

            modelBuilder.Entity("ProyectoFinal.Proveedores", b =>
                {
                    b.Navigation("Inventario");
                });
#pragma warning restore 612, 618
        }
    }
}