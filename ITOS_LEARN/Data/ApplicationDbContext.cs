using Microsoft.EntityFrameworkCore;
using ITOS_LEARN.Models;

namespace ITOS_LEARN.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        public DbSet<Role> Roles { get; set; }
        public DbSet<Menu> Menus { get; set; }
        public DbSet<MenuAction> Actions { get; set; }
        public DbSet<Permission> Permissions { get; set; }
        public DbSet<Person> People { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Login> Logins { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            base.OnModelCreating(modelBuilder);

            // เรียกใช้ Seed Data
            modelBuilder.SeedRoles();
            modelBuilder.SeedMenus();
            modelBuilder.SeedMenuActions();
            modelBuilder.SeedPermissions();

            // กำหนดความสัมพันธ์ระหว่างตาราง
            modelBuilder.Entity<Permission>()
                .Property(p => p.UserId)
                .IsRequired(false);

            modelBuilder.Entity<Permission>()
                .HasOne(p => p.Role)
                .WithMany(r => r.Permissions)
                .HasForeignKey(p => p.RoleId);

            modelBuilder.Entity<Permission>()
                .HasOne(p => p.Menu)
                .WithMany(m => m.Permissions)
                .HasForeignKey(p => p.MenuId);

            modelBuilder.Entity<Permission>()
                .HasOne(p => p.Action)
                .WithMany(a => a.Permissions)
                .HasForeignKey(p => p.ActionId);

            modelBuilder.Entity<Login>()
                .Property(l => l.ID)
                .ValueGeneratedOnAdd(); // ให้ฐานข้อมูลจัดการการสร้าง ID

            modelBuilder.Entity<User>()
                .Property(u => u.User_ID)
                .ValueGeneratedOnAdd(); // ให้ฐานข้อมูลจัดการการสร้าง User_ID

            modelBuilder.Entity<Role>()
                .Property(r => r.RoleId)
                .ValueGeneratedOnAdd(); // ให้ฐานข้อมูลจัดการการสร้าง RoleId

            modelBuilder.Entity<Menu>()
                .Property(m => m.MenuId)
                .ValueGeneratedOnAdd(); // ให้ฐานข้อมูลจัดการการสร้าง MenuId

            modelBuilder.Entity<MenuAction>()
                .Property(a => a.ActionId)
                .ValueGeneratedOnAdd(); // ให้ฐานข้อมูลจัดการการสร้าง ActionId

            modelBuilder.Entity<Permission>()
                .Property(p => p.PermissionId)
                .ValueGeneratedOnAdd(); // ให้ฐานข้อมูลจัดการการสร้าง PermissionId
        }
    }
}