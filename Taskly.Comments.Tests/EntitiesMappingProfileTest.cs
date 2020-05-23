using System;
using AutoMapper;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Taskly.Comments.Application;
using Taskly.Comments.Application.Entities;
using Taskly.Comments.Model;

namespace Taskly.Comments.Tests
{
    [TestClass]
    public class EntitiesMappingProfileTest
    {
        [TestInitialize]
        public void TestInitialize()
        {
            _configuration = new MapperConfiguration(cfg => { cfg.AddMaps(typeof(EntitiesMappingProfile)); });
            _mapper = _configuration.CreateMapper();
        }

        [TestMethod]
        public void ConfigurationIsValid()
        {
            _configuration.AssertConfigurationIsValid();
        }

        [TestMethod]
        public void MapCommentToCommentEntityIsCorrect()
        {
            var locator = new Locator("First", "Second", "42");
            var comment = new Comment(152, "350", locator, "hello!");

            CommentEntity entity = _mapper.Map<CommentEntity>(comment);

            Assert.AreEqual(0, entity.Id);
            Assert.AreEqual(152, entity.ParentId);
            Assert.AreEqual("350", entity.UserId);
            Assert.AreEqual(CommentStatus.Active, entity.Status);
            Assert.AreEqual("First", entity.LocatorSection);
            Assert.AreEqual("Second", entity.LocatorSubsection);
            Assert.AreEqual("42", entity.LocatorElement);
            Assert.AreEqual("hello!", entity.Text);
            Assert.AreEqual(comment.Timestamp, entity.Timestamp);
            Assert.IsNull(entity.RemovalUserId);
            Assert.IsNull(entity.RemovalTimestamp);
        }

        [TestMethod]
        public void MapCommentEntityToCommentIsCorrect()
        {
            var entity = new CommentEntity
            {
                Id = 36,
                ParentId = 18,
                UserId = "325",
                Status = CommentStatus.Active,
                LocatorSection = "AAA",
                LocatorSubsection = "BBB",
                LocatorElement = "333",
                Text = "Some text.",
                Timestamp = new DateTime(2020, 3, 1)
            };

            Comment comment = _mapper.Map<Comment>(entity);

            Assert.AreEqual(36, comment.Id);
            Assert.AreEqual(18, comment.ParentId);
            Assert.AreEqual("325", comment.UserId);
            Assert.AreEqual(CommentStatus.Active, comment.Status);
            Assert.IsNotNull(comment.Locator);
            Assert.AreEqual("AAA", comment.Locator.Section);
            Assert.AreEqual("BBB", comment.Locator.Subsection);
            Assert.AreEqual("333", comment.Locator.Element);
            Assert.AreEqual("Some text.", comment.Text);
            Assert.AreEqual(new DateTime(2020, 3, 1), comment.Timestamp);
        }

        [TestMethod]
        public void MapDeletedCommentToCommentEntityIsCorrect()
        {
            var locator = new Locator("First", "Second", "42");
            var comment = new Comment(152, "350", locator, "hello!");
            var deletedComment = new DeletedComment(comment, "126");

            CommentEntity entity = _mapper.Map<CommentEntity>(deletedComment);

            Assert.AreEqual(0, entity.Id);
            Assert.AreEqual(152, entity.ParentId);
            Assert.AreEqual("350", entity.UserId);
            Assert.AreEqual(CommentStatus.Deleted, entity.Status);
            Assert.AreEqual("First", entity.LocatorSection);
            Assert.AreEqual("Second", entity.LocatorSubsection);
            Assert.AreEqual("42", entity.LocatorElement);
            Assert.AreEqual("hello!", entity.Text);
            Assert.AreEqual(deletedComment.Timestamp, entity.Timestamp);
            Assert.AreEqual("126", entity.RemovalUserId);
            Assert.AreEqual(deletedComment.RemovalTimestamp, entity.RemovalTimestamp);
        }

        [TestMethod]
        public void MapCommentEntityToDeletedCommentIsCorrect()
        {
            var entity = new CommentEntity
            {
                Id = 36,
                ParentId = 18,
                UserId = "325",
                Status = CommentStatus.Deleted,
                LocatorSection = "AAA",
                LocatorSubsection = "BBB",
                LocatorElement = "333",
                Text = "Some text.",
                Timestamp = new DateTime(2020, 3, 1),
                RemovalUserId = "126",
                RemovalTimestamp = new DateTime(2020, 3, 3)
            };

            DeletedComment deletedComment = _mapper.Map<DeletedComment>(entity);

            Assert.AreEqual(36, deletedComment.Id);
            Assert.AreEqual(18, deletedComment.ParentId);
            Assert.AreEqual("325", deletedComment.UserId);
            Assert.AreEqual(CommentStatus.Deleted, deletedComment.Status);
            Assert.IsNotNull(deletedComment.Locator);
            Assert.AreEqual("AAA", deletedComment.Locator.Section);
            Assert.AreEqual("BBB", deletedComment.Locator.Subsection);
            Assert.AreEqual("333", deletedComment.Locator.Element);
            Assert.AreEqual("Some text.", deletedComment.Text);
            Assert.AreEqual(new DateTime(2020, 3, 1), deletedComment.Timestamp);
            Assert.AreEqual("126", deletedComment.RemovalUserId);
            Assert.AreEqual(new DateTime(2020, 3, 3), deletedComment.RemovalTimestamp);
        }

        private MapperConfiguration _configuration;
        private IMapper _mapper;
    }
}
