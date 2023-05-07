using System;
using System.Collections.Generic;
using System.Text;

namespace School.Model.Response
{
    public class User
    {
        public string Email { get; set; }
        public Guid Id { get; set; }
        public Entity.Role Role { get; set; }
        public string Token { get; set; }
    }
}
