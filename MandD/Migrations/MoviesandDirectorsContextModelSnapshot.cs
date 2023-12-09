﻿// <auto-generated />
using System;
using MandD;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace MandD.Migrations
{
    [DbContext(typeof(MoviesandDirectorsContext))]
    partial class MoviesandDirectorsContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.12")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("MandD.Director", b =>
                {
                    b.Property<int>("DirectorId")
                        .HasColumnType("int");

                    b.Property<int>("Age")
                        .HasColumnType("int");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasMaxLength(100)
                        .IsUnicode(false)
                        .HasColumnType("varchar(100)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(100)
                        .IsUnicode(false)
                        .HasColumnType("varchar(100)");

                    b.Property<string>("Nationality")
                        .IsRequired()
                        .HasMaxLength(100)
                        .IsUnicode(false)
                        .HasColumnType("varchar(100)");

                    b.HasKey("DirectorId")
                        .HasName("PK__Director__26C69E464D3B1115");

                    b.ToTable("Director", (string)null);
                });

            modelBuilder.Entity("MandD.Movie", b =>
                {
                    b.Property<int>("MovieId")
                        .HasColumnType("int");

                    b.Property<decimal>("Budget")
                        .HasColumnType("decimal(18, 2)");

                    b.Property<decimal?>("Collections")
                        .HasColumnType("decimal(18, 2)");

                    b.Property<string>("Description")
                        .HasMaxLength(1000)
                        .IsUnicode(false)
                        .HasColumnType("varchar(1000)");

                    b.Property<int?>("DirectorId")
                        .HasColumnType("int");

                    b.Property<string>("Genre")
                        .HasMaxLength(100)
                        .IsUnicode(false)
                        .HasColumnType("varchar(100)");

                    b.Property<DateTime?>("ReleaseDate")
                        .HasColumnType("date");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasMaxLength(100)
                        .IsUnicode(false)
                        .HasColumnType("varchar(100)");

                    b.HasKey("MovieId")
                        .HasName("PK__Movie__4BD2941AB27BF383");

                    b.HasIndex("DirectorId");

                    b.ToTable("Movie", (string)null);
                });

            modelBuilder.Entity("MandD.Movie", b =>
                {
                    b.HasOne("MandD.Director", "Director")
                        .WithMany("Movies")
                        .HasForeignKey("DirectorId")
                        .HasConstraintName("FK__Movie__DirectorI__31EC6D26");

                    b.Navigation("Director");
                });

            modelBuilder.Entity("MandD.Director", b =>
                {
                    b.Navigation("Movies");
                });
#pragma warning restore 612, 618
        }
    }
}