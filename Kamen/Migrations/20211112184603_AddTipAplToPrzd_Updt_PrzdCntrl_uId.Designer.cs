﻿// <auto-generated />
using Kamen.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Kamen.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20211112184603_AddTipAplToPrzd_Updt_PrzdCntrl_uId")]
    partial class AddTipAplToPrzd_Updt_PrzdCntrl_uId
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.11")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Kamen.Models.Proizvod", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<double>("Cijena")
                        .HasColumnType("float");

                    b.Property<string>("Naziv")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Opis")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Slika")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("TipAplId")
                        .HasColumnType("int");

                    b.Property<int>("VrstaId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("TipAplId");

                    b.HasIndex("VrstaId");

                    b.ToTable("Proizvod");
                });

            modelBuilder.Entity("Kamen.Models.TipApl", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Naziv")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("TipApl");
                });

            modelBuilder.Entity("Kamen.Models.Vrsta", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Naziv")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("RedPrikaz")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("Vrsta");
                });

            modelBuilder.Entity("Kamen.Models.Proizvod", b =>
                {
                    b.HasOne("Kamen.Models.TipApl", "TipApl")
                        .WithMany()
                        .HasForeignKey("TipAplId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Kamen.Models.Vrsta", "Vrsta")
                        .WithMany()
                        .HasForeignKey("VrstaId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("TipApl");

                    b.Navigation("Vrsta");
                });
#pragma warning restore 612, 618
        }
    }
}
