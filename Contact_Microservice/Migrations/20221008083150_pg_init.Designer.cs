﻿// <auto-generated />
using System;
using Contact_Microservice.DataAccess.DbContexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Contact_Microservice.Migrations
{
    [DbContext(typeof(PgDbContext))]
    [Migration("20221008083150_pg_init")]
    partial class pg_init
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.9")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("Contact_Microservice.Entities.Contact", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<int>("ContactType")
                        .HasColumnType("integer");

                    b.Property<string>("Contents")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<Guid>("PersonUID")
                        .HasColumnType("uuid");

                    b.HasKey("Id");

                    b.HasIndex("PersonUID");

                    b.ToTable("Contacts");

                    b.HasData(
                        new
                        {
                            Id = new Guid("747c94fa-61c4-4524-9b7e-3e180b44f961"),
                            ContactType = 1,
                            Contents = "12312312312",
                            PersonUID = new Guid("dc661f81-ddcb-473f-9658-bdac1db4b6fb")
                        });
                });

            modelBuilder.Entity("Contact_Microservice.Entities.Person", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("Company")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("SurName")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("Persons");

                    b.HasData(
                        new
                        {
                            Id = new Guid("dc661f81-ddcb-473f-9658-bdac1db4b6fb"),
                            Company = "gozili a.ş1",
                            Name = "süleyman",
                            SurName = "güzel"
                        });
                });

            modelBuilder.Entity("Contact_Microservice.Entities.Contact", b =>
                {
                    b.HasOne("Contact_Microservice.Entities.Person", "Person")
                        .WithMany("Contacts")
                        .HasForeignKey("PersonUID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Person");
                });

            modelBuilder.Entity("Contact_Microservice.Entities.Person", b =>
                {
                    b.Navigation("Contacts");
                });
#pragma warning restore 612, 618
        }
    }
}
