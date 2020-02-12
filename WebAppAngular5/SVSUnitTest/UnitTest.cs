using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using WebAppAngular5.Controllers;
using WebAppAngular5.Models;
namespace SVSUnitTest
{
    [TestClass]
    public class UnitTest
    {
        [TestMethod]
        public void TestMethod()
        {
            var rand = new Random();
            var id = rand.Next(1,1000);

            var user = new AccountModel
            {
                UserName = id + "JDASoftware",
                Password = "jda@123",
                Email = id + "jda@gmail.com",
                FirstName = id + "JDA",
                LastName = "Software",
                Role = "SecurityAdmin"
            };
            var account = new AccountController();

            var result = account.Register(user);

            Assert.AreEqual(true, result.Succeeded);
            User dbUser;
            using (var repository = new Repository())
            {
                dbUser = repository.Users.Where(x => x.UserName == user.UserName).FirstOrDefault();    
            }
            Assert.IsNotNull(dbUser);
            Assert.AreEqual(user.FirstName, dbUser.FirstName);
            Assert.AreEqual(user.LastName, dbUser.LastName);
            Assert.AreEqual(user.Email, dbUser.Email);
        }
    }
}
