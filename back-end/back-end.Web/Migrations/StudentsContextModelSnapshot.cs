﻿// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using students_db.Repository;

#nullable disable

namespace back_end.Web.Migrations
{
    [DbContext(typeof(StudentsContext))]
    partial class StudentsContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.9")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            modelBuilder.Entity("students_db.Models.Student", b =>
                {
                    b.Property<int>("RA")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<long>("CPF")
                        .HasColumnType("bigint");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.HasKey("RA");

                    b.ToTable("Students");
                });
#pragma warning restore 612, 618
        }
    }
}
