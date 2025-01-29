﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Testamente.DataAccess;

#nullable disable

namespace Testamente.Web.Migrations
{
    [DbContext(typeof(TestamenteContext))]
    [Migration("20250127205359_ChangedToEnableSoftDeleteAutomaticallyFalse")]
    partial class ChangedToEnableSoftDeleteAutomaticallyFalse
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "9.0.1")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("Testamente.DataAccess.PersonEntity", b =>
                {
                    b.Property<Guid>("PersonEntityId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Address")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateOnly>("BirthDate")
                        .HasColumnType("date");

                    b.Property<Guid?>("FatherId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<bool>("IsAlive")
                        .HasColumnType("bit");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("bit");

                    b.Property<Guid?>("MotherId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid?>("SpouseId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("PersonEntityId");

                    b.HasIndex("FatherId");

                    b.HasIndex("MotherId");

                    b.HasIndex("SpouseId");

                    b.ToTable("People");
                });

            modelBuilder.Entity("Testamente.DataAccess.ReportSectionEntity", b =>
                {
                    b.Property<Guid>("ReportSectionEntityId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Body")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("ReportSectionEntityId");

                    b.ToTable("ReportSections");
                });

            modelBuilder.Entity("Testamente.DataAccess.PersonEntity", b =>
                {
                    b.HasOne("Testamente.DataAccess.PersonEntity", "Father")
                        .WithMany()
                        .HasForeignKey("FatherId")
                        .OnDelete(DeleteBehavior.NoAction);

                    b.HasOne("Testamente.DataAccess.PersonEntity", "Mother")
                        .WithMany()
                        .HasForeignKey("MotherId")
                        .OnDelete(DeleteBehavior.NoAction);

                    b.HasOne("Testamente.DataAccess.PersonEntity", "Spouse")
                        .WithMany()
                        .HasForeignKey("SpouseId")
                        .OnDelete(DeleteBehavior.NoAction);

                    b.Navigation("Father");

                    b.Navigation("Mother");

                    b.Navigation("Spouse");
                });
#pragma warning restore 612, 618
        }
    }
}
