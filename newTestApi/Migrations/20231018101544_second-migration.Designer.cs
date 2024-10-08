﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using newTestApi.Data;

#nullable disable

namespace newTestApi.Migrations
{
    [DbContext(typeof(StudentDbContext))]
    [Migration("20231018101544_second-migration")]
    partial class secondmigration
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.12")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("newTestApi.Models.Domain.Marks", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Chemistry")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Maths")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("Pass")
                        .HasColumnType("bit");

                    b.Property<string>("Physics")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("StudentRegNo")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Marks");
                });

            modelBuilder.Entity("newTestApi.Models.Domain.Student", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Address")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid>("MarksId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("RegNo")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Remarks")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("MarksId");

                    b.ToTable("Students");
                });

            modelBuilder.Entity("newTestApi.Models.Domain.Student", b =>
                {
                    b.HasOne("newTestApi.Models.Domain.Marks", "Marks")
                        .WithMany()
                        .HasForeignKey("MarksId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Marks");
                });
#pragma warning restore 612, 618
        }
    }
}
