using AutoMapper;
using Entity = School.Model.Entity;
using Response = School.Model.Response;

namespace School.Automapper
{
    public class ObjectMapper : Profile
    {
        public ObjectMapper()
        {

            CreateMap<Entity.User, Response.User>();
            CreateMap<Entity.User, Response.Client>();
            CreateMap<Entity.User, Response.Partner>();
            CreateMap<Entity.Role, Response.Role>();
        }
    }
}
