using School.Model.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace School.Model.Response
{
    public class Partner : BaseEntity
    {
        public string FullName { get; set; }
        public string Email { get; set; }
    }
}
