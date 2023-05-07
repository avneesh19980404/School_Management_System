using System;
using System.Collections.Generic;
using System.Text;

namespace School.Model.Response
{
    public class LoggedInUser
    {
        public Guid Id { get; set; }
        public string Email { get; set; }
        public string RoleId { get; set; }
        public string RoleName { get; set; }

        public string Token { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}
