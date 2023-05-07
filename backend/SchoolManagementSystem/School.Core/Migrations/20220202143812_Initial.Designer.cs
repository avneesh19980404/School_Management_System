﻿// <auto-generated />
using System;
using School.Core.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace School.Core.Migrations
{
    [DbContext(typeof(AppDbContext))]
    [Migration("20220202143812_Initial")]
    partial class Initial
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("School.Model.Entity.Role", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("UpdatedAt")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.ToTable("Roles");

                    b.HasData(
                        new
                        {
                            Id = new Guid("71b7cd13-4f32-413a-b8c8-75d3b8aaf103"),
                            CreatedAt = new DateTime(2022, 2, 2, 14, 38, 11, 889, DateTimeKind.Utc).AddTicks(1855),
                            Name = "Admin",
                            UpdatedAt = new DateTime(2022, 2, 2, 14, 38, 11, 889, DateTimeKind.Utc).AddTicks(2615)
                        },
                        new
                        {
                            Id = new Guid("8c4039ff-6fcd-467d-98ac-36f47078d872"),
                            CreatedAt = new DateTime(2022, 2, 2, 14, 38, 11, 889, DateTimeKind.Utc).AddTicks(3317),
                            Name = "Partner",
                            UpdatedAt = new DateTime(2022, 2, 2, 14, 38, 11, 889, DateTimeKind.Utc).AddTicks(3338)
                        },
                        new
                        {
                            Id = new Guid("9f643888-a178-4ce0-b3e7-2b1a68abdfcd"),
                            CreatedAt = new DateTime(2022, 2, 2, 14, 38, 11, 889, DateTimeKind.Utc).AddTicks(3362),
                            Name = "Client",
                            UpdatedAt = new DateTime(2022, 2, 2, 14, 38, 11, 889, DateTimeKind.Utc).AddTicks(3417)
                        });
                });

            modelBuilder.Entity("School.Model.Entity.User", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("FirstName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("IsActive")
                        .HasColumnType("bit");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("bit");

                    b.Property<string>("LastName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Password")
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid>("RoleId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("UpdatedAt")
                        .HasColumnType("datetime2");

                    b.Property<string>("Username")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("Users");

                    b.HasData(
                        new
                        {
                            Id = new Guid("5ba72018-f382-41a2-aaa3-fc48d8a854f3"),
                            CreatedAt = new DateTime(2022, 2, 2, 14, 38, 11, 917, DateTimeKind.Utc).AddTicks(6530),
                            Email = "admin@gmail.com",
                            FirstName = "",
                            IsActive = true,
                            IsDeleted = false,
                            LastName = "",
                            Password = "BJJcVkTHd9Qkv8iCM8srzsyU50CjkRe4ckHbWkHVlAc=",
                            RoleId = new Guid("71b7cd13-4f32-413a-b8c8-75d3b8aaf103"),
                            UpdatedAt = new DateTime(2022, 2, 2, 14, 38, 11, 917, DateTimeKind.Utc).AddTicks(6576),
                            Username = "Admin"
                        });
                });

            modelBuilder.Entity("School.Model.Entity.User", b =>
                {
                    b.HasOne("School.Model.Entity.Role", "Role")
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
