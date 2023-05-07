using School.Common.Constants;
using School.Core.Helper;
using School.Service.Repository;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Entity = School.Model.Entity;

namespace School.Service.Client
{
    public class Client : IClient
    {
        private readonly IRepository<Entity.User> _userContext;
        private readonly IRepository<Entity.Role> _roleContext;

        public Client(IRepository<Entity.User> userContext,IRepository<Entity.Role> roleContext)
        {
            _userContext = userContext;
            _roleContext = roleContext;
        }

        public Entity.User GetClientById(Guid id)
        {
            try
            {
                Guard.NotNull(id, nameof(id));
                var client = _userContext.GetById(id);
                Guard.NotNull(client, nameof(client));
                var role = _roleContext.GetById(client.RoleId);
                Guard.NotNull(role, nameof(role));
                if (role.Name.Equals(Roles.Client))
                {
                    client.Role = role;
                    return client;
                }
                return null;
            }
            catch (Exception ex) {
                return null;
            }
        }

        public IEnumerable<Model.Entity.User> GetClients()
        {
            var clients = _userContext.GetAll().AsQueryable().Include(c => c.Role).Where(e => e.Role.Name.Equals(Roles.Client));
           return clients;
        }
    }
}
