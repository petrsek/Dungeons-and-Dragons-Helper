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
    public class NoteServiceTests
    {
        private AppDbContext _dbContext = null!;
        private Mock<IUserContext> _userContextMock = null!;
        private NoteService _service = null!;

        [TestInitialize]
        public void SetUp()
        {
            var options = new DbContextOptionsBuilder<AppDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;

            _dbContext = new AppDbContext(options);
            _userContextMock = new Mock<IUserContext>();

            _service = new NoteService(_userContextMock.Object, _dbContext);
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

            Assert.Throws<UnauthorizedAccessException>(() => _service.Add(1, "Test"));
        }

        [TestMethod]
        public void Add_CampaignNotExistsOrNoPermission_ThrowsInvalidOperationException()
        {
            var strategyMock = new Mock<DnDH.Business.UserHandling.PermisionStrategy.IPermissionStrategy>();
            strategyMock.Setup(s => s.CanEdit(It.IsAny<IQueryable<Campaign>>())).Returns(Array.Empty<Campaign>().AsQueryable());

            _userContextMock.Setup(uc => uc.IsLoggedIn).Returns(true);
            _userContextMock.Setup(uc => uc.User).Returns(new UserDTO(1, "TestUser", strategyMock.Object));

            Assert.Throws<InvalidOperationException>(() => _service.Add(999, "Tests"));
        }

        [TestMethod]
        public void Add_Valid_CreatesNote()
        {
            var campaign = new Campaign("Camp1", 1) { Id = 10 };
            _dbContext.Campaigns.Add(campaign);
            _dbContext.SaveChanges();

            var strategyMock = new Mock<DnDH.Business.UserHandling.PermisionStrategy.IPermissionStrategy>();
            strategyMock.Setup(s => s.CanEdit(It.IsAny<IQueryable<Campaign>>())).Returns(_dbContext.Campaigns.AsQueryable());

            _userContextMock.Setup(uc => uc.IsLoggedIn).Returns(true);
            _userContextMock.Setup(uc => uc.User).Returns(new UserDTO(1, "TestUser", strategyMock.Object));

            var result = _service.Add(10, "New Note");

            Assert.IsNotNull(result);
            Assert.AreEqual("New Note", result.Title);
            Assert.AreEqual(10, result.CampaignId);
            Assert.AreEqual(1, _dbContext.Notes.Count());
        }
            [TestMethod]
            public void Delete_Valid_DeletesNote()
            {
                var note = new Note("Title", 10) { Id = 5, Text = "ToDelete" };
                _dbContext.Notes.Add(note);
                var campaign = new Campaign("Camp1", 1) { Id = 10 };
                _dbContext.Campaigns.Add(campaign);
                _dbContext.SaveChanges();

                var strategyMock = new Mock<DnDH.Business.UserHandling.PermisionStrategy.IPermissionStrategy>();
                strategyMock.Setup(s => s.CanEdit(It.IsAny<IQueryable<Campaign>>())).Returns(_dbContext.Campaigns.AsQueryable());

                _userContextMock.Setup(uc => uc.IsLoggedIn).Returns(true);
                _userContextMock.Setup(uc => uc.User).Returns(new UserDTO(1, "TestUser", strategyMock.Object));

                _service.Delete(5);

                Assert.AreEqual(0, _dbContext.Notes.Count());
            }

            [TestMethod]
            public void Update_Valid_UpdatesNote()
            {
                var note = new Note("Old Title", 10) { Id = 5, Text = "Old Text" };
                _dbContext.Notes.Add(note);
                var campaign = new Campaign("Camp1", 1) { Id = 10 };
                _dbContext.Campaigns.Add(campaign);
                _dbContext.SaveChanges();

                var strategyMock = new Mock<DnDH.Business.UserHandling.PermisionStrategy.IPermissionStrategy>();
                strategyMock.Setup(s => s.CanViewDetail(It.IsAny<IQueryable<Campaign>>())).Returns(_dbContext.Campaigns.AsQueryable());

                _userContextMock.Setup(uc => uc.IsLoggedIn).Returns(true);
                _userContextMock.Setup(uc => uc.User).Returns(new UserDTO(1, "TestUser", strategyMock.Object));

                var result = _service.Update(5, "New Title", "New Text");

                Assert.IsNotNull(result);
                Assert.AreEqual("New Title", result.Title);
                Assert.AreEqual("New Text", result.Text);

                var updatedNote = _dbContext.Notes.First();
                Assert.AreEqual("New Title", updatedNote.Title);
                Assert.AreEqual("New Text", updatedNote.Text);
            }

            [TestMethod]
            public void GetCampaignNotes_Valid_ReturnsNotes()
            {
                var note1 = new Note("N1", 10) { Id = 1 };
                var note2 = new Note("N2", 10) { Id = 2 };
                _dbContext.Notes.AddRange(note1, note2);
                var campaign = new Campaign("Camp1", 1) { Id = 10 };
                _dbContext.Campaigns.Add(campaign);
                _dbContext.SaveChanges();

                var strategyMock = new Mock<DnDH.Business.UserHandling.PermisionStrategy.IPermissionStrategy>();
                strategyMock.Setup(s => s.CanViewDetail(It.IsAny<IQueryable<Campaign>>())).Returns(_dbContext.Campaigns.AsQueryable());

                _userContextMock.Setup(uc => uc.IsLoggedIn).Returns(true);
                _userContextMock.Setup(uc => uc.User).Returns(new UserDTO(1, "TestUser", strategyMock.Object));

                var result = _service.GetCampaignNotes(10);

                Assert.IsNotNull(result);
                Assert.HasCount(2, result);
            }
        }
    }