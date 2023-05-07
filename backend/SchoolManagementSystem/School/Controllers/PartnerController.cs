using AutoMapper;
using School.Attibutes;
using School.Common.Constants;
using School.Service.Partner;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using Response = School.Model.Response;

namespace School.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PartnerController : ControllerBase
    {
        private readonly IPartner _partnerContext;
        private readonly IMapper _mapper;

        public PartnerController(IPartner partnerContext, IMapper mapper)
        {
            _partnerContext = partnerContext;
            _mapper = mapper;
        }

        // GET: api/partner
        [Authorize(Roles.Admin)]
        [HttpGet]
        public IActionResult GetPartners()
        {
            try
            {
                var partners = _partnerContext.GetPartners();
                var responsePartners = _mapper.Map<List<Response.Partner>>(partners);
                return Ok(new Response.CommonResponse(true, responsePartners, null));
            }
            catch (Exception ex)
            {
                return BadRequest(new Response.CommonResponse(false, null, ex.Message));
            }
        }

        // GET: api/partner/iewuryhfhsdhfiuhrfsidhfiuh
        [Authorize(Roles.Admin)]
        [HttpGet("{id}")]
        public IActionResult GetPartnerById(Guid id)
        {
            var partner = _partnerContext.GetPartnerById(id);
            if (partner != null)
            {
                var responsePartner = _mapper.Map<Response.Partner>(partner);
                return Ok(new Response.CommonResponse(true, responsePartner, null));
            }
            return BadRequest(new Response.CommonResponse(false, null, "Partner not found"));
        }

    }
}
