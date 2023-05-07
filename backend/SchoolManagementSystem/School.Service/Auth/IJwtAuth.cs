using System;
using System.Collections.Generic;
using System.Text;

namespace School.Service.Auth
{
    public interface IJwtAuth
    {
        string GenerateToken(string userId);
    }
}
