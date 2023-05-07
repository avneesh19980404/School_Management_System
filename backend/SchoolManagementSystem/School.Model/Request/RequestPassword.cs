using System.ComponentModel.DataAnnotations;

namespace School.Model.Request
{
    public class RequestPassword
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        public string OldPassword { get; set; }
        [Required]
        public string NewPassword { get; set; }

        public bool IsPasswordEqual => string.IsNullOrEmpty(OldPassword)?false:OldPassword.Equals(NewPassword);
    }
}
