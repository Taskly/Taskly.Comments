using System;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Taskly.Comments.Application;
using Taskly.Comments.Application.Entities;
using Taskly.Comments.Model;
using Taskly.Comments.Model.Exceptions;

namespace Taskly.Comments.Tests
{
    [TestClass]
    public class CommentsServiceTest
    {
        [TestInitialize]
        public void TestInitialize()
        {
            InitializeMapper();
            InitializeDbContext();
        }

        [TestCleanup]
        public void TestCleanup()
        {
            _dbContext.Dispose();
        }

        [TestMethod]
        public async Task GetCommentByIdIsCorrect()
        {
            ICommentsService commentsService = new CommentsService(_mapper, _dbContext);
            Comment comment = await commentsService.GetCommentById(2);

            Assert.AreEqual(2, comment.Id);
            Assert.AreEqual(1, comment.ParentId);
            Assert.AreEqual("350", comment.UserId);
            Assert.IsNotNull(comment.Locator);
            Assert.AreEqual(CommentStatus.Active, comment.Status);
            Assert.AreEqual("First", comment.Locator.Section);
            Assert.AreEqual("Second", comment.Locator.Subsection);
            Assert.AreEqual("42", comment.Locator.Element);
            Assert.AreEqual("Some text . . .", comment.Text);
            Assert.AreEqual(new DateTime(2020, 3, 4), comment.Timestamp);
        }

        [TestMethod]
        public async Task GetDeletedCommentByIdIsCorrect()
        {
            ICommentsService commentsService = new CommentsService(_mapper, _dbContext);
            Comment comment = await commentsService.GetCommentById(4);

            Assert.AreEqual(4, comment.Id);
            Assert.AreEqual(CommentStatus.Deleted, comment.Status);
        }

        [TestMethod]
        [ExpectedException(typeof(NotFoundDomainException))]
        public async Task GetCommentByIdThrowExceptionIfCommentNotFound()
        {
            ICommentsService commentsService = new CommentsService(_mapper, _dbContext);
            await commentsService.GetCommentById(5);
        }

        [TestMethod]
        public async Task AddCommentIsCorrect()
        {
            ICommentsService commentsService = new CommentsService(_mapper, _dbContext);
            Comment newComment = new Comment(0, "240", new Locator("111-111", string.Empty, "222-222"), "My comment!");

            Comment addedComment = await commentsService.AddComment(newComment);

            Assert.AreNotEqual(0, addedComment.Id);
            Assert.AreEqual(0, addedComment.ParentId);
            Assert.AreEqual("240", addedComment.UserId);
            Assert.AreEqual(CommentStatus.Active, addedComment.Status);
            Assert.IsNotNull(addedComment.Locator);
            Assert.AreEqual("111-111", addedComment.Locator.Section);
            Assert.IsNull(addedComment.Locator.Subsection);
            Assert.AreEqual("222-222", addedComment.Locator.Element);
            Assert.AreEqual("My comment!", addedComment.Text);
            Assert.AreNotEqual(default, addedComment.Timestamp);

            Assert.AreNotEqual(5, newComment.Id);
            Assert.AreEqual(0, newComment.ParentId);
            Assert.AreEqual("240", newComment.UserId);
            Assert.AreEqual(CommentStatus.Active, newComment.Status);
            Assert.IsNotNull(newComment.Locator);
            Assert.AreEqual("111-111", newComment.Locator.Section);
            Assert.IsNull(newComment.Locator.Subsection);
            Assert.AreEqual("222-222", newComment.Locator.Element);
            Assert.AreEqual("My comment!", newComment.Text);
            Assert.AreNotEqual(default, newComment.Timestamp);
        }

        [TestMethod]
        public async Task MarkAsDeletedIsCorrect()
        {
            ICommentsService commentsService = new CommentsService(_mapper, _dbContext);

            DeletedComment deletedComment = await commentsService.MarkAsDeleted(1, "129");
            CommentEntity deletedCommentEntity = await _dbContext.Comments.FirstOrDefaultAsync(x => x.Id == 1);

            Assert.AreEqual(1, deletedComment.Id);
            Assert.AreEqual(CommentStatus.Deleted, deletedComment.Status);
            Assert.AreEqual("129", deletedComment.RemovalUserId);
            Assert.AreNotEqual(default, deletedComment.RemovalTimestamp);

            Assert.AreEqual(1, deletedCommentEntity.Id);
            Assert.AreEqual(CommentStatus.Deleted, deletedCommentEntity.Status);
            Assert.AreEqual("129", deletedCommentEntity.RemovalUserId);
            Assert.AreNotEqual(default(DateTime), deletedCommentEntity.RemovalTimestamp);
        }

