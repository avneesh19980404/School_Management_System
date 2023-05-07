using School.Common.Configurations;
using School.Common.Constants;
using School.Core.Helper;
using School.Model.Request;
using School.Service.Auth;
using School.Service.Mail;
using School.Service.Repository;
using Microsoft.Extensions.Options;
using System;
using System.Linq;
using System.Text;
using Entity = School.Model.Entity;
using Response = School.Model.Response;
using Request = School.Model.Request;
using Microsoft.EntityFrameworkCore;
using School.Model.Response;

namespace School.Service.User
{
    public class Account : IAccount
    {
        private readonly IRepository<Entity.User> _context;

        private readonly AppSettings _setting;
        private readonly IMailService _mailService;
        private readonly IJwtAuth _jwtAuth;

        public Account(IRepository<Entity.User> userContext, IJwtAuth auth, IOptions<AppSettings> setting,IMailService mailService,IJwtAuth jwtAuth)
        {
            _context = userContext;
            _setting = setting.Value;
            _mailService = mailService;
            _jwtAuth = jwtAuth;
   
        }
        public Response.User Login(string user, string pass)
        {
            string encryptPassword = HashPassword.EncryptPassword(pass, Encoding.UTF32.GetBytes(_setting.Salt)).Hash;
            Entity.User resultantUser = _context.GetAll().AsQueryable().Include(u=>u.Role).Where(u =>u.Email.Equals(user) && u.Password.Equals(encryptPassword) && u.IsActive && !u.IsDeleted).FirstOrDefault();
            string token = null;
            if (resultantUser != null)
            {
                token = _jwtAuth.GenerateToken(resultantUser.Id.ToString());

                return new Response.User
                { 
                    Email = resultantUser.Email,
                    Id = resultantUser.Id,
                    Role = resultantUser.Role,
                    Token = token
                };
            }

                return null;

        }
        public bool ResetPassword(Request.RequestPassword password,Entity.User user) {
            try
            {
                var userResponse = _context.GetAll().Where(u=>u.Email.Equals(password.Email)).FirstOrDefault();
                if (userResponse != null && user.Id.Equals(userResponse.Id))
                {
                    if (string.IsNullOrEmpty(password.OldPassword)) {
                        userResponse.Password = HashPassword.EncryptPassword(password.NewPassword, Encoding.UTF32.GetBytes(_setting.Salt)).Hash;
                        _context.Update(userResponse);
                        return true;
                    }
                    else if(HashPassword.VerifyPassword(password.OldPassword, Encoding.UTF32.GetBytes(_setting.Salt), userResponse.Password))
                    {
                        userResponse.Password = HashPassword.EncryptPassword(password.NewPassword, Encoding.UTF32.GetBytes(_setting.Salt)).Hash;
                        _context.Update(userResponse);
                        return true;
                    }
                    
                }
                return false;
            }
            catch (Exception ex) {
                return false;
            }
            
        }
        public (bool,string) Register(Model.Entity.User user)
        {
            try
            {
                var IsUserAlreadyExist = _context.GetAll().Where(u => u.Email.Equals(user.Email)).Any();
                if (IsUserAlreadyExist) {
                    return (false,"User already exist");
                }
                if (!string.IsNullOrEmpty(user.Password)) {
                    user.Password = HashPassword.EncryptPassword(user.Password, Encoding.UTF32.GetBytes(_setting.Salt)).Hash;
                }
                _context.Insert(user);
                var token = _jwtAuth.GenerateToken($"{user.Id}");
                var mailRequest = new MailRequest(user.Email, "School : Password Reset Link", string.Format("Reset Password Link : <a href='{1}?token={0}&email={2}'>click here to reset password</a>",token,UserInterfaceLink.ResetPasswordLink,user.Email));
                bool isMailSent = _mailService.sendEmail(mailRequest);
                return (true,"User successfully registered");
            }
            catch (Exception ex)
            {   
                return (false,"Something went wrong ! please contact admin");
            }
        }
    }
}
