﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using sports_fest_dotangular.Server.Data.Sf;

#nullable disable

namespace sports_fest_dotangular.Server.Data.Migrations
{
    [DbContext(typeof(SfContext))]
    [Migration("20240824201005_Initial")]
    partial class Initial
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.8")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            MySqlModelBuilderExtensions.AutoIncrementColumns(modelBuilder);

            modelBuilder.Entity("UserDiscipline", b =>
                {
                    b.Property<Guid>("DisciplineId")
                        .HasColumnType("char(36)");

                    b.Property<Guid>("UserId")
                        .HasColumnType("char(36)");

                    b.HasKey("DisciplineId", "UserId");

                    b.HasIndex("UserId");

                    b.ToTable("UserDiscipline");
                });

            modelBuilder.Entity("sports_fest_dotangular.Server.Models.Core.Entity<System.Guid>", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("char(36)");

                    b.Property<DateTime>("CreatedAt")
                        .IsConcurrencyToken()
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime(6)");

                    MySqlPropertyBuilderExtensions.UseMySqlIdentityColumn(b.Property<DateTime>("CreatedAt"));

                    b.Property<DateTime>("UpdatedAt")
                        .IsConcurrencyToken()
                        .ValueGeneratedOnAddOrUpdate()
                        .HasColumnType("datetime(6)");

                    b.HasKey("Id");

                    b.ToTable("Entity<Guid>");

                    b.UseTptMappingStrategy();
                });

            modelBuilder.Entity("sports_fest_dotangular.Server.Models.SF.Entry", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("char(36)");

                    b.Property<string>("Comment")
                        .IsRequired()
                        .HasMaxLength(1024)
                        .HasColumnType("varchar(1024)");

                    b.Property<DateTime>("CreatedAt")
                        .IsConcurrencyToken()
                        .ValueGeneratedOnAddOrUpdate()
                        .HasColumnType("datetime(6)");

                    b.Property<Guid?>("DisciplineId")
                        .IsRequired()
                        .HasColumnType("char(36)");

                    b.Property<double>("Score")
                        .HasColumnType("double");

                    b.Property<Guid?>("StudentId")
                        .IsRequired()
                        .HasColumnType("char(36)");

                    b.Property<DateTime>("UpdatedAt")
                        .IsConcurrencyToken()
                        .ValueGeneratedOnAddOrUpdate()
                        .HasColumnType("datetime(6)");

                    b.HasKey("Id");

                    b.HasIndex("DisciplineId");

                    b.HasIndex("StudentId");

                    b.ToTable("Entries", (string)null);
                });

            modelBuilder.Entity("sports_fest_dotangular.Server.Models.Auth.User", b =>
                {
                    b.HasBaseType("sports_fest_dotangular.Server.Models.Core.Entity<System.Guid>");

                    b.Property<string>("ApiKey")
                        .IsRequired()
                        .HasMaxLength(256)
                        .HasColumnType("varchar(256)");

                    b.Property<Guid?>("ClassId")
                        .HasColumnType("char(36)");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasMaxLength(256)
                        .HasColumnType("varchar(256)");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasMaxLength(256)
                        .HasColumnType("varchar(256)");

                    b.Property<int>("Role")
                        .HasColumnType("int");

                    b.HasIndex("ClassId");

                    b.ToTable("Users", (string)null);
                });

            modelBuilder.Entity("sports_fest_dotangular.Server.Models.SF.Class", b =>
                {
                    b.HasBaseType("sports_fest_dotangular.Server.Models.Core.Entity<System.Guid>");

                    b.Property<string>("Comment")
                        .IsRequired()
                        .HasMaxLength(1024)
                        .HasColumnType("varchar(1024)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(64)
                        .HasColumnType("varchar(64)");

                    b.Property<Guid?>("RoomId")
                        .HasColumnType("char(36)");

                    b.Property<string>("ShortName")
                        .IsRequired()
                        .HasMaxLength(8)
                        .HasColumnType("varchar(8)");

                    b.HasIndex("RoomId");

                    b.HasIndex("ShortName")
                        .IsUnique();

                    b.ToTable("Classes", (string)null);
                });

            modelBuilder.Entity("sports_fest_dotangular.Server.Models.SF.Discipline", b =>
                {
                    b.HasBaseType("sports_fest_dotangular.Server.Models.Core.Entity<System.Guid>");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasMaxLength(1024)
                        .HasColumnType("varchar(1024)");

                    b.Property<Guid?>("LocationId")
                        .HasColumnType("char(36)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(64)
                        .HasColumnType("varchar(64)");

                    b.Property<string>("ShortName")
                        .IsRequired()
                        .HasMaxLength(8)
                        .HasColumnType("varchar(8)");

                    b.HasIndex("LocationId");

                    b.HasIndex("ShortName")
                        .IsUnique();

                    b.ToTable("Disciplines", (string)null);
                });

            modelBuilder.Entity("sports_fest_dotangular.Server.Models.SF.Location", b =>
                {
                    b.HasBaseType("sports_fest_dotangular.Server.Models.Core.Entity<System.Guid>");

                    b.Property<string>("Comment")
                        .IsRequired()
                        .HasMaxLength(1024)
                        .HasColumnType("varchar(1024)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(64)
                        .HasColumnType("varchar(64)");

                    b.Property<string>("ShortName")
                        .IsRequired()
                        .HasMaxLength(8)
                        .HasColumnType("varchar(8)");

                    b.HasIndex("ShortName")
                        .IsUnique();

                    b.ToTable("Locations", (string)null);
                });

            modelBuilder.Entity("UserDiscipline", b =>
                {
                    b.HasOne("sports_fest_dotangular.Server.Models.SF.Discipline", null)
                        .WithMany()
                        .HasForeignKey("DisciplineId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("sports_fest_dotangular.Server.Models.Auth.User", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("sports_fest_dotangular.Server.Models.SF.Entry", b =>
                {
                    b.HasOne("sports_fest_dotangular.Server.Models.SF.Discipline", "Discipline")
                        .WithMany("Entries")
                        .HasForeignKey("DisciplineId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("sports_fest_dotangular.Server.Models.Auth.User", "Student")
                        .WithMany("Entries")
                        .HasForeignKey("StudentId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Discipline");

                    b.Navigation("Student");
                });

            modelBuilder.Entity("sports_fest_dotangular.Server.Models.Auth.User", b =>
                {
                    b.HasOne("sports_fest_dotangular.Server.Models.SF.Class", "Class")
                        .WithMany("Users")
                        .HasForeignKey("ClassId");

                    b.HasOne("sports_fest_dotangular.Server.Models.Core.Entity<System.Guid>", null)
                        .WithOne()
                        .HasForeignKey("sports_fest_dotangular.Server.Models.Auth.User", "Id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Class");
                });

            modelBuilder.Entity("sports_fest_dotangular.Server.Models.SF.Class", b =>
                {
                    b.HasOne("sports_fest_dotangular.Server.Models.Core.Entity<System.Guid>", null)
                        .WithOne()
                        .HasForeignKey("sports_fest_dotangular.Server.Models.SF.Class", "Id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("sports_fest_dotangular.Server.Models.SF.Location", "Room")
                        .WithMany("Classes")
                        .HasForeignKey("RoomId");

                    b.Navigation("Room");
                });

            modelBuilder.Entity("sports_fest_dotangular.Server.Models.SF.Discipline", b =>
                {
                    b.HasOne("sports_fest_dotangular.Server.Models.Core.Entity<System.Guid>", null)
                        .WithOne()
                        .HasForeignKey("sports_fest_dotangular.Server.Models.SF.Discipline", "Id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("sports_fest_dotangular.Server.Models.SF.Location", "Location")
                        .WithMany("Disciplines")
                        .HasForeignKey("LocationId");

                    b.Navigation("Location");
                });

            modelBuilder.Entity("sports_fest_dotangular.Server.Models.SF.Location", b =>
                {
                    b.HasOne("sports_fest_dotangular.Server.Models.Core.Entity<System.Guid>", null)
                        .WithOne()
                        .HasForeignKey("sports_fest_dotangular.Server.Models.SF.Location", "Id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("sports_fest_dotangular.Server.Models.Auth.User", b =>
                {
                    b.Navigation("Entries");
                });

            modelBuilder.Entity("sports_fest_dotangular.Server.Models.SF.Class", b =>
                {
                    b.Navigation("Users");
                });

            modelBuilder.Entity("sports_fest_dotangular.Server.Models.SF.Discipline", b =>
                {
                    b.Navigation("Entries");
                });

            modelBuilder.Entity("sports_fest_dotangular.Server.Models.SF.Location", b =>
                {
                    b.Navigation("Classes");

                    b.Navigation("Disciplines");
                });
#pragma warning restore 612, 618
        }
    }
}
