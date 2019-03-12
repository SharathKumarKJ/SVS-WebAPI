using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System.Collections.Generic;
using System.Security.Claims;
using System.Web.Http;
using WebAppAngular5.Models;
using System.Linq;
using System;
namespace WebAppAngular5.Controllers
{
    public class AccountController : ApiController
    {

        [Route("api/User/Register")]
        [HttpPost]
        [AllowAnonymous]
        public IdentityResult Register(AccountModel model)
        {
            var userStore = new UserStore<User>(new Repository());

            var manager = new UserManager<User>(userStore);

            var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(new Repository()));

            var user = new User() { UserName = model.UserName, Email = model.Email };

            user.FirstName = model.FirstName;

            user.LastName = model.LastName;

            var result = manager.Create(user, model.Password);

            var roleName = roleManager.Roles.Where(x => x.Name == model.Role).Select(x => x.Name).FirstOrDefault();

            if (string.IsNullOrEmpty(roleName))
            {
                throw new ArgumentException("Role cannot be empty", "Role");
            }
            if (result.Succeeded)
            {
                manager.AddToRolesAsync(user.Id, roleName);
            }

            return result;
        }

        [HttpGet]
        [Route("api/GetUserClaims")]
        public AccountModel GetUserClaims()
        {
            var identityClaims = (ClaimsIdentity)User.Identity;
            IEnumerable<Claim> claims = identityClaims.Claims;
            AccountModel model = new AccountModel()
            {
                UserName = identityClaims.FindFirst("Username").Value,
                Email = identityClaims.FindFirst("Email").Value,
                FirstName = identityClaims.FindFirst("FirstName").Value,
                LastName = identityClaims.FindFirst("LastName").Value,
                LoggedOn = identityClaims.FindFirst("LoggedOn").Value
            };
            return model;
        }


        [HttpGet]
        [Authorize(Roles = "Admin,SecurityAdmin")]
        [Route("api/AdminRole")]
        public string AdminRole()
        {
            return "Admin and security admin roles are working fine";
        }

        [HttpGet]
        [Authorize(Roles = "Teacher")]
        [Route("api/TeacherRole")]
        public string TeacherRole()
        {
            return "Teacher role is working fine";
        }

        [HttpGet]
        [Authorize(Roles = "Student")]
        [Route("api/StudentrRole")]
        public string StudentrRole()
        {
            return "Student role is working fine";
        }

    }
}
