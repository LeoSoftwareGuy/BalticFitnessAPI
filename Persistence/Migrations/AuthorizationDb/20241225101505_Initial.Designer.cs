﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using Persistence.SqlDataBase.AuthorizationDB;

#nullable disable

namespace Persistence.Migrations.AuthorizationDb
{
    [DbContext(typeof(AuthorizationDbContext))]
    [Migration("20241225101505_Initial")]
    partial class Initial
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.11")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("Domain.Authentication.AppUser", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uuid")
                        .HasColumnName("id");

                    b.Property<int>("Age")
                        .HasColumnType("integer")
                        .HasColumnName("age");

                    b.Property<string>("EmailAddress")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("character varying(50)")
                        .HasColumnName("emailaddress");

                    b.Property<string>("Gender")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("gender");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("character varying(50)")
                        .HasColumnName("name");

                    b.Property<string>("Nationality")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("nationality");

                    b.Property<string>("PasswordHashed")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("passwordhashed");

                    b.Property<string>("Surname")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("surname");

                    b.HasKey("Id");

                    b.ToTable("appusers", (string)null);
                });

            modelBuilder.Entity("Domain.Authentication.RefreshTokens", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("id");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<Guid>("AppUserId")
                        .HasColumnType("uuid")
                        .HasColumnName("appuserid");

                    b.Property<DateTime>("ExpirationDate")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("expirationdate");

                    b.Property<string>("Token")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("token");

                    b.HasKey("Id");

                    b.HasIndex("AppUserId");

                    b.ToTable("appusertokens");
                });

            modelBuilder.Entity("Domain.Authentication.RefreshTokens", b =>
                {
                    b.HasOne("Domain.Authentication.AppUser", "User")
                        .WithMany()
                        .HasForeignKey("AppUserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });
#pragma warning restore 612, 618
        }
    }
}
