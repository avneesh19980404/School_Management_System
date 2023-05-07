using Request = School.Model.Request;
using Response = School.Model.Response;
using Entity = School.Model.Entity;

namespace School.Service.User
{
    public interface IAccount
    {
        Response.User Login(string email, string password);
        (bool,string) Register(Model.Entity.User user);

        public bool ResetPassword(Request.RequestPassword password,Entity.User user);

    }
}
