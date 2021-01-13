﻿// <auto-generated />
using System;
using MVC.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace MVC.Migrations.ApplicationDb
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20210111132940_priceDecimal")]
    partial class priceDecimal
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.10")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("MVC.Models.ArtPiece", b =>
                {
                    b.Property<int>("ArtPieceId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Author")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("AuthorsNote")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("Horizontal")
                        .HasColumnType("bit");

                    b.Property<string>("Info")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("ModifiedDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("PicUrl")
                        .HasColumnType("nvarchar(max)");

                    b.Property<float>("Price")
                        .HasColumnType("real");

                    b.Property<bool>("Reserved")
                        .HasColumnType("bit");

                    b.Property<string>("Style")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("TypeOfArt")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("ArtPieceId");

                    b.ToTable("ArtPiece");
                });
#pragma warning restore 612, 618
        }
    }
}
