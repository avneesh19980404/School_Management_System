using School.Common.Configurations;
using School.Core.Helper;
using School.Model.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Text;
using System.Threading.Tasks;
using Constants = School.Common.Constants;

namespace School.Core.Data
{
    public class AppDbContext : DbContext
    {
        private readonly AppSettings _settings;

        public AppDbContext(DbContextOptions<AppDbContext> options,IOptions<AppSettings> settings) : base(options) {
            _settings = settings.Value;
        }
        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            Guid adminRoleId = Guid.NewGuid();
            Guid clientRoleId = Guid.NewGuid();
            Guid partnerRoleId = Guid.NewGuid();
            modelBuilder.Entity<Role>().HasData(
                new Role
            {
                Id = adminRoleId,
                Name = Constants.Roles.Admin,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow,
            }, 
                new Role
            {
                Id = partnerRoleId,
                Name = Constants.Roles.Partner,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow,
            },
                new Role
                {
                    Id = clientRoleId,
                    Name = Constants.Roles.Client,
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow,
                }
                );
            Guid adminId = Guid.NewGuid();
            modelBuilder.Entity<User>().HasData(new User
            {
                Id = adminId,
                Username = "Admin",
                FirstName = "",
                LastName = "",
                Email = "admin@gmail.com",
                RoleId = adminRoleId,
                Password = HashPassword.EncryptPassword("123456789", Encoding.UTF32.GetBytes(_settings.Salt)).Hash,
                IsActive = true,
                CreatedAt = DateTime.UtcNow,
                IsDeleted = false,
                UpdatedAt = DateTime.UtcNow
            });
        }
    }
}
