using DnDH.Business.DTOs;
using DnDH.Business.Services;
using DnDH.Business.UserHandling;
using DnDH.Repo;
using Microsoft.EntityFrameworkCore;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DnDH.Test
{
    [TestClass]
    public class CampaignServiceTests
    {
        private AppDbContext _dbContext = null!;
        private Mock<IUserContext> _userContextMock = null!;
        private CampaignService _service = null!;

        [TestInitialize]
        public void SetUp()
        {
            var options = new DbContextOptionsBuilder<AppDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;

            _dbContext = new AppDbContext(options);
            _userContextMock = new Mock<IUserContext>();

            _service = new CampaignService(_userContextMock.Object, _dbContext);
        }

        [TestCleanup]
        public void CleanUp()
        {
            _dbContext.Database.EnsureDeleted();
            _dbContext.Dispose();
        }

        [TestMethod]
        public void Add_UserNotLoggedIn_ThrowsUnauthorizedAccessException()
        {
            _userContextMock.Setup(uc => uc.IsLoggedIn).Returns(false);

            Assert.Throws<UnauthorizedAccessException>(() => _service.Add("Test"));
        }

        [TestMethod]
        public void Add_UserLacksPermission_ThrowsInvalidOperationException()
        {
            var strategyMock = new Mock<DnDH.Business.UserHandling.PermisionStrategy.IPermissionStrategy>();
            strategyMock.Setup(s => s.CanCreateCampaigns).Returns(false);

            _userContextMock.Setup(uc => uc.IsLoggedIn).Returns(true);
            _userContextMock.Setup(uc => uc.User).Returns(new UserDTO(1, "TestUser", strategyMock.Object));

            Assert.Throws<InvalidOperationException>(() => _service.Add("Tests"));
        }

        [TestMethod]
        public void Add_Valid_CreatesCampaign()
        {
            var strategyMock = new Mock<DnDH.Business.UserHandling.PermisionStrategy.IPermissionStrategy>();
            strategyMock.Setup(s => s.CanCreateCampaigns).Returns(true);

            _userContextMock.Setup(uc => uc.IsLoggedIn).Returns(true);
            _userContextMock.Setup(uc => uc.User).Returns(new UserDTO(1, "TestUser", strategyMock.Object));

            var result = _service.Add("New Campaign");

            Assert.IsNotNull(result);
            Assert.AreEqual("New Campaign", result.Name);
            Assert.AreEqual(1, _dbContext.Campaigns.Count());
            Assert.AreEqual(1, result.AuthorId);
        }
        [TestMethod]
        public void Delete_Valid_DeletesCampaign()
        {
            var campaign = new Campaign("ToDelete", 1) { Id = 10 };
            _dbContext.Campaigns.Add(campaign);
            _dbContext.SaveChanges();

            var strategyMock = new Mock<DnDH.Business.UserHandling.PermisionStrategy.IPermissionStrategy>();
            strategyMock.Setup(s => s.CanDelete(It.IsAny<IQueryable<Campaign>>())).Returns(_dbContext.Campaigns.AsQueryable());

            _userContextMock.Setup(uc => uc.IsLoggedIn).Returns(true);
            _userContextMock.Setup(uc => uc.User).Returns(new UserDTO(1, "TestUser", strategyMock.Object));

            _service.Delete(10);

            Assert.AreEqual(0, _dbContext.Campaigns.Count());
        }

        [TestMethod]
        public void GetDetail_Valid_ReturnsCampaignDTO()
        {
            var campaign = new Campaign("My Campaign", 1) { Id = 20 };
            _dbContext.Campaigns.Add(campaign);
            _dbContext.SaveChanges();

            var strategyMock = new Mock<DnDH.Business.UserHandling.PermisionStrategy.IPermissionStrategy>();
            strategyMock.Setup(s => s.CanViewDetail(It.IsAny<IQueryable<Campaign>>())).Returns(_dbContext.Campaigns.AsQueryable());

            _userContextMock.Setup(uc => uc.IsLoggedIn).Returns(true);
            _userContextMock.Setup(uc => uc.User).Returns(new UserDTO(1, "TestUser", strategyMock.Object));

            var result = _service.GetDetail(20);

            Assert.IsNotNull(result);
            Assert.AreEqual(20, result.Id);
            Assert.AreEqual("My Campaign", result.Name);
        }

        [TestMethod]
        public void GetAllAvailable_ReturnsAllAllowedCampaigns()
        {
            var campaign1 = new Campaign("Camp 1", 1) { Id = 1 };
            var campaign2 = new Campaign("Camp 2", 2) { Id = 2 };
            _dbContext.Campaigns.AddRange(campaign1, campaign2);
            _dbContext.SaveChanges();

            var strategyMock = new Mock<DnDH.Business.UserHandling.PermisionStrategy.IPermissionStrategy>();
            strategyMock.Setup(s => s.CanView(It.IsAny<IQueryable<Campaign>>())).Returns(_dbContext.Campaigns.AsQueryable());

            _userContextMock.Setup(uc => uc.IsLoggedIn).Returns(true);
            _userContextMock.Setup(uc => uc.User).Returns(new UserDTO(1, "TestUser", strategyMock.Object));

            var result = _service.GetAllAvailable();

            Assert.IsNotNull(result);
            Assert.HasCount(2, result);
        }
    }
}