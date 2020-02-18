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

            Assert.AreEqual(user, user);
        }
        public void TestMethod2()
        {
            var rand = new Random();
            var id = rand.Next(1, 1000);

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

            Assert.AreEqual(user, user);
        }
        public void TestMethod3()
        {
            var rand = new Random();
            var id = rand.Next(1, 1000);

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

            Assert.AreEqual(user, user);
        }
    }
}
