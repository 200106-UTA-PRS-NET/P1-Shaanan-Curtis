using System;
using PizzaBox.Storing.Entities;
using PizzaBox.Storing.Interfaces;
using PizzaBox.Storing.Repositories;
using Xunit;

namespace PizzaBox.Testing
{
    public class RepoTest
    {
        //Dependency Injection
        private readonly IPizzaBoxRepository _PBrepository;
        public RepoTest(IPizzaBoxRepository PBrepository)
        {
            _PBrepository = PBrepository;
        }

        [Fact]
        public void Fail_Authentication()
        {
            // ARRANGE
            User Expected = null;
            string[] username_auth = { "admin", "Uname23", "Admin", "uname23", "ad", "Uname", "Password123", "adminpass" };
            string[] password_auth = { "adminpas", "Password", "adminpass", "Password123", "admin", "password123", "Uname23", "admin" };
            
            for (int i = 0; i < username_auth.Length; i++)
            {
                // ACT
                User Actual = _PBrepository.UserAuthentication(username_auth[i], password_auth[i]);

                // ASSERT
                Assert.Equal(Expected, Actual);
            }
        }

        //Reflects positive results in database
        [Fact]
        public void Check_AddUser()
        {
            // ARRANGE
            User U1 = new User
            {
                Username = "whaa",
                Pass = "pa",
                FullName = "Yah Who",
                SessionLive = 0
            };
            User Expected = U1;

            // ACT
            _PBrepository.AddUser(U1);
            User Actual = _PBrepository.GetUserById("whaa");

            // ASSERT
            Assert.Equal(Expected, Actual);
        }

        //Reflects positive results in database
        [Fact]
        public void Check_UpdateUser()
        {
            // ARRANGE
            User U1 = new User()
            {
                Username = "Uname23",
                Pass = "Password123",
                FullName = "That Way",
                SessionLive = 1
            };
            User Expected = U1;

            // ACT
            _PBrepository.UpdateUser(U1);
            User Actual = _PBrepository.GetUserById("Uname23");

            // ASSERT
            Assert.Equal(Expected, Actual);
        }
  
    }
}
