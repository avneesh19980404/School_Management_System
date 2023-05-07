using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace School.Model.Entity
{
    public class User : BaseEntity
    {
        public User()
        {
            Id = Guid.NewGuid();
            IsActive = true;
            IsDeleted = false;
            CreatedAt = DateTime.UtcNow;
            UpdatedAt = DateTime.UtcNow;
        }
        public string Username { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }

        [NotMapped]
        public string FullName => string.Format("{0} {1}", FirstName, LastName);
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        public Role Role { get; set; }

        [Required]
        [ForeignKey("Role")]
        public Guid RoleId { get; set; }      
        public string Password { get; set; }
        public bool IsActive { get; set; }
        public bool IsDeleted { get; set; }
    }
}
