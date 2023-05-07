using School.Attibutes;
using School.Model.Response;
using School.Service.User;
using Microsoft.AspNetCore.Mvc;
using System;
using Entity = School.Model.Entity;
using Request = School.Model.Request;
using Roles = School.Common.Constants.Roles;
namespace School.Controllers
{
    [ApiController]
    [Route("api/[controller]")]             
    public class UserController : ControllerBase
    {
        private readonly IAccount _account;

        public UserController(IAccount account)
        {
            _account = account;
        }
        /// <summary>
        /// User Login Here
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        [HttpPost("login")]
        public IActionResult Post(Request.User user)
        {
            try
            {
                var result = _account.Login(user.Email, user.Password);
                if (result != null)
                {
                    return Ok(new CommonResponse(true,result,null));
                }
                return BadRequest(new CommonResponse(false, null, "email or password is incorrect !!"));
            }
            catch (Exception ex)
            {
          
                return BadRequest(new CommonResponse(false,null,"Something went wrong !!"));
            }

        }
        /// <summary>
        /// User Register Here
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        [Authorize(Roles.Admin)]
        [HttpPost("register")]
    
        public IActionResult Register(Entity.User user)
        {
            var result = _account.Register(user);
            if (result.Item1)
            {
                return Ok(new CommonResponse(result.Item1,result.Item2,null));
            }
            return BadRequest(new CommonResponse(result.Item1, null,result.Item2));
        }
        /// <summary>
        /// Reset Password
        /// </summary>
        /// <param name="password"></param>
        /// <returns></returns>
        [HttpPost("reset-password")]
        public IActionResult ResetPassword(Request.RequestPassword password)
        {
            if (!string.IsNullOrEmpty(password.OldPassword) && !password.IsPasswordEqual)
            {
                return BadRequest(new CommonResponse(false, null, "Password should be equal"));

            }
            var user = (Entity.User)HttpContext.Items["User"];
                var isUpdated = _account.ResetPassword(password, user);
                if (isUpdated)
                {
                    return Ok(new CommonResponse(true, "Password reset successfully", null));
                }
                return BadRequest(new CommonResponse(false, null, "Unable to reset the password !"));
           
        }
        /// <summary>
        /// verification of the token
        /// </summary>
        /// <returns></returns>
        [HttpPost("/api/verify")]
        [Authorize]
        public IActionResult Verify() {
            return Ok(new CommonResponse(true,"verified",null));
        }
    }
}
