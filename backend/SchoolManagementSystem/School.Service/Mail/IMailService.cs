using School.Model.Request;
using System;
using System.Collections.Generic;
using System.Text;

namespace School.Service.Mail
{
    public interface IMailService
    {
        bool sendEmail(MailRequest mailData);
    }
}
