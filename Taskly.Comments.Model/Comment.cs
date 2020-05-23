using System;
using Taskly.Comments.Model.Exceptions;

namespace Taskly.Comments.Model
{
    public class Comment
    {
        public Comment(int parentId, string userId, Locator locator, string text)
        {
            Validate(userId, locator, text);

            ParentId = parentId;
            UserId = userId;
            Locator = locator;
            Text = text;
            Timestamp = DateTime.UtcNow;
            Status = CommentStatus.Active;
        }

        protected Comment()
        {
        }

        public int Id { get; protected set; }

        public int ParentId { get; protected set; }

        public string UserId { get; protected set; }

        public CommentStatus Status { get; protected set; }

        public Locator Locator { get; protected set; }

        public string Text { get; protected set; }

        public DateTime Timestamp { get; protected set; }

        private void Validate(string userId, Locator locator, string text)
        {
            if (string.IsNullOrEmpty(userId))
            {
                throw new InvalidArgumentDomainException("Comment User ID required.");
            }

            if (locator is null)
            {
                throw new InvalidArgumentDomainException("Comment Locator required.");
            }

            if (string.IsNullOrEmpty(text))
            {
                throw new InvalidArgumentDomainException("Comment text required.");
            }
        }
    }
}
