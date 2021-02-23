//@QnSCodeCopy
using System.ComponentModel.DataAnnotations;

namespace QnSTradingCompany.BlazorApp.Models.Modules.Account
{
    public partial class LoginModel
    {
        [Required]
        public string Email { get; set; }

        [Required]
        public string Password { get; set; }

        public string OptionalInfo { get; set; }
        public bool RememberMe { get; set; }
    }
}
