using ITOS_LEARN.Models;
using Microsoft.EntityFrameworkCore;

namespace ITOS_LEARN.Data
{
    public static class ModelBuilderExtensions
    {
        public static void SeedRoles(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Role>().HasData(
                new Role { RoleId = "R001", RoleName = "Admin" },
                new Role { RoleId = "R002", RoleName = "Creater" },
                new Role { RoleId = "R003", RoleName = "Editer" },
                new Role { RoleId = "R004", RoleName = "User" }
            );
        }

        public static void SeedMenus(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Menu>().HasData(
                new Menu { MenuId = "M001", MenuName = "Home", Description = "เข้าสู่หน้า Home" },
                new Menu { MenuId = "M002", MenuName = "Register", Description = "เข้าสู่หน้า Register" },
                new Menu { MenuId = "M003", MenuName = "Login", Description = "เข้าสู่หน้า Login" },
                new Menu { MenuId = "M004", MenuName = "Waiting", Description = "เข้าสู่หน้า Waiting" },
                new Menu { MenuId = "M005", MenuName = "Detail", Description = "เข้าสู่หน้า Detail" },
                new Menu { MenuId = "M006", MenuName = "Admin Approval", Description = "เข้าสู่หน้า Admin Approval" },
                new Menu { MenuId = "M007", MenuName = "Add New Person", Description = "เข้าสู่หน้า Add New Person" },
                new Menu { MenuId = "M008", MenuName = "Edit", Description = "เข้าสู่หน้า Edit" },
                new Menu { MenuId = "M009", MenuName = "Delete", Description = "เข้าสู่หน้า Delete" }
            );
        }

        public static void SeedMenuActions(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<MenuAction>().HasData(
                new MenuAction { ActionId = "A001", ActionName = "Search", Description = "ดูข้อมูลใน DB ว่ามีการลงทะเบียนไว้แล้วหรือยัง และ ตรวจสอบข้อมูลที่ซ้ำกันใน DB" },
                new MenuAction { ActionId = "A002", ActionName = "Add", Description = "เพิ่มข้อมูลการลงทะเบียน ลง DB" },
                new MenuAction { ActionId = "A003", ActionName = "Edit", Description = "แก้ไขข้อมูลของ people ในระบบ" },
                new MenuAction { ActionId = "A004", ActionName = "Delete", Description = "ลบข้อมูลของ people ในระบบ" },
                new MenuAction { ActionId = "A005", ActionName = "NoAction", Description = "ไม่สามารถทำอะไรได้" }
            );
        }

        public static void SeedPermissions(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Permission>().HasData(
            new Permission { PermissionId = "P001", UserId = null, RoleId = "R001", MenuId = "M001", ActionId = "A001", IsAllow = true },
            new Permission { PermissionId = "P002", UserId = null, RoleId = "R001", MenuId = "M002", ActionId = "A001", IsAllow = true },
            new Permission { PermissionId = "P003", UserId = null, RoleId = "R001", MenuId = "M002", ActionId = "A002", IsAllow = true },
            new Permission { PermissionId = "P004", UserId = null, RoleId = "R001", MenuId = "M003", ActionId = "A001", IsAllow = false },
            new Permission { PermissionId = "P005", UserId = null, RoleId = "R001", MenuId = "M003", ActionId = "A002", IsAllow = true },
            new Permission { PermissionId = "P006", UserId = null, RoleId = "R001", MenuId = "M005", ActionId = "A001", IsAllow = true },
            new Permission { PermissionId = "P007", UserId = null, RoleId = "R001", MenuId = "M005", ActionId = "A002", IsAllow = true },
            new Permission { PermissionId = "P008", UserId = null, RoleId = "R001", MenuId = "M005", ActionId = "A003", IsAllow = true },
            new Permission { PermissionId = "P009", UserId = null, RoleId = "R001", MenuId = "M005", ActionId = "A004", IsAllow = true },
            new Permission { PermissionId = "P010", UserId = null, RoleId = "R001", MenuId = "M006", ActionId = "A002", IsAllow = true },
            new Permission { PermissionId = "P011", UserId = null, RoleId = "R001", MenuId = "M007", ActionId = "A002", IsAllow = true },
            new Permission { PermissionId = "P012", UserId = null, RoleId = "R001", MenuId = "M008", ActionId = "A003", IsAllow = true },
            new Permission { PermissionId = "P013", UserId = null, RoleId = "R001", MenuId = "M009", ActionId = "A004", IsAllow = true },

            // Creater - สามารถเข้าถึงหน้า Detail และ Add New Person ได้
            new Permission { PermissionId = "P014", UserId = null, RoleId = "R002", MenuId = "M005", ActionId = "A005", IsAllow = false }, // Detail - ไม่มีสิทธิ์ทำ Action
            new Permission { PermissionId = "P015", UserId = null, RoleId = "R002", MenuId = "M007", ActionId = "A002", IsAllow = true }, // Add New Person

            // Editor - สามารถทำ Action Edit และ Delete ในหน้า Edit และ Delete
            new Permission { PermissionId = "P016", UserId = null, RoleId = "R003", MenuId = "M008", ActionId = "A003", IsAllow = true }, // Edit
            new Permission { PermissionId = "P017", UserId = null, RoleId = "R003", MenuId = "M009", ActionId = "A004", IsAllow = true }, // Delete

            // User - สามารถเข้าถึงหน้า Home และ Waiting โดยไม่มีสิทธิ์ทำ Action
            new Permission { PermissionId = "P018", UserId = null, RoleId = "R004", MenuId = "M001", ActionId = "A005", IsAllow = true }, // Home - ไม่มีสิทธิ์ทำ Action
            new Permission { PermissionId = "P019", UserId = null, RoleId = "R004", MenuId = "M004", ActionId = "A005", IsAllow = true } // Waiting - ไม่มีสิทธิ์ทำ Action
            );
        }
    }
}
