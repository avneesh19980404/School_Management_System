using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;

namespace School.Model.Request
{
    public class MailRequest
    {
        public string ToEmail { get; set; }
        public string Subject { get; set; }
        public string Body { get; set; }
        public List<IFormFile> Attachments { get; set; }

        public MailRequest(string email,string subject,string body)
        {
            this.ToEmail = email;
            this.Subject = subject;
            this.Body = body;
        }
    }
}
