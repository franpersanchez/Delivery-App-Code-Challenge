﻿// <auto-generated />
using System;
using DB;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace DB.Migrations
{
    [DbContext(typeof(DeliveryAppContext))]
    partial class DeliveryAppContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.10")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("Delivery_App_Code_Challenge.DB.Models.Cliente", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<long>("Id"));

                    b.Property<string>("Apellidos")
                        .HasColumnType("text");

                    b.Property<string>("Email")
                        .HasColumnType("text");

                    b.Property<string>("Nombre")
                        .HasColumnType("text");

                    b.Property<string>("Telefono")
                        .HasColumnType("text");

                    b.Property<float>("Ubicacion_latitud")
                        .HasColumnType("real");

                    b.Property<float>("Ubicacion_longitud")
                        .HasColumnType("real");

                    b.HasKey("Id");

                    b.ToTable("Clientes");
                });

            modelBuilder.Entity("Delivery_App_Code_Challenge.DB.Models.Pedido", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<long>("Id"));

                    b.Property<long>("ClienteId")
                        .HasColumnType("bigint");

                    b.Property<string>("Commentarios")
                        .HasColumnType("text");

                    b.Property<int>("EstadoPedido")
                        .HasColumnType("integer");

                    b.Property<DateTime>("HoraCreacion")
                        .HasColumnType("timestamp with time zone");

                    b.Property<long>("VehiculoId")
                        .HasColumnType("bigint");

                    b.HasKey("Id");

                    b.HasIndex("ClienteId");

                    b.HasIndex("VehiculoId");

                    b.ToTable("Pedidos");
                });

            modelBuilder.Entity("Delivery_App_Code_Challenge.DB.Models.RegistroUbicacion", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<long>("Id"));

                    b.Property<DateTime>("FechaRegistro")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("Ubicacion_latitud")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Ubicacion_longitud")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<long>("VehiculoId")
                        .HasColumnType("bigint");

                    b.HasKey("Id");

                    b.HasIndex("VehiculoId");

                    b.ToTable("RegistroUbicacion");
                });

            modelBuilder.Entity("Delivery_App_Code_Challenge.DB.Models.Vehiculo", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<long>("Id"));

                    b.Property<string>("Matricula")
                        .HasColumnType("text");

                    b.Property<string>("NombreConductor")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("Vehiculos");
                });

            modelBuilder.Entity("Delivery_App_Code_Challenge.DB.Models.Pedido", b =>
                {
                    b.HasOne("Delivery_App_Code_Challenge.DB.Models.Cliente", "Cliente")
                        .WithMany("Pedidos")
                        .HasForeignKey("ClienteId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Delivery_App_Code_Challenge.DB.Models.Vehiculo", "Vehiculo")
                        .WithMany("Pedidos")
                        .HasForeignKey("VehiculoId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Cliente");

                    b.Navigation("Vehiculo");
                });

            modelBuilder.Entity("Delivery_App_Code_Challenge.DB.Models.RegistroUbicacion", b =>
                {
                    b.HasOne("Delivery_App_Code_Challenge.DB.Models.Vehiculo", "Vehiculo")
                        .WithMany("RegistroUbicaciones")
                        .HasForeignKey("VehiculoId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Vehiculo");
                });

            modelBuilder.Entity("Delivery_App_Code_Challenge.DB.Models.Cliente", b =>
                {
                    b.Navigation("Pedidos");
                });

            modelBuilder.Entity("Delivery_App_Code_Challenge.DB.Models.Vehiculo", b =>
                {
                    b.Navigation("Pedidos");

                    b.Navigation("RegistroUbicaciones");
                });
#pragma warning restore 612, 618
        }
    }
}