        [TestMethod]
        [ExpectedException(typeof(NotFoundDomainException))]
        public async Task MarkAsDeletedThrowExceptionIfNotFound()
        {
            ICommentsService commentsService = new CommentsService(_mapper, _dbContext);
            await commentsService.MarkAsDeleted(7, "123");
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationDomainException))]
        public async Task MarkAsDeletedThrowExceptionIfAlreadyDeleted()
        {
            ICommentsService commentsService = new CommentsService(_mapper, _dbContext);
            await commentsService.MarkAsDeleted(4, "123");
        }

        [TestMethod]
        public async Task GetCommentsByLocatorIsCorrect()
        {
            ICommentsService commentsService = new CommentsService(_mapper, _dbContext);

            Locator locator = new Locator("97c3fe03", "d9c8", "a10");
            PaginatedList<Comment> comments = await commentsService.GetCommentsByLocator(locator, 1, 100);
            Assert.AreEqual(5, comments.Count);

            locator = new Locator("97c3fe03", null, "b20");
            comments = await commentsService.GetCommentsByLocator(locator, 1, 100);
            Assert.AreEqual(3, comments.Count);

            locator = new Locator("97c3fe03");
            comments = await commentsService.GetCommentsByLocator(locator, 1, 100);
            Assert.AreEqual(58, comments.Count);
        }

        [TestMethod]
        public async Task PaginationIsCorrect()
        {
            ICommentsService commentsService = new CommentsService(_mapper, _dbContext);

            Locator locator = new Locator("97c3fe03"); // 58

            PaginatedList<Comment> comments = await commentsService.GetCommentsByLocator(locator, 1, 100);
            Assert.AreEqual(58, comments.Count);
            Assert.AreEqual(1, comments.PageIndex);
            Assert.AreEqual(1, comments.TotalPages);
            Assert.AreEqual(58, comments.TotalItems);

            comments = await commentsService.GetCommentsByLocator(locator, 1, 25);
            Assert.AreEqual(25, comments.Count);
            Assert.AreEqual(1, comments.PageIndex);
            Assert.AreEqual(3, comments.TotalPages);
            Assert.AreEqual(58, comments.TotalItems);

            comments = await commentsService.GetCommentsByLocator(locator, 2, 25);
            Assert.AreEqual(25, comments.Count);
            Assert.AreEqual(2, comments.PageIndex);
            Assert.AreEqual(3, comments.TotalPages);
            Assert.AreEqual(58, comments.TotalItems);

            comments = await commentsService.GetCommentsByLocator(locator, 3, 25);
            Assert.AreEqual(8, comments.Count);
            Assert.AreEqual(3, comments.PageIndex);
            Assert.AreEqual(3, comments.TotalPages);
            Assert.AreEqual(58, comments.TotalItems);

            comments = await commentsService.GetCommentsByLocator(locator, 10, 25);
            Assert.AreEqual(0, comments.Count);
            Assert.AreEqual(10, comments.PageIndex);
            Assert.AreEqual(3, comments.TotalPages);
            Assert.AreEqual(58, comments.TotalItems);
        }

        private void InitializeMapper()
        {
            var configuration = new MapperConfiguration(cfg => { cfg.AddMaps(typeof(EntitiesMappingProfile)); });
            _mapper = configuration.CreateMapper();
        }

        private void InitializeDbContext()
        {
            var options = new DbContextOptionsBuilder<CommentsDbContext>()
                .UseInMemoryDatabase("comments_service")
                .Options;

            _dbContext = new CommentsDbContext(options);
            _dbContext.Database.EnsureDeleted();
            _dbContext.Database.EnsureCreated();

            _dbContext.Comments.Add(new CommentEntity
            {
                Id = 1,
                ParentId = 0,
                UserId = "352",
                Status = CommentStatus.Active,
                LocatorSection = "First",
                LocatorSubsection = "Second",
                LocatorElement = "42",
                Text = "Comment Test",
                Timestamp = new DateTime(2019, 8, 3)
            });
            _dbContext.Comments.Add(new CommentEntity
            {
                Id = 2,
                ParentId = 1,
                UserId = "350",
                Status = CommentStatus.Active,
                LocatorSection = "First",
                LocatorSubsection = "Second",
                LocatorElement = "42",
                Text = "Some text . . .",
                Timestamp = new DateTime(2020, 3, 4)
            });
            _dbContext.Comments.Add(new CommentEntity
            {
                Id = 3,
                ParentId = 0,
                UserId = "1290",
                Status = CommentStatus.Active,
                LocatorSection = "some-section",
                LocatorElement = "some-id",
                Text = "bla-bla-bla",
                Timestamp = new DateTime(2020, 1, 12)
            });
            _dbContext.Comments.Add(new CommentEntity
            {
                Id = 4,
                ParentId = 0,
                UserId = "1890",
                Status = CommentStatus.Deleted,
                LocatorSection = "abc",
                Text = "Some bad text.",
                Timestamp = new DateTime(2018, 2, 4)
            });

            for (int i = 0; i < 5; ++i)
            {
                _dbContext.Comments.Add(new CommentEntity
                {
                    Id = 100 + i,
                    ParentId = 0,
                    UserId = "1890",
                    Status = CommentStatus.Active,
                    LocatorSection = "97c3fe03",
                    LocatorSubsection = "d9c8",
                    LocatorElement = $"a10",
                    Text = "Some bad text.",
                    Timestamp = new DateTime(2018, 2, 4).AddMinutes(i)
                });
            }

            for (int i = 0; i < 3; ++i)
            {
                _dbContext.Comments.Add(new CommentEntity
                {
                    Id = 200 + i,
                    ParentId = 0,
                    UserId = "1890",
                    Status = CommentStatus.Active,
                    LocatorSection = "97c3fe03",
                    LocatorElement = $"b20",
                    Text = "Some bad text.",
                    Timestamp = new DateTime(2018, 3, 4).AddMinutes(i)
                });
            }

            for (int i = 0; i < 50; ++i)
            {
                _dbContext.Comments.Add(new CommentEntity
                {
                    Id = 300 + i,
                    ParentId = 0,
                    UserId = "1890",
                    Status = CommentStatus.Active,
                    LocatorSection = "97c3fe03",
                    Text = "Some bad text.",
                    Timestamp = new DateTime(2018, 4, 4).AddMinutes(i)
                });
            }

            _dbContext.SaveChanges();
        }

        private IMapper _mapper;
        private CommentsDbContext _dbContext;
    }
}
