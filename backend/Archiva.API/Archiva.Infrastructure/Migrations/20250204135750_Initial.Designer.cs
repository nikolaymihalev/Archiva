﻿// <auto-generated />
using System;
using Archiva.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Archiva.Infrastructure.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20250204135750_Initial")]
    partial class Initial
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "9.0.1")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("Archiva.Infrastructure.Models.Document", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasComment("Document identifier");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)")
                        .HasComment("Document description");

                    b.Property<DateTime>("EndDate")
                        .HasColumnType("datetime2")
                        .HasComment("Document end date");

                    b.Property<byte[]>("Image")
                        .IsRequired()
                        .HasColumnType("varbinary(max)")
                        .HasComment("Document image");

                    b.Property<DateTime>("IssueDate")
                        .HasColumnType("datetime2")
                        .HasComment("Document issue date");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)")
                        .HasComment("Document name");

                    b.HasKey("Id");

                    b.ToTable("Documents", t =>
                        {
                            t.HasComment("Represents the Document");
                        });
                });

            modelBuilder.Entity("Archiva.Infrastructure.Models.User", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)")
                        .HasComment("User identifier");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)")
                        .HasComment("User email");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)")
                        .HasComment("User first name");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)")
                        .HasComment("User last name");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)")
                        .HasComment("User password");

                    b.HasKey("Id");

                    b.ToTable("Users", t =>
                        {
                            t.HasComment("Represents the User");
                        });
                });

            modelBuilder.Entity("Archiva.Infrastructure.Models.UserDocument", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("nvarchar(450)")
                        .HasComment("User identifier");

                    b.Property<int>("DocumentId")
                        .HasColumnType("int")
                        .HasComment("Document identifier");

                    b.HasKey("UserId", "DocumentId");

                    b.HasIndex("DocumentId");

                    b.ToTable("UserDocuments", t =>
                        {
                            t.HasComment("Represents mapping between User and Document");
                        });
                });

            modelBuilder.Entity("Archiva.Infrastructure.Models.UserDocument", b =>
                {
                    b.HasOne("Archiva.Infrastructure.Models.Document", "Document")
                        .WithMany("UserDocuments")
                        .HasForeignKey("DocumentId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Archiva.Infrastructure.Models.User", "User")
                        .WithMany("UserDocuments")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Document");

                    b.Navigation("User");
                });

            modelBuilder.Entity("Archiva.Infrastructure.Models.Document", b =>
                {
                    b.Navigation("UserDocuments");
                });

            modelBuilder.Entity("Archiva.Infrastructure.Models.User", b =>
                {
                    b.Navigation("UserDocuments");
                });
#pragma warning restore 612, 618
        }
    }
}
