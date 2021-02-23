//@QnSCodeCopy
using System;
using System.Collections.Generic;
using System.Linq;

namespace QnSTradingCompany.BlazorApp.Models.Modules.Session
{
    public class AuthorizationSession
    {
        public int IdentityId { get; set; }
        public string Username { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Token { get; set; }
        public IEnumerable<string> Roles { get; set; }
        public bool HasRole(string role) => Roles != null && Roles.Any(r => r.Equals(role, StringComparison.CurrentCultureIgnoreCase));
        public DateTime LoginTime { get; set; }
    }
}
