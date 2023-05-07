using School.Common.Constants;
using School.Core.Helper;
using School.Service.Repository;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace School.Service.Partner
{
    public class Partner : IPartner
    {
        private readonly IRepository<Model.Entity.User> _userContext;
        private readonly IRepository<Model.Entity.Role> _roleContext;

        public Partner(IRepository<Model.Entity.User> userContext,IRepository<Model.Entity.Role> roleContext)
        {
            _userContext = userContext;
            _roleContext = roleContext;
        }
        public Model.Entity.User GetPartnerById(Guid id)
        {
            try
            {
                Guard.NotNull(id, nameof(id));
                var partner = _userContext.GetById(id);
                Guard.NotNull(partner, nameof(partner));
                var role = _roleContext.GetById(partner.RoleId);
                Guard.NotNull(role, nameof(role));
                if (role.Name.Equals(Roles.Partner))
                {
                    partner.Role = role;
                    return partner;
                }
                return null;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public IEnumerable<Model.Entity.User> GetPartners()
        {
            var partners = _userContext.GetAll().AsQueryable().Include(p => p.Role).Where(e => e.Role.Name.Equals(Roles.Partner));
            return partners;
        }
    }
}
