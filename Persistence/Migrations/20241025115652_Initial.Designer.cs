﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using Persistence;

#nullable disable

namespace Persistence.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20241025115652_Initial")]
    partial class Initial
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.10")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("Domain.Order", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<DateTime>("DeliveryTime")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("District")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<decimal>("Weight")
                        .HasColumnType("numeric");

                    b.HasKey("Id");

                    b.ToTable("Orders");

                    b.HasData(
                        new
                        {
                            Id = new Guid("679858f7-48fb-4d5c-99a1-4bf9907d91b1"),
                            DeliveryTime = new DateTime(2024, 9, 23, 3, 56, 33, 0, DateTimeKind.Unspecified),
                            District = "Москва, Южная 25",
                            Weight = 35m
                        },
                        new
                        {
                            Id = new Guid("8bc64465-470f-4d25-99e2-d2c881f46a26"),
                            DeliveryTime = new DateTime(2024, 12, 13, 21, 56, 13, 0, DateTimeKind.Unspecified),
                            District = "Москва, Новороссийская 20",
                            Weight = 35m
                        });
                });
#pragma warning restore 612, 618
        }
    }
}
