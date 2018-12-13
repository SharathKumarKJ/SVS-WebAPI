using Microsoft.AspNet.Identity.EntityFramework;
using System;

namespace WebAppAngular5.Models
{
    public class User : IdentityUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTimeOffset? Created { get; set; }
        public DateTimeOffset? Updated { get; set; }
        public bool IsActive { get; set; }

    }
}