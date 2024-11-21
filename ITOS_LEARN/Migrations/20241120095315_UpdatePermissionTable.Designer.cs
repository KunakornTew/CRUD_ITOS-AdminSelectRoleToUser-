﻿// <auto-generated />
using System;
using ITOS_LEARN.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace ITOS_LEARN.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20241120095315_UpdatePermissionTable")]
    partial class UpdatePermissionTable
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.10")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("ITOS_LEARN.Models.Login", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ID"));

                    b.Property<string>("IpAddress")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<DateTime>("LoginTime")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("LogoutTime")
                        .HasColumnType("datetime2");

                    b.Property<string>("Username")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.HasKey("ID");

                    b.ToTable("Logins");
                });

            modelBuilder.Entity("ITOS_LEARN.Models.Menu", b =>
                {
                    b.Property<string>("MenuId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("MenuName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("MenuId");

                    b.ToTable("Menus");

                    b.HasData(
                        new
                        {
                            MenuId = "M001",
                            Description = "เข้าสู่หน้า Home",
                            MenuName = "Home"
                        },
                        new
                        {
                            MenuId = "M002",
                            Description = "เข้าสู่หน้า Register",
                            MenuName = "Register"
                        },
                        new
                        {
                            MenuId = "M003",
                            Description = "เข้าสู่หน้า Login",
                            MenuName = "Login"
                        },
                        new
                        {
                            MenuId = "M004",
                            Description = "เข้าสู่หน้า Waiting",
                            MenuName = "Waiting"
                        },
                        new
                        {
                            MenuId = "M005",
                            Description = "เข้าสู่หน้า Detail",
                            MenuName = "Detail"
                        },
                        new
                        {
                            MenuId = "M006",
                            Description = "เข้าสู่หน้า Admin Approval",
                            MenuName = "Admin Approval"
                        },
                        new
                        {
                            MenuId = "M007",
                            Description = "เข้าสู่หน้า Add New Person",
                            MenuName = "Add New Person"
                        },
                        new
                        {
                            MenuId = "M008",
                            Description = "เข้าสู่หน้า Edit",
                            MenuName = "Edit"
                        },
                        new
                        {
                            MenuId = "M009",
                            Description = "เข้าสู่หน้า Delete",
                            MenuName = "Delete"
                        });
                });

            modelBuilder.Entity("ITOS_LEARN.Models.MenuAction", b =>
                {
                    b.Property<string>("ActionId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ActionName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("MenuId")
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("ActionId");

                    b.HasIndex("MenuId");

                    b.ToTable("Actions");

                    b.HasData(
                        new
                        {
                            ActionId = "A001",
                            ActionName = "Search",
                            Description = "ดูข้อมูลใน DB ว่ามีการลงทะเบียนไว้แล้วหรือยัง และ ตรวจสอบข้อมูลที่ซ้ำกันใน DB"
                        },
                        new
                        {
                            ActionId = "A002",
                            ActionName = "Add",
                            Description = "เพิ่มข้อมูลการลงทะเบียน ลง DB"
                        },
                        new
                        {
                            ActionId = "A003",
                            ActionName = "Edit",
                            Description = "แก้ไขข้อมูลของ people ในระบบ"
                        },
                        new
                        {
                            ActionId = "A004",
                            ActionName = "Delete",
                            Description = "ลบข้อมูลของ people ในระบบ"
                        },
                        new
                        {
                            ActionId = "A005",
                            ActionName = "NoAction",
                            Description = "ไม่สามารถทำอะไรได้"
                        });
                });

            modelBuilder.Entity("ITOS_LEARN.Models.Permission", b =>
                {
                    b.Property<string>("PermissionId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ActionId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<bool>("IsAllow")
                        .HasColumnType("bit");

                    b.Property<string>("MenuId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("RoleId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<int?>("UserId")
                        .HasColumnType("int");

                    b.HasKey("PermissionId");

                    b.HasIndex("ActionId");

                    b.HasIndex("MenuId");

                    b.HasIndex("RoleId");

                    b.HasIndex("UserId");

                    b.ToTable("Permissions");

                    b.HasData(
                        new
                        {
                            PermissionId = "P001",
                            ActionId = "A001",
                            IsAllow = true,
                            MenuId = "M001",
                            RoleId = "R001"
                        },
                        new
                        {
                            PermissionId = "P002",
                            ActionId = "A001",
                            IsAllow = true,
                            MenuId = "M002",
                            RoleId = "R001"
                        },
                        new
                        {
                            PermissionId = "P003",
                            ActionId = "A002",
                            IsAllow = true,
                            MenuId = "M002",
                            RoleId = "R001"
                        },
                        new
                        {
                            PermissionId = "P004",
                            ActionId = "A001",
                            IsAllow = false,
                            MenuId = "M003",
                            RoleId = "R001"
                        },
                        new
                        {
                            PermissionId = "P005",
                            ActionId = "A002",
                            IsAllow = true,
                            MenuId = "M003",
                            RoleId = "R001"
                        },
                        new
                        {
                            PermissionId = "P006",
                            ActionId = "A001",
                            IsAllow = true,
                            MenuId = "M005",
                            RoleId = "R001"
                        },
                        new
                        {
                            PermissionId = "P007",
                            ActionId = "A002",
                            IsAllow = true,
                            MenuId = "M005",
                            RoleId = "R001"
                        },
                        new
                        {
                            PermissionId = "P008",
                            ActionId = "A003",
                            IsAllow = true,
                            MenuId = "M005",
                            RoleId = "R001"
                        },
                        new
                        {
                            PermissionId = "P009",
                            ActionId = "A004",
                            IsAllow = true,
                            MenuId = "M005",
                            RoleId = "R001"
                        },
                        new
                        {
                            PermissionId = "P010",
                            ActionId = "A002",
                            IsAllow = true,
                            MenuId = "M006",
                            RoleId = "R001"
                        },
                        new
                        {
                            PermissionId = "P011",
                            ActionId = "A002",
                            IsAllow = true,
                            MenuId = "M007",
                            RoleId = "R001"
                        },
                        new
                        {
                            PermissionId = "P012",
                            ActionId = "A003",
                            IsAllow = true,
                            MenuId = "M008",
                            RoleId = "R001"
                        },
                        new
                        {
                            PermissionId = "P013",
                            ActionId = "A004",
                            IsAllow = true,
                            MenuId = "M009",
                            RoleId = "R001"
                        },
                        new
                        {
                            PermissionId = "P014",
                            ActionId = "A005",
                            IsAllow = false,
                            MenuId = "M005",
                            RoleId = "R002"
                        },
                        new
                        {
                            PermissionId = "P015",
                            ActionId = "A002",
                            IsAllow = true,
                            MenuId = "M007",
                            RoleId = "R002"
                        },
                        new
                        {
                            PermissionId = "P016",
                            ActionId = "A003",
                            IsAllow = true,
                            MenuId = "M008",
                            RoleId = "R003"
                        },
                        new
                        {
                            PermissionId = "P017",
                            ActionId = "A004",
                            IsAllow = true,
                            MenuId = "M009",
                            RoleId = "R003"
                        },
                        new
                        {
                            PermissionId = "P018",
                            ActionId = "A005",
                            IsAllow = true,
                            MenuId = "M001",
                            RoleId = "R004"
                        },
                        new
                        {
                            PermissionId = "P019",
                            ActionId = "A005",
                            IsAllow = true,
                            MenuId = "M004",
                            RoleId = "R004"
                        });
                });

            modelBuilder.Entity("ITOS_LEARN.Models.Person", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ID"));

                    b.Property<DateTime>("BirthDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasMaxLength(70)
                        .HasColumnType("nvarchar(70)");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasMaxLength(30)
                        .HasColumnType("nvarchar(30)");

                    b.Property<string>("JobPosition")
                        .IsRequired()
                        .HasMaxLength(30)
                        .HasColumnType("nvarchar(30)");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasMaxLength(30)
                        .HasColumnType("nvarchar(30)");

                    b.Property<string>("PhoneNumber")
                        .IsRequired()
                        .HasMaxLength(15)
                        .HasColumnType("nvarchar(15)");

                    b.HasKey("ID");

                    b.ToTable("People");
                });

            modelBuilder.Entity("ITOS_LEARN.Models.Role", b =>
                {
                    b.Property<string>("RoleId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("RoleName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("RoleId");

                    b.ToTable("Roles");

                    b.HasData(
                        new
                        {
                            RoleId = "R001",
                            RoleName = "Admin"
                        },
                        new
                        {
                            RoleId = "R002",
                            RoleName = "Creater"
                        },
                        new
                        {
                            RoleId = "R003",
                            RoleName = "Viewer"
                        },
                        new
                        {
                            RoleId = "R004",
                            RoleName = "User"
                        });
                });

            modelBuilder.Entity("ITOS_LEARN.Models.User", b =>
                {
                    b.Property<int>("User_ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("User_ID"));

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<bool>("IsConfirmed")
                        .HasColumnType("bit");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("Role")
                        .IsRequired()
                        .HasMaxLength(10)
                        .HasColumnType("nvarchar(10)");

                    b.Property<string>("Username")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.HasKey("User_ID");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("ITOS_LEARN.Models.MenuAction", b =>
                {
                    b.HasOne("ITOS_LEARN.Models.Menu", "Menu")
                        .WithMany()
                        .HasForeignKey("MenuId");

                    b.Navigation("Menu");
                });

            modelBuilder.Entity("ITOS_LEARN.Models.Permission", b =>
                {
                    b.HasOne("ITOS_LEARN.Models.MenuAction", "Action")
                        .WithMany("Permissions")
                        .HasForeignKey("ActionId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("ITOS_LEARN.Models.Menu", "Menu")
                        .WithMany("Permissions")
                        .HasForeignKey("MenuId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("ITOS_LEARN.Models.Role", "Role")
                        .WithMany("Permissions")
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("ITOS_LEARN.Models.User", "User")
                        .WithMany("Permissions")
                        .HasForeignKey("UserId");

                    b.Navigation("Action");

                    b.Navigation("Menu");

                    b.Navigation("Role");

                    b.Navigation("User");
                });

            modelBuilder.Entity("ITOS_LEARN.Models.Menu", b =>
                {
                    b.Navigation("Permissions");
                });

            modelBuilder.Entity("ITOS_LEARN.Models.MenuAction", b =>
                {
                    b.Navigation("Permissions");
                });

            modelBuilder.Entity("ITOS_LEARN.Models.Role", b =>
                {
                    b.Navigation("Permissions");
                });

            modelBuilder.Entity("ITOS_LEARN.Models.User", b =>
                {
                    b.Navigation("Permissions");
                });
#pragma warning restore 612, 618
        }
    }
}
