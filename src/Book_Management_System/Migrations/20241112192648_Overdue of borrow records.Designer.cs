﻿// <auto-generated />
using System;
using Domain.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Book_Management_System.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20241112192648_Overdue of borrow records")]
    partial class Overdueofborrowrecords
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.10")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("Domain.Entities.Book", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Author")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("Borrowed")
                        .HasColumnType("bit");

                    b.Property<string>("Language")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Books");
                });

            modelBuilder.Entity("Domain.Entities.BorrowRecord", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int?>("BookId")
                        .HasColumnType("int");

                    b.Property<DateTime>("EndDate")
                        .HasColumnType("datetime2");

                    b.Property<bool>("IsOverdue")
                        .HasColumnType("bit");

                    b.Property<int?>("MemberId")
                        .HasColumnType("int");

                    b.Property<DateTime>("StartDate")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.HasIndex("BookId")
                        .IsUnique()
                        .HasFilter("[BookId] IS NOT NULL");

                    b.HasIndex("MemberId");

                    b.ToTable("BorrowRecords");
                });

            modelBuilder.Entity("Domain.Entities.Member", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Phone")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Members");
                });

            modelBuilder.Entity("Domain.Entities.BorrowRecord", b =>
                {
                    b.HasOne("Domain.Entities.Book", "Book")
                        .WithOne("BorrowRecord")
                        .HasForeignKey("Domain.Entities.BorrowRecord", "BookId");

                    b.HasOne("Domain.Entities.Member", "Member")
                        .WithMany("BorrowRecords")
                        .HasForeignKey("MemberId");

                    b.Navigation("Book");

                    b.Navigation("Member");
                });

            modelBuilder.Entity("Domain.Entities.Book", b =>
                {
                    b.Navigation("BorrowRecord");
                });

            modelBuilder.Entity("Domain.Entities.Member", b =>
                {
                    b.Navigation("BorrowRecords");
                });
#pragma warning restore 612, 618
        }
    }
}
