using DnDH.Business.DTOs;
using DnDH.Business.UserHandling;
using Moq;

namespace DnDH.Test
{
    [TestClass]
    public class UserContextTests
    {
        [TestMethod]
        public void InitialState_IsNotLoggedIn()
        {
            var userContext = new UserContext();
            Assert.IsFalse(userContext.IsLoggedIn);
            Assert.IsNull(userContext.User);
        }

        [TestMethod]
        public void Login_SetsUserAndLoggedInStatus()
        {
            var userContext = new UserContext();
            var mockPermissionTracker = new Mock<DnDH.Business.UserHandling.PermisionStrategy.IPermissionStrategy>();
            var userDto = new UserDTO(1, "testuser", mockPermissionTracker.Object);

            userContext.Login(userDto);

            Assert.IsTrue(userContext.IsLoggedIn);
            Assert.AreEqual(userDto, userContext.User);
        }

        [TestMethod]
        public void Logout_ClearsUserAndLoggedInStatus()
        {
            var userContext = new UserContext();
            var mockPermissionTracker = new Mock<DnDH.Business.UserHandling.PermisionStrategy.IPermissionStrategy>();
            var userDto = new UserDTO(1, "testuser", mockPermissionTracker.Object);

            userContext.Login(userDto);
            userContext.Logout();

            Assert.IsFalse(userContext.IsLoggedIn);
            Assert.IsNull(userContext.User);
        }
    }
}