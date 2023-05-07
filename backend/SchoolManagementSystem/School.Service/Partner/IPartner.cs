using System;
using System.Collections.Generic;
using System.Text;

namespace School.Service.Partner
{
    public interface IPartner
    {
        IEnumerable<Model.Entity.User> GetPartners();

        Model.Entity.User GetPartnerById(Guid id);
    }
}
