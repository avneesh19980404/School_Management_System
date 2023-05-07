using System;
using System.Collections.Generic;
using System.Text;

namespace School.Service.Client
{
    public interface IClient
    {
        IEnumerable<Model.Entity.User> GetClients();

        Model.Entity.User GetClientById(Guid id);
    }
}
