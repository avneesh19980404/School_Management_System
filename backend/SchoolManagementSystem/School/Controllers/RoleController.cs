using AutoMapper;
using School.Attibutes;
using School.Common.Constants;
using School.Model.Response;
using School.Service.Repository;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using Entity = School.Model.Entity;
using Response = School.Model.Response;
// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace School.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RoleController : ControllerBase
    {
        private readonly IRepository<Entity.Role> _roleContext;
        private readonly IMapper _mapper;

        public RoleController(IRepository<Entity.Role> roleContext,IMapper mapper)
        {
            _roleContext = roleContext;
            _mapper = mapper;
        }
       
        // GET: api/<RoleController>
        [Authorize(Roles.Admin)]
        [HttpGet]
        public IActionResult Get()
        {
            var roles = _roleContext.GetAll().Where(r=>!r.Name.Equals(Roles.Admin));
            var responseRoles = _mapper.Map<List<Role>>(roles);
            return Ok(new Response.CommonResponse(true,responseRoles,null));
        }

        // GET api/<RoleController>/5
        [Authorize(Roles.Admin)]
        [HttpGet("{id}")]
        public IActionResult Get(Guid id)
        {
            var role = _roleContext.GetById(id);
            var responseRole = _mapper.Map<Role>(role);
            return Ok(new Response.CommonResponse(true,responseRole, null)); 
        }
    }
}
