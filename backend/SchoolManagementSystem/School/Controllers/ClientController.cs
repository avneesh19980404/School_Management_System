using AutoMapper;
using School.Attibutes;
using School.Common.Constants;
using School.Service.Client;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using Response = School.Model.Response;

namespace School.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClientController : ControllerBase
    {
        private readonly IClient _clientContext;
        private readonly IMapper _mapper;

        public ClientController(IClient clientContext,IMapper mapper)
        {
            _clientContext = clientContext;
            _mapper = mapper;
        }

        // GET: api/Client
        [Authorize(Roles.Admin)]
        [HttpGet]
        public IActionResult GetClients()
        {
            try
            {
                var clients = _clientContext.GetClients();
                var responseClients = _mapper.Map<List<Response.Client>>(clients);
                return Ok(new Response.CommonResponse(true, responseClients, null));
            }
            catch (Exception ex)
            {
                return BadRequest(new Response.CommonResponse(false, null, ex.Message));
            }
        }

        // GET: api/Client/iewuryhfhsdhfiuhrfsidhfiuh
        [Authorize(Roles.Admin)]
        [HttpGet("{id}")]
        public IActionResult GetClientById(Guid id)
        {
            var client = _clientContext.GetClientById(id);
            if (client != null) {
                var responseClient = _mapper.Map<Response.Client>(client);
                return Ok(new Response.CommonResponse(true, responseClient, null));
            }
            return BadRequest(new Response.CommonResponse(false, null, "Client not found"));
        }

    }
}
